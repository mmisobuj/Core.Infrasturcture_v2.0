﻿using Core.Infrastructure.Extensions;
using Core.Infrastructure.Grid;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Infrastructure.Paging
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <returns>A DataSourceResult object populated from the processed IQueryable.</returns>
        //public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, int take, int skip, List<Sort> sort, Filter filter)
        //{
        //    return queryable.ToDataSourceResult(take, skip, sort, filter, null, null);
        //}
        public static Task<GridEntity<T>> GridDataSourceAync<T>(this IQueryable<T> queryable, GridOptions option)
        {
            return Task.Run(() => queryable.GridDataSource(option));

        }
        public static GridEntity<T> GridDataSource<T>(this IQueryable<T> queryable, GridOptions option)
        {

            if (option != null)
            {

                return queryable.ToPaging(option);

                //List<Sort> sort = null;
                //if (option.sort != null)
                //{
                //    foreach (var item in option.sort)
                //    {
                //        sort.Add(new Sort { Dir = item.dir, Field = item.field });
                //    }
                //}
                //Filter filter = null;
                //if (option.filter != null)
                //{
                //    filter = new Filter();
                //    filter.Logic = option.filter.Logic;
                //    filter.Filters = new List<Filter>();
                //    if (option.filter.Filters != null)
                //    {
                //        foreach (var item in option.filter.Filters)
                //        {
                //            filter.Filters.Add(new Filter { Field = item.Field, Operator = item.Operator, Value = item.Value });
                //        }
                //    }

                //}

                //var dataReust = queryable.ToDataSourceResult(option.take, option.skip, sort, filter, null, null);
                //var list = dataReust.Items;
                //return new GridEntity<T>() { Items = (List<T>)list, TotalCount = dataReust.TotalCount };
            }
            return new GridEntity<T>() { Items = queryable.ToList(), TotalCount = queryable.Count() }; ;
        }

        public static GridEntity<T> ToGridDataSource<T>(this IEnumerable<T> queryable, GridOptions option)
        {

            if (option != null)
            {
                return queryable.ToPaging(option);

                //List<Sort> sort = null;
                //if (option.sort != null)
                //{
                //    foreach (var item in option.sort)
                //    {
                //        sort.Add(new Sort { Dir = item.dir, Field = item.field });
                //    }
                //}
                //Filter filter = null;
                //if (option.filter != null)
                //{
                //    filter = new Filter();
                //    filter.Logic = option.filter.Logic;
                //    filter.Filters = new List<Filter>();
                //    if (option.filter.Filters != null)
                //    {
                //        foreach (var item in option.filter.Filters)
                //        {
                //            filter.Filters.Add(new Filter { Field = item.Field, Operator = item.Operator, Value = item.Value });
                //        }
                //    }

                //}

                //var dataReust = queryable.AsQueryable().ToDataSourceResult(option.take, option.skip, sort, filter, null, null);
                //var list = dataReust.Items;
                //return new GridEntity<T>() { Items = (List<T>)list, TotalCount = dataReust.TotalCount };
            }
            return new GridEntity<T>() { Items = queryable.ToList(), TotalCount = queryable.Count() }; ;
        }

        /// <summary>
        ///  Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="request">The DataSourceRequest object containing take, skip, sort, filter, aggregates, and groups data.</param>
        /// <returns>A DataSourceResult object populated from the processed IQueryable.</returns>
        //public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, DataSourceRequest request)
        //{
        //    return queryable.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        //}

        /// <summary>
        /// Applies data processing (paging, sorting, filtering and aggregates) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <param name="aggregates">Specifies the current aggregates.</param>
        /// <param name="group">Specifies the current groups.</param>
        /// <returns>A DataSourceResult object populated from the processed IQueryable.</returns>
        //public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, int take, int skip, List<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates, IEnumerable<GroupQuery> group)
        //{
        //    var errors = new List<object>();

        //    // Filter the data first
        //    if (filter != null)
        //    {
        //        queryable = Filters(queryable, filter, errors);
        //    }

        //    // Calculate the total number of records (needed for paging)            
        //    var total = queryable.Count();

        //    // Calculate the aggregates
        //    var aggregate = Aggregates(queryable, aggregates);

        //    if (group?.Any() == true)
        //    {
        //        //if(sort == null) sort = GetDefaultSort(queryable.ElementType, sort);
        //        if (sort == null) sort = new List<Sort>();

        //        foreach (var source in group.Reverse())
        //        {
        //            sort.Add(new Sort
        //            {
        //                Field = source.Field,
        //                Dir = source.Dir
        //            });
        //        }
        //    }

        //    // Sort the data
        //    queryable = Sort(queryable, sort);

        //    // Finally page the data
        //    if (take > 0)
        //    {
        //        queryable = Page(queryable, take, skip);
        //    }

        //    var result = new DataSourceResult
        //    {
        //        TotalCount = total,
        //        Aggregates = aggregate
        //    };

        //    //// Group By
        //    //if (group?.Any() == true)
        //    //{
        //    //    //result.Groups = queryable.ToList().GroupByMany(group);                
        //    //    result.Groups = queryable.GroupByMany(group);
        //    //}
        //    //else
        //    //{
        //    //    result.Items = queryable.ToList();
        //    //}

        //    // Set errors if any
        //    if (errors.Count > 0)
        //    {
        //        result.Errors = errors;
        //    }

        //    return result;
        //}

        /// <summary>
        /// Asynchronously applies data processing (paging, sorting, filtering and aggregates) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <param name="aggregates">Specifies the current aggregates.</param>
        /// <param name="group">Specifies the current groups.</param>
        /// <returns>A DataSourceResult object populated from the processed IQueryable.</returns>
        //public static Task<DataSourceResult> ToDataSourceResultAsync<T>(this IQueryable<T> queryable, int take, int skip, List<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates = null, IEnumerable<GroupQuery> group = null)
        //{
        //    return Task.Run(() => queryable.ToDataSourceResult(take, skip, sort, filter, aggregates, group));
        //}

        //private static IQueryable<T> Filters<T>(IQueryable<T> queryable, Filter filter, List<object> errors)
        //{
        //    if (filter?.Logic != null)
        //    {
        //        // Pretreatment some work
        //        filter = PreliminaryWork(typeof(T), filter);

        //        // Collect a flat list of all filters
        //        var filters = filter.All();

        //        /* Method.1 Use the combined expression string */
        //        // Step.1 Create a predicate expression e.g. Field1 = @0 And Field2 > @1
        //        string predicate;
        //        try
        //        {
        //            predicate = filter.ToExpression(typeof(T), filters);
        //        }
        //        catch (Exception ex)
        //        {
        //            errors.Add(ex.Message);
        //            return queryable;
        //        }

        //        // Step.2 Get all filter values as array (needed by the Where method of Dynamic Linq)
        //        var values = filters.Select(f => f.Value).ToArray();

        //        // Step.3 Use the Where method of Dynamic Linq to filter the data
        //        queryable = queryable.Where(predicate, values);

        //        /* Method.2 Use the combined lambda expression */
        //        // Step.1 Create a parameter "p"
        //        //var parameter = Expression.Parameter(typeof(T), "p");

        //        // Step.2 Make up expression e.g. (p.Number >= 3) AndAlso (p.Company.Name.Contains("M"))
        //        //Expression expression;
        //        //try 
        //        //{
        //        //    expression = filter.ToLambdaExpression<T>(parameter, filters);         
        //        //}
        //        //catch(Exception ex)
        //        //{
        //        //    errors.Add(ex.Message);
        //        //    return queryable;
        //        //} 

        //        // Step.3 The result is e.g. p => (p.Number >= 3) AndAlso (p.Company.Name.Contains("M"))
        //        //var predicateExpression = Expression.Lambda<Func<T, bool>>(expression, parameter);
        //        //queryable = queryable.Where(predicateExpression);
        //    }

        //    return queryable;
        //}

        internal static object Aggregates<T>(IQueryable<T> queryable, IEnumerable<Aggregator> aggregates)
        {
            if (aggregates?.Any() == true)
            {
                var objProps = new Dictionary<DynamicProperty, object>();
                var groups = aggregates.GroupBy(g => g.Field);
                Type type = null;

                foreach (var group in groups)
                {
                    var fieldProps = new Dictionary<DynamicProperty, object>();
                    foreach (var aggregate in group)
                    {
                        var prop = typeof(T).GetProperty(aggregate.Field);
                        var param = Expression.Parameter(typeof(T), "s");
                        var selector = aggregate.Aggregate == "count" && Nullable.GetUnderlyingType(prop.PropertyType) != null
                            ? Expression.Lambda(Expression.NotEqual(Expression.MakeMemberAccess(param, prop), Expression.Constant(null, prop.PropertyType)), param)
                            : Expression.Lambda(Expression.MakeMemberAccess(param, prop), param);
                        var mi = aggregate.MethodInfo(typeof(T));
                        if (mi == null) continue;

                        var val = queryable.Provider.Execute(Expression.Call(null, mi, aggregate.Aggregate == "count" && Nullable.GetUnderlyingType(prop.PropertyType) == null
                                  ? new[] { queryable.Expression }
                                  : new[] { queryable.Expression, Expression.Quote(selector) }));

                        fieldProps.Add(new DynamicProperty(aggregate.Aggregate, typeof(object)), val);
                    }

                    type = DynamicClassFactory.CreateType(fieldProps.Keys.ToList());
                    var fieldObj = Activator.CreateInstance(type);
                    foreach (var p in fieldProps.Keys)
                    {
                        type.GetProperty(p.Name).SetValue(fieldObj, fieldProps[p], null);
                    }
                    objProps.Add(new DynamicProperty(group.Key, fieldObj.GetType()), fieldObj);
                }

                type = DynamicClassFactory.CreateType(objProps.Keys.ToList());

                var obj = Activator.CreateInstance(type);
                foreach (var p in objProps.Keys)
                {
                    type.GetProperty(p.Name).SetValue(obj, objProps[p], null);
                }

                return obj;
            }

            return null;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort?.Any() == true)
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = string.Join(",", sort.Select(s => s.ToExpression()));

                // Use the OrderBy method of Dynamic Linq to sort the data
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }

        //private static IQueryable<T> Page<T>(IQueryable<T> queryable, int take, int skip)
        //{
        //    return queryable.Skip(skip).Take(take);
        //}

        /// <summary>
        /// Pretreatment of specific DateTime type and convert some illegal value type
        /// </summary>
        /// <param name="filter"></param>                  +
        //private static Filter PreliminaryWork(Type type, Filter filter)
        //{
        //    if (filter.Filters != null && filter.Logic != null)
        //    {
        //        var newFilters = new List<Filter>();
        //        foreach (var f in filter.Filters)
        //        {
        //            newFilters.Add(PreliminaryWork(type, f));
        //        }

        //        filter.Filters = newFilters;
        //    }

        //    if (filter.Value == null) return filter;

        //    // When we have a decimal value, it gets converted to an integer/double that will result in the query break
        //    var currentPropertyType = Filter.GetLastPropertyType(type, filter.Field);
        //    if ((currentPropertyType == typeof(decimal) || currentPropertyType == typeof(decimal?)) && decimal.TryParse(filter.Value.ToString(), out decimal number))
        //    {
        //        filter.Value = number;
        //        return filter;
        //    }

        //    // if(currentPropertyType.GetTypeInfo().IsEnum && int.TryParse(filter.Value.ToString(), out int enumValue))
        //    // {           
        //    //     filter.Value = Enum.ToObject(currentPropertyType, enumValue);
        //    //     return filter;
        //    // }

        //    // Convert datetime-string to DateTime
        //    if (currentPropertyType == typeof(DateTime) && DateTime.TryParse(filter.Value.ToString(), out DateTime dateTime))
        //    {
        //        filter.Value = dateTime;

        //        // Copy the time from the filter
        //        var localTime = dateTime.ToLocalTime();

        //        // Used when the datetime's operator value is eq and local time is 00:00:00 
        //        if (filter.Operator == "eq")
        //        {
        //            if (localTime.Hour != 0 || localTime.Minute != 0 || localTime.Second != 0)
        //                return filter;

        //            var newFilter = new Filter { Logic = "and" };
        //            newFilter.Filters = new List<Filter>
        //            {
        //                // Instead of comparing for exact equality, we compare as greater than the start of the day...
        //                new Filter
        //                {
        //                    Field = filter.Field,
        //                    Filters = filter.Filters,
        //                    Value = new DateTime(localTime.Year, localTime.Month, localTime.Day, 0, 0, 0),
        //                    Operator = "gte"
        //                },
        //                // ...and less than the end of that same day (we're making an additional filter here)
        //                new Filter
        //                {
        //                    Field = filter.Field,
        //                    Filters = filter.Filters,
        //                    Value = new DateTime(localTime.Year, localTime.Month, localTime.Day, 23, 59, 59),
        //                    Operator = "lte"
        //                }
        //            };

        //            return newFilter;
        //        }

        //        // Convert datetime to local 
        //        filter.Value = new DateTime(localTime.Year, localTime.Month, localTime.Day, localTime.Hour, localTime.Minute, localTime.Second, localTime.Millisecond);
        //    }

        //    return filter;
        //}

        /// <summary>
        /// The way this extension works it pages the records using skip and takes to do that we need at least one sort property.
        /// </summary>
        private static IEnumerable<Sort> GetDefaultSort(Type type, IEnumerable<Sort> sort)
        {
            if (sort == null)
            {
                var elementType = type;
                var properties = elementType.GetProperties().ToList();

                //by default make dir desc
                var sortByObject = new Sort
                {
                    Dir = "desc"
                };

                PropertyInfo propertyInfo;
                //look for property that is called id
                if (properties.Any(p => string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase)))
                {
                    propertyInfo = properties.FirstOrDefault(p => string.Equals(p.Name, "id", StringComparison.OrdinalIgnoreCase));
                }
                //or contains id
                else if (properties.Any(p => p.Name.IndexOf("id", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    propertyInfo = properties.FirstOrDefault(p => p.Name.IndexOf("id", StringComparison.OrdinalIgnoreCase) >= 0);
                }
                //or just get the first property
                else
                {
                    propertyInfo = properties.FirstOrDefault();
                }

                if (propertyInfo != null)
                {
                    sortByObject.Field = propertyInfo.Name;
                }
                sort = new List<Sort> { sortByObject };
            }

            return sort;
        }


        //public static IQueryable<T> FilterDataSourceRequest<T>(this IQueryable<T> query, FilterDescriptorCollection filters)
        //{
        //    if (filters != null && filters.Any())
        //    {
        //        foreach (FilterDescriptor filter in filters)
        //        {
        //            query = ApplyFilter(query, filter);
        //        }
        //    }

        //    return query;
        //}

        //private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, FilterDescriptor filter)
        //{
        //    var parameter = Expression.Parameter(typeof(T), "x");
        //    var property = Expression.Property(parameter, filter.Member);
        //    var constant = Expression.Constant(filter.Value);

        //    Expression condition;

        //    switch (filter.Operator)
        //    {
        //        case FilterOperator.Contains:
        //            condition = Expression.Call(property, typeof(string).GetMethod("Contains"), constant);
        //            break;

        //        case FilterOperator.IsEqualTo:
        //            condition = Expression.Equal(property, constant);
        //            break;

        //        // Add more cases for other filter operators as needed

        //        default:
        //            throw new NotSupportedException($"Filter operator '{filter.Operator}' is not supported.");
        //    }

        //    var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
        //    return query.Where(lambda);
        //}

        public static GridEntity<T> ToPaging<T>(this IEnumerable<T> list, GridOptions options)
        {
            var pagingStr = JsonConvert.SerializeObject(options);
            var pagingOption = JsonConvert.DeserializeObject<PagingQuery>(pagingStr);

            return list.ToPaging(pagingOption);
        }
        public static GridEntity<T> ToPaging<T>(this IEnumerable<T> list, PagingQuery options)
        {


            var data = list.AsQueryable();
            int total = data.Count();
            List<T> resultData = new List<T>();
            bool isGrouped = false;
            //   GridRequest request = new GridRequest();
            var aggregates = new Dictionary<string, Dictionary<string, string>>();

            if (options.Sorts != null)
            {
                data = data.Sort(options.Sorts);
            }

            if (options.Filter != null)
            {
                data = data.Filter(options.Filter);
                total = data.Count();
            }

            //if (options.Aggregates != null)
            //{
            //    aggregates = data.CalculateAggregates(options.Aggregates);
            //}
            if (options.Take > 0)
            {
                data = data
               .Skip(options.Skip)
               .Take(options.Take);
                // data = data.Page(options.Skip, options.Take);
            }

            //if (options.Groups != null && options.Groups.Count > 0 && !options.GroupPaging)
            //{
            //    resultData = (data.Group(options.Groups).Cast<GroupRequest>()).ToList();
            //    isGrouped = true;
            //}
            //else
            //{
            //    resultData = data.ToList();
            //}
            resultData = data.ToList();
            // var result = new GridResponse(resultData, aggregates, total, isGrouped).ToResult();
            return new GridEntity<T>() { Items = resultData, TotalCount = total };
        }
        public static IQueryable<T> Filter<T>(this IQueryable<T> data, Filter filter)
        {
            var filterExpression = ExpressionExtension.GenerateFilterExpression<T>(filter);

            if (filterExpression != null)
            {
                return data.Where(filterExpression);
            }

            return data;
        }

        public static string GetOperator(string key)
        {
            Dictionary<string, string> operators = new Dictionary<string, string>()
            {
                { "eq", "Equals" },
                { "neq", "DoesNotEqual" },
                { "doesnotcontain", "DoesNotContain" },
                { "contains", "Contains" },
                { "startswith", "StartsWith" },
                { "endswith", "EndsWith" },
                { "isnull", "IsNull" },
                { "isnotnull", "IsNotNull" },
                { "isempty", "IsEmpty" },
                { "isnotempty", "IsNotEmpty" },
                { "gt", "GreaterThan" },
                { "gte", "GreaterThanOrEqual" },
                { "lt", "LessThan" },
                { "lte", "LessThanOrEqual" }
            };

            return operators[key];
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> data, List<Sort> sorts)
        {
            bool isFirst = true;

            foreach (var sort in sorts)
            {
                string methodName = string.Empty;

                if (isFirst)
                {
                    isFirst = false;
                    methodName = sort.Dir == "asc" ? "OrderBy" : "OrderByDescending";
                }
                else
                {
                    methodName = sort.Dir == "asc" ? "ThenBy" : "ThenByDescending";
                }

                data = data.ApplySort(methodName, sort.Field);
            }

            return data;
        }

        public static IQueryable<T> GroupSort<T>(this IQueryable<T> data, string dir)
        {
            var methodName = dir == "asc" ? "OrderBy" : "OrderByDescending";

            return data.ApplySort(methodName, "Key");
        }

        private static IQueryable<T> ApplySort<T>(this IQueryable<T> data, string methodName, string field)
        {
            var propInfo = CommonExtension.GetPropertyInfo(typeof(T), field);
            var expr = ExpressionExtension.BuildLambda(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { data, expr });
        }


    }
}
