﻿using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);
        IPhotoFilterBuilder ApplyNameContainsFilter(string filter);
        IPhotoFilterBuilder ApplyNameBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyNameEndsWithFilter(string filter);
        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplyAiFilter(bool @is);
        IPhotoFilterBuilder ApplyPaging(int page, int size);
        IPhotoFilterBuilder ApplyEqualsFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyContainsFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyStartsWithFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyEndsWithFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyContainsDigitsFilter(string propertyName);
        IPhotoFilterBuilder ApplyGreaterThanFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyGreaterThanOrEqualFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyLessThanFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyLessThanOrEqualFilter(string propertyName, string value);
        IPhotoFilterBuilder ApplyContainsSpecialCharsFilter(string propertyName);
    }
}
