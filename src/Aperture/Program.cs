using Aperture.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = builder.AddAutoBoundConfiguration();

// Add services to the container.
builder.Services.AddControllersWithViews().WithContext();

builder.Services.AddApplicationServices();
builder.Services.AddAuth0Authentication(config.AuthSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
