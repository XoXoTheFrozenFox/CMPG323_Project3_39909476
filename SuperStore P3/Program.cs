using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;
using EcoPower_Logistics.Repository;
using Models;

// Create a new WebApplication instance
var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from the configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Entity Framework DbContext for your application's data context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add developer exception page for easier debugging in development mode
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add DbContext for SuperStoreContext (if needed)
builder.Services.AddDbContext<SuperStoreContext>();

// Configure Identity with default user (IdentityUser) and specify options
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC controllers and views
builder.Services.AddControllersWithViews();

// Register the generic repository with a factory method
builder.Services.AddScoped(typeof(IGenericRepository<,>), serviceProvider =>
{
    var dbContext = serviceProvider.GetRequiredService<SuperStoreContext>();
    return new GenericRepository<Customer, int>(dbContext); // Change Customer and int to the desired entity and key types
});
// Register your repository interfaces with their implementations
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable the developer exception page for a better debugging experience in development mode
    app.UseMigrationsEndPoint();
}
else
{
    // Use a custom error page for non-development environments
    app.UseExceptionHandler("/Home/Error");
    // You can enable HSTS (HTTP Strict Transport Security) if needed
}

// Enable serving static files (e.g., CSS, JavaScript)
app.UseStaticFiles();

// Set up routing for MVC
app.UseRouting();

// Configure authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Define the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable Razor Pages (if needed)
app.MapRazorPages();

// Start the application
app.Run();