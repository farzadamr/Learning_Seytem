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
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				return new PersonService(connectionString);
			});
			services.AddTransient<IFileUploadService, FileUploadService>();
			return services;
		}
	}
}
