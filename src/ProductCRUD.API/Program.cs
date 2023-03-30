using Microsoft.EntityFrameworkCore;
using ProductCRUD.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblyName = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductDbContext>(c => c.UseNpgsql(connectionString, m => m.MigrationsAssembly(assemblyName)));
var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
if (context.Database.GetPendingMigrations().Any())
{
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();