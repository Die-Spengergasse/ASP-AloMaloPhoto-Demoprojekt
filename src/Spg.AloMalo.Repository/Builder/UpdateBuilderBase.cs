using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Builder
{
    public class UpdateBuilderBase<TEntity, TUpdateBuilder> : IUpdateBuilderBase<TEntity, TUpdateBuilder>
        where TEntity : class
        where TUpdateBuilder : class, IEntityUpdateBuilder<TEntity>
    {
        private TUpdateBuilder _updateBuilder;

        public UpdateBuilderBase(TUpdateBuilder updateBuilder)
        {
            _updateBuilder = updateBuilder;
        }

        public TUpdateBuilder WithEntity(TEntity entity)
        {
            _updateBuilder.Entity = entity;
            return _updateBuilder;
        }
    }
}