using BLL.Interfaces;
using DAL.Repositories.Users;
using Microsoft.AspNetCore.Identity;

namespace Learning_System.EndPoint.Infrastructure
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
            return services;
        }
    }
}
