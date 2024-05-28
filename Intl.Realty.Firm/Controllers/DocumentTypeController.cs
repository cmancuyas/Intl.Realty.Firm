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
        public async Task<IActionResult> Index()
        {   
            List<DocumentType> documentTypeList = await _unitOfWork.DocumentType.GetAllAsync() as List<DocumentType> ?? throw new ArgumentException();

            List<DocumentTypeViewModel> documentTypeViewModel = documentTypeList.Select(x => x.ToDocumentTypeViewModel()).ToList();

            return View(documentTypeViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentType obj)
        {
            var checkIfExists = await _unitOfWork.DocumentType.GetAsync(x=>x.Name == obj.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "Document Type already exists");
            }

            obj.CreatedBy = 1;
            obj.IsActive = true;
            obj.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                await _unitOfWork.DocumentType.AddAsync(obj);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType created successfully";
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
            DocumentType? documentTypeFromDb = await _unitOfWork.DocumentType.GetAsync(u => u.Id == id);

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
        public async Task<IActionResult> Edit(DocumentType model)
        {
            if (ModelState.IsValid)
            {
                DocumentType? documentTypeFromDb = await _unitOfWork.DocumentType.GetAsync(u => u.Id == model.Id);
                model.CreatedBy = documentTypeFromDb.CreatedBy;
                model.CreatedAt = documentTypeFromDb.CreatedAt;
                model.UpdatedBy = 1;
                model.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.DocumentType.UpdateAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentType? documentTypeFromDb = await _unitOfWork.DocumentType.GetAsync(u => u.Id == id);

            var documentTypeViewModel = documentTypeFromDb.ToDocumentTypeViewModel();

            if (documentTypeViewModel == null)
            {
                return NotFound();
            }
            return View(documentTypeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            DocumentType? obj = await _unitOfWork.DocumentType.GetAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            await _unitOfWork.DocumentType.RemoveAsync(obj);
            _unitOfWork.Save();
            TempData["success"] = "Document Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
