using Spg.AloMalo.Application.Mock;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.MockController
{
    public class AlbumControllerMock
    {
        private readonly IAlbumService _albumService;

        public AlbumControllerMock(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public object GetAlbums()
        {
            var result = _albumService
                .GetAll404()
                .ResultOrExceptions<IQueryable<Album>, AlbumSerivceException, ArgumentException>(
                    r => r, 
                    e => $"{e.GetType().Name} - {e.Message}", 
                    e => $"{e.GetType().Name} - {e.Message}");

            return result;
        }
    }
}
