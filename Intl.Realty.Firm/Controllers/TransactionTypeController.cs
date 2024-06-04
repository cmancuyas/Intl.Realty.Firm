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
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.TransactionType.GetAllAsync();

            var viewModel = modelList.ToTransactionTypeListViewModel();

            return PartialView("~/Views/TransactionType/Partial/ListPartial.cshtml", viewModel);
        }
        public IActionResult CreateModal()
        {
            CreateTransactionTypeViewModel viewModel = new CreateTransactionTypeViewModel();
            return PartialView("~/Views/TransactionType/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionTypeViewModel viewModel)
        {
            var checkIfExists = await _unitOfWork.TransactionType.GetAsync(x => x.Name == viewModel.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "User Type already exists");
            }

            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToTransactionTypeModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.TransactionType.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "TransactionType created successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> EditModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TransactionType? model = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditTransactionTypeViewModel viewModel = model.ToEditTransactionTypeModel();

            return PartialView("~/Views/TransactionType/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("TransactionType/Edit/{id:int}")]

        public async Task<IActionResult> Edit(EditTransactionTypeViewModel viewModel)
        {

            TransactionType? model = await _unitOfWork.TransactionType.GetAsync(u => u.Id == viewModel.Id);

            model.IsActive = viewModel.IsActive;
            model.Name = viewModel.Name;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.TransactionType.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "TransactionType updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TransactionType? userTypeFromDb = await _unitOfWork.TransactionType.GetAsync(u => u.Id == id);

            var viewModel = userTypeFromDb.ToTransactionTypeViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/TransactionType/Modal/DeleteModal.cshtml", viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
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
