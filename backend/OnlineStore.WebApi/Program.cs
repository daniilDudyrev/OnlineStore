using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repositories;
using OnlineStore.Domain.RepositoryInterfaces;
using OnlineStore.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

const string dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddCors();
var app = builder.Build();

app.UseCors(policy =>
{
    policy
        .WithOrigins("http://localhost:7079","http://localhost:5050")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();