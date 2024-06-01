using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<UserType> modelList = await _unitOfWork.UserType.GetAllAsync() as List<UserType> ?? throw new ArgumentException();

            List<UserTypeViewModel> viewModelList = modelList.Select(x => x.ToUserTypeViewModel()).ToList();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> UserTypeListPartialView()
        {
            var modelList = await _unitOfWork.UserType.GetAllAsync();

            var viewModel = modelList.ToUserTypeListViewModel();

            return PartialView("~/Views/UserType/Partial/UserTypeListPartial.cshtml", viewModel);
        }
        public IActionResult CreateModal()
        {
            CreateUserTypeViewModel viewModel= new CreateUserTypeViewModel();
            ViewBag.ViewModel = viewModel;
            return PartialView("~/Views/UserType/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserTypeViewModel viewModel)
        {
            var checkIfExists = await _unitOfWork.UserType.GetAsync(x => x.Name == viewModel.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "User Type already exists");
            }

            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToUserTypeModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.UserType.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "UserType created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> EditModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            UserType? model = await _unitOfWork.UserType.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditUserTypeViewModel viewModel = model.ToEditUserTypeModel();

            return PartialView("~/Views/UserType/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("UserType/Edit/{id:int}")]
        
        public async Task<IActionResult> Edit(EditUserTypeViewModel viewModel)
        {

            UserType? model = await _unitOfWork.UserType.GetAsync(u => u.Id == viewModel.Id);

            model.IsActive = viewModel.IsActive;
            model.Name = viewModel.Name;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.UserType.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "UserType updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            UserType? userTypeFromDb = await _unitOfWork.UserType.GetAsync(u => u.Id == id);

            var viewModel = userTypeFromDb.ToUserTypeViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/UserType/Modal/DeleteModal.cshtml", viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            UserType? obj = await _unitOfWork.UserType.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.UserType.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "UserType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

