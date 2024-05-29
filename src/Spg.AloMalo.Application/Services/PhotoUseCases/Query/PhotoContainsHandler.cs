using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query {
    public class PhotoContainsHandler {

        private IPhotoFilterBuilder _builder;

        public PhotoContainsHandler(IPhotoFilterBuilder builder) {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query) {
            string[] queryParts = query.Split(' ');

            if (queryParts[1] != "ct") {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameContainsFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionContainsFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}
