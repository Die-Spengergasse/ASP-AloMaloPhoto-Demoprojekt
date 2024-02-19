using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Test.Helpers
{
    public class TimeStampProvider : ITimeStampProvider
    {
        public DateTime Now 
            => new DateTime(2024, 01, 10);
    }
}
