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
            IEnumerable<DocumentTypeAssignment> modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(includeProperties:"DocumentType,TransactionType");

            List<DocumentTypeAssignmentViewModel> viewModelList = modelList.Select(x => x.ToDocumentTypeAssignmentViewModel()).ToList();

            return View(viewModelList);
        }

        public async Task<IActionResult> Create()
        {
            List<DocumentType> documentTypeList = await _unitOfWork.DocumentType.GetAllAsync() as List<DocumentType> ?? throw new ArgumentException();
            List<TransactionType> transactionTypeList = await _unitOfWork.TransactionType.GetAllAsync() as List<TransactionType> ?? throw new ArgumentException();
            ViewBag.DocumentTypeSelectListItem = SelectListConverter.CreateSelectList(documentTypeList, x=>x.Id, x=>x.Name);
            ViewBag.TransactionTypeSelectListItem = SelectListConverter.CreateSelectList(transactionTypeList, x => x.Id, x => x.Name);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentTypeAssignment model)
        {
            var checkIfExists = await _unitOfWork.DocumentTypeAssignment.GetAsync(x=>x.DocumentTypeId == model.DocumentTypeId);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Document Type Assignment already exists");
            }

            string selectedDocumentTypeId = Request.Form["DocumentTypeDDL"].ToString();
            string selectedTransactionTypeId = Request.Form["TransactionTypeDDL"].ToString();
            model.DocumentTypeId = Convert.ToInt32(selectedDocumentTypeId);
            model.TransactionTypeId = Convert.ToInt32(selectedTransactionTypeId);
            model.CreatedBy = 1;
            model.IsActive = true;
            model.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                await _unitOfWork.DocumentTypeAssignment.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "Document Type Assignment created successfully";
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
            DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);
            
            var viewModel = model.ToEditDocumentTypeAssignmentViewModel();
            var documentTypeList = await _unitOfWork.DocumentType.GetAllAsync(); 
            var transactionTypeList = await _unitOfWork.TransactionType.GetAllAsync(); 
            viewModel.DocumentTypeList = documentTypeList.ToList();
            viewModel.TransactionTypeList = transactionTypeList.ToList();
            viewModel.UpdatedBy = 1;
            viewModel.UpdatedAt = DateTime.UtcNow;

            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DocumentTypeAssignment fromView)
        {
            if (ModelState.IsValid)
            {

                DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == fromView.Id);
                model.DocumentTypeId = fromView.DocumentTypeId;
                model.DocumentType = fromView.DocumentType;
                model.TransactionTypeId = fromView.TransactionTypeId;
                model.TransactionType = fromView.TransactionType;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.DocumentTypeAssignment.UpdateAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentTypeAssignment updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> DocumentTypeAssignmentListPartialView()
        {
            var documentTypeList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(includeProperties: "DocumentType,TransactionType");

            var viewModel = documentTypeList.ToDocumentTypeAssignmentListViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Partial/DocumentTypeAssignmentListPartial.cshtml", viewModel);
        }
        public async Task<IActionResult> DeleteModal(int? id)
        {
              if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);

            var viewModel = model.ToDocumentTypeAssignmentViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }

            viewModel.DocumentType = await _unitOfWork.DocumentType.GetAsync(u => u.Id == model.DocumentTypeId);
            viewModel.TransactionType = await _unitOfWork.TransactionType.GetAsync(u => u.Id == model.TransactionTypeId);

            return PartialView("~/Views/DocumentTypeAssignment/Modal/DeleteModal.cshtml", viewModel);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            DocumentTypeAssignment? model = await _unitOfWork.DocumentTypeAssignment.GetAsync(u => u.Id == id);

            if (model == null)
            {
                return NotFound();
            }
            await _unitOfWork.DocumentTypeAssignment.RemoveAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "DocumentTypeAssignment deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
