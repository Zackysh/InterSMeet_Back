using InterSMeet.BLL.Contracts;
using InterSMeet.BLL.Implementations;
using InterSMeet.Core.MapperProfiles;
using InterSMeet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dependency injection
builder.Services.AddScoped<IUserBL, UserB>();
builder.Services.AddScoped<IConfiguration>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSetOrigins", options =>
    {
        options.WithOrigins("http://localhost:8080");
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure DbContext
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<InterSMeetDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(builder.Configuration["ConnectionStrings:InterSMeetDb"], serverVersion)
                // Disable on prod
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                );

app.UseCors("AllowSetOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
