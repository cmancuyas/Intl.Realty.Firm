using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class UserTypeController : Controller
    {
        private int _userId = 1;
        private readonly IUnitOfWork _unitOfWork;
        public UserTypeController(IUnitOfWork unitOfWork)
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
            CreateUserTypeViewModel viewModel = new CreateUserTypeViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.IsActive = true;
                viewModel.CreatedBy = _userId;
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToUserTypeModel();

                await _unitOfWork.UserType.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditUserTypeViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToUserTypeModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;

                await _unitOfWork.UserType.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var userTypeIEnum = await _unitOfWork.UserType.GetAllAsync();

            var viewModelIEnum = userTypeIEnum.ToUserTypeIEnumViewModel();

            return PartialView("~/Views/UserType/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.UserType.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteUserTypeIEnumViewModel = modelIEnum.ToDeleteUserTypeIEnumViewModel;

            return PartialView("~/Views/UserType/Modal/DeleteMultipleModal.cshtml", deleteUserTypeIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteUserTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.UserType.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.UserType.RemoveRangeAsync(modelList);

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
