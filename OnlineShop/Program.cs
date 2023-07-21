using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using OnlineShop.DB;
using Serilog;
using System.Globalization;
using WomanShop.Interfaces;
using WomanShop.Storages;
using System.Configuration;
using OnlineShop.DB.Storages;
using OnlineShop.DB.Interfaces;

namespace WomanShop
{
    public class Program
    {
      
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            
            builder.Services.AddControllersWithViews();
            string connection = builder.Configuration.GetConnectionString("online_shop");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
            builder.Services.AddSingleton<IRolesStorage, InMemoryRolesStorage>();
            builder.Services.AddTransient<IProductsStorage, DbProductsStorage>();
            builder.Services.AddTransient<IFavoritesStorage, DbFavoritesStorage>();
            builder.Services.AddSingleton<IUsersStorage, InMemoryUsersStorage>();
            builder.Services.AddTransient<ICartsStorage,DbCartsStorage>();
            builder.Services.AddTransient<IOrdersStorage,DbOrdersStorage>();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US") };
                options.DefaultRequestCulture=new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            }
            );
            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();

            app.UseStaticFiles();

            app.UseRouting();
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
                app.UseRequestLocalization(localizationOptions);
            app.MapControllerRoute(
                name:"MyArea",
                pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}