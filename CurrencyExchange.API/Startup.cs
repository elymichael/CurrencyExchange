namespace CurrencyExchange.API
{
    using CurrencyExchange.API.Constants;
    using CurrencyExchange.API.model;
    using CurrencyExchange.API.services;
    using CurrencyExchange.Entities;
    using CurrencyExchange.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Exchange API", Version = "V1" });
            });

            //Custom Services
            var configuration = Configuration.GetSection(ApiConstants.SETTINGS_CONFIGURATION);
            services.Configure<Configuration>(configuration);

            services.AddSingleton<IRequest, Request>();
            services.AddSingleton<ICurrencyRate, CurrencyRate>();

            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<IRateTransactionRepository, RateTransactionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<CurrencyDBContext>(opt => opt.UseInMemoryDatabase("Service"));

            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Exchange API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("default", "{controller=Default}/{action=Index}/{Id?}");
            });
            //app.UseMvc();
        }
    }
}
