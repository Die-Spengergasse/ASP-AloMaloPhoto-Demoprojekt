using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Repository.Extensions
{
    public static class PhotoOrderExtensions
    {
        public static IPhotoFilterBuilder ApplyOrderByNameAsc(this IPhotoFilterBuilder builder)
        {
            builder.EntityList = builder.EntityList.OrderBy(e => e.Name);
            return builder;
        }
        public static IPhotoFilterBuilder ApplyOrderByNameDesc(this IPhotoFilterBuilder builder)
        {
            builder.EntityList = builder.EntityList.OrderByDescending(e => e.Name);
            return builder;
        }
    }
}
