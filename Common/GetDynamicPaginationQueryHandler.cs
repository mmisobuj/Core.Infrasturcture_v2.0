
using Core.Infrastructure.Abstractions.Messaging;
using Core.Infrastructure.Common.Repo;
using Core.Infrastructure.Models;

namespace Core.Infrastructure.Common
{
	public class GetDynamicPaginationQuery : PaginationBaseQuery, IQuery<object>
    {
        public string SpName { get; set; }
        public object Parameters { get; set; }


        public GetDynamicPaginationQuery(string spName, object parameters = null)
        {
            SpName = spName; Parameters = parameters;
        }

    }
    public class GetDynamicPaginationQueryHandler : IQueryHandler<GetDynamicPaginationQuery, object>
    {
        private readonly ICommonLibRepository _repo;

        public GetDynamicPaginationQueryHandler(ICommonLibRepository repo)
        {
            _repo = repo;
        }
        public async Task<object> Handle(GetDynamicPaginationQuery request, CancellationToken cancellationToken)
        {

            return await _repo.GetSpGridData(request.Options, request.SpName, request.Parameters);
        }
    }
}
