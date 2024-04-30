using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Repository.Extensions
{
    public static class PhotoPagingExtensions
    {
        public static IPhotoFilterBuilder ApplyPaging(this IPhotoFilterBuilder builder, int page, int size)
        {
            builder.EntityList = builder.EntityList
                .Skip(page * size)
                .Take(size);
            return builder;
        }
    }
}
