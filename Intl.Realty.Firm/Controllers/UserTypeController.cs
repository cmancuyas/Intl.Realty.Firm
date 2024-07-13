using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Models.ViewModel;
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
        public IActionResult Create(int? regionId, int? provinceId, int? municipalityId)
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
                var model = viewModel.FromCreateToUserTypeModel();

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

            var viewModel = model.ToEditUserTypeListViewModel();

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
                model = viewModel.FromEditToUserTypeModel();
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
            var UserTypeIEnum = await _unitOfWork.UserType.GetAllAsync();

            var viewModelIEnum = UserTypeIEnum.FromIEnumToUserTypeIEnumViewModel();

            return PartialView("~/Views/UserType/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editUserTypeViewModel = model.ToEditUserTypeListViewModel();

            return PartialView("~/Views/UserType/Modal/DeleteModal.cshtml", editUserTypeViewModel);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<UserType> modelList = new List<UserType>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var UserTypeViewModelList = modelList.ToUserTypeListViewModel();

            IEnumerable<UserTypeViewModel> modelIEnum = UserTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/UserType/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var modelList = model.FromModelToUserTypeListModel();

            await _unitOfWork.UserType.RemoveRangeAsync(modelList);

            return RedirectToAction(nameof(Index), new { deleteSuccess = true });

        }
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(IEnumerable<UserTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var UserTypeList = await _unitOfWork.UserType.GetAllAsync();
            var modelList = UserTypeList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.UserType.RemoveRangeAsync(modelList);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            return RedirectToAction(nameof(Index), new { deleteSuccess = true });
        }
    }
}
