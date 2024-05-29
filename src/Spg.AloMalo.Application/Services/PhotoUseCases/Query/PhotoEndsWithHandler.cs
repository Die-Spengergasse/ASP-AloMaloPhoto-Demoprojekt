using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query {
    public class PhotoEndsWithHandler {
        private IPhotoFilterBuilder _builder;

        public PhotoEndsWithHandler(IPhotoFilterBuilder builder) {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query) {
            string[] queryParts = query.Split(' ');

            if (queryParts[1] != "ew") {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameEndsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionEndsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}