using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Test.Helpers
{
    public class UnitTestContext : PhotoContext
    {
        public DbSet<AlbumPhoto> AlbumPhotos => Set<AlbumPhoto>();

        public UnitTestContext(DbContextOptions options)
            : base(options)
        { }
    }
}
