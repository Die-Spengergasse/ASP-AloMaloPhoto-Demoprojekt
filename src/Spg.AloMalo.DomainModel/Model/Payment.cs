using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public enum PaymentTypes { Visa, MasterCard }

    public class Payment
    {
        public int Id { get; private set; }
        public DateTime TimeStamp { get; set; }
        public decimal Amount { get; set; }
        public PaymentTypes PaymentType { get; set; }
    }
}
