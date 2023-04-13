using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Data.Common;
using ProductService.Data.Common.Repositories;
using ProductService.Data.Repositories;
using ProductService.Data.Seeding;
using ProductService.Services.Data.Product;
using ProductService.Services.Data.Vendor;
using ProductService.Services.DTOModels.VendorModels;
using ProductService.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseInMemoryDatabase(databaseName: "ProductsDb");
    });
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("VendorsConn"));
    });
}

// Data repositories
builder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDbQueryRunner, DbQueryRunner>();

// Application services
builder.Services.AddTransient<IProductService, ProductService.Services.Data.Product.ProductService>();
builder.Services.AddTransient<IVendorService, VendorService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

var app = builder.Build();

// Seed data on application startup
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (app.Environment.IsProduction())
    {
        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not run migrations: {e.Message}, {e.InnerException?.Message}");
        }

    }
    
    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
}

AutoMapperConfig.RegisterMappings(
                typeof(VendorCreateModel).GetTypeInfo().Assembly);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
