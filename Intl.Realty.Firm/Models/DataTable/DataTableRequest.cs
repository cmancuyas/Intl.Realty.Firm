using Intl.Realty.Firm.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Intl.Realty.Firm.Models.Models.DataTable
{
    public class DataTableRequest
    {
        public string sEcho { get; set; }
        public int iColumns { get; set; }
        public string sColumns { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public string sSearch { get; set; }
        public bool bRegex { get; set; }
        public int iSortingCols { get; set; }

        public List<DataTableColumnActions> Columns { get; set; }
        public List<DataTableSort> Sorts { get; set; }

        public IEnumerable<TResult> ToList<T, TResult>(IQueryable<T> Data, Expression<Func<T, TResult>> selector) where T : class where TResult : class
        {

            if (string.IsNullOrEmpty(sSearch) == false)
            {
                Data = Data.Filter(sSearch);
            }

            IQueryable<TResult> result = Data.Select(selector);

            if (Sorts.Count > 0 && Columns.Count > 0)
            {
                foreach (var sort in Sorts)
                {
                    var column = Columns[sort.iSortCol];
                    result = result.OrderBy(column.mDataProp + " " + sort.sSortDir);
                }
            }

            return result.Skip(iDisplayStart).Take(iDisplayLength).AsEnumerable();
        }

        public IEnumerable<T> ToList<T>(IQueryable<T> Data) where T : class
        {
            var result = Data;

            if (string.IsNullOrEmpty(sSearch) == false)
            {
                result = result.Filter(sSearch);
            }

            if (Sorts.Count > 0 && Columns.Count > 0)
            {
                foreach (var sort in Sorts)
                {
                    var column = Columns[sort.iSortCol];
                    result = result.OrderBy(column.mDataProp + " " + sort.sSortDir);
                }
            }

            return result.Skip(iDisplayStart).Take(iDisplayLength).AsEnumerable();
        }

        public object ToDataTableResult<T, TResult>(IQueryable<T> Data, Expression<Func<T, TResult>> selector) where T : class where TResult : class
        {
            IQueryable<T> inData = Data;

            if (string.IsNullOrEmpty(sSearch) == false)
            {
                inData = inData.Filter(sSearch);
            }

            IQueryable<TResult> result = inData.Select(selector);

            if (Sorts.Count > 0 && Columns.Count > 0)
            {
                foreach (var sort in Sorts)
                {
                    var column = Columns[sort.iSortCol];
                    result = result.OrderBy(column.mDataProp + " " + sort.sSortDir);
                }
            }

            return new DataTableResult<TResult>
            {
                Draw = Convert.ToInt32(sEcho),
                RecordsTotal = Data.Count(),
                RecordsFiltered = result.Count(),
                Data = result.Skip(iDisplayStart).Take(iDisplayLength).AsEnumerable()
            };
        }

        public object ToDataTableResult<T>(IQueryable<T> Data) where T : class
        {
            var result = Data;

            if (string.IsNullOrEmpty(sSearch) == false)
            {
                result = result.Filter(sSearch);
            }

            if (Sorts.Count > 0 && Columns.Count > 0)
            {
                foreach (var sort in Sorts)
                {
                    var column = Columns[sort.iSortCol];
                    result = result.OrderBy(column.mDataProp + " " + sort.sSortDir);
                }
            }

            return new DataTableResult<T>
            {
                Draw = Convert.ToInt32(sEcho),
                RecordsTotal = Data.Count(),
                RecordsFiltered = result.Count(),
                Data = result.Skip(iDisplayStart).Take(iDisplayLength).AsEnumerable()
            };
        }

    }
    public class DataTableResult<T>
    {

        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("data")]
        public IEnumerable<T>? Data { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string? Error { get; set; }

        public string? PartialView { get; set; }
    }
    public class DataTableColumnActions
    {
        public string mDataProp { get; set; }
        public string sSearch { get; set; }
        public bool bRegex { get; set; }
        public bool bSearchable { get; set; }
        public bool bSortable { get; set; }
    }

    public class DataTableSort
    {
        public int iSortCol { get; set; }
        public string? sSortDir { get; set; }
    }

    public class DataTableRequestProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(DataTableRequest))
                return new DataTableRequestBinder();

            return null!;
        }


        public class DataTableRequestBinder : Attribute, IModelBinder
        {
            public async Task BindModelAsync(ModelBindingContext bindingContext)
            {
                await Task.Run(() =>
                {
                    int index;
                    var request = new DataTableRequest();

                    request.bRegex = Convert.ToBoolean(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "bRegex").Select(i => i.Value.ToString()).FirstOrDefault());
                    request.sSearch = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "sSearch").Select(i => i.Value.ToString()).FirstOrDefault();
                    request.sEcho = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "sEcho").Select(i => i.Value.ToString()).FirstOrDefault();
                    request.sColumns = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "sColumns").Select(i => i.Value.ToString()).FirstOrDefault();
                    request.iColumns = Convert.ToInt32(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "iColumns").Select(i => i.Value.ToString()).FirstOrDefault());
                    request.iDisplayLength = Convert.ToInt32(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "iDisplayLength").Select(i => i.Value.ToString()).FirstOrDefault());
                    request.iDisplayStart = Convert.ToInt32(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "iDisplayStart").Select(i => i.Value.ToString()).FirstOrDefault());
                    request.iSortingCols = Convert.ToInt32(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "iSortingCols").Select(i => i.Value.ToString()).FirstOrDefault());

                    request.Sorts = new List<DataTableSort>();
                    request.Columns = new List<DataTableColumnActions>();


                    index = 0;
                    while (bindingContext.HttpContext.Request.Query.Keys.Any(k => k == "iSortCol_" + index))
                    {
                        var sort = new DataTableSort();
                        sort.iSortCol = Convert.ToInt32(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "iSortCol_" + index).Select(i => i.Value.ToString()).FirstOrDefault());
                        sort.sSortDir = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "sSortDir_" + index).Select(i => i.Value.ToString()).FirstOrDefault();
                        request.Sorts.Add(sort);
                        index++;
                    }

                    index = 0;
                    while (bindingContext.HttpContext.Request.Query.Keys.Any(k => k == "mDataProp_" + index))
                    {
                        var column = new DataTableColumnActions();
                        column.bRegex = Convert.ToBoolean(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "bRegex_" + index).Select(i => i.Value.ToString()).FirstOrDefault());
                        column.bSearchable = Convert.ToBoolean(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "bSearchable_" + index).Select(i => i.Value.ToString()).FirstOrDefault());
                        column.bSortable = Convert.ToBoolean(bindingContext.HttpContext.Request.Query.Where(i => i.Key == "bSortable_" + index).Select(i => i.Value.ToString()).FirstOrDefault());
                        column.mDataProp = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "mDataProp_" + index).Select(i => i.Value.ToString()).FirstOrDefault();
                        column.sSearch = bindingContext.HttpContext.Request.Query.Where(i => i.Key == "sSearch_" + index).Select(i => i.Value.ToString()).FirstOrDefault();
                        request.Columns.Add(column);
                        index++;
                    }

                    bindingContext.Result = ModelBindingResult.Success(request);
                    return Task.CompletedTask;

                });
            }

        }
    }
}
