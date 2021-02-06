using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingService.Application.Interfaces;
using ParkingService.Persistence;
using Shared.Infrastructure;

namespace ParkingService.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(Assembly.GetAssembly(typeof(IUnitOfWork)));

            services.AddOpenApi();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppContext>(x =>
            {
                //x.UseSqlServer(connectionString);
                //x.UseLazyLoadingProxies();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi(env.ApplicationName);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
