using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VendorService.Client.AsyncDataServices;
using VendorService.Client.HttpClient;
using VendorService.Data;
using VendorService.Data.Common;
using VendorService.Data.Common.Repositories;
using VendorService.Data.Repositories;
using VendorService.Data.Seeding;

using VendorService.Services.Data.Vendor;
using VendorService.Services.DTOModels.VendorModels;
using VendorService.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseInMemoryDatabase(databaseName: "VendorsDb");
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
builder.Services.AddTransient<IVendorService, VendoreService>();

//Add HTTP Client
builder.Services.AddHttpClient<IProductDataClient, HttpProductDataClient>();

//Add MessageBus Client
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

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
