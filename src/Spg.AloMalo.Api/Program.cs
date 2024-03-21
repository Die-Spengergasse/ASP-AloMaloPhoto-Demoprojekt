using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.Application.Services.Helpers;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowSpecificOrigins", policy => {
        policy.WithOrigins("http://localhost:4200");
        policy.WithHeaders("ACCESS-CONTROL-ALLOW-ORIGIN", "CONTENT-TYPE", "other-info");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowSpecificOrigins");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
