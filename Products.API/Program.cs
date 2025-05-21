using Products.Application.Interfaces;
using Products.Application.Services;
using Products.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
IConfiguration config = builder.Configuration;

// Register Infrastructure layer (DbContext and repositories)
builder.Services.AddInfrastructure(config);

// Register Application layer services
builder.Services.AddScoped<IProductservice, Productservice>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow any origin (development use)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS (AllowAll)
app.UseCors("AllowAll");

// Enable Authorization
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Run the application
app.Run();
