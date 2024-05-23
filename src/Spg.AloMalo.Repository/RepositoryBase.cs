using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces.Findables;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Validators.RichTypes;
using Spg.AloMalo.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace Spg.AloMalo.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly PhotoContext _photoContext;

        public RepositoryBase(PhotoContext photoContext)
        {
            _photoContext = photoContext;
        }

        public TEntity? GetByPK<TKey, TProperty>(
            TKey pk,
            Expression<Func<TEntity, IEnumerable<TProperty>>>? includeCollection = null,
            Expression<Func<TEntity, TProperty>>? includeReference = null)
            where TProperty : class
        {
            TEntity? entity = _photoContext.Set<TEntity>().Find(pk);
            if (entity is not null)
            {
                if (includeCollection is not null)
                {
                    _photoContext.Entry(entity).Collection(includeCollection).Load();
                }
                if (includeReference is not null)
                {
                    _photoContext.Entry(entity).Reference(includeReference!).Load();
                }
            }
            return entity;
        }

        public TEntity? GetByPKAndIncudes<TKey, TProperty>(
            TKey pk,
            List<Expression<Func<TEntity, IEnumerable<TProperty>>>?>? includeCollection = null,
            Expression<Func<TEntity, TProperty>>? includeReference = null)
            where TProperty : class
        {
            TEntity? entity = _photoContext.Set<TEntity>().Find(pk);
            if (entity is not null)
            {
                if (includeCollection is not null)
                {
                    foreach (Expression<Func<TEntity, IEnumerable<TProperty>>>? item in includeCollection)
                    {
                        if (item is not null)
                        {
                            _photoContext.Entry(entity).Collection(item).Load();
                        }
                    }
                }
                if (includeReference is not null)
                {
                    _photoContext.Entry(entity).Reference(includeReference!).Load();
                }
            }
            return entity;
        }

        public T? GetByGuid<T>(Guid guid)
            where T : class, IFindableByGuid
        {
            return _photoContext
                .Set<T>()
                .SingleOrDefault(e => e.Guid == guid);
        }

        public T? GetByEMail<T>(string eMail)
            where T : class, IFindableByEMail
        {
            return _photoContext
                .Set<T>()
                .SingleOrDefault(e => e.EMail == eMail);
        }

        public void Create(TEntity newEntity)
        {
            _photoContext.Set<TEntity>().Add(newEntity);
            try
            {
                _photoContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw PhotoRepositoryException.FromCreate(ex);
            }
        }

        public void Delete<TId, RichType>(IRichType<TId> richType)
        {
            TEntity foundEntity = _photoContext.Set<TEntity>().Find(richType) ??
                throw PhotoRepositoryException.FromDelete();

            _photoContext.Set<TEntity>().Remove(foundEntity);
            try
            {
                _photoContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw PhotoRepositoryException.FromDelete(ex);
            }
        }
    }
}