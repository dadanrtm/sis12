using Microsoft.EntityFrameworkCore;
using ApiSIS.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. TAMBAHKAN INI - Konfigurasi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            // Izinkan semua origin untuk development
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();

            // Atau spesifik ke frontend Anda
            // policy.WithOrigins("http://localhost:9000", "http://localhost:8080")
            //       .AllowAnyMethod()
            //       .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. TAMBAHKAN INI - Gunakan CORS (SEBELUM UseAuthorization)
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();