using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using BusMeal.API.Helpers;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using BusMeal.API.Core.Models;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Persistence.Repository;
using BusMeal.API.Persistence;
using FluentValidation.AspNetCore;
using FluentValidation;
using BusMeal.API.Core.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BusMeal.API
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
      services.AddMvc(options =>
      {
        // var policy = new AuthorizationPolicyBuilder()
        //               .RequireAuthenticatedUser()
        //               .Build();
        // options.Filters.Add(new AuthorizeFilter(policy));

      })
      .AddJsonOptions(opt =>
      {
        opt.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      })

      .AddFluentValidation(
        fv => fv.RegisterValidatorsFromAssemblyContaining<DepartmentValidation>()
      )
      .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


      services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

      services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
      services.AddScoped<IDepartmentRepository, DepartmentRepository>();
      services.AddScoped<IEmployeeRepository, EmployeeRepository>();
      services.AddScoped<IModuleRightsRepository, ModuleRightsRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IUserDepartmentRepository, UserDepartmentRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserModuleRightsRepository, UserModuleRightsRepository>();
      services.AddScoped<IDormitoryBlockRepository, DormitoryBlockRepository>();
      services.AddScoped<IBusTimeRepository, BusTimeRepository>();
      services.AddScoped<IMealtypeRepository, MealTypeRepository>();
      services.AddScoped<IMealVendorRepository, MealVendorRepository>();
      services.AddScoped<ICounterRepository, CounterRepository>();
      services.AddScoped<IAuditRepository, AuditRepository>();
      services.AddScoped<IMealOrderRepository, MealOrderRepository>();
      services.AddScoped<IMealOrderVerificationRepository, MealOrderVerificationRepository>();
      services.AddScoped<IBusOrderRepository, BusOrderRepository>();
      services.AddScoped<IBusOrderVerificationRepository, BusOrderVerificationRepository>();

      services.AddAutoMapper(typeof(Startup));
      services.AddCors();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                          .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
              ValidateIssuer = false,
              ValidateAudience = false
            };
          });

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusMeal API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(builder =>
        {
          builder.Run(async context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
              context.Response.AddApplicationError(error.Error.Message);
              await context.Response.WriteAsync(error.Error.Message);
            }
          });
        });
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        // app.UseHsts();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BusMeal API end point v1");
        c.RoutePrefix = string.Empty;
      });

      //app.UseHttpsRedirection();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      // Use JWT authentication
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
