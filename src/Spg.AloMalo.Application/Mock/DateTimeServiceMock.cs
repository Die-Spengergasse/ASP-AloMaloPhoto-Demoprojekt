using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Mock
{
    public class DateTimeServiceMock : IDateTimeService
    {
        public DateTime Now
            => new DateTime(2020, 05, 15);
    }
}
