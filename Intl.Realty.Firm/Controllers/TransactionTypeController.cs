using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class TransactionTypeController : Controller
    {
        private int _userId = 1;
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
        public async Task<IActionResult> Create(int? regionId, int? provinceId, int? municipalityId)
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
                viewModel.IsActive = true;
                viewModel.CreatedBy = _userId;
                viewModel.CreatedAt = DateTime.UtcNow;  
                var model = viewModel.FromCreateToTransactionTypeModel();

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

            var viewModel = model.ToEditTransactionTypeListViewModel();

            viewModel.UpdatedBy = _userId;
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
                model = viewModel.FromEditToTransactionTypeModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;
                await _unitOfWork.TransactionType.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var TransactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();

            var viewModelIEnum = TransactionTypeIEnum.FromIEnumToTransactionTypeIEnumViewModel();

            return PartialView("~/Views/TransactionType/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editTransactionTypeViewModel = model.ToEditTransactionTypeListViewModel();

            return PartialView("~/Views/TransactionType/Modal/DeleteModal.cshtml", editTransactionTypeViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<TransactionType> modelList = new List<TransactionType>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var TransactionTypeViewModelList = modelList.ToTransactionTypeListViewModel();

            IEnumerable<TransactionTypeViewModel> modelIEnum = TransactionTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/TransactionType/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var modelList = model.FromModelToTransactionTypeListModel();

            await _unitOfWork.TransactionType.RemoveRangeAsync(modelList);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<TransactionTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var TransactionTypeList = await _unitOfWork.TransactionType.GetAllAsync();
            var modelList = TransactionTypeList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.TransactionType.RemoveRangeAsync(modelList);
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
