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
        public IActionResult Index()
        {
            List<TransactionType> transactionTypeList = _unitOfWork.TransactionType.GetAll().ToList();

            List<TransactionTypeViewModel> transactionTypeViewModel = transactionTypeList.Select(x => x.ToTransactionTypeViewModel()).ToList();

            return View(transactionTypeViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransactionType obj)
        {
            var checkIfExists = _unitOfWork.TransactionType.Get(x => x.Name == obj.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Transaction Type already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _unitOfWork.TransactionType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "TransactionType created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TransactionType? transactionTypeFromDb = _unitOfWork.TransactionType.Get(u => u.Id == id);

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
        public IActionResult Edit(TransactionType model)
        {
            if (ModelState.IsValid)
            {
                TransactionType? transactionTypeFromDb = _unitOfWork.TransactionType.Get(u => u.Id == model.Id);
                model.CreatedBy = transactionTypeFromDb.CreatedBy;
                model.CreatedAt = transactionTypeFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.TransactionType.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "TransactionType updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TransactionType? transactionTypeFromDb = _unitOfWork.TransactionType.Get(u => u.Id == id);

            var transactionTypeViewModel = transactionTypeFromDb.ToTransactionTypeViewModel();

            if (transactionTypeViewModel == null)
            {
                return NotFound();
            }
            return View(transactionTypeViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            TransactionType? obj = _unitOfWork.TransactionType.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.TransactionType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "TransactionType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
