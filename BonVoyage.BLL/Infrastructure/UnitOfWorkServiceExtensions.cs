using Microsoft.Extensions.DependencyInjection;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Repositories;

namespace BonVoyage.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
