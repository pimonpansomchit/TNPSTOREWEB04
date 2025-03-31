#nullable disable

using Microsoft.EntityFrameworkCore;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Middleware;
using TNPSTOREWEB.Model.Request;
using FastReport.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace TNPSTOREWEB
{
    public class Startup(IConfiguration configuration)
    {
#pragma warning disable CA2211
        public static string databaseUrl="";
        public static string databaseUrlst = "";
        public IConfiguration Configuration { get; } = configuration;
       
        public object OpenIdConnectDefaults { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {


            var ConnectionString =  Configuration.GetConnectionString("TNPConnection");
            var ConnectionStringst = Configuration.GetConnectionString("STOREConnection");



            if (ConnectionString == null)
            {
                databaseUrl=string.Empty;
                databaseUrlst = string.Empty;

            }
            else
            {
                databaseUrl = ConnectionString;
                databaseUrlst = ConnectionStringst;

            }




            services.AddFastReport();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            ////Entity Framework  
            services.AddControllersWithViews();
            services.AddDbContext<TNPSYSCTLDBContext>(options => options.UseSqlServer(ConnectionString));

            services.AddControllersWithViews();
            services.AddDbContext<TNPSTORESYSDBContext>(options => options.UseSqlServer(ConnectionStringst));


            services.AddDistributedMemoryCache();
            services.Configure<JsonAppSetting>(Configuration.GetSection("JsonAppSetting"));

            services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor,
            HttpContextAccessor>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");            
            services.AddMvc();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddDetection();
            
            

        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseFastReport();
            app.UseAuthorization();


            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
           

            //Used Cookie
            var cookiePolicyOption = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.None
            };
            app.UseCookiePolicy(cookiePolicyOption);
          
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                     name: "default",
                    pattern: "{controller=Authen}/{action=Login}/{id?}");
            });

            app.MapWhen(context => context.Request.Path.ToString().EndsWith(".ashx"),
            appBuilder => {
                appBuilder.UseQrcodeFileMiddelware();

                // For Module
                app.UseIPQRcodeModuleMiddleware();

                // For Handler
                app.MapWhen(context => context.Request.Path.ToString().EndsWith(".ashx"),
                         appBuilder => {
                             appBuilder.UseQrcodeFileMiddelware();
                });
            });
        }
    }
}
