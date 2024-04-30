using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Spg.AloMalo.Api.AuthPolicies;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.Application.Services.Helpers;
using Spg.AloMalo.Application.Services.PhotoUseCases.Command;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// Configure DB Context
string? connectionString = builder.Configuration.GetConnectionString("SqLite");
builder.Services.AddDbContext<PhotoContext>(o => o.UseSqlite(connectionString));

// Configure Services and Repositories
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
//
// Configure CorsPolicy
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.WithHeaders("ACCESS-CONTROL-ALLOW-ORIGIN", "CONTENT-TYPE", "other-info", "Authentication");
    });
});
//
// Configure Authentication/Authorisation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
//
// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-8.0
// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0
// Role-Based:
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => 
    {
        policy.RequireRole("admin");
    });
});
//
// Policy-Based:
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("NoHomers", p =>
    {
        p.Requirements.Add(new ForbiddenFirstNameRequirement("homer"));
    });
});
//
// Configure Policy-Handler
builder.Services.AddSingleton<IAuthorizationHandler, ForbiddenFirstNameHandler>();
builder.Services.AddAuthorization();
//
// Configure MediatR
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddTransient<IRequestHandler<CreatePhotoCommandModel, CreatePhotoReponseDto>, CreatePhotoCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetPhotosQueryModel, List<PhotoDto>>, GetPhotosQueryHandler>();
//
// Build App
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

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public partial class Program
{ }