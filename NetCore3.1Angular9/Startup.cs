using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreBusinessEngine.Contracts;
using NetCoreBusinessEngine.Implementaion;
using NetCoreData.DataContracts;
using NetCoreData.DataContext;
using NetCoreData.DbModels;
using NetCoreData.Implementaion;
 

namespace NetCore3._1Angular9
{
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
            services.AddControllers();
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            services.AddScoped<IItemBusinessEngine,ItemBusinessEngine>();
            services.AddScoped<ICustomerBusinessEngine, CustomerBusinessEngine>();
            services.AddScoped<IOrderBusinessEngine, OrderBusinessEngine>();

            services.AddDbContext<NetCoreDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
                                .AddEntityFrameworkStores<NetCoreDbContext>();

            services.AddScoped<IApplicationUserBusinessEngine, ApplicationUserBusinessEngine>();
            services.AddCors();

            var appSettingsSection = Configuration.GetSection("AppSettings");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
             
        }
    }
}
