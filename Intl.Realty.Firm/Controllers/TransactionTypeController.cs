using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;

namespace Intl.Realty.Firm.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _userId = 1;
        public TransactionTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateModal()
        {
            CreateTransactionTypeViewModel viewModel = new CreateTransactionTypeViewModel();
            return PartialView("~/Views/TransactionType/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTransactionTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new TransactionType
                {
                    Name = viewModel.Name,
                    IsActive = true,
                    CreatedAt = DateTime.Now, // Set the CreatedAt property to the current date/time
                    CreatedBy = _userId,

                    // Set other properties as needed
                };

                await _unitOfWork.TransactionType.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditModal(int? id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            EditTransactionTypeViewModel viewModel = model.ToEditTransactionTypeViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return PartialView("~/Views/TransactionType/Modal/EditModal.cshtml", viewModel);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(int id, TransactionTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.TransactionType.GetAsync(x=>x.Id== id);
                if (model == null)
                {
                    return NotFound();
                }
                model.Name = viewModel.Name;
                model.IsActive = viewModel.IsActive;
                model.UpdatedBy = _userId;
                model.UpdatedAt = DateTime.Now;

                // Update other properties as needed

                await _unitOfWork.TransactionType.UpdateAsync(model);

                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var transactionTypeList = await _unitOfWork.TransactionType.GetAllAsync();

            var viewModel = transactionTypeList.ToTransactionTypeListViewModel();

            return PartialView("~/Views/TransactionType/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editTransactionTypeViewModel = model.ToEditTransactionTypeViewModel();

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

            var transactionTypeViewModelList = modelList.ToTransactionTypeListViewModel();

            IEnumerable<TransactionTypeViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

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
            await _unitOfWork.TransactionType.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<TransactionTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var tramsactonTypeList = await _unitOfWork.TransactionType.GetAllAsync();
            var modelList = tramsactonTypeList.Where(o => ids.Contains(o.Id)).ToList();

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