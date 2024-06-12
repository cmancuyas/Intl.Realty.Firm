using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Intl.Realty.Firm.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _userId = 1;
        public UserTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateModal()
        {
            CreateUserTypeViewModel viewModel = new CreateUserTypeViewModel();
            return PartialView("~/Views/UserType/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new UserType
                {
                    Name = viewModel.Name,
                    IsActive = true,
                    CreatedAt = DateTime.Now, // Set the CreatedAt property to the current date/time
                    CreatedBy = _userId,

                    // Set other properties as needed
                };

                await _unitOfWork.UserType.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditModal(int? id)
        {
            var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            EditUserTypeViewModel viewModel = model.ToEditUserTypeViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return PartialView("~/Views/UserType/Modal/EditModal.cshtml", viewModel);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int id, UserTypeViewModel viewModel)
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
                model.Name = viewModel.Name;
                model.IsActive = viewModel.IsActive;
                model.UpdatedBy = _userId;
                model.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.UserType.UpdateAsync(model);

                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var transactionTypeList = await _unitOfWork.UserType.GetAllAsync();

            var viewModel = transactionTypeList.ToUserTypeListViewModel();

            return PartialView("~/Views/UserType/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.UserType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editUserTypeViewModel = model.ToEditUserTypeViewModel();

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

            var transactionTypeViewModelList = modelList.ToUserTypeListViewModel();

            IEnumerable<UserTypeViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

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
            await _unitOfWork.UserType.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<UserTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var tramsactonTypeList = await _unitOfWork.UserType.GetAllAsync();
            var modelList = tramsactonTypeList.Where(o => ids.Contains(o.Id)).ToList();

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

            return StatusCode(StatusCodes.Status200OK, ModelState);
        }
    }

}