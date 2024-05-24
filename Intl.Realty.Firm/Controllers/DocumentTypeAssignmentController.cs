using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
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
        public IActionResult Index()
        {
            List<DocumentTypeAssignment> documentTypeAssignmentList = _unitOfWork.DocumentTypeAssignment.GetAll().ToList();

            List<DocumentTypeAssignmentViewModel> documentTypeAssignmentViewModel = documentTypeAssignmentList.Select(x => x.ToDocumentTypeAssignmentViewModel()).ToList();

            return View(documentTypeAssignmentViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DocumentTypeAssignment obj)
        {
            var checkIfExists = _unitOfWork.DocumentTypeAssignment.Get(x=>x.DocumentTypeId == obj.DocumentTypeId);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Document Type Assignment already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _unitOfWork.DocumentTypeAssignment.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Document Type Assignment created successfully";
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
            DocumentTypeAssignment? documentTypeAssignmentFromDb = _unitOfWork.DocumentTypeAssignment.Get(u => u.Id == id);

            var documentTypeAssignmentViewModel = documentTypeAssignmentFromDb.ToDocumentTypeAssignmentViewModel();

            documentTypeAssignmentViewModel.UpdatedBy = 1;
            documentTypeAssignmentViewModel.UpdatedAt = DateTime.UtcNow;

            if (documentTypeAssignmentViewModel == null)
            {
                return NotFound();
            }
            return View(documentTypeAssignmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit(DocumentTypeAssignment model)
        {
            if (ModelState.IsValid)
            {

                DocumentTypeAssignment? documentTypeAssignmentFromDb = _unitOfWork.DocumentTypeAssignment.Get(u => u.Id == model.Id);
                model.CreatedBy = documentTypeAssignmentFromDb.CreatedBy;
                model.CreatedAt = documentTypeAssignmentFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.DocumentTypeAssignment.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentTypeAssignment updated successfully";
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
            DocumentTypeAssignment? documentTypeAssignmentFromDb = _unitOfWork.DocumentTypeAssignment.Get(u => u.Id == id);

            var documentTypeAssignmentViewModel = documentTypeAssignmentFromDb.ToDocumentTypeAssignmentViewModel();

            if (documentTypeAssignmentViewModel == null)
            {
                return NotFound();
            }
            return View(documentTypeAssignmentViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            DocumentTypeAssignment? obj = _unitOfWork.DocumentTypeAssignment.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.DocumentTypeAssignment.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "DocumentTypeAssignment deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
