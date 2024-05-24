using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;

namespace Intl.Realty.Firm.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocumentTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<DocumentType> documentTypeList = _unitOfWork.DocumentType.GetAll().ToList();

            List<DocumentTypeViewModel> documentTypeViewModel = documentTypeList.Select(x => x.ToDocumentTypeViewModel()).ToList();

            return View(documentTypeViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DocumentType obj)
        {
            var checkIfExists = _unitOfWork.DocumentType.Get(x=>x.Name == obj.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Document Type already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                _unitOfWork.DocumentType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType created successfully";
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
            DocumentType? documentTypeFromDb = _unitOfWork.DocumentType.Get(u => u.Id == id);

            var documentTypeViewModel = documentTypeFromDb.ToDocumentTypeViewModel();

            documentTypeViewModel.UpdatedBy = 1;
            documentTypeViewModel.UpdatedAt = DateTime.UtcNow;

            if (documentTypeViewModel == null)
            {
                return NotFound();
            }
            return View(documentTypeViewModel);
        }
        [HttpPost]
        public IActionResult Edit(DocumentType model)
        {
            if (ModelState.IsValid)
            {

                DocumentType? documentTypeFromDb = _unitOfWork.DocumentType.Get(u => u.Id == model.Id);
                model.CreatedBy = documentTypeFromDb.CreatedBy;
                model.CreatedAt = documentTypeFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.DocumentType.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType updated successfully";
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
            DocumentType? documentTypeFromDb = _unitOfWork.DocumentType.Get(u => u.Id == id);

            var documentTypeViewModel = documentTypeFromDb.ToDocumentTypeViewModel();

            if (documentTypeViewModel == null)
            {
                return NotFound();
            }
            return View(documentTypeViewModel);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            DocumentType? obj = _unitOfWork.DocumentType.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.DocumentType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "DocumentType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
