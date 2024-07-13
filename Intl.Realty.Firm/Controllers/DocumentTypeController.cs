using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class DocumentTypeController : Controller
    {
        private int _userId = 1;
        private readonly IUnitOfWork _unitOfWork;
        public DocumentTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? regionId, int? provinceId, int? municipalityId)
        {
            CreateDocumentTypeViewModel viewModel = new CreateDocumentTypeViewModel();
            viewModel.IsRequired = true;
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentTypeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.IsActive = true;
                viewModel.CreatedBy = _userId;
                viewModel.CreatedAt = DateTime.UtcNow;  
                var model = viewModel.FromCreateToDocumentTypeModel();

                await _unitOfWork.DocumentType.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditDocumentTypeListViewModel();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDocumentTypeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.FromEditToDocumentTypeModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;
                await _unitOfWork.DocumentType.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();

            var viewModelIEnum = documentTypeIEnum.FromIEnumToDocumentTypeIEnumViewModel();

            return PartialView("~/Views/DocumentType/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.DocumentType.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editDocumentTypeViewModel = model.ToEditDocumentTypeListViewModel();

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

            var DocumentTypeViewModelList = modelList.ToDocumentTypeListViewModel();

            IEnumerable<DocumentTypeViewModel> modelIEnum = DocumentTypeViewModelList.AsEnumerable();

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

            var modelList = model.FromModelToDocumentTypeListModel();

            await _unitOfWork.DocumentType.RemoveRangeAsync(modelList);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DocumentTypeViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var DocumentTypeList = await _unitOfWork.DocumentType.GetAllAsync();
            var modelList = DocumentTypeList.Where(o => ids.Contains(o.Id)).ToList();

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
