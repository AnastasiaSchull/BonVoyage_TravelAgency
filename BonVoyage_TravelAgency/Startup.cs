using Microsoft.AspNetCore.Builder;
using React.AspNet;

namespace BonVoyage_TravelAgency
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
