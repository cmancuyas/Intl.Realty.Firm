using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Helpers;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Service.IServices;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Intl.Realty.Firm.Controllers
{
    public class SaleListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationService _configurationService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly string _backupFileDirectory = "Uploads\\Documents\\";
        private readonly string _saleListingName = "Sale Listing";
        private IEnumerable<Claim> _userClaims;
        public SaleListingController(IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfigurationService configurationService,
                                    IFileHandlerService fileHandlerService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _configurationService = configurationService;
            _fileHandlerService = fileHandlerService;

            _userClaims = httpContextAccessor.HttpContext.User.Claims;

        }
        public async Task<IActionResult> Index()
        {

            //Get values from the current user

            List<SaleListing> modelList = await _unitOfWork.SaleListing.GetAllAsync(includeProperties: "TransactionType") as List<SaleListing> ?? throw new ArgumentException();

            List<SaleListingViewModel> viewModelList = modelList.ToSaleListingListViewModel();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.SaleListing.GetAllAsync(includeProperties: "TransactionType,IRFDeal");

            var viewModel = modelList.ToSaleListingListViewModel();

            return PartialView("~/Views/SaleListing/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateSaleListingViewModel viewModel = new CreateSaleListingViewModel();
            string transactionTypeName = _saleListingName; // 1 = Sale Listing
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionTypeName);
            viewModel.TransactionTypeId = viewModel.TransactionType.Id;
            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionTypeName);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleListingViewModel viewModel)
        {
            var userId = Session.UserId;

            string transactionTypeName = _saleListingName; // 1 = Sale Listing
            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == _saleListingName); //Sale Listing
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionType.Description);
            viewModel.TransactionTypeId = viewModel.TransactionType.Id;
            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionType.Description);

            // Create IRF Deal Data
            viewModel.TransactionType = transactionType;
            if (ModelState.IsValid)
            {
                var createIRFDealModel = viewModel.CreateIRFDealViewModel?.ToIRFDealModel();
                createIRFDealModel!.CreatedAt = DateTime.Now;
                createIRFDealModel.CreatedBy = userId;
                createIRFDealModel.IsActive = true;
                await _unitOfWork.IRFDeal.AddAsync(createIRFDealModel);

                var NewIRFDeal = await _unitOfWork.IRFDeal.GetAsync(x => x.Id == createIRFDealModel.Id);

                // Get IRF Deal Data
                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.TransactionType = transactionType;
                viewModel.IRFDealId = NewIRFDeal.Id;
                if (NewIRFDeal != null)
                {
                    viewModel.CreateIRFDealViewModel!.IsActive = NewIRFDeal.IsActive;
                    viewModel.CreateIRFDealViewModel.CreatedBy = NewIRFDeal.CreatedBy;
                    viewModel.CreateIRFDealViewModel.CreatedAt = NewIRFDeal.CreatedAt;
                }

                // Create SaleListing Data
                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.IRFDealId = NewIRFDeal!.Id;

                viewModel.IsActive = viewModel.IsActive;
                viewModel.CreatedBy = viewModel.CreatedBy;
                viewModel.CreatedAt = viewModel.CreatedAt;

                var saleListingModel = viewModel.ToSaleListingModel();
                await _unitOfWork.SaleListing.AddAsync(saleListingModel);

                // Create FileUploadData
                if (viewModel.CreateFileUploadListViewModel.CreateFileUploadsViewModel != null)
                {
                    var uploadPath = CreateFilePath(saleListingModel.Id);


                    await CreateFileUploadData(viewModel.CreateFileUploadListViewModel.CreateFileUploadsViewModel,
                                                saleListingModel.Id,
                                                transactionType.Id,
                                                viewModel.DocumentTypeList,
                                                uploadPath);

                }
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        private async Task<bool> CreateFileUploadData(List<CreateFileUploadViewModel> createFileUploadsViewModel,
                                                    int saleListingModelId,
                                                    int transactionTypeId,
                                                    IEnumerable<DocumentType> documentTypeList,
                                                    string uploadPath)
        {

            var userId = Session.UserId;

            string updatedUploadPath = string.Empty;
            FileUpload createFileUpload = new FileUpload();
            int index = 0;
            foreach (var createFileUploadViewModel in createFileUploadsViewModel)
            {
                var createFileUploadDocumentType = documentTypeList.Where(x => x.Id == createFileUploadViewModel.DocumentTypeId).FirstOrDefault();
                if (createFileUploadDocumentType != null)
                {
                    updatedUploadPath = Path.Combine(uploadPath, createFileUploadDocumentType.Description + "\\");
                }

                var files = createFileUploadViewModel.Files;

                    if (files != null)
                    {
                        foreach (var file in files)
                        {
                            var (fileNameWithoutExtension, fileExtension, originalFileName) = await _fileHandlerService.UploadFile(file, updatedUploadPath);
                            createFileUpload = new FileUpload()
                            {
                                FileName = fileNameWithoutExtension,
                                FileExtension = fileExtension,
                                OriginalFileName = originalFileName,
                                Directory = updatedUploadPath,
                                FullPath = updatedUploadPath + fileNameWithoutExtension + fileExtension,
                                FileSize = file.Length.ToString(),
                                IsActive = true,
                                CreatedAt = DateTime.Now,
                                CreatedBy = userId,
                                SaleListingId = saleListingModelId,
                                TransactionTypeId = transactionTypeId,
                                DocumentTypeId = createFileUploadDocumentType!.Id

                            };
                            await _unitOfWork.FileUpload.AddAsync(createFileUpload);
                        }              
                }
                index++;
            }
            return true;
        }
        public async Task<List<DocumentType>> GetDocumentTypesFromDocumentTypeAssignment(string transactionTypeName)
        {
            List<DocumentType> documentTypeList = new List<DocumentType>();

            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == transactionTypeName);

            var documentTypeAssignmentListViewModel = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionType.Id, includeProperties: "DocumentType,TransactionType") as List<DocumentTypeAssignment>;

            var documentTypeIds = documentTypeAssignmentListViewModel?
                                    .GroupBy(x => x.DocumentType)
                                    .Select(grp => new DocumentType
                                    {
                                        Id = grp.Key!.Id,
                                        Code = grp.Key.Code,
                                        Description = grp.Key.Description,
                                        IsActive = grp.Key.IsActive,
                                        CreatedAt = grp.Key.CreatedAt,
                                        CreatedBy = grp.Key.CreatedBy,
                                        UpdatedAt = grp.Key.UpdatedAt,
                                        UpdatedBy = grp.Key.UpdatedBy
                                    }).ToList();
            if (documentTypeIds != null)
            {
                var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync(x => documentTypeIds.Contains(x));
                documentTypeList = documentTypeIEnum.ToList();
            }
            return documentTypeList;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var saleListingModel = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id, includeProperties: "IRFDeal,TransactionType");

            int saleListingId = saleListingModel.Id;

            var fileUploads = await _unitOfWork.FileUpload.GetFileUploadsBySaleListingIdAsync(saleListingId);

            if (saleListingModel == null)
            {
                return NotFound();
            }
            var viewModel = saleListingModel.ToEditSaleListingViewModel();

            viewModel.FileUploads = fileUploads;

            var fileUploadsWithFiles = FilterFileUploadsWithFilesOnly(viewModel.FileUploads);

            viewModel.FileUploads = fileUploadsWithFiles;

            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(saleListingModel?.TransactionType?.Description ?? _saleListingName);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditSaleListingViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = Session.UserId;

                string transactionTypeName = _saleListingName; // 1 = Sale Listing
                var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == _saleListingName, tracked: true); //Sale Listing
                viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionType.Description);
                viewModel.TransactionTypeId = viewModel.TransactionType.Id;
                viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionType.Description);

                var saleListingModel = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id, includeProperties: ("IRFDeal,FileUploads,TransactionType"), tracked: true);
                if (saleListingModel == null)
                {
                    return NotFound();
                }
                var fileUploadsWithFiles = FilterFileUploadsWithFilesOnly(saleListingModel.FileUploads);
                saleListingModel.FileUploads = fileUploadsWithFiles;
                saleListingModel.IRFDeal = viewModel.EditIRFDealViewModel?.ToIRFDealModel();

                // Update IRD Deal
                if (saleListingModel.IRFDeal != null)
                {
                    await _unitOfWork.IRFDeal.UpdateAsync(saleListingModel.IRFDeal);
                }

                saleListingModel.IsActive = viewModel.IsActive;
                saleListingModel.UpdatedBy = userId;
                saleListingModel.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.SaleListing.UpdateAsync(saleListingModel);

                // Create FileUploadData

                var uploadPath = CreateFilePath(saleListingModel.Id);

                if (viewModel.CreateFileUploadListViewModel?.CreateFileUploadsViewModel != null)
                {
                    await CreateFileUploadData(viewModel.CreateFileUploadListViewModel.CreateFileUploadsViewModel,
                                                 saleListingModel.Id,
                                                 transactionType.Id,
                                                 viewModel.DocumentTypeList,
                                                 uploadPath);
                }

                return RedirectToAction(nameof(Index), new { addSuccess = true });

            }

            return View(viewModel);
        }

        private string CreateFilePath(int saleListingId)
        {
            var userId = Session.UserId;

            var uploadPath = string.Empty;
            var defaultPathFromConfig = _configurationService.GetDefaultUploadPathFromConfig();
            if (defaultPathFromConfig != null)
            {
                uploadPath = defaultPathFromConfig;
            }
            else
            {
                uploadPath = _backupFileDirectory;
            }
            var userIdPath = userId.ToString() + "\\";

            uploadPath = Path.Combine(uploadPath, userIdPath + saleListingId + "\\");

            return uploadPath;
        }

        private List<FileUpload>? FilterFileUploadsWithFilesOnly(List<FileUpload>? fileUploads)
        {
            List<FileUpload> fileUploadsWithFiles = new List<FileUpload>();
            if (fileUploads != null)
            {
                foreach (var fileUpload in fileUploads)
                {
                    var isExists = _fileHandlerService.CheckIfFileExists(fileUpload.FullPath);
                    if (isExists.Result == true)
                    {
                        fileUploadsWithFiles.Add(fileUpload);
                    }
                }
            }

            return fileUploadsWithFiles;

        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editSaleListingViewModel = model.ToEditSaleListingViewModel();

            return PartialView("~/Views/SaleListing/Modal/DeleteModal.cshtml", editSaleListingViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<SaleListing> modelList = new List<SaleListing>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null || !modelList.Any())
            {
                return NotFound();
            }

            var transactionTypeViewModelList = modelList.ToSaleListingListViewModel();

            IEnumerable<SaleListingViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/SaleListing/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            await _unitOfWork.SaleListing.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<SaleListingViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var saleListingIds = viewModelList.Select(x => x.Id).ToList();
            var saleListingListToBeDeleted = await _unitOfWork.SaleListing.GetAllAsync(x => saleListingIds.Contains(x.Id));

            var IRFDealIds = saleListingListToBeDeleted.Select(x => x.IRFDealId).ToList();
            var IRFDealListToBeDeleted = await _unitOfWork.IRFDeal.GetAllAsync(x => IRFDealIds.Contains(x.Id));

            var fileUploadList = await _unitOfWork.FileUpload.GetAllAsync(x => saleListingIds.Contains(x.SaleListingId));

            if (saleListingListToBeDeleted != null)
            {
                try
                {
                    foreach (var file in fileUploadList)
                    {
                        await _fileHandlerService.DeleteFile(file.FullPath);
                    }

                    await _unitOfWork.FileUpload.RemoveRangeAsync(fileUploadList);
                    await _unitOfWork.SaleListing.RemoveRangeAsync(saleListingListToBeDeleted);
                    await _unitOfWork.IRFDeal.RemoveRangeAsync(IRFDealListToBeDeleted);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            return StatusCode(StatusCodes.Status200OK, ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Download(int id)
        {
            var fileUpload = await _unitOfWork.FileUpload.GetAsync(x => x.Id == id);
            if (fileUpload != null)
            {
                var result = await _fileHandlerService.DownloadFile(fileUpload.Directory, fileUpload.FileName, fileUpload.FileExtension);
                return File(result.Item1, result.Item2, result.Item3);
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var fileUpload = await _unitOfWork.FileUpload.GetAsync(x => x.Id == id);
            if (fileUpload != null)
            {
                await _unitOfWork.FileUpload.RemoveAsync(fileUpload);

                var isSuccess = await _fileHandlerService.DeleteFile(fileUpload.FullPath);

                return RedirectToAction(nameof(Edit), new { id = fileUpload.SaleListingId });
            }

            return NotFound();
        }
        public async Task<IActionResult> PreviewFile(int id)
        {
            var fileUpload = await _unitOfWork.FileUpload.GetAsync(x => x.Id == id);

            if (fileUpload != null)
            {
                FileStream fs = new FileStream(fileUpload.FullPath, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");

            }
            return NotFound();
        }

        public async Task<JsonResult> GetFileDetails(int id)
        {
            var record = JsonConvert.SerializeObject(await _unitOfWork.FileUpload.GetAsync(x => x.Id == id));
            return Json(record);
        }

    }
}
