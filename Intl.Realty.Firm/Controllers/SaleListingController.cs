using DENR_FAPIS.Utilities;
using Intl.Realty.Firm.Models.Helpers;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class SaleListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadRepository _fileUploadRepository;
        private readonly string _backupFileDirectory = "Uploads\\Documents\\";

        private int _userId = 1;
        public SaleListingController(IUnitOfWork unitOfWork, IFileUploadRepository fileUploadRepository)
        {
            _unitOfWork = unitOfWork;
            _fileUploadRepository = fileUploadRepository;
        }
        public async Task<IActionResult> Index()
        {
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
            string transactionTypeName = "Sale Listing"; // 1 = Sale Listing
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionTypeName);
            viewModel.TransactionTypeId = viewModel.TransactionType.Id;
            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionTypeName);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleListingViewModel viewModel)
        {
            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == "Sale Listing");

            // Create IRF Deal Data
            viewModel.TransactionType = transactionType;
            if (ModelState.IsValid)
            {
                var createIRFDealModel = viewModel.CreateIRFDealViewModel?.ToIRFDealModel();
                createIRFDealModel.CreatedAt = DateTime.Now;
                createIRFDealModel.CreatedBy = 1;
                await _unitOfWork.IRFDeal.AddAsync(createIRFDealModel);

                var NewIRFDeal = await _unitOfWork.IRFDeal.GetAsync(x => x.Id == createIRFDealModel.Id);

                // Get IRF Deal Data
                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.TransactionType = transactionType;
                viewModel.IRFDealId = NewIRFDeal.Id;
                if (NewIRFDeal != null)
                {
                    viewModel.CreateIRFDealViewModel.IsActive = NewIRFDeal.IsActive;
                    viewModel.CreateIRFDealViewModel.CreatedBy = NewIRFDeal.CreatedBy;
                    viewModel.CreateIRFDealViewModel.CreatedAt = NewIRFDeal.CreatedAt;
                }

                // Create SaleListing Data
                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.IRFDealId = NewIRFDeal.Id;

                viewModel.IsActive = viewModel.IsActive;
                viewModel.CreatedBy = viewModel.CreatedBy;
                viewModel.CreatedAt = viewModel.CreatedAt;

                var saleListingModel = viewModel.ToSaleListingModel();
                await _unitOfWork.SaleListing.AddAsync(saleListingModel);

                // Create FileUploadData
                if (viewModel.FileUploadList != null)
                {

                    var uploadPath = string.Empty;
                    var defaultPathFromConfig = _fileUploadRepository.GetDefaultUploadPathFromConfig();
                    if (defaultPathFromConfig != null)
                    {
                        uploadPath = defaultPathFromConfig;
                    }
                    else
                    {
                        uploadPath = _backupFileDirectory;
                    }
                    var userIdPath = _userId.ToString() + "\\";
                    uploadPath = Path.Combine(uploadPath, userIdPath);

                    if (viewModel.FileUploadList != null)
                    {
                        var fileUploadList = viewModel.FileUploadList.Files;
                        if (fileUploadList != null)
                        {
                            int index = 0;
                            foreach (var fileUpload in fileUploadList)
                            {
                                var (fileNameWithoutExtension, fileExtension) = await _fileUploadRepository.UploadFile(fileUpload, uploadPath);

                                if (viewModel.CreateFileUploadsViewModel != null)
                                {
                                    var createFileUpload = new FileUpload()
                                    {
                                        FileName = fileNameWithoutExtension,
                                        FileExtension = fileExtension,
                                        Directory = uploadPath,
                                        FullPath = uploadPath + fileNameWithoutExtension + fileExtension,
                                        FileSize = fileUpload.Length.ToString(),
                                        IsActive = true,
                                        CreatedAt = DateTime.Now,
                                        CreatedBy = _userId,
                                        SaleListingId = saleListingModel.Id,
                                        TransactionTypeId = transactionType.Id,
                                        DocumentTypeId = viewModel.CreateFileUploadsViewModel[index].DocumentTypeId

                                    };
                                    await _fileUploadRepository.AddAsync(createFileUpload);
                                }

                                index++;

                            }
                        }

                    }

                }



                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        //public List<FileUpload> CreateFileUploadData(FormFileUploadList formFileUploadList,
        //                                            int userId,
        //                                            int saleListingId,
        //                                            int transactionTypeId)
        //{
        //    // Set the folder and file names
        //    string folderName = "USER-" + userId;
        //    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\Documents\\" + folderName);
        //    string webDirectoryPath = "/Files/Documents/" + folderName;

        //    // Ensure the directory exists
        //    if (!Directory.Exists(directoryPath))
        //    {
        //        Directory.CreateDirectory(directoryPath);
        //    }

        //    var transactionType = _unitOfWork.TransactionType.GetAsync(x=>x.Id== transactionTypeId);
        //    var documentTypeTaskList = GetDocumentTypesFromDocumentTypeAssignment(transactionType.Result.Description);

        //    var documentTypes = documentTypeTaskList.Result;

        //    List<FileUpload> fileUploadList = new List<FileUpload>();
        //    FileUpload fileUpload = new FileUpload();
        //    int i = 0;
        //    if (formFileUploadList.Files != null)
        //    {
        //        foreach (var file in formFileUploadList.Files)
        //        {
        //            string fileNameWithPath = Path.Combine(directoryPath, file.FileName);
        //            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            fileUpload.TransactionTypeId = transactionTypeId;
        //            fileUpload.DocumentTypeId = documentTypes[i].Id;
        //            fileUpload.FileName = fileName;
        //            fileUpload.FilePath = directoryPath;
        //            fileUpload.FileSize = file.FileName.Length.ToString();
        //            fileUpload.FileType = Path.GetExtension(file.FileName);
        //            fileUpload.WebDirectoryPath = webDirectoryPath;
        //            fileUpload.OriginalFileName = file.FileName;
        //            fileUpload.IsActive = true;
        //            fileUpload.CreatedAt = DateTime.Now;
        //            fileUpload.CreatedBy = userId;
        //            fileUpload.SaleListingId = saleListingId;
        //            fileUploadList.Add(fileUpload);

        //        }
        //    }

        //    return fileUploadList;
        //}

        public async Task<List<DocumentType>> GetDocumentTypesFromDocumentTypeAssignment(string transactionTypeName)
        {
            List<DocumentType> documentTypeList = new List<DocumentType>();

            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == transactionTypeName);

            var documentTypeAssignmentListViewModel = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionType.Id, includeProperties: "DocumentType,TransactionType") as List<DocumentTypeAssignment>;

            var documentTypeIds = documentTypeAssignmentListViewModel?
                                    .GroupBy(x => x.DocumentType)
                                    .Select(grp => new DocumentType
                                    {
                                        Id = grp.Key.Id,
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
            var model = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id, includeProperties: "IRFDeal");
            var fileUploads = await _unitOfWork.FileUpload.GetAllAsync(x => x.SaleListingId == model.Id, includeProperties: "TransactionType,DocumentType,SaleListing");
            model.FileUploads = fileUploads.ToList();
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditSaleListingViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaleListingViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.SaleListing.GetAsync(x => x.Id == id, includeProperties: ("IRFDeal,FileUploads"));
                if (model == null)
                {
                    return NotFound();
                }

                model.IRFDeal = viewModel.IRFDeal;
                model.FileUploads = viewModel.FileUploads;

                model.IsActive = viewModel.IsActive;
                model.UpdatedBy = _userId;
                model.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.SaleListing.UpdateAsync(model);

                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
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

            var fileUploadList = await _unitOfWork.FileUpload.GetAllAsync(x => saleListingIds.Contains(x.Id));

            if (saleListingListToBeDeleted != null)
            {
                try
                {
                    foreach (var file in fileUploadList)
                    {
                        _fileUploadRepository.DeleteFile(file.FullPath);
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

        public FileResult DownloadFile(string filePath, string fileName)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + "/" + fileName);

            return File(fileBytes, "application/force-download", fileName);
        }

    }
}
