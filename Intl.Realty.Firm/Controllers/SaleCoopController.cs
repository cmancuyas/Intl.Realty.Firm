using DENR_FAPIS.Models.Utilities;
using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.DataTable;
using Intl.Realty.Firm.Models.Models.ViewModel.SaleCoopVM;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Service.IServices;
using Intl.Realty.Firm.Utilities;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Intl.Realty.Firm.Controllers
{
    public class SaleCoopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigurationService _configurationService;
        private readonly IExportService<SaleCoopExport> _exportService;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly string _backupFileDirectory = "Uploads\\Documents\\";
        private readonly string _transactionTypeName = "Sale Coop";
        public SaleCoopController(IUnitOfWork unitOfWork,
                                    IConfigurationService configurationService,
                                    IExportService<SaleCoopExport> exportService,
                                    IFileHandlerService fileHandlerService)
        {
            _unitOfWork = unitOfWork;
            _configurationService = configurationService;
            _exportService = exportService;
            _fileHandlerService = fileHandlerService;
        }
        public async Task<IActionResult> Index()
        {

            //Get values from the current user

            List<SaleCoop> modelList = await _unitOfWork.SaleCoop.GetAllAsync(includeProperties: "IRFDeal,TransactionType,DealStatus") as List<SaleCoop> ?? throw new ArgumentException();

            List<SaleCoopViewModel> viewModelList = modelList.ToSaleCoopListViewModel();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var userId = Session.UserId;

            var modelList = await _unitOfWork.SaleCoop
                            .GetAllAsync(includeProperties: "IRFDeal,TransactionType,DealStatus");


            var userIdList = modelList.Select(x => x.CreatedBy).ToList();

            var users = await _unitOfWork.User.GetAllAsync(x => userIdList.Contains(x.Id));

            SaleCoopListViewModel listViewModel = new SaleCoopListViewModel();

            listViewModel.SaleCoopsViewModel = modelList.ToSaleCoopListViewModel();
            listViewModel.Users = users.ToList();

            return PartialView("~/Views/SaleCoop/Partial/ListPartial.cshtml", listViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            CreateSaleCoopViewModel viewModel = new CreateSaleCoopViewModel();

            string transactionTypeName = _transactionTypeName; // 1 = Sale Listing
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionTypeName);
            viewModel.TransactionTypeId = viewModel.TransactionType.Id;
            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionTypeName);


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleCoopViewModel viewModel)
        {
            var userId = Session.UserId;

            string transactionTypeName = _transactionTypeName; // 1 = Sale Listing
            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == _transactionTypeName); //Sale Listing
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionType.Description);
            viewModel.TransactionTypeId = viewModel.TransactionType.Id;
            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionType.Description);

            // Create IRF Deal Data
            viewModel.TransactionType = transactionType;
            if (ModelState.IsValid)
            {
                var createIRFDealModel = viewModel.CreateIRFDealViewModel?.ToIRFDealModel();
                createIRFDealModel!.CreatedAt = DateTime.UtcNow;
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

                // Create SaleCoop Data
                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.IRFDealId = NewIRFDeal!.Id;
                if (viewModel.DealStatusId == 0)
                {
                    viewModel.DealStatusId = 1;

                }
                viewModel.IsActive = true;
                viewModel.CreatedBy = userId;
                viewModel.CreatedAt = DateTime.UtcNow;

                var saleCoopModel = viewModel.ToSaleCoopModel();
                await _unitOfWork.SaleCoop.AddAsync(saleCoopModel);

                // Create FileUploadData
                if (viewModel.CreateFileUploadListViewModel?.CreateFileUploadsViewModel != null)
                {
                    var uploadPath = CreateFilePath(saleCoopModel.Id);


                    await CreateFileUploadData(viewModel.CreateFileUploadListViewModel.CreateFileUploadsViewModel,
                                                saleCoopModel.Id,
                                                transactionType.Id,
                                                viewModel.DocumentTypeList,
                                                uploadPath);

                }
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        private async Task<bool> CreateFileUploadData(List<CreateFileUploadViewModel> createFileUploadsViewModel,
                                                    int saleCoopModelId,
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
                    updatedUploadPath = Path.Combine(uploadPath, _transactionTypeName, createFileUploadDocumentType.Description + "\\");
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
                            SaleCoopId = saleCoopModelId,
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

            var documentTypeAssignmentListViewModel = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionType.Id, includeProperties: "TransactionType,DocumentType") as List<DocumentTypeAssignment>;

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
            var saleCoopModel = await _unitOfWork.SaleCoop.GetAsync(x => x.Id == id, includeProperties: "IRFDeal,TransactionType,DealStatus");

            int saleCoopId = saleCoopModel.Id;

            var fileUploads = await _unitOfWork.FileUpload.GetAllAsync(x => x.SaleCoopId == saleCoopId, includeProperties: "SaleCoop,TransactionType,DocumentType");

            if (saleCoopModel == null)
            {
                return NotFound();
            }
            var viewModel = saleCoopModel.ToEditSaleCoopViewModel();

            viewModel.FileUploads = fileUploads.ToList();

            var fileUploadsWithFiles = FilterFileUploadsWithFilesOnly(viewModel.FileUploads);

            viewModel.FileUploads = fileUploadsWithFiles;

            viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(saleCoopModel?.TransactionType?.Description ?? _transactionTypeName);
            
            var dealStatusIEnum = await _unitOfWork.DealStatus.GetAllAsync();
            viewModel.DealStatusList = dealStatusIEnum.ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditSaleCoopViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = Session.UserId;

                string transactionTypeName = _transactionTypeName; // 1 = Sale Listing
                var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == _transactionTypeName); //Sale Listing
                viewModel.TransactionType = await _unitOfWork.TransactionType.GetByNameAsync(transactionType.Description);
                viewModel.TransactionTypeId = viewModel.TransactionType.Id;
                viewModel.DocumentTypeList = await GetDocumentTypesFromDocumentTypeAssignment(transactionType.Description);

                var saleCoopModel = await _unitOfWork.SaleCoop.GetAsync(x => x.Id == id, includeProperties: "IRFDeal");
                if (saleCoopModel == null)
                {
                    return NotFound();
                }
                var fileUploadsWithFiles = FilterFileUploadsWithFilesOnly(saleCoopModel.FileUploads);
                saleCoopModel.FileUploads = fileUploadsWithFiles;
                saleCoopModel.IRFDeal = viewModel.EditIRFDealViewModel?.ToIRFDealModel();

                string selectedDealStatusId = Request.Form["DealStatusDDL"].ToString();
                saleCoopModel.DealStatusId = Convert.ToInt32(selectedDealStatusId);
                // Update IRD Deal
                if (saleCoopModel.IRFDeal != null)
                {
                    await _unitOfWork.IRFDeal.UpdateAsync(saleCoopModel.IRFDeal);
                }

                saleCoopModel.IsActive = viewModel.IsActive;
                saleCoopModel.UpdatedBy = userId;
                saleCoopModel.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.SaleCoop.UpdateAsync(saleCoopModel);

                // Create FileUploadData

                var uploadPath = CreateFilePath(saleCoopModel.Id);

                if (viewModel.CreateFileUploadListViewModel?.CreateFileUploadsViewModel != null)
                {
                    await CreateFileUploadData(viewModel.CreateFileUploadListViewModel.CreateFileUploadsViewModel,
                                                 saleCoopModel.Id,
                                                 transactionType.Id,
                                                 viewModel.DocumentTypeList,
                                                 uploadPath);
                }

                return RedirectToAction(nameof(Index), new { addSuccess = true });

            }

            return View(viewModel);
        }

        private string CreateFilePath(int saleCoopId)
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

            uploadPath = Path.Combine(uploadPath, userIdPath + saleCoopId + "\\");

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
            var model = await _unitOfWork.SaleCoop.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editSaleCoopViewModel = model.ToEditSaleCoopViewModel();

            return PartialView("~/Views/SaleCoop/Modal/DeleteModal.cshtml", editSaleCoopViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<SaleCoop> modelList = new List<SaleCoop>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.SaleCoop.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null || !modelList.Any())
            {
                return NotFound();
            }

            var transactionTypeViewModelList = modelList.ToSaleCoopListViewModel();

            IEnumerable<SaleCoopViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/SaleCoop/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.SaleCoop.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            await _unitOfWork.SaleCoop.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<SaleCoopViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var saleCoopIds = viewModelList.Select(x => x.Id).ToList();
            var saleCoopListToBeDeleted = await _unitOfWork.SaleCoop.GetAllAsync(x => saleCoopIds.Contains(x.Id));

            var IRFDealIds = saleCoopListToBeDeleted.Select(x => x.IRFDealId).ToList();
            var IRFDealListToBeDeleted = await _unitOfWork.IRFDeal.GetAllAsync(x => IRFDealIds.Contains(x.Id));
            if (saleCoopIds.Any() && saleCoopListToBeDeleted.Any())
            {
                var fileUploadList = await _unitOfWork.FileUpload.GetAllAsync(x => saleCoopIds.Contains(x.SaleCoopId ?? 0));

                if (saleCoopListToBeDeleted != null)
                {
                    try
                    {
                        foreach (var file in fileUploadList)
                        {
                            await _fileHandlerService.DeleteFile(file.FullPath);
                        }

                        await _unitOfWork.FileUpload.RemoveRangeAsync(fileUploadList);
                        await _unitOfWork.SaleCoop.RemoveRangeAsync(saleCoopListToBeDeleted);
                        await _unitOfWork.IRFDeal.RemoveRangeAsync(IRFDealListToBeDeleted);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }

                return StatusCode(StatusCodes.Status200OK, ModelState);
            }
            return BadRequest(ModelState);
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

                return RedirectToAction(nameof(Edit), new { id = fileUpload.SaleCoopId });
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

        public async Task<JsonResult> GetDocumentTypesThatHasFileUploads(int saleCoopId)
        {
            var fileUploads = await _unitOfWork.FileUpload.GetAllAsync(x => x.SaleCoopId == saleCoopId);
            var documentTypeIds = fileUploads.Select(x => x.DocumentTypeId).ToArray();

            return Json(documentTypeIds);
        }
        [HttpPost]
        public async Task<IActionResult> GetSaleCoopList([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            // Default order criteria
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            // Initialize order criteria
            (orderCriteria, orderAscendingDirection) = InitializeOrderCriteria(dtParameters, orderCriteria, orderAscendingDirection);

            // Query initialization
            var result = await _unitOfWork.SaleCoop.AsQueryableAsync(includeProperties: "IRFDeal,TransactionType,DealStatus");

            if (!string.IsNullOrEmpty(searchBy))
            {
                if (searchBy.Contains(":") || searchBy.Contains("+"))
                {
                    result = FilterSearchWithParameters(result, searchBy);
                }
                else
                {
                    result = DefaultFilterSearch(result, searchBy);
                }
            }

            // Apply ordering
            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria!, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria!, DtOrderDir.Desc);

            // Get the count of filtered and total results
            var filteredResultsCount = result.Count();
            var totalResultsCount = await _unitOfWork.SaleCoop.CountAsync();

            // Prepare the result for DataTables
            var jsonResult = new DtResult<SaleCoop>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            };

            return Json(jsonResult);
        }

        private IQueryable<SaleCoop> DefaultFilterSearch(IQueryable<SaleCoop> result, string searchBy)
        {
            result = result.Where(r => r.TransactionType!.Description != null && r.TransactionType.Description.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.DealStatus!.Description != null && r.DealStatus.Description.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.PropertyAddress != null && r.IRFDeal!.PropertyAddress.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyerAgentName != null && r.IRFDeal!.BuyerAgentName.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyerBrokerage != null && r.IRFDeal!.BuyerBrokerage.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyerBrokerageFax != null && r.IRFDeal!.BuyerBrokerageFax.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyerName != null && r.IRFDeal!.BuyerName.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyerBrokerageFax != null && r.IRFDeal!.BuyerBrokerageFax.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyersLawyer != null && r.IRFDeal!.BuyersLawyer.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyersLawyerAddress != null && r.IRFDeal!.BuyersLawyerAddress.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyersPhoneNumber != null && r.IRFDeal!.BuyersPhoneNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.BuyingCommissionPercentage.ToString() != null && r.IRFDeal!.BuyingCommissionPercentage.ToString().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.SellersLawyer != null && r.IRFDeal!.SellersLawyer.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.SellersLawyerAddress != null && r.IRFDeal!.SellersLawyerAddress.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.SellersPhoneNumber != null && r.IRFDeal!.SellersPhoneNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.ListingAgentName != null && r.IRFDeal!.ListingAgentName.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.ListingBrokerage != null && r.IRFDeal!.ListingBrokerage.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.ListingBrokerageFax != null && r.IRFDeal!.ListingBrokerageFax.ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.ListingCommissionPercentage.ToString() != null && r.IRFDeal!.ListingCommissionPercentage.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.FinalSalePrice.ToString() != null && r.IRFDeal!.FinalSalePrice.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.FinalClosingDate.ToString() != null && r.IRFDeal!.FinalClosingDate.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.DepositAmount.ToString() != null && r.IRFDeal!.DepositAmount.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                   r.IRFDeal!.DepositDate.ToString() != null && r.IRFDeal!.DepositDate.ToString().ToUpper().Contains(searchBy.ToUpper())
                   );
            return result;
        }
        private IQueryable<SaleCoop> FilterSearchWithParameters(IQueryable<SaleCoop> query, string searchBy)
        {
            var filters = searchBy.Split('+'); // Split the input by '+'

            foreach (var filter in filters)
            {
                var keyValue = filter.Split(':'); // Split each filter by ':'
                if (keyValue.Length != 2) continue;

                var key = keyValue[0].Trim().ToLower();
                var value = keyValue[1].Trim().ToUpper();

                switch (key)
                {
                    case "municipality":
                        query = query.Where(r => r.TransactionType != null && r.TransactionType.Description.ToUpper().Contains(value));
                        break;
                    case "buyeragentname":
                        query = query.Where(r => r.IRFDeal!.BuyerAgentName != null && r.IRFDeal.BuyerAgentName.ToUpper().Contains(value));
                        break;
                    case "BuyerBrokerage":
                        query = query.Where(r => r.IRFDeal!.BuyerBrokerage != null && r.IRFDeal.BuyerBrokerage.ToUpper().Contains(value));
                        break;
                    case "BuyerBrokerageFax":
                        query = query.Where(r => r.IRFDeal!.BuyerBrokerageFax != null && r.IRFDeal.BuyerBrokerageFax.ToUpper().Contains(value));
                        break;
                    case "BuyerName":
                        query = query.Where(r => r.IRFDeal!.BuyerName != null && r.IRFDeal.BuyerName.ToUpper().Contains(value));
                        break;
                    case "buyerslawyer":
                        query = query.Where(r => r.IRFDeal!.BuyersLawyer != null && r.IRFDeal.BuyersLawyer.ToUpper().Contains(value));
                        break;
                    case "BuyersLawyerAddress":
                        query = query.Where(r => r.IRFDeal!.BuyersLawyerAddress != null && r.IRFDeal.BuyersLawyerAddress.ToUpper().Contains(value));
                        break;
                    case "BuyersPhoneNumber":
                        query = query.Where(r => r.IRFDeal!.BuyersPhoneNumber != null && r.IRFDeal.BuyersPhoneNumber.ToUpper().Contains(value));
                        break;
                    case "BuyingCommissionPercentage":
                        query = query.Where(r => r.IRFDeal!.BuyingCommissionPercentage.ToString().ToUpper().Contains(value));
                        break;
                    case "SellersLawyer":
                        query = query.Where(r => r.IRFDeal!.SellersLawyer != null && r.IRFDeal.SellersLawyer.ToUpper().Contains(value));
                        break;
                    case "SellersLawyerAddress":
                        query = query.Where(r => r.IRFDeal!.SellersLawyerAddress != null && r.IRFDeal.SellersLawyerAddress.ToUpper().Contains(value));
                        break;
                    case "SellersPhoneNumber":
                        query = query.Where(r => r.IRFDeal!.SellersPhoneNumber != null && r.IRFDeal.SellersPhoneNumber.ToUpper().Contains(value));
                        break;
                    case "ListingAgentName":
                        query = query.Where(r => r.IRFDeal!.ListingAgentName != null && r.IRFDeal.ListingAgentName.ToUpper().Contains(value));
                        break;
                    case "ListingBrokerage":
                        query = query.Where(r => r.IRFDeal!.ListingBrokerage != null && r.IRFDeal.ListingBrokerage.ToUpper().Contains(value));
                        break;
                    case "ListingBrokerageFax":
                        query = query.Where(r => r.IRFDeal!.ListingBrokerageFax != null && r.IRFDeal.ListingBrokerageFax.ToUpper().Contains(value));
                        break;
                    case "ListingCommissionPercentage":
                        query = query.Where(r => r.IRFDeal!.ListingCommissionPercentage.ToString().ToUpper().Contains(value));
                        break;
                    case "FinalSalePrice":
                        query = query.Where(r => r.IRFDeal!.FinalSalePrice.ToString() != null && r.IRFDeal.FinalSalePrice.ToString().ToUpper().Contains(value));
                        break;
                    case "FinalClosingDate":
                        query = query.Where(r => r.IRFDeal!.FinalClosingDate.ToString() != null && r.IRFDeal.FinalClosingDate.ToString().ToUpper().Contains(value));
                        break;
                    case "DepositAmount":
                        query = query.Where(r => r.IRFDeal!.DepositAmount.ToString() != null && r.IRFDeal.DepositAmount.ToString().Contains(value));
                        break;
                    case "DepositDate":
                        query = query.Where(r => r.IRFDeal!.DepositDate.ToString() != null && r.IRFDeal.DepositDate.ToString().Contains(value));
                        break;
                    case "DealStatus":
                        query = query.Where(r => r.DealStatus!.Description! != null && r.DealStatus!.Description!.ToUpper().Contains(value));
                        break;
                    default:
                        // If there's an unrecognized filter key, you can choose to ignore it or handle it as needed
                        break;
                }
            }

            return query;
        }

        private (string, bool) InitializeOrderCriteria(DtParameters dtParameters, string orderCriteria, bool orderAscendingDirection)
        {
            var orderCriteriaTemp = "Id";
            var orderAscendingDirectionTemp = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                try
                {
                    orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirectionTemp = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                catch (Exception ex)
                {
                    try
                    {
                        orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[1].Column].Data;
                        orderAscendingDirectionTemp = dtParameters.Order[1].Dir.ToString().ToLower() == "asc";
                    }
                    catch (Exception ex1)
                    {
                        orderCriteriaTemp = "Id";
                        orderAscendingDirectionTemp = true;
                    }
                }
            }

            if (orderCriteriaTemp == null)
            {
                orderCriteriaTemp = "Id";
                orderAscendingDirectionTemp = true;
            }

            if (orderCriteriaTemp != null)
            {
                orderCriteria = orderCriteriaTemp;
                orderAscendingDirection = orderAscendingDirectionTemp;
            }

            return (orderCriteria, orderAscendingDirection);
        }
        [HttpPost]
        public async Task<IActionResult> ExportTable([FromQuery] string format, [FromForm] string dtParametersJson)
        {
            var dtParameters = new DtParameters();
            if (!string.IsNullOrEmpty(dtParametersJson))
            {
                dtParameters = JsonConvert.DeserializeObject<DtParameters>(dtParametersJson);
            }
            if (dtParameters != default)
            {
                var searchBy = dtParameters.Search?.Value;

                var orderCriteria = "Id";
                var orderAscendingDirection = true;

                (orderCriteria, orderAscendingDirection) = InitializeOrderCriteria(dtParameters, orderCriteria, orderAscendingDirection);

                var result = _unitOfWork.SaleCoop.AsQueryable(includeProperties: "IRFDeal,TransactionType,DealStatus");

                if (!string.IsNullOrEmpty(searchBy))
                {
                    if (searchBy.Contains(":") || searchBy.Contains("+"))
                    {
                        result = FilterSearchWithParameters(result, searchBy);
                    }
                    else
                    {
                        result = DefaultFilterSearch(result, searchBy);
                    };

                }

                result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

                var resultList = await result.ToListAsync();

                var resultListForExport = resultList.ToSaleCoopListExport();

                switch (format)
                {
                    case ExportFormat.Excel:
                        return File(
                            await _exportService.ExportToExcel(resultListForExport),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "data.xlsx");

                    case ExportFormat.Csv:
                        return File(_exportService.ExportToCsv(resultListForExport),
                            "application/csv",
                            "data.csv");

                    case ExportFormat.Html:
                        return File(_exportService.ExportToHtml(resultListForExport),
                            "application/csv",
                            "data.html");

                    case ExportFormat.Json:
                        return File(_exportService.ExportToJson(resultListForExport),
                            "application/json",
                            "data.json");

                    case ExportFormat.Xml:
                        return File(_exportService.ExportToXml(resultListForExport),
                            "application/xml",
                            "data.xml");

                    case ExportFormat.Yaml:
                        return File(_exportService.ExportToYaml(resultListForExport),
                            "application/yaml",
                            "data.yaml");
                }
            }
            return null;
        }
    }
}
