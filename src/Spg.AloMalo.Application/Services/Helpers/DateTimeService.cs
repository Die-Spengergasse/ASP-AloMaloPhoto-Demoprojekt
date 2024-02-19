using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Helpers
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now 
            => DateTime.Now;
    }
}
