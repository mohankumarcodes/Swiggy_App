using Microsoft.EntityFrameworkCore;
using Swiggy_App.Data;
using Swiggy_App.Services;

var builder = WebApplication.CreateBuilder(args);

// Read appsettings.json in WebHook URL
var configuration = builder.Configuration;

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbCOntext Services here
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the EmailService for Dependency Injection
builder.Services.AddScoped<EmailServices>(); // Use AddTransient/AddSingleton based on your needs


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
