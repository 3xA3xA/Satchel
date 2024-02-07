using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Interfaces;
using SatchelAPI.Interfaces.ServicesInterfaces;
using SatchelAPI.Profiles;
using SatchelAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // URL Angular
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("Satchel");

builder.Services.AddDbContext<SatchelDbContext>(options =>
    options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'Satchel' not found")));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFavouritesService, FavouritesService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISizeTypeService, SizeTypeService>();
builder.Services.AddScoped<IGenderTypeService, GenderTypeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowMyOrigin"); // Use CORS policy

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
