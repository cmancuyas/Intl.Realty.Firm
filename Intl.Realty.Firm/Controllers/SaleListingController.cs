using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.SaleListingVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;
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
            var transactionTypeList = await _unitOfWork.TransactionType.GetAllAsync();
            var documentTypeAssignmentList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync();
            var documentTypeList = await _unitOfWork.DocumentType.GetAllAsync();
            viewModel.TransactionTypeIEnum = SelectListConverter.CreateSelectList(transactionTypeList.ToList(), x => x.Id, x => x.Name);
            viewModel.DocumentTypeAssignmentList = documentTypeAssignmentList.ToList();
            viewModel.DocumentTypeList = documentTypeList.ToList();

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
    }
}
