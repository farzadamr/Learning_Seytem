using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Category;
using DAL.Repositories.Course;
using DAL.Repositories.Person;
using DAL.Repositories.Product;
using DAL.Repositories.SearchAndGet;
using DAL.Repositories.Student;
using DAL.Repositories.Teacher;
using DAL.Repositories.Users;
using DAL.Repositories.Wallet;
using Learning_System.EndPoint.Mapper;
using Microsoft.AspNetCore.Identity;

namespace Learning_System.EndPoint.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IUserManager>(provider =>
            {
                return new UserManager(connectionString);
            });
            services.AddTransient<IProductService>(provider =>
            {
                return new ProductService(connectionString);
            });
			services.AddTransient<ICategoryService>(provider =>
			{
				return new CategoryService(connectionString);
			});
            services.AddTransient<ITeacherService>(provider =>
            {
                return new TeacherService(connectionString);
            });
            services.AddTransient<IPersonService>(provider =>
            {
                return new PersonService(connectionString);
            });
            services.AddTransient<ISearchService>(provider =>
            {
                return new SearchService(connectionString);
            });
			services.AddTransient<IStudentService>(provider =>
			{
				return new StudentService(connectionString);
			});
			services.AddTransient<IWalletService>(provider =>
			{
				return new WalletService(connectionString);
			});
            services.AddTransient<ICourseService>(provider =>
            {
                return new CourseService(connectionString);
            });
            services.AddTransient<IUriComposer, UriComposer>();
            services.AddAutoMapper(typeof(AuthMappingProfile)); 
            return services;
        }
    }
}
