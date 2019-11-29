using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserList.Data;
using UserList.Helpers.Filters;
using UserList.Helpers.Middlewares;
using UserList.Inf.Interfaces.Unit;
using UserList.Inf.Interfaces.User;
using UserList.Inf.Repos.Unit;
using UserList.Inf.Repos.User;

namespace UserList
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

            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddRouting();
            services.AddMvc().AddMvcOptions(opts => opts.Filters.Add(new RequestValidationFilterAttribute())).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IUser, UserRepo>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("error");

            loggerFactory.AddFile("Logs/log-{Date}.txt");

            app.UseErrorLogging();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
