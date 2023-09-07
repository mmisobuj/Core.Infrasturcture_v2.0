
using Core.Infrastructure.Common.Repo;
using Core.Infrastructure.Models;
using MediatR;

namespace Core.Infrastructure.Common
{
    public class GetCommonComboQuery : IRequest<IEnumerable<CommonComboVm>>
    {
        public string TableName { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string? CascadingField { get; set; }
        public string? CascadingValue { get; set; }


    }
    public class GetCommonComboHandlerQuery : IRequestHandler<GetCommonComboQuery, IEnumerable<CommonComboVm>>
    {
        private readonly ICommonLibRepository _repo;
        public GetCommonComboHandlerQuery(ICommonLibRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CommonComboVm>> Handle(GetCommonComboQuery request, CancellationToken cancellationToken)
        {
            string commaSepartedString = $"{request.ValueField},{request.TextField},{request.TableName}";
            if (!string.IsNullOrEmpty(request.CascadingField))
            {
                commaSepartedString += $",{request.CascadingField},{request.CascadingValue}";
            }

            return await _repo.GetCommonComboData(commaSepartedString);
        }
    }
}
