using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Models;
using Core.Infrastructure.Abstractions.Messaging;
using Core.Infrastructure.Common.Repo;
using Core.Infrastructure.Base;

namespace Core.Infrastructure.Common
{
    public class DynamicCommand : BaseCommand, ICommand<ResponseResult>
    {
        public string SpName { get; set; }
        public object Parameters { get; set; }


        public DynamicCommand(string spName, object parameters = null)
        {
            SpName = spName; Parameters = parameters;
        }

    }
    public class DynamicCommandHandler : ICommandHandler<DynamicCommand, ResponseResult>
    {
        private readonly ICommonLibRepository _repo;

        public DynamicCommandHandler(ICommonLibRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResponseResult> Handle(DynamicCommand request, CancellationToken cancellationToken)
        {

            //  return await _repo.GetStoredProcedureData(request.SpName, request.Parameters);

            ResponseResult rr = new();
            try
            {
            
                int res = await _repo.ExecuteStoredProcedure(request.SpName, request.Parameters);
                if (res > 0)
                {
                    rr.Id = res.ToString();
                    rr.Message = "Data has been stored successfully";
                    rr.IsSuccessStatus = true;
                }
                else
                {
                    rr.Id = res.ToString();
                    rr.Message = "Data not saved";
                    rr.IsSuccessStatus = false;
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {   }
            return await Task.Run(() => rr);
        }
    }
}
