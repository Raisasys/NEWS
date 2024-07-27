using Microsoft.Extensions.DependencyInjection;
using Core;

namespace QueryServices;

public static class QueryServiceInstaller
{
	public static void AddQueryServices(this IServiceCollection services)
	{
		services.InstallQueryService<NewsQueryService>();

		services.AddAutoMapper(typeof(NewsMapperProfile));

	}

}