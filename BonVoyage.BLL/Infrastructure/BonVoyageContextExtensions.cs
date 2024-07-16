using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BonVoyage.DAL.EF;

namespace BonVoyage.BLL.Infrastructure
{
    public static class BonVoyageContextExtensions
    {
        public static void AddBonVoyageContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<BonVoyageContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
        }
    }
}
