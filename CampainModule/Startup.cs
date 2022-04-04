using CampainModule.Bll.BackgroundServiceManagers;
using CampainModule.Bll.ServiceManager;
using CampainModule.Bll.Services;
using CampainModule.Data;
using Microsoft.EntityFrameworkCore;

namespace CampainModule
{
    public static class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddDbContext<CampaignModuleDbContext>();

            //var optionBuilder = new DbContextOptionsBuilder<CampaignModuleDbContext>();
            //optionBuilder.UseSqlServer("");


            //builder.Services.AddSingleton<IProductService, ProductServiceManager>(d => new CampaignModuleDbContext(optionBuilder.Options));
            builder.Services.AddSingleton<IProductService, ProductServiceManager>();
            builder.Services.AddSingleton<IOrderService, OrderServiceManager>();
            builder.Services.AddSingleton<ICampaignService, CampaignServiceManager>();
            builder.Services.AddSingleton<IRunCampaignService, RunCampaignServiceManager>();

            builder.Services.AddHostedService<Worker>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

        }

        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }
    }
}
