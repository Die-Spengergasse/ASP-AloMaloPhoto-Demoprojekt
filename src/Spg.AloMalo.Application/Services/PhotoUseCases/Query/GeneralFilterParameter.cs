//using Spg.AloMalo.DomainModel.Interfaces;
//using Spg.AloMalo.DomainModel.Interfaces.Repositories;
//using Spg.AloMalo.DomainModel.Model;
//using System;
//using System.Collections.Generic;

//namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
//{
//    public class GeneralFilterParameter : IQueryParameter
//    {
//        public IPhotoFilterBuilder Compile(IPhotoFilterBuilder builder, string queryParameter)
//        {
//            var filters = ParseFilters(queryParameter);

//            foreach (var filter in filters)
//            {
//                builder = builder.ApplyFilter(filter);
//            }

//            return builder;
//        }

//        private IEnumerable<IFilter<Photo>> ParseFilters(string filterQuery)
//        {
//            var filters = new List<IFilter<Photo>>();

//            if (string.IsNullOrWhiteSpace(filterQuery))
//                return filters;

//            var parts = filterQuery.Split(' ');

//            for (int i = 0; i < parts.Length; i += 3)
//            {
//                if (i + 2 >= parts.Length)
//                {
//                    continue;
//                }

//                var property = parts[i]?.Trim();
//                var operation = parts[i + 1]?.Trim().ToLower();
//                var value = parts[i + 2]?.Trim();

//                if (string.IsNullOrEmpty(property) || string.IsNullOrEmpty(operation) || string.IsNullOrEmpty(value))
//                {
//                    continue;
//                }

//                switch (operation)
//                {
//                    case "eq":
//                        filters.Add(new EqualsFilter<Photo>(property, value));
//                        break;
//                    case "ct":
//                        filters.Add(new ContainsFilter<Photo>(property, value));
//                        break;
//                    case "bw":
//                        filters.Add(new StartsWithFilter<Photo>(property, value));
//                        break;
//                    case "ew":
//                        filters.Add(new EndsWithFilter<Photo>(property, value));
//                        break;
//                }
//            }

//            return filters;
//        }
//    }
//}
