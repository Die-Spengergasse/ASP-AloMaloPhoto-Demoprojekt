using Spg.AloMalo.DomainModel.Interfaces.Findables;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public class Photographer : User, IFindableByGuid
    {
        public PhotographerId Id { get; set; } = default!;
        public Guid Guid { get; private set; }
        public Address StudioAddress { get; set; } = default!;
        public PhoneNumber MobilePhoneNumber { get; set; } = default!;
        public PhoneNumber BusinessPhoneNumber { get; set; } = default!;

        private List<Photo> _photos = new();
        public IReadOnlyList<Photo> Photos => _photos;

        private List<Album> _albums = new();
        public IReadOnlyList<Album> Albums => _albums;

        private List<EMail> _eMails = new();
        public IReadOnlyList<EMail> EMails => _eMails;

        protected Photographer()
        { }
        public Photographer(
            Guid guid,
            string firstName,
            string lastName,
            Address studioAddress,
            PhoneNumber mobilePhoneNumber,
            PhoneNumber businessPhoneNumber,
            List<EMail> eMails,
            EMail username)
            :base(firstName, lastName, username)
        {
            Guid = guid;
            StudioAddress = studioAddress;
            MobilePhoneNumber = mobilePhoneNumber;
            BusinessPhoneNumber = businessPhoneNumber;
            _eMails = eMails;
        }
    }
}
