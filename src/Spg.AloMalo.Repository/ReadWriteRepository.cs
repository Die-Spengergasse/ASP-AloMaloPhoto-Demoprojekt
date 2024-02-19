using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces.Findables;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository
{
    public class ReadWriteRepository<TEntity, TReadBilder, TUpdateBuilder> 
        : ReadOnlyRepository<TEntity, TReadBilder>, IReadWriteRepository<TEntity, TUpdateBuilder>
        where TEntity : class
        where TReadBilder : class, IEntityReaderBuilder<TEntity>
        where TUpdateBuilder : class, IEntityUpdateBuilder<TEntity>
    {
        public IUpdateBuilderBase<TEntity, TUpdateBuilder> UpdateBuilder { get; }

        public ReadWriteRepository(
            PhotoContext photoContext,
            TReadBilder readBuilder,
            TUpdateBuilder updateBuilder)
                : base(photoContext, readBuilder)
        {
            UpdateBuilder = new UpdateBuilderBase<TEntity, TUpdateBuilder>(updateBuilder);
        }
    }
}
