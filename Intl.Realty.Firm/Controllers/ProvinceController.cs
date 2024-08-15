using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProvinceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateProvinceViewModel viewModel = new CreateProvinceViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProvinceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Session.UserId;
                viewModel.IsActive = true;
                viewModel.CreatedBy = userId;
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToProvinceModel();

                await _unitOfWork.Province.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _unitOfWork.Province.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            var userId = Session.UserId;
            var viewModel = model.ToEditProvinceViewModel();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProvinceViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.Province.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                var userId = Session.UserId;
                model = viewModel.ToProvinceModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;
                await _unitOfWork.Province.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var provinceIEnum = await _unitOfWork.Province.GetAllAsync();

            var viewModelIEnum = provinceIEnum.ToProvinceIEnumViewModel();

            return PartialView("~/Views/Province/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.Province.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteProvinceIEnumViewModel = modelIEnum.ToDeleteProvinceIEnumViewModel();

            return PartialView("~/Views/Province/Modal/DeleteMultipleModal.cshtml", deleteProvinceIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteProvinceViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.Province.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.Province.RemoveRangeAsync(modelList);

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
