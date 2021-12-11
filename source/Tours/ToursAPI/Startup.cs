using System;
using System.IO;
using System.Reflection;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

using ToursWeb.ModelsDB;
using ToursWeb.Controllers;
using ToursWeb.Repositories;
using ToursWeb.ImpRepositories;

namespace ToursAPI
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
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter{});
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ToursAPI", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                
            });
            services.AddSwaggerGenNewtonsoftSupport();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            AddDbContext(services, config);
            AddLogging(services, config);
            
            AddRepositories(services);
            AddControllers(services);
            
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToursAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ToursContext>(option => option.UseNpgsql(config["Connections:Current"]));
        }
        
        private static void AddLogging(IServiceCollection services, IConfiguration config)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File(config["Logger"])
                .CreateLogger();
            
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(logger: log, dispose: true));
        }
        
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddScoped<TourController>();
            services.AddScoped<HotelRepository>();
            services.AddScoped<FoodController>();
            services.AddScoped<HotelController>();
            services.AddScoped<UserController>();
            services.AddScoped<TransferController>();
        }
    }
}