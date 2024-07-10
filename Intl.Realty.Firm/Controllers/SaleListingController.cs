using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.FileUploadVM;
using Intl.Realty.Firm.Models.Models.ViewModel.IRFDealVM;
using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;

namespace Intl.Realty.Firm.Controllers
{
    public class SaleListingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaleListingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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


                // Create FileUploadData

                var listFileUploadModel = viewModel.CreateFileUploadsViewModel?.ToListFileUploadModel();
                if (listFileUploadModel != null)
                {
                    await _unitOfWork.FileUpload.AddRangeAsync(listFileUploadModel);
                }

                if (listFileUploadModel != null)
                {
                    var listFileUploadIds = listFileUploadModel.Select(x => x.Id).ToList();
                    var createdFileUploads = await _unitOfWork.FileUpload.GetAllByIdsAsync(listFileUploadIds);
                }

                //viewModel.CreateFileUploadViewModel.IsActive = newFileUpload.IsActive;
                //viewModel.CreateFileUploadViewModel.CreatedBy = newFileUpload.CreatedBy;
                //viewModel.CreateFileUploadViewModel.CreatedAt = newFileUpload.CreatedAt;

                viewModel.TransactionTypeId = transactionType.Id;
                viewModel.IRFDealId = NewIRFDeal.Id;
                //viewModel.FileUploadId = newFileUpload.Id;

                viewModel.IsActive = viewModel.IsActive;
                viewModel.CreatedBy = viewModel.CreatedBy;
                viewModel.CreatedAt = viewModel.CreatedAt;

                var saleListingModel = viewModel.ToSaleListingModel();
                await _unitOfWork.SaleListing.AddAsync(saleListingModel);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        public async Task<List<DocumentType>> GetDocumentTypesFromDocumentTypeAssignment(string transactionTypeName)
        {
            List<DocumentType> documentTypeList = new List<DocumentType>();

            var transactionType = await _unitOfWork.TransactionType.GetAsync(x => x.Description == transactionTypeName);

            var documentTypeAssignmentList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionType.Id, includeProperties: "DocumentType,TransactionType") as List<DocumentTypeAssignment>;

            var documentTypeIds = documentTypeAssignmentList?
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

            if (modelList == null)
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

            var ids = viewModelList.Select(o => o.Id).ToList();
            var documentTypeList = await _unitOfWork.SaleListing.GetAllAsync();
            var modelList = documentTypeList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.SaleListing.RemoveRangeAsync(modelList);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            return StatusCode(StatusCodes.Status200OK, ModelState);
        }
    }
}
