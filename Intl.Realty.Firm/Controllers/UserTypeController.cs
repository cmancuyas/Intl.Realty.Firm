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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserType obj)
        {
            var checkIfExists = await _unitOfWork.UserType.GetAsync(x => x.Name == obj.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Transaction Type already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                await _unitOfWork.UserType.AddAsync(obj);
                _unitOfWork.Save();
                TempData["success"] = "UserType created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> Edit(int? id)
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
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserType model)
        {
            if (ModelState.IsValid)
            {
                UserType? userTypeFromDb = await _unitOfWork.UserType.GetAsync(u => u.Id == model.Id);
                model.CreatedBy = userTypeFromDb.CreatedBy;
                model.CreatedAt = userTypeFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.UserType.UpdateAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "UserType updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
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
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
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

