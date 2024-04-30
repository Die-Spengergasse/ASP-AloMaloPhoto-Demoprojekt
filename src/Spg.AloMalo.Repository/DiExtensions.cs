using Microsoft.Extensions.DependencyInjection;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;

namespace Spg.AloMalo.Repository
{
    public static class DiExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWritablePhotoRepository, PhotoRepository>();
            services.AddScoped<IReadOnlyPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoFilterBuilder, PhotoFilterBuilder>(f =>
            {
                return new PhotoFilterBuilder(f.GetRequiredService<PhotoContext>().Photos);
            });
            services.AddScoped<IPhotoUpdateBuilder, PhotoUpdateBuilder>(f =>
            {
                return new PhotoUpdateBuilder(f.GetRequiredService<PhotoContext>());
            });
            services.AddScoped<IReadOnlyAlbumRepository, AlbumRepository>();
            services.AddScoped<IAlbumFilterBuilder, AlbumFilterBuilder>(f =>
            {
                return new AlbumFilterBuilder(f.GetRequiredService<PhotoContext>().Albums);
            });
            services.AddScoped<IPhotographerRepository, PhotographerRepository>();
        }
    }
}
