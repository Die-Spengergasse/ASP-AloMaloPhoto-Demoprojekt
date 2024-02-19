using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Extensions
{
    public static class UpdatePhotoExtensions
    {
        public static (PhotoContext, Photo) CreatePhoto(this PhotoContext context) // + alle Photo-Parameter
        {
            Photo photo = null; // new Photo(..alle Photo-Parameter..)
            return (context, photo);
        }
        public static (PhotoContext, Photo) UpdatePhoto(this PhotoContext context, Photo photo)
        {
            return (context, photo);
        }

        public static (PhotoContext, Photo) WithName(this (PhotoContext, Photo) input, string name)
        {
            // Original
            (PhotoContext context, Photo photo) = input;
            photo.Name = name;

            return (context, photo);
        }
        public static (PhotoContext, Photo) WithDescription(this (PhotoContext, Photo) input, string description)
        {
            // Vereinfacht:
            (_, Photo photo) = input;
            photo.Description = description;

            return input;
        }
        public static (PhotoContext, Photo) WithOrienatation(this (PhotoContext, Photo) input, Orientations orientation)
        {
            // Vereinfacht:
            (_, Photo photo) = input;
            photo.Orientation = orientation;

            return input;
        }

        public static PhotoContext Save(this (PhotoContext, Photo) input)
        {
            (PhotoContext context, Photo photo) = input;
            context.Photos.Update(photo);

            return context;
        }
    }
}
