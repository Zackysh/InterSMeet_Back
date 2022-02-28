using InterSMeet.API;
using InterSMeet.BLL.Contracts;
using InterSMeet.BLL.Implementations;
using InterSMeet.Core.Email;
using InterSMeet.Core.MapperProfiles;
using InterSMeet.Core.Security;
using InterSMeet.DAL.Entities;
using InterSMeet.DAL.Repositories.Contracts;
using InterSMeet.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));
// Dependency injection
// User (oder matters)
builder.Services.AddScoped<IAuthBL, AuthBL>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
// Student
builder.Services.AddScoped<IStudentBL, StudentBL>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// Company
builder.Services.AddScoped<ICompanyBL, CompanyBL>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
// Offer
builder.Services.AddScoped<IOfferBL, OfferBL>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
// Exception handling
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Configure authentication

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:AccessSecret").Value);

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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

var app = builder.Build();
//app.Urls.Add("http://0.0.0.0:5000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSetOrigins");

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
