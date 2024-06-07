using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DepartmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<Department> modelList = await _unitOfWork.Department.GetAllAsync() as List<Department> ?? throw new ArgumentException();

            List<DepartmentViewModel> viewModelList = modelList.Select(x => x.ToDepartmentViewModel()).ToList();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.Department.GetAllAsync();

            var viewModel = modelList.ToDepartmentListViewModel();

            return PartialView("~/Views/Department/Partial/ListPartial.cshtml", viewModel);
        }
        public IActionResult CreateModal()
        {
            CreateDepartmentViewModel viewModel= new CreateDepartmentViewModel();
            return PartialView("~/Views/Department/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentViewModel viewModel)
        {
            var checkIfExists = await _unitOfWork.Department.GetAsync(x => x.Name == viewModel.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Department already exists");
            }

            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToDepartmentModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.Department.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "Department created successfully";
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }
            //return RedirectToAction(nameof(Index), new { addSuccess = false });

            return View("Index");
        }
        public async Task<IActionResult> EditModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Department? model = await _unitOfWork.Department.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditDepartmentViewModel viewModel = model.ToEditDepartmentModel();

            return PartialView("~/Views/Department/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("Department/Edit/{id:int}")]
        
        public async Task<IActionResult> Edit(EditDepartmentViewModel viewModel)
        {

            Department? model = await _unitOfWork.Department.GetAsync(u => u.Id == viewModel.Id);

            model.IsActive = viewModel.IsActive;
            model.Name = viewModel.Name;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Department.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "Department updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Department? model = await _unitOfWork.Department.GetAsync(u => u.Id == id);

            var viewModel = model.ToDepartmentViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/Department/Modal/DeleteModal.cshtml", viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            Department? obj = await _unitOfWork.Department.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.Department.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "Department deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

