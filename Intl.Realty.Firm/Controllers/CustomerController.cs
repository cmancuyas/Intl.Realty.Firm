using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.CustomerVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Intl.Realty.Firm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork
                                    )
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateCustomerViewModel viewModel = new CreateCustomerViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;
                viewModel.IsActive = true;
                viewModel.CreatedBy = Convert.ToInt32(userId);
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToCustomerModel();

                await _unitOfWork.Customer.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = 15;
            var model = await _unitOfWork.Customer.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditCustomerViewModel();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCustomerViewModel viewModel)
        {
            var userId = Session.UserId;

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.Customer.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToCustomerModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.Customer.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.Customer.GetAllAsync();

            var viewModelIEnum = departmentIEnum.ToCustomerIEnumViewModel(); ;

            return PartialView("~/Views/Customer/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.Customer.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteCustomerIEnumViewModel = modelIEnum.ToDeleteCustomerIEnumViewModel();

            return PartialView("~/Views/Customer/Modal/DeleteMultipleModal.cshtml", deleteCustomerIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteCustomerViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.Customer.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.Customer.RemoveRangeAsync(modelList);

                    return RedirectToAction(nameof(Index), new { deleteSuccess = true });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
