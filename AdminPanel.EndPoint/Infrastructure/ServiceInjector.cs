using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Category;
using DAL.Repositories.Person;
using DAL.Repositories.SearchAndGet;
using DAL.Repositories.Student;
using DAL.Repositories.Teacher;
using DAL.Repositories.Users;
using DAL.Repositories.Wallet;

namespace AdminPanel.EndPoint.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection InjectServices(
		this IServiceCollection services,
		IConfiguration configuration)
		{
            var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddTransient<IPersonService>(provider =>
			{
				return new PersonService(connectionString);
			});
			services.AddTransient<IStudentService>(provider =>
			{
				return new StudentService(connectionString);
			});
			services.AddTransient<ITeacherService>(provider =>
			{
				return new TeacherService(connectionString);
			});
			services.AddTransient<IWalletService>(provider =>
			{
				return new WalletService(connectionString);
			});
			services.AddTransient<ICategoryService>(provider =>
			{
				return new CategoryService(connectionString);
			});
			services.AddTransient<ISearchService>(provider =>
			{
				return new SearchService(connectionString);
			});
			//file services
			services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IUriComposer, UriComposer>();
            return services;
		}
	}
}
