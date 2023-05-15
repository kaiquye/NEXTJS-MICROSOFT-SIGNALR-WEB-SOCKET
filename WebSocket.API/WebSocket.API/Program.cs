using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebSocket.Api;
using WebSocket.Api.Middlewares;
using WebSocket.Application.Services;
using WebSocket.Application.utility;
using WebSocket.Domain.Interfaces;
using WebSocket.Domain.Interfaces.repositories;
using WebSocket.Infra.Repositories;
using WebSocket.Infrastructure.Context;
ConfigEnv.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<DbContextPg>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));
// Add services to the container.
builder.Services.AddTransient<IPersonServices, PersonServices>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ICredentialsRepository, CredentialsRepository>();
builder.Services.AddTransient<UnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPasswordHash, PasswordHash>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

byte[] secretBytes = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret_key"));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = "Bearer";
    x.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // https 
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo() { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSignalR();
var app = builder.Build();
app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(); 

app.UseAuthorization();

app.MapControllers();

app.MapHub<PatientHub>("/patient");

app.Run();