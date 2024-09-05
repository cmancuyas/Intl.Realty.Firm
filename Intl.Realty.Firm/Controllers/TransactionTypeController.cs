using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionTypeController(IUnitOfWork unitOfWork)
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
            CreateTransactionTypeViewModel viewModel = new CreateTransactionTypeViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTransactionTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;
                viewModel.IsActive = true;
                viewModel.CreatedBy = userId;
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToTransactionTypeModel();

                await _unitOfWork.TransactionType.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditTransactionTypeViewModel();

            var userId = Session.UserId;
            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTransactionTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                var userId = Session.UserId;
                model = viewModel.ToTransactionTypeModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.TransactionType.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.TransactionType.GetAllAsync();

            var viewModelIEnum = departmentIEnum.ToTransactionTypeIEnumViewModel();

            return PartialView("~/Views/TransactionType/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.TransactionType.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteTransactionTypeIEnumViewModel = modelIEnum.ToDeleteTransactionTypeIEnumViewModel();

            return PartialView("~/Views/TransactionType/Modal/DeleteMultipleModal.cshtml", deleteTransactionTypeIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteTransactionTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.TransactionType.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.TransactionType.RemoveRangeAsync(modelList);

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
