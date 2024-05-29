using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query {
    public class ContainsFilter<T> where T : class {
        private IEntityFilterBuilder<T> _builder;

        public ContainsFilter(IEntityFilterBuilder<T> builder) {
            _builder = builder;
        }
    }
}
