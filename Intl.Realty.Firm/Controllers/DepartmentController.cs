using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Intl.Realty.Firm.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork
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
            CreateDepartmentViewModel viewModel = new CreateDepartmentViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;
                viewModel.IsActive = true;
                viewModel.CreatedBy = Convert.ToInt32(userId);
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToDepartmentModel();

                await _unitOfWork.Department.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = Session.UserId;
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditDepartmentViewModel();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDepartmentViewModel viewModel)
        {
            var userId = Session.UserId;

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToDepartmentModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.Department.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.Department.GetAllAsync();

            var viewModelIEnum = departmentIEnum.ToDepartmentIEnumViewModel(); ;

            return PartialView("~/Views/Department/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.Department.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteDepartmentIEnumViewModel = modelIEnum.ToDeleteDepartmentIEnumViewModel();

            return PartialView("~/Views/Department/Modal/DeleteMultipleModal.cshtml", deleteDepartmentIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteDepartmentViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.Department.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.Department.RemoveRangeAsync(modelList);

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
