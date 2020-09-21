using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectA.Models;
using ProjectA.Actions;
using ProjectA.Actions.Abstraction;

namespace ProjectA
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings
                .ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EfCoreContext>(
                options => options.UseSqlServer(connection)) ;

            
            services.AddScoped<ICountryLogic, CountryLogic>();
            services.AddScoped<INationalCompetitionLogic, NationalCompetitionLogic>();
            services.AddScoped<IGLobalCompetitionLogic, GlobalCompetitionLogic>();
            services.AddScoped<IPlayerLogic, PlayerLogic>();
            services.AddScoped<ITeamLogic, TeamLogic>();
            services.AddScoped<IRegionLogic, RegionLogic>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectA", Version = "v1" });
            });

        }
               
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<EfCoreContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectA API V1");
            });
        }
    }
}
