using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.UserVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Intl.Realty.Firm.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateUserViewModel viewModel = new CreateUserViewModel();
            var departmentIEnum = await _unitOfWork.Department.GetAllAsync();
            var roleIEnum = await _unitOfWork.Role.GetAllAsync();
            var employmentStatusIEnum = await _unitOfWork.EmploymentStatus.GetAllAsync();
            viewModel.DepartmentIEnum = SelectListConverter.CreateSelectList(departmentIEnum.ToList(), x => x.Id, x => x.Description);
            viewModel.RoleIEnum = SelectListConverter.CreateSelectList(roleIEnum.ToList(), x => x.Id, x => x.Description);
            viewModel.EmploymentStatusIEnum = SelectListConverter.CreateSelectList(employmentStatusIEnum.ToList(), x => x.Id, x => x.Description);
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;

                string selectedDepartmentId = Request.Form["DepartmentDDL"].ToString();
                string selectedRoleId = Request.Form["RoleDDL"].ToString();
                string selectedEmploymentStatusId = Request.Form["EmploymentStatusDDL"].ToString();

                viewModel.IsActive = true;
                viewModel.CreatedBy = Convert.ToInt32(userId);
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToUserModel();

                model.DepartmentId = Convert.ToInt32(selectedDepartmentId);
                model.RoleId = Convert.ToInt32(selectedRoleId);
                model.EmploymentStatusId = 1; // Set to 1 - Active during create
                await _unitOfWork.User.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = Session.UserId;
            var model = await _unitOfWork.User.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditUserViewModel();

            var departmentIEnum = await _unitOfWork.Department.GetAllAsync();
            var roleIEnum = await _unitOfWork.Role.GetAllAsync();
            var employmentStatusIEnum = await _unitOfWork.EmploymentStatus.GetAllAsync();

            viewModel.DepartmentList = departmentIEnum.ToList();
            viewModel.RoleList = roleIEnum.ToList();
            viewModel.EmploymentStatusList = employmentStatusIEnum.ToList();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserViewModel viewModel)
        {
            var userId = Session.UserId;

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.User.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToUserModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.User.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.User.GetAllAsync(includeProperties:"Department,Role,EmploymentStatus");

            var viewModelIEnum = departmentIEnum.ToUserIEnumViewModel(); ;

            return PartialView("~/Views/User/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.User.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteUserIEnumViewModel = modelIEnum.ToDeleteUserIEnumViewModel();

            return PartialView("~/Views/User/Modal/DeleteMultipleModal.cshtml", deleteUserIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteUserViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.User.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.User.RemoveRangeAsync(modelList);

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
