using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.DocumentTypeAssignmentVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Intl.Realty.Firm.Utility.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class DocumentTypeAssignmentController : Controller
    {
        private int _userId = 1;
        private readonly IUnitOfWork _unitOfWork;
        public DocumentTypeAssignmentController(IUnitOfWork unitOfWork)
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
            CreateDocumentTypeAssignmentViewModel viewModel = new CreateDocumentTypeAssignmentViewModel();
            var transactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();
            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();
            viewModel.TransactionTypeIEnum = SelectListConverter.CreateSelectList(transactionTypeIEnum.ToList(), x => x.Id, x => x.Description);
            viewModel.DocumentTypeIEnum = SelectListConverter.CreateSelectList(documentTypeIEnum.ToList(), x => x.Id, x => x.Description);
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDocumentTypeAssignmentViewModel viewModel)
        {
            string selectedTransactionTypeId = Request.Form["TransactionTypeDDL"].ToString();
            string selectedDocumentTypeId = Request.Form["DocumentTypeDDL"].ToString();

            var modelList = await _unitOfWork.DocumentTypeAssignment
                                    .GetAllAsync(x => x.TransactionTypeId == Convert.ToInt32(selectedDocumentTypeId), includeProperties:"DocumentType,TransactionType");
            if (modelList.Any())
            {
                var modelCheckIfExists = modelList.Where(x => x.DocumentTypeId == Convert.ToInt32(selectedDocumentTypeId));
                if (modelCheckIfExists.Any())
                {
                    ModelState.AddModelError("name", "Document Type assignment already exists");
                }
            }

            viewModel.TransactionTypeId = Convert.ToInt32(selectedTransactionTypeId);
            viewModel.DocumentTypeId = Convert.ToInt32(selectedDocumentTypeId);
            viewModel.CreatedBy = 1;
            viewModel.IsActive = true;
            viewModel.CreatedAt = DateTime.UtcNow;

            var model = viewModel.ToDocumentTypeAssignment();

            if (ModelState.IsValid)
            {
                await _unitOfWork.DocumentTypeAssignment.AddAsync(model);
                _unitOfWork.Save();
                TempData["success"] = "DocumentType Assignment created successfully";
                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }
            return RedirectToAction(nameof(Index), new { addSuccess = false });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.DocumentTypeAssignment.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var transactionTypeIEnum = await _unitOfWork.TransactionType.GetAllAsync();

            var documentTypeIEnum = await _unitOfWork.DocumentType.GetAllAsync();

            var viewModel = model.ToEditDocumentTypeAssignmentViewModel();

            viewModel.TransactionTypeList = transactionTypeIEnum.ToList();
            viewModel.DocumentTypeList = documentTypeIEnum.ToList();

            viewModel.UpdatedBy = _userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDocumentTypeAssignmentViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.DocumentTypeAssignment.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToDocumentTypeAssignment();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;
                await _unitOfWork.DocumentTypeAssignment.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var DocumentTypeAssignmentIEnum = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(includeProperties: "DocumentType,TransactionType");

            var viewModelIEnum = DocumentTypeAssignmentIEnum.ToDocumentTypeAssignmentIEnumViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList == null)
            {
                return NotFound();
            }

            var deleteDocumentTypeAssignmentIEnumViewModel = modelList.ToDeleteDocumentTypeAssignmentIEnumViewModel();

            return PartialView("~/Views/DocumentTypeAssignment/Modal/DeleteMultipleModal.cshtml", deleteDocumentTypeAssignmentIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteDocumentTypeAssignmentViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.DocumentTypeAssignment.GetAllAsync(x=>ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.DocumentTypeAssignment.RemoveRangeAsync(modelList);

                    return RedirectToAction(nameof(Index), new { deleteSuccess = true });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
