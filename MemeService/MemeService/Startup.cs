using MemeService.Configuration;
using MemeService.Context;
using MemeService.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeService
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
            // requires using Microsoft.Extensions.Options
            services.Configure<MemeDatabaseSettings>(
                Configuration.GetSection(nameof(MemeDatabaseSettings)));

            services.AddSingleton<IMemeDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MemeDatabaseSettings>>().Value);

            services.AddCors(o => o.AddPolicy("AllowCorsOriginFromEveryone", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(Configuration.GetSection("MemeDatabaseSettings").GetSection("ConnectionString").Value));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddDependecyInjectionConfiguration();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerConfiguration(env);
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowCorsOriginFromEveryone");

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
