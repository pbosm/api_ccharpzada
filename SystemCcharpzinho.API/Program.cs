using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SystemCcharpzinho.API.Extensions;
using SystemCcharpzinho.API.Middleware;
using SystemCcharpzinho.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Get connection strings from environment variables
var postGreConnection = Environment.GetEnvironmentVariable("POSTGRE_CONNECTION");
var mySqlConnection   = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");

// Set connection strings in configuration
builder.Configuration["ConnectionStrings:PostGreConnection"]  = postGreConnection;
builder.Configuration["ConnectionStrings:MySQLConnection"]    = mySqlConnection;

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container. || MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), 
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddService();
builder.Services.AddRepository();

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost",
            ValidAudience = "https://localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY") ?? "development_key")),
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmailPolicy", policy =>
        policy.RequireAssertion(context =>
        {
            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
            return userEmail == "mockuser111@example.com";
        }));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSystemSwagger();

var app = builder.Build();

// Middleware CORS
app.UseCors("AllowAll");

// Error handling middleware
app.UseMiddleware<ErrorReponseMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SytemCcharpzinho V1");
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();