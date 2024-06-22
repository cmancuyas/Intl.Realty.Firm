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

            var checkIfExists = await _unitOfWork.DocumentType.GetAsync(x => x.Description == viewModel.Description);
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
            model.Code = viewModel.Code;
            model.Description = viewModel.Description;
            model.UpdatedBy = 1;
            model.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.DocumentType.UpdateAsync(model);
            _unitOfWork.Save();
            TempData["success"] = "DocumentType updated successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editDocumentTypeViewModel = model.ToEditDocumentTypeViewModel();

            return PartialView("~/Views/DocumentType/Modal/DeleteModal.cshtml", editDocumentTypeViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<DocumentType> modelList = new List<DocumentType>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var transactionTypeViewModelList = modelList.ToDocumentTypeListViewModel();

            IEnumerable<DocumentTypeViewModel> modelIEnum = transactionTypeViewModelList.AsEnumerable();

            return PartialView("~/Views/DocumentType/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            await _unitOfWork.DocumentType.RemoveAsync(model);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMultiple(IEnumerable<DocumentTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var documentTypeList = await _unitOfWork.DocumentType.GetAllAsync();
            var modelList = documentTypeList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.DocumentType.RemoveRangeAsync(modelList);
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
