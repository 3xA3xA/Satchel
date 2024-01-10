using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
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
        builder => builder.WithOrigins("http://localhost:4200") // URL Angular-����������
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("Satchel");

builder.Services.AddDbContext<SatchelDbContext>(options =>
    options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'Satchel' not found")));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

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
