using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;
using Intl.Realty.Firm.Models.Models.ViewModel.TransactionTypeVM;
using System.Linq;
using Intl.Realty.Firm.Models.Models.ViewModel.UserTypeVM;
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
            List<DocumentType> modelList = await _unitOfWork.DocumentType.GetAllAsync() as List<DocumentType> ?? throw new ArgumentException();

            List<DocumentTypeViewModel> viewModelList = modelList.Select(x => x.ToDocumentTypeViewModel()).ToList();

            return View(viewModelList);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var modelList = await _unitOfWork.DocumentType.GetAllAsync();

            var viewModel = modelList.ToDocumentTypeListViewModel();

            return PartialView("~/Views/DocumentType/Partial/ListPartial.cshtml", viewModel);
        }
        public IActionResult CreateModal()
        {
            CreateDocumentTypeViewModel viewModel = new CreateDocumentTypeViewModel();
            return PartialView("~/Views/DocumentType/Modal/CreateModal.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentTypeViewModel viewModel)
        {

            var checkIfExists = await _unitOfWork.DocumentType.GetAsync(x => x.Name == viewModel.Name);
            if (checkIfExists != null)
            {
                ModelState.AddModelError("name", "User Type already exists");
            }

            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToDocumentTypeModel();

            if (ModelState.IsValid)
            {
                await _unitOfWork.DocumentType.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType created successfully";
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }
            //return RedirectToAction(nameof(Index), new { addSuccess = false });

            return View("Index");

        }

        public async Task<IActionResult> EditModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentType? model = await _unitOfWork.DocumentType.GetAsync(u => u.Id == id);

            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            if (model == null)
            {
                return NotFound();
            }

            EditDocumentTypeViewModel viewModel = model.ToEditDocumentTypeModel();

            return PartialView("~/Views/DocumentType/Modal/EditModal.cshtml", viewModel);
        }

        [HttpPut("DocumentType/Edit/{id:int}")]

        public async Task<IActionResult> Edit(EditDocumentTypeViewModel viewModel)
        {

            DocumentType? model = await _unitOfWork.DocumentType.GetAsync(u => u.Id == viewModel.Id);

            model.IsActive = viewModel.IsActive;
            model.Name = viewModel.Name;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.DocumentType.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "DocumentType updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            DocumentType? userTypeFromDb = await _unitOfWork.DocumentType.GetAsync(u => u.Id == id);

            var viewModel = userTypeFromDb.ToDocumentTypeViewModel();

            if (viewModel == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/DocumentType/Modal/DeleteModal.cshtml", viewModel);
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
            TempData["success"] = "DocumentType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
