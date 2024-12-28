using BLL.ExternalApi;
using BLL.Interfaces;
using DAL.Repositories.Category;
using DAL.Repositories.Product;
using DAL.Repositories.Users;
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
			services.AddTransient<IUriComposer, UriComposer>();
            services.AddAutoMapper(typeof(AuthMappingProfile)); 
            return services;
        }
    }
}
