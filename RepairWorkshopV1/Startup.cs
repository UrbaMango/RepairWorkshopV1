using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepairWorkshopV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RepairWorkshopV1.Interfaces;
using RepairWorkshopV1.Services;
using RepairWorkshopV1.Helpers;
using RepairWorkshopV1.Requirements;
using RepairWorkshopV1.Handlers;
using Microsoft.AspNetCore.Authorization;


namespace RepairWorkshopV1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowedDomains = "AllowedDomains";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connection = Configuration.GetConnectionString("RepairWorkshop");
            services.AddDbContext<RepairWorkshopContext>(options => options.UseSqlServer(connection));
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = TokenHelper.Issuer,
                        ValidAudience = TokenHelper.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))
                    };
                });

            services.AddAuthorization(options =>
               {
                   options.AddPolicy("OnlyNonBlockedEmployee", policy =>
                   {
                       policy.Requirements.Add(new EmployeeStatusRequirement(false));
                   });
               });

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedDomains,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                  });
            });

            services.AddSingleton<IAuthorizationHandler, EmployeeBlockedStatusHandler>();

            services.AddScoped<ILoginService, LoginService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowedDomains);

            app.UseAuthentication();

            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
