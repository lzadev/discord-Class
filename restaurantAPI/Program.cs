using Microsoft.EntityFrameworkCore;
using RestaurantAPI;
using RestaurantAPI.Repositories.CategoryRepo;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

services.AddTransient<ICategoryRepository, CategoryRepository>()
        .AddTransient<ICategoryService, CategoryService>()
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
