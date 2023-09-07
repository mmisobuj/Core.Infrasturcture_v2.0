using Core.Infrastructure.Abstractions.Messaging;
using Core.Infrastructure.Models;
using Core.Infrastructure.Common.Repo;

namespace Core.Infrastructure.Common
{
	public class GetDynamicListQuery : BaseQuery, IQuery<IEnumerable<object>>
	{
		public string SpName { get; set; }
		public object Parameters { get; set; }


		public GetDynamicListQuery(string spName, object parameters = null)
		{
			SpName = spName; Parameters = parameters;
		}

	}
	public class GetDynamicListQueryHandler : IQueryHandler<GetDynamicListQuery, IEnumerable<object>>
	{
		private readonly ICommonLibRepository _repo;

		public GetDynamicListQueryHandler(ICommonLibRepository repo)
		{
			_repo = repo;
		}
		public async Task<IEnumerable<dynamic>> Handle(GetDynamicListQuery request, CancellationToken cancellationToken)
		{

			return await _repo.GetStoredProcedureData(request.SpName, request.Parameters);
		}
	}
}
