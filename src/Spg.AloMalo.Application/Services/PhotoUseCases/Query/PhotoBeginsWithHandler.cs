using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query {
    public class PhotoBeginsWithHandler {
        private IPhotoFilterBuilder _builder;

        public PhotoBeginsWithHandler(IPhotoFilterBuilder builder) {
            _builder = builder;
        }

        public IPhotoFilterBuilder WithQuery(string query) {
            string[] queryParts = query.Split(' ');

            if (queryParts[1] != "bw") {
                return _builder;
            }

            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Name", _builder.ApplyNameBeginsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);
            new EntityPropertyFilterMapper<IPhotoFilterBuilder, string>("Description", _builder.ApplyDescriptionBeginsWithFilter)
                .ExecuteDeligateIfValid(queryParts[0], queryParts[2]);

            return _builder;
        }
    }
}