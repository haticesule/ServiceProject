using Entitiess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Runtime.CompilerServices;

namespace ServiceProject.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) => services.AddDbContext<RepositoryContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        public static void ConfigureCustomerRepositoryBase(this IServiceCollection services) =>
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureCustomerManager(this IServiceCollection services) =>
            services.AddScoped<ICustomerService, CustomerManager>();

        public static void ConfigreServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();    
    }
}
