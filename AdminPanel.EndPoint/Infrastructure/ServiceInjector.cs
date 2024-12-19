using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Users;

namespace AdminPanel.EndPoint.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection InjectServices(
		this IServiceCollection services,
		IConfiguration configuration)
		{
			services.AddTransient<IUserManager>(provider =>
			{
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				return new UserManager(connectionString);
			});
			services.AddTransient<IFileUploadService, FileUploadService>();
			return services;
		}
	}
}
