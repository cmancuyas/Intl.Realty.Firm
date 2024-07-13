using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class DepartmentController : Controller
    {
        private int _userId = 1;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? regionId, int? provinceId, int? municipalityId)
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
                viewModel.IsActive = true;
                viewModel.CreatedBy = _userId;
                viewModel.CreatedAt = DateTime.UtcNow;  
                var model = viewModel.FromCreateToDepartmentModel();

                await _unitOfWork.Department.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditDepartmentListViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDepartmentViewModel viewModel)
        {
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
                model = viewModel.FromEditToDepartmentModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;
                await _unitOfWork.Department.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var DepartmentIEnum = await _unitOfWork.Department.GetAllAsync();

            var viewModelIEnum = DepartmentIEnum.FromIEnumToDepartmentIEnumViewModel();

            return PartialView("~/Views/Department/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editDepartmentViewModel = model.ToEditDepartmentListViewModel();

            return PartialView("~/Views/Department/Modal/DeleteModal.cshtml", editDepartmentViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<Department> modelList = new List<Department>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var DepartmentViewModelList = modelList.ToDepartmentListViewModel();

            IEnumerable<DepartmentViewModel> modelIEnum = DepartmentViewModelList.AsEnumerable();

            return PartialView("~/Views/Department/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var modelList = model.FromModelToDepartmentListModel();

            await _unitOfWork.Department.RemoveRangeAsync(modelList);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DepartmentViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var DepartmentList = await _unitOfWork.Department.GetAllAsync();
            var modelList = DepartmentList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.Department.RemoveRangeAsync(modelList);
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
