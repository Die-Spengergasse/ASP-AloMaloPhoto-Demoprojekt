using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Test
{
    public class PhotographerTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _databaseFixture;

        public PhotographerTests(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        [Fact()]
        public void Create_ShouldCreate_WhenParametersValid()
        {
            Photographer photographer = DatabaseUtilities.GetSeedingPhotographers()[0];

            // Act
            _databaseFixture.Db.Photographers.Add(photographer);
            _databaseFixture.Db.SaveChanges();

            // Assert
            Assert.Equal(1, _databaseFixture.Db.Photographers.Count());
        }
    }
}
