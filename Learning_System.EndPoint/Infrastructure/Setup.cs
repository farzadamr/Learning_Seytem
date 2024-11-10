using BLL.Interfaces;
using DAL.Repositories.Users;
using Microsoft.AspNetCore.Identity;

namespace Learning_System.EndPoint.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(
        this IServiceCollection services)
        {
            services.AddTransient<IUserManager, UserManager>();

            return services;
        }
    }
}
