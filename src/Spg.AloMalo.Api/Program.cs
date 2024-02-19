using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

var app = builder.Build();



// ** DB Seeding Hard Coded (Bad Code) **
//DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
//dbContextOptionsBuilder.UseSqlite("Data Source = C:\\HTL\\Unterricht\\SJ2324\\4BHIF\\POS\\sj23-24-4bhif-pos-schrutek\\Spg.AloMalo\\src\\Spg.AloMalo.Api\\Photo.db");
//PhotoContext _db = new PhotoContext(dbContextOptionsBuilder.Options);
//_db = new PhotoContext(dbContextOptionsBuilder.Options);
//_db.Database.EnsureDeleted();
//_db.Database.EnsureCreated();
//SeedHelper.Seed(_db);
// ** DB Seeding Hard Coded **



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
