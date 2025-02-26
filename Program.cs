using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureApiWithJwt.Configurations;
using SecureApiWithJwt.Models;
using SecureApiWithJwt.Repositories;
using SecureApiWithJwt.Services;
using SecureApiWithJwt.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Dang ky cau hinh tu appsettings.json
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Load file .env
Env.Load();

// Cau hinh JWT
builder.Services.ConfigureJwtAuthentication(builder.Configuration);

//Lay gia tri cua bien DATABASE_URL trong file .env
var connectionString = Env.GetString("DATABASE_URL");

// Add services to the container.

// Add service and repositoty user
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add service and repositoty role
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Add service and repositoty AllowAccess
builder.Services.AddScoped<AllowAccessRepository>();
builder.Services.AddScoped<IAllowAccessService, AllowAccessService>();

// Add service and repositoty Intern
builder.Services.AddScoped<InternRepository>();
builder.Services.AddScoped<IInternService, InternService>();

// Add service and repositoty jwt
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context => CustomValidationResponse.GenerateResponse(context);
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
