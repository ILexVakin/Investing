using Investing.Data;
using Investing.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MainContext>(options =>
            options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection")));

            Log.Logger = Logger.BaseLogger();
            services.AddLogging(c => c.AddSerilog()) ;
            services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpClient();
            services.AddRegistrationServices();
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                                             ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));
            services.AddScoped<StackExchange.Redis.IDatabase>(sp =>
                             sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            //if (env.IsDevelopment())
            //{
             app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    app.UseHsts();
            //}
            //app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
