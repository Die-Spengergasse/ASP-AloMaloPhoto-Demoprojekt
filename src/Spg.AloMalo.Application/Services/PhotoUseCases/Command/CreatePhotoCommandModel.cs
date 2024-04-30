using MediatR;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Command
{
    public class CreatePhotoCommandModel : IRequest<CreatePhotoReponseDto>
    {
        public CreatePhotoCommand CreatePhotoCommand { get; }

        public CreatePhotoCommandModel(CreatePhotoCommand createPhotoCommand)
        {
            CreatePhotoCommand = createPhotoCommand;
        }
    }
}
