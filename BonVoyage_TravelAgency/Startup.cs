using BonVoyage_TravelAgency.SignalR;
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
           
            // Регистрация SignalR в сервисах приложения
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Настройка маршрутов для SignalR хабов
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Настройка маршрута для SignalR хаба
                endpoints.MapHub<ChatHub>("/chat");

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
