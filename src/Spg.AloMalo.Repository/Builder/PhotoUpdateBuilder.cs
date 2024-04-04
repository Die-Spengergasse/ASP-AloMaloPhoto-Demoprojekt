using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Builder
{
    /// <summary>
    /// https://betterprogramming.pub/the-power-of-extension-methods-b3b1962475c4
    /// </summary>
    public class PhotoUpdateBuilder : IPhotoUpdateBuilder
    {
        private readonly PhotoContext _db;

        public Photo Entity { get; set; } = default!;

        public PhotoUpdateBuilder(PhotoContext db)
        {
            _db = db;
        }

        public IPhotoUpdateBuilder WithName(string name)
        {
            Entity.Name = name;
            return this;
        }

        public int Save()
        {
            _db.Update(Entity);
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw PhotoRepositoryException.FromUpdate(ex);
            }
        }
    }
}
