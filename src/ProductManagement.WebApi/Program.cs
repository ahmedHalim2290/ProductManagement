using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using Microsoft.OpenApi.Models;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Data;

using ProductManagement.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Profiles;
using Microsoft.AspNetCore.Hosting;
using ProductManagement.WebApi.MiddleWare;

namespace ProductManagement.WebApi {
    public class Program {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevClient",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            // Configure Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagement API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Enable Swagger UI in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement API v1");
                });
            }
            app.UseCors("AllowAngularDevClient");
            //app.UseMiddleware<ExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
