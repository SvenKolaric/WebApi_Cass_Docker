using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CS_WebApi_Cass_Docker
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

            var secretKey = Environment.GetEnvironmentVariable("JWT_KEY"); //IIS CSWebApiPool -> loadUserProfile = TRUE

            //Register JWT authentication schema,
            //For the JWT to be valid:
            //1.Validate that the server created that token (ValidateIssuer = true)
            //2.Recipient is authorized to receive it (ValidateAudience = true)
            //3.Check expiration date and the signing key of the issuer (ValidateLifetime = true)
            //4.Verify signing key is part of a list of trusted keys (ValidateIssuerSigningKey = true)

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; //sets HTTPS req, only set false during development
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost/CSApi", //dodaj više ovih i za local host http://localhost:2684/
                    ValidAudience = "http://localhost/CSApi",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("Role", "admin"));

                options.AddPolicy("User",
                    policy => policy.RequireClaim("Role", "user"));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
