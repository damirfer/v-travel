using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Service;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using Model;
using AutoMapper;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                         .SetBasePath(environment.ContentRootPath)
                         .AddJsonFile("appsettings.json")
                         .AddJsonFile($"appsettings.{environment.EnvironmentName.ToLower()}.json", true)
                         .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbGen = Configuration.GetConnectionString("ProdDB");

            services.AddDbContext<LastaContext>(opt => opt.UseSqlServer(dbGen));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAccommodationService, AccommodationService>();
            services.AddScoped<IAccommodationTypeService, AccommodationTypeService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IGuideService, GuideService>();
            services.AddScoped<ITravelerService, TravelerService>();
            services.AddScoped<IPlaceTypeService, PlaceTypeService>();
            services.AddScoped<ITransportTypeService, TransportTypeService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IHelperService, HelperService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "dev",
                            ValidAudience = "dev",
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                        };
                    });



            services.AddCors(options =>
            {
            options.AddPolicy("AllowSpecificOrigin", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Sighter Travel API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
             app.UseDeveloperExceptionPage();
            
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigin");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Default}/{action=Index}/{id?}");

            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
} 
