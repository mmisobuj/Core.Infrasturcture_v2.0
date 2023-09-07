using Core.Infrastructure.Base;
using Core.Infrastructure.Common;
using Core.Infrastructure.Common.Repo;
using Core.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, AppSettings appSettings)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddScoped<IConnectionFactory, ConnectionFactory>(s => new ConnectionFactory(appSettings));
			services.AddScoped<IBaseDapperRepository, BaseDapperRepository>();
			services.AddScoped<ICommonLibRepository, CommonLibRepository>();

			return services;
		}
	}
}
