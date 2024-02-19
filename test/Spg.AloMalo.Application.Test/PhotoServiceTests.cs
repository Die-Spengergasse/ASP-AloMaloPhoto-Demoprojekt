using Spg.AloMalo.Application.Mock;
using Spg.AloMalo.Application.Services;
using Spg.AloMalo.Application.Test.Helpers;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.Repository;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Test
{
    public class PhotoServiceTests
    {
        public void ShouldNOTCreate_WhenCreationDateInPast()
        {
            //var db = DatabaseUtilities.CreateDb();
            //// Arrange
            //CreatePhotoCommand command = new CreatePhotoCommand(
            //    "",
            //    "",
            //    DomainModel.Model.ImageTypes.Unknown,
            //    new DomainModel.Model.Location(123, 123), 123, 123,
            //    DomainModel.Model.Orientations.Portrait,
            //    false, new DateTime(2020, 05, 16));

            //var ut = new PhotoService(
            //    null, 
            //    new PhotoRepository(db, 
            //        new PhotoUpdateBuilder(db)), 
            //            new DateTimeServiceMock());

            //// Act

            //// Assert
        }
    }
}
