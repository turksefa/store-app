using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace StoreApp.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
