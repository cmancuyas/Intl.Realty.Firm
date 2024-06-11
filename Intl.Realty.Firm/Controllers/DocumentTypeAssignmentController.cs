using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Intl.Realty.Firm.Controllers
{
    public class DocumentTypeAssignmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _userId = 1;
        public DocumentTypeAssignmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateModal()
        {
            CreateDocumentTypeAssignmentViewModel viewModel = new CreateDocumentTypeAssignmentViewModel();
            var transactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();
            var transactionTypeList = transactionTypeIEnum.FromIEnumToTransactionTypeList();
            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();
            var documentTypeList = documentTypeIEnum.FromIEnumToDocumentTypeList();
            viewModel.TransactionTypeIEnum = SelectListConverter.CreateSelectList(transactionTypeList, x => x.Id, x => x.Name);
            viewModel.DocumentTypeIEnum = SelectListConverter.CreateSelectList(documentTypeList, x => x.Id, x => x.Name);

            return PartialView("~/Views/DocumentTypeAssignment/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentTypeAssignmentViewModel viewModel)
        {
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
        [HttpPut]
        public async Task<IActionResult> Edit(int id, DocumentTypeAssignmentViewModel viewModel)
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
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(includeProperties: "DocumentType,TransactionType");

            var viewModel = modelList.ToDocumentTypeAssignmentListViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Partial/ListPartial.cshtml", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.DocumentTypeAssignment.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editDocumentTypeAssignmentViewModel = model.ToEditDocumentTypeAssignmentViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Modal/DeleteModal.cshtml", editDocumentTypeAssignmentViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<DocumentTypeAssignment> modelList = new List<DocumentTypeAssignment>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.DocumentTypeAssignment.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var transactionTypeViewModelList = modelList.ToDocumentTypeAssignmentListViewModel();

            IEnumerable<DocumentTypeAssignmentViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/DocumentTypeAssignment/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> DeleteMultiple(IEnumerable<DocumentTypeAssignmentViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var tramsactonTypeList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync();
            var modelList = tramsactonTypeList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.DocumentTypeAssignment.RemoveRangeAsync(modelList);
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
