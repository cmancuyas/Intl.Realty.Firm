using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.ViewModel.ProvinceVM;
using Intl.Realty.Firm.Models.ViewModel;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Intl.Realty.Firm.Controllers
{
    public class ProvinceController : Controller
    {
        private int _userId = 1;
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
        public async Task<IActionResult> Create(int? regionId, int? provinceId, int? municipalityId)
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
                viewModel.IsActive = true;
                viewModel.CreatedBy = _userId;
                viewModel.CreatedAt = DateTime.UtcNow;  
                var model = viewModel.FromCreateToProvinceModel();

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

            var viewModel = model.ToEditProvinceListViewModel();

            viewModel.UpdatedBy = _userId;
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
                model = viewModel.FromEditToProvinceModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = _userId;
                await _unitOfWork.Province.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var ProvinceIEnum = await _unitOfWork.Province.GetAllAsync();

            var viewModelIEnum = ProvinceIEnum.FromIEnumToProvinceIEnumViewModel();

            return PartialView("~/Views/Province/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteModal(int id)
        {
            var model = await _unitOfWork.Province.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var editProvinceViewModel = model.ToEditProvinceListViewModel();

            return PartialView("~/Views/Province/Modal/DeleteModal.cshtml", editProvinceViewModel);
        }

        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            List<Province> modelList = new List<Province>();
            foreach (var id in ids)
            {
                var model = await _unitOfWork.Province.GetAsync(x => x.Id == id);
                modelList.Add(model);
            }

            if (modelList == null)
            {
                return NotFound();
            }

            var ProvinceViewModelList = modelList.ToProvinceListViewModel();

            IEnumerable<ProvinceViewModel> modelIEnum = ProvinceViewModelList.AsEnumerable();

            return PartialView("~/Views/Province/Modal/DeleteMultipleModal.cshtml", modelIEnum);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.Province.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var modelList = model.FromModelToProvinceListModel();

            await _unitOfWork.Province.RemoveRangeAsync(modelList);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<ProvinceViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            var ids = viewModelList.Select(o => o.Id).ToList();
            var ProvinceList = await _unitOfWork.Province.GetAllAsync();
            var modelList = ProvinceList.Where(o => ids.Contains(o.Id)).ToList();

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.Province.RemoveRangeAsync(modelList);
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
