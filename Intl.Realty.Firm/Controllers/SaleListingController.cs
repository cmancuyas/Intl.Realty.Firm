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
            var transactionTypeId = 28; // 1 = SaleListing
            viewModel.DocumentTypeList = await GetDocumentTypeFromDocumentTypeAssignment(transactionTypeId);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleListingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string selectedTransactionTypeId = Request.Form["TransactionTypeDDL"].ToString();
                var model = new IRFDeal
                {
                    PropertyAddress = viewModel.PropertyAddress ?? "",
                    TransactionTypeId = Convert.ToInt32(selectedTransactionTypeId),
                    IsActive = true,
                    CreatedAt = DateTime.Now, // Set the CreatedAt property to the current date/time
                    CreatedBy = 1

                    // Set other properties as needed
                };

                await _unitOfWork.IRFDeal.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        public async Task<List<DocumentType>> GetDocumentTypeFromDocumentTypeAssignment(int transactionTypeId)
        {
            var documentTypeAssignmentList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == transactionTypeId, includeProperties:"DocumentType,TransactionType") as List<DocumentTypeAssignment>;

            var documentTypeIds = documentTypeAssignmentList?
                                    .GroupBy(x => x.DocumentType)
                                    .Select(grp => new DocumentType
                                    {
                                        Id = grp.Key.Id,
                                        Name = grp.Key.Name,
                                        IsActive = grp.Key.IsActive,
                                        CreatedAt = grp.Key.CreatedAt,
                                        CreatedBy = grp.Key.CreatedBy,
                                        UpdatedAt = grp.Key.UpdatedAt,
                                        UpdatedBy = grp.Key.UpdatedBy
                                    }).ToList();
            if (documentTypeIds != null)
            {
                var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync(x => documentTypeIds.Contains(x));
                return documentTypeIEnum.ToList();
            }
            return null;
        }
    }
}
