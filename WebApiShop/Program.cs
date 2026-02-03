using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddDbContext<MyShop_216128025Context>(options=>options.UseSqlServer ("Data Source=srv2\\pupils;Initial Catalog=MyShopYaffi2026;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
