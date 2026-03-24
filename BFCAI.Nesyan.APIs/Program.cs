using BFCAI.Nesyan;
using BFCAI.Nesyan.APIs.Extensions;
using BFCAI.Nesyan.Application;
using BFCAI.Nesyan.Infrastructure.Presistence;
using Microsoft.AspNetCore.Mvc;

namespace BFCAI.Nesyan.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPresistenceService(builder.Configuration);
            builder.Services.AddApplicationService();
            #endregion

            var app = builder.Build();

            #region Database Initialization
            await app.InitializerStoreContextAsync();
            #endregion  

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
