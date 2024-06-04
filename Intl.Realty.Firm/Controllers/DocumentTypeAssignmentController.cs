using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class DocumentTypeAssignmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocumentTypeAssignmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            List<DocumentTypeAssignment> modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync() as List<DocumentTypeAssignment> ?? throw new ArgumentException();

            List<DocumentTypeAssignmentViewModel> viewModelList = modelList.Select(x => x.ToDocumentTypeAssignmentViewModel()).ToList();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(includeProperties:"DocumentType,TransactionType");

            var viewModel = modelList.ToDocumentTypeAssignmentListViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Partial/ListPartial.cshtml", viewModel);
        }
        public async Task<IActionResult> CreateModal()
        {
            CreateDocumentTypeAssignmentViewModel viewModel = new CreateDocumentTypeAssignmentViewModel();
            var transactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();
            var transactionTypeList = transactionTypeIEnum.FromIEnumToTransactionTypeList();
            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();
            var documentTypeList = documentTypeIEnum.FromIEnumToDocumentTypeList();
            ViewBag.TransactionTypeSelectListItem = SelectListConverter.CreateSelectList(transactionTypeList, x => x.Id, x => x.Name);
            ViewBag.DocumentTypeSelectListItem = SelectListConverter.CreateSelectList(documentTypeList, x => x.Id, x => x.Name);
            ViewBag.ViewModel = viewModel;
            return PartialView("~/Views/DocumentTypeAssignment/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentTypeAssignmentViewModel viewModel)
        {
            var modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => x.TransactionTypeId == viewModel.TransactionTypeId, includeProperties: "DocumentType,TransactionType");
            if (modelList.Any())
            {
                var modelCheckIfExists = modelList.Where(x => x.DocumentTypeId == viewModel.DocumentTypeId);
                if (modelCheckIfExists.Any())
                {
                    ModelState.AddModelError("name", "Document Type assignment already exists");
                }
            }
            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToDocumentTypeAssignmentModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.DocumentTypeAssignment.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType Assignment created successfully";
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }
            return RedirectToAction(nameof(Index), new { addSuccess = false });

        }

        public async Task<IActionResult> EditModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditDocumentTypeAssignmentViewModel viewModel = model.ToEditDocumentTypeAssignmentModel();
            var transactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();

            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();
            viewModel.TransactionTypeList = transactionTypeIEnum.FromIEnumToTransactionTypeList();
            viewModel.DocumentTypeList = documentTypeIEnum.FromIEnumToDocumentTypeList();
            return PartialView("~/Views/DocumentTypeAssignment/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("DocumentTypeAssignment/Edit/{id:int}")]
        public async Task<IActionResult> Edit(EditDocumentTypeAssignmentViewModel viewModel)
        {

            DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == viewModel.Id);

            model.TransactionTypeId = viewModel.TransactionTypeId;
            model.DocumentTypeId = viewModel.DocumentTypeId;
            model.IsActive = viewModel.IsActive;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.DocumentTypeAssignment.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "Document Type Assignment updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentTypeAssignment? userTypeFromDb = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);

            var viewModel = userTypeFromDb.ToDocumentTypeAssignmentViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/DocumentTypeAssignment/Modal/DeleteModal.cshtml", viewModel);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            DocumentTypeAssignment? obj = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.DocumentTypeAssignment.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "DocumentTypeAssignment deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
