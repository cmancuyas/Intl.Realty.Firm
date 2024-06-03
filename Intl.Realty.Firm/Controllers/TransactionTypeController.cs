using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using Intl.Realty.Firm.Models.Models;
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
        public async Task<IActionResult> Index()
        {
            List<TransactionType> modelList = await _unitOfWork.TransactionType.GetAllAsync() as List<TransactionType> ?? throw new ArgumentException();

            List<TransactionTypeViewModel> viewModelList = modelList.Select(x => x.ToTransactionTypeViewModel()).ToList();

            return View(viewModelList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionType obj)
        {
            var checkIfExists = await _unitOfWork.TransactionType.GetAsync(x => x.Name == obj.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Transaction Type already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                await _unitOfWork.TransactionType.AddAsync(obj);
                _unitOfWork.Save();
                TempData["success"] = "TransactionType created successfully";
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
            TransactionType? transactionTypeFromDb = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);

            transactionTypeFromDb.UpdatedBy = 1;
            transactionTypeFromDb.UpdatedAt = DateTime.UtcNow;

            if (transactionTypeFromDb == null)
            {
                return NotFound();
            }
            return View(transactionTypeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransactionType model)
        {
            if (ModelState.IsValid)
            {
                TransactionType? transactionTypeFromDb = await _unitOfWork.TransactionType.GetAsync(u => u.Id == model.Id);
                model.CreatedBy = transactionTypeFromDb.CreatedBy;
                model.CreatedAt = transactionTypeFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.TransactionType.UpdateAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "TransactionType updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.TransactionType.GetAsync(x=>x.Id == id);
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
                var model = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var transactionTypeViewModelList = modelList.ToTransactionTypeListViewModel();

            IEnumerable<TransactionTypeViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

            ViewBag.ViewModelIENumList = modelIEnum;

            return PartialView("~/Views/TransactionType/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var model = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            await _unitOfWork.TransactionType.RemoveAsync(model);

            return View("Index");

        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<TransactionTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return PartialView("~/Views/TransactionType/Partial/TransactionTypeListPartial.cshtml", viewModelList);
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var position = await _unitOfWork.TransactionType.GetAllAsync();
            var modelList = position.Where(o => ids.Contains(o.Id)).ToList();

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
