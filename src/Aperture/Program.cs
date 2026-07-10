using Aperture.Configuration;
using Aperture.Data;

var builder = WebApplication.CreateBuilder(args);

var config = builder.AddAutoBoundConfiguration();

// Add services to the container.
builder.Services.AddControllersWithViews().WithContext();

builder.Services.AddApplicationServices();
builder.Services.AddDataContext<ApertureDb>(config.DbSettings);
builder.Services.AddApplicationAuthentication(config.AuthSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/error/500");
app.UseStatusCodePagesWithReExecute("/error/{0}");

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
