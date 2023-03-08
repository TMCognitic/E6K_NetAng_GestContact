using E6K_NetAng_GestContact.Dal.Repositories;
using E6K_NetAng_GestContact.Dal.Services;
using E6K_NetAng_GestContact.Domain;
using E6K_NetAng_GestContact.Domain.Services;
using System.Data;
using System.Data.SqlClient;

namespace E6K_NetAng_GestContact.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;
            string connectionString = configuration.GetConnectionString("Default");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ContactDbContext>();

            builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));
            builder.Services.AddScoped<IContactRepository, EFContactService>();

            var app = builder.Build();

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
        }
    }
}