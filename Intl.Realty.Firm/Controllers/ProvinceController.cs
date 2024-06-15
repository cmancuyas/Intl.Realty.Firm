using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProvinceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<Province> modelList = await _unitOfWork.Province.GetAllAsync() as List<Province> ?? throw new ArgumentException();

            List<ProvinceViewModel> viewModelList = modelList.Select(x => x.ToProvinceViewModel()).ToList();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.Province.GetAllAsync();

            var viewModel = modelList.ToProvinceListViewModel();

            return PartialView("~/Views/Province/Partial/ListPartial.cshtml", viewModel);
        }
        public IActionResult CreateModal()
        {
            CreateProvinceViewModel viewModel = new CreateProvinceViewModel();
            return PartialView("~/Views/Province/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProvinceViewModel viewModel)
        {

            var checkIfExists = await _unitOfWork.Province.GetAsync(x => x.Description == viewModel.Description);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "User Type already exists");
            }

            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToProvinceModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.Province.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "Province created successfully";
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
            Province? model = await _unitOfWork.Province.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditProvinceViewModel viewModel = model.ToEditProvinceModel();

            return PartialView("~/Views/Province/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("Province/Edit/{id:int}")]

        public async Task<IActionResult> Edit(EditProvinceViewModel viewModel)
        {

            Province? model = await _unitOfWork.Province.GetAsync(u => u.Id == viewModel.Id);

            model.IsActive = viewModel.IsActive;
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Province.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "Province updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Province? userTypeFromDb = await _unitOfWork.Province.GetAsync(u => u.Id == id);

            var viewModel = userTypeFromDb.ToProvinceViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/Province/Modal/DeleteModal.cshtml", viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            Province? obj = await _unitOfWork.Province.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.Province.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "Province deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

