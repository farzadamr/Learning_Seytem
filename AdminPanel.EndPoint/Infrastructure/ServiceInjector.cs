using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Person;
using DAL.Repositories.Users;

namespace AdminPanel.EndPoint.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection InjectServices(
		this IServiceCollection services,
		IConfiguration configuration)
		{
			services.AddTransient<IUriComposer, UriComposer>();
			services.AddTransient<IPersonService>(provider =>
			{
				var composer = provider.GetService<IUriComposer>() ?? throw new Exception("uriComposer not registered!");
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				return new PersonService(connectionString, composer);
			});
			services.AddTransient<IFileUploadService, FileUploadService>();
			return services;
		}
	}
}
