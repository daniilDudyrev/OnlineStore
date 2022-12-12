using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repositories;
using OnlineStore.Domain.RepositoryInterfaces;
using OnlineStore.Domain.Services;
using OnlineStore.WebApi.Middlewares;
using OnlineStore.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestHeaders
                            | HttpLoggingFields.ResponseHeaders
                            | HttpLoggingFields.RequestBody
                            | HttpLoggingFields.ResponseBody;
});

const string dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddCors();
var app = builder.Build();

// app.Use(async (context, next) =>
// {
//     var userAgent = context.Request.Headers.UserAgent.ToString();
//     if (userAgent.Contains("Edg"))
//     {
//         await next();
//     }
//     else
//     {
//         await context.Response.WriteAsJsonAsync(new { Message = "Only Edge is supported." });
//     }
// });
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseCors(policy =>
{
    policy
        .WithOrigins("http://localhost:7079", "http://localhost:5050")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();