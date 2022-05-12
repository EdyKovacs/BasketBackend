using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using MediatR;
using AutoMapper;
using Data;
using basketbackend.Service.BasketTotalCalculator;
using basketbackend.Presentation.Filters;
using basketbackend.Service.BasketService;
using basketbackend.Data;
using basketbackend.Data.UnitOfWork;
using basketbackend.Presentation.MappingProfiles;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace basketbackend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONN_STRING")));
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IBasketTotalNetCalculator, BasketTotalNetCalculator>();
            services.AddSingleton<IBasketTotalGrossCalculator, BasketTotalGrossCalculator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BasketProfile(provider.GetService<IBasketTotalNetCalculator>(), provider.GetService<IBasketTotalGrossCalculator>()));
            }).CreateMapper());

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSwaggerGen();
            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
