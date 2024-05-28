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
            List<TransactionType> transactionTypeList = await _unitOfWork.TransactionType.GetAllAsync() as List<TransactionType> ?? throw new ArgumentException();

            List<TransactionTypeViewModel> transactionTypeViewModel = transactionTypeList.Select(x => x.ToTransactionTypeViewModel()).ToList();

            return View(transactionTypeViewModel);
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TransactionType? transactionTypeFromDb = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);

            var transactionTypeViewModel = transactionTypeFromDb.ToTransactionTypeViewModel();

            if (transactionTypeViewModel == null)
            {
                return NotFound();
            }
            return View(transactionTypeViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            TransactionType? obj = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.TransactionType.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "TransactionType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
