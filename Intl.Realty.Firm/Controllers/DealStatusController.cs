using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.DealStatusVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Intl.Realty.Firm.Controllers
{
    public class DealStatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DealStatusController(IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor
                                    )
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
            CreateDealStatusViewModel viewModel = new CreateDealStatusViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDealStatusViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;
                viewModel.IsActive = true;
                viewModel.CreatedBy = Convert.ToInt32(userId);
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToDealStatusModel();

                await _unitOfWork.DealStatus.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = Session.UserId;
            var model = await _unitOfWork.DealStatus.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditDealStatusViewModel();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDealStatusViewModel viewModel)
        {
            var userId = Session.UserId;

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.DealStatus.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToDealStatusModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.DealStatus.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.DealStatus.GetAllAsync();

            var viewModelIEnum = departmentIEnum.ToDealStatusIEnumViewModel(); ;

            return PartialView("~/Views/DealStatus/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.DealStatus.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteDealStatusIEnumViewModel = modelIEnum.ToDeleteDealStatusIEnumViewModel();

            return PartialView("~/Views/DealStatus/Modal/DeleteMultipleModal.cshtml", deleteDealStatusIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteDealStatusViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.DealStatus.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.DealStatus.RemoveRangeAsync(modelList);

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
