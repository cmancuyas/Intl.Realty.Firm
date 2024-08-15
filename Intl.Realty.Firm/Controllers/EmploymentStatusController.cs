using DENR_FAPIS.Models.Utilities;
using Intl.Realty.Firm.Helper;
using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.DataTable;
using Intl.Realty.Firm.Models.Models.ViewModel.EmploymentStatusVM;
using Intl.Realty.Firm.Repository.IRepository;
using Intl.Realty.Firm.Service.IServices;
using Intl.Realty.Firm.Utilities;
using Intl.Realty.Firm.Utility.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Intl.Realty.Firm.Models.Models.DataTable.DataTableRequestProvider;

namespace Intl.Realty.Firm.Controllers
{
    public class EmploymentStatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExportService<EmploymentStatusExport> _exportService;

        public EmploymentStatusController(IUnitOfWork unitOfWork, IExportService<EmploymentStatusExport> exportService)
        {
            _unitOfWork = unitOfWork;
            _exportService = exportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateEmploymentStatusViewModel viewModel = new CreateEmploymentStatusViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmploymentStatusViewModel viewModel)
        {
            var userId = Session.UserId;
            if (ModelState.IsValid)
            {
                viewModel.IsActive = true;
                viewModel.CreatedBy = userId;
                viewModel.CreatedAt = DateTime.UtcNow;
                var model = viewModel.ToEmploymentStatusModel();

                await _unitOfWork.EmploymentStatus.AddAsync(model);

                return RedirectToAction(nameof(Index), new { addSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = Session.UserId;
            var model = await _unitOfWork.EmploymentStatus.GetAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = model.ToEditEmploymentStatusViewModel();

            viewModel.UpdatedBy = userId;
            viewModel.UpdatedAt = DateTime.Now;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmploymentStatusViewModel viewModel)
        {
            var userId = Session.UserId;
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var model = await _unitOfWork.EmploymentStatus.GetAsync(x => x.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                model = viewModel.ToEmploymentStatusModel();
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = userId;

                await _unitOfWork.EmploymentStatus.UpdateAsync(model);
                return RedirectToAction(nameof(Index), new { editSuccess = true });
            }

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListPartialView()
        {
            var departmentIEnum = await _unitOfWork.EmploymentStatus.GetAllAsync();

            var viewModelIEnum = departmentIEnum.ToEmploymentStatusIEnumViewModel(); ;

            return PartialView("~/Views/EmploymentStatus/Partial/ListPartial.cshtml", viewModelIEnum);
        }
        public async Task<IActionResult> DeleteMultipleModal(List<int> ids)
        {
            var modelIEnum = await _unitOfWork.EmploymentStatus.GetAllAsync(x => ids.Contains(x.Id));

            if (modelIEnum == null)
            {
                return NotFound();
            }

            var deleteEmploymentStatusIEnumViewModel = modelIEnum.ToDeleteEmploymentStatusIEnumViewModel();

            return PartialView("~/Views/EmploymentStatus/Modal/DeleteMultipleModal.cshtml", deleteEmploymentStatusIEnumViewModel);
        }

        public async Task<IActionResult> DeleteMultiple(IEnumerable<DeleteEmploymentStatusViewModel> viewModelList)
        {
            if (viewModelList == null || !viewModelList.Any())
            {
                return RedirectToAction(nameof(Index));
            }
            var ids = viewModelList.Select(x => x.Id).ToList();

            var modelList = await _unitOfWork.EmploymentStatus.GetAllAsync(x => ids.Contains(x.Id));

            if (modelList != null)
            {
                try
                {
                    await _unitOfWork.EmploymentStatus.RemoveRangeAsync(modelList);

                    return RedirectToAction(nameof(Index), new { deleteSuccess = true });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> GetEmploymentStatusList([FromBody] DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            var orderCriteriaTemp = "Id";
            var orderAscendingDirectionTemp = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                try
                {
                    orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                    orderAscendingDirectionTemp = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                }
                catch (Exception ex)
                {
                    try
                    {
                        orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[1].Column].Data;
                        orderAscendingDirectionTemp = dtParameters.Order[1].Dir.ToString().ToLower() == "asc";
                    }
                    catch (Exception ex1)
                    {
                        orderCriteriaTemp = "Id";
                        orderAscendingDirectionTemp = true;
                    }
                }
            }

            if (orderCriteriaTemp == null)
            {
                orderCriteriaTemp = "Id";
                orderAscendingDirectionTemp = true;
            }

            if (orderCriteriaTemp != null)
            {
                orderCriteria = orderCriteriaTemp;
                orderAscendingDirection = orderAscendingDirectionTemp;
            }

            var result = _unitOfWork.EmploymentStatus.AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Code != null && r.Code.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Description != null && r.Description.ToUpper().Contains(searchBy.ToUpper())
                                           );
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria!, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria!, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _unitOfWork.EmploymentStatus.CountAsync();

            var jsonResult = new DtResult<EmploymentStatus>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            };

            return Json(jsonResult);

        }
        public JsonResult Read([DataTableRequestBinder] DataTableRequest request)
        {
            var data = _unitOfWork.EmploymentStatus.GetAllAsync(pageNumber: 10, pageSize: 1);

            var query = _unitOfWork.EmploymentStatus.AsQueryable();

            return Json(request.ToDataTableResult<EmploymentStatus>(query));
        }
        [HttpPost]
        public async Task<IActionResult> ExportTable([FromQuery] string format, [FromForm] string dtParametersJson)
        {
            var dtParameters = new DtParameters();
            if (!string.IsNullOrEmpty(dtParametersJson))
            {
                dtParameters = JsonConvert.DeserializeObject<DtParameters>(dtParametersJson);
            }
            if (dtParameters != default)
            {
                var searchBy = dtParameters.Search?.Value;

                // if we have an empty search then just order the results by Id ascending
                var orderCriteria = "Id";
                var orderAscendingDirection = true;

                var orderCriteriaTemp = "Id";
                var orderAscendingDirectionTemp = true;

                if (dtParameters.Order != null)
                {
                    // in this example we just default sort on the 1st column
                    try
                    {
                        orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                        orderAscendingDirectionTemp = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            orderCriteriaTemp = dtParameters.Columns[dtParameters.Order[1].Column].Data;
                            orderAscendingDirectionTemp = dtParameters.Order[1].Dir.ToString().ToLower() == "asc";
                        }
                        catch (Exception ex1)
                        {
                            orderCriteriaTemp = "Id";
                            orderAscendingDirectionTemp = true;
                        }
                    }
                }

                if (orderCriteriaTemp == null)
                {
                    orderCriteriaTemp = "Id";
                    orderAscendingDirectionTemp = true;
                }

                if (orderCriteriaTemp != null)
                {
                    orderCriteria = orderCriteriaTemp;
                    orderAscendingDirection = orderAscendingDirectionTemp;
                }

                var result = _unitOfWork.EmploymentStatus.AsQueryable();

                if (!string.IsNullOrEmpty(searchBy))
                {
                    result = result.Where(r => r.Code != null && r.Code.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Description != null && r.Description.ToUpper().Contains(searchBy.ToUpper())
                                               );
                }

                result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

                var resultList = await result.ToListAsync();

                var resultListForExport = resultList.ToEmploymentStatusListExport();

                switch (format)
                {
                    case ExportFormat.Excel:
                        return File(
                            await _exportService.ExportToExcel(resultListForExport),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "data.xlsx");

                    case ExportFormat.Csv:
                        return File(_exportService.ExportToCsv(resultListForExport),
                            "application/csv",
                            "data.csv");

                    case ExportFormat.Html:
                        return File(_exportService.ExportToHtml(resultListForExport),
                            "application/csv",
                            "data.html");

                    case ExportFormat.Json:
                        return File(_exportService.ExportToJson(resultListForExport),
                            "application/json",
                            "data.json");

                    case ExportFormat.Xml:
                        return File(_exportService.ExportToXml(resultListForExport),
                            "application/xml",
                            "data.xml");

                    case ExportFormat.Yaml:
                        return File(_exportService.ExportToYaml(resultListForExport),
                            "application/yaml",
                            "data.yaml");
                }
            }

            return null;
        }
    }
}
