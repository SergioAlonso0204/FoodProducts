using FoodProducts.Application.Interfaces;
using FoodProducts.Application.Services;
using FoodProducts.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 1. Load configuration from appsettings.json
IConfiguration config = builder.Configuration;

// 2. Register the Infrastructure layer (DbContext and repositories)
builder.Services.AddInfrastructure(config);

// 3. Register Application layer services
builder.Services.AddScoped<IFoodProductService, FoodProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// 4. Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Middleware: Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6. Middleware: HTTPS and Authorization (if any)
//app.UseHttpsRedirection();
app.UseAuthorization();

// 7. Map controllers
app.MapControllers();

// 8. Run the application
app.Run();
