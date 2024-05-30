using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Repository.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class MockPhoto : IReadOnlyPhotoRepository
    {
        public static readonly EMail email = new EMail("jfk.isnotdead@really.com");
        public static readonly Photographer photographer = new(
            Guid.NewGuid(),
            "Hans",
            "Hens",
            new Address("Handstrasse", "1212", "Niemans", "Land"),
            new PhoneNumber(21, 52, "69F"),
            new PhoneNumber(21, 52, "69F"),
            [email],
            email
        );
        public static readonly Photo photo1 = new(
            Guid.NewGuid(),
            "Park",
            "Ein schöner Park",
            DateTime.Now,
            ImageTypes.Png,
            new Location(20.245, 40.111),
            1920,
            720,
            false,
            photographer
        );
        public static readonly Photo photo2 = new(
            Guid.NewGuid(),
            "Stadt",
            "Keine schöne Stadt",
            DateTime.Now,
            ImageTypes.Jpg,
            new Location(20.245, 40.111),
            1920,
            720,
            true,
            photographer
        );
        public List<Photo> photos = new([
            photo1,
            photo2
        ]);

        public IPhotoFilterBuilder FilterBuilder => new PhotoFilterBuilder(photos.AsQueryable());

        public Photo? GetByPK<TKey, TProperty>(TKey pk, Expression<Func<Photo, IEnumerable<TProperty>>>? includeCollection = null, System.Linq.Expressions.Expression<Func<Photo, TProperty>>? includeReference = null) where TProperty : class
        {
            throw new NotImplementedException();
        }

        public Photo? GetByPKAndIncudes<TKey, TProperty>(TKey pk, List<Expression<Func<Photo, IEnumerable<TProperty>>>?>? includeCollection = null, System.Linq.Expressions.Expression<Func<Photo, TProperty>>? includeReference = null) where TProperty : class
        {
            throw new NotImplementedException();
        }

        T? IRepositoryBase<Photo>.GetByEMail<T>(string eMail) where T : class
        {
            throw new NotImplementedException();
        }

        T? IRepositoryBase<Photo>.GetByGuid<T>(Guid guid) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
