using Spg.AloMalo.DomainModel.Model.RichTypes;
using Spg.AloMalo.DomainModel.Validators.RichTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public class Person : User
    {
        public PersonId Id { get; set; } = default!;
        public string NickName { get; set; } = string.Empty;

        protected Person()
            : base()
        { }
        public Person(string firstName, string lastName, EMail username, string nickName)
            : base(firstName, lastName, username)
        {
            NickName = nickName;
        }
    }
}
