using Intl.Realty.Firm.Models.Models;
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
            var modelList = await _unitOfWork.SaleListing.GetAllAsync();

            var viewModel = modelList.ToSaleListingListViewModel();

            return PartialView("~/Views/SaleListing/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateSaleListingViewModel viewModel = new CreateSaleListingViewModel();
            string transactionTypeName = "Sale Listing"; // 1 = Sale Listing
            viewModel.DocumentTypeList = await GetDocumentTypeFromDocumentTypeAssignment(transactionTypeName);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleListingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string selectedTransactionTypeId = Request.Form["TransactionTypeDDL"].ToString();

                var IRFDealModel = viewModel.CreateIRFDealViewModel?.ToIRFDealModel();

                IRFDealModel.CreatedAt = DateTime.Now;
                IRFDealModel.CreatedBy = 1;

                await _unitOfWork.IRFDeal.AddAsync(IRFDealModel);

                var IRFDealId = _unitOfWork.IRFDeal.GetAsync(x=>x.Id == IRFDealModel.Id);

                viewModel.CreateIRFDealViewModel.CreateFileUploadViewModel.IsActive = true;
                viewModel.CreateIRFDealViewModel.CreateFileUploadViewModel.CreatedBy = 1;
                viewModel.CreateIRFDealViewModel.CreateFileUploadViewModel.CreatedAt = DateTime.Now;
                var fileUploadViewModel = viewModel.CreateIRFDealViewModel.CreateFileUploadViewModel.ToFileUploadModel();

                await _unitOfWork.FileUpload.AddAsync(fileUploadViewModel);

                var model = viewModel.ToSaleListingModel();
                await _unitOfWork.SaleListing.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        public async Task<List<DocumentType>> GetDocumentTypeFromDocumentTypeAssignment(string transactionTypeName)
        {
            List<DocumentType> documentTypeList = new List<DocumentType>();

            var transactionType = await _unitOfWork.TransactionType.GetAsync(x=>x.Description == transactionTypeName);

            var documentTypeAssignmentList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionType.Id, includeProperties:"DocumentType,TransactionType") as List<DocumentTypeAssignment>;

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
    }
}
