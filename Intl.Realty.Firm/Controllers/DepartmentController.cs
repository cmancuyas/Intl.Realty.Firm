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
        private int _userId = 1;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateModal()
        {
            CreateDepartmentViewModel viewModel = new CreateDepartmentViewModel();
            return PartialView("~/Views/Department/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new Department
                {
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    IsActive = true,
                    CreatedAt = DateTime.Now, // Set the CreatedAt property to the current date/time
                    CreatedBy = _userId,

                    // Set other properties as needed
                };

                await _unitOfWork.Department.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditModal(int? id)
        {
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            EditDepartmentViewModel viewModel = model.ToEditDepartmentViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return PartialView("~/Views/Department/Modal/EditModal.cshtml", viewModel);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel viewModel)
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
                model.Code = viewModel.Code;
                model.Description = viewModel.Description;
                model.IsActive = viewModel.IsActive;
                model.UpdatedBy = _userId;
                model.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.Department.UpdateAsync(model);

                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var transactionTypeList = await _unitOfWork.Department.GetAllAsync();

            var viewModel = transactionTypeList.ToDepartmentListViewModel();

            return PartialView("~/Views/Department/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.Department.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editDepartmentViewModel = model.ToEditDepartmentViewModel();

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

            var transactionTypeViewModelList = modelList.ToDepartmentListViewModel();

            IEnumerable<DepartmentViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

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
            await _unitOfWork.Department.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<DepartmentViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var departmentTypeList = await _unitOfWork.Department.GetAllAsync();
            var modelList = departmentTypeList.Where(o => ids.Contains(o.Id)).ToList();

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