using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.Application.Services.Helpers;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("SqLite");

// DB Context registrieren:
builder.Services.AddDbContext<PhotoContext>(o => o.UseSqlite(connectionString));

builder.Services.AddScoped<IWritablePhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IReadOnlyPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IPhotoFilterBuilder, PhotoFilterBuilder>(f =>
{
    return new PhotoFilterBuilder(f.GetRequiredService<PhotoContext>().Photos);
});
builder.Services.AddScoped<IPhotoUpdateBuilder, PhotoUpdateBuilder>(f =>
{
    return new PhotoUpdateBuilder(f.GetRequiredService<PhotoContext>());
});
builder.Services.AddScoped<IReadOnlyAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IAlbumFilterBuilder, AlbumFilterBuilder>(f =>
{
    return new AlbumFilterBuilder(f.GetRequiredService<PhotoContext>().Albums);
});
builder.Services.AddScoped<IPhotographerRepository, PhotographerRepository>();

builder.Services.AddScoped<IDateTimeService, DateTimeService>();

builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<IPhotoService>(s => new PhotoServiceWrapper(s.GetRequiredService<PhotoService>()));

builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<IAlbumService>(s => new AlbumServiceWrapper(s.GetRequiredService<AlbumService>()));

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
