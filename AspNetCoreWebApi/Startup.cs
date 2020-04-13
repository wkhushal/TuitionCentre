using System;
using AspNetCoreWebApi.Core.Interfaces;
using AspNetCoreWebApi.DBContexts;
using AspNetCoreWebApi.Models;
using AspNetCoreWebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace AspNetCoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            });

            services.AddDbContext<TuitionAgencyContext>(opt =>
            {
                //opt.UseInMemoryDatabase("TuitionAgencyDB");
                opt.UseSqlServer(Configuration.GetConnectionString("TuitionAgencyDB"));
            });
            
            AddRepositoryServices(services);
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services
                .AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            if (env.IsDevelopment())
            {
                EnsureDatabaseExists(app);
            }
        }

        private void AddRepositoryServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<Course>), typeof(CourseRepository));
            services.AddTransient(typeof(IRepository<TuitionAgency>), typeof(TuitionAgencyRepository));
        }

        private void EnsureDatabaseExists(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<TuitionAgencyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
