using kontorExpert.BusinessLogic;
using kontorExpert.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryAccess, CategoryAccess>();
builder.Services.AddScoped<CategoryLogic>();
builder.Services.AddScoped<IProductAccess, ProductAccess>();
builder.Services.AddScoped<ProductLogic>();
builder.Services.AddScoped<IProductImageAccess, ProductImageAccess>();
builder.Services.AddScoped<ProductImageLogic>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
