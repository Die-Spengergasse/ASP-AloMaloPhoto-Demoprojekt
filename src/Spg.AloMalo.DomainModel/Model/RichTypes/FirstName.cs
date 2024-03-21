using Spg.AloMalo.DomainModel.Validators.RichTypes;

namespace Spg.AloMalo.DomainModel.Model.RichTypes
{
    public record FirstName 
        : RichTypeBase<FirstName, string>, IValidateable<FirstName>
    {
        public FirstName()
        { }
        public FirstName(string value) 
            : base(value)
        { }

        public override (bool, string?) IsValid()
        {
            if (Value?.Length > 5)
            {
                return (false, "First Name max 5");
            }
            return (true, null!);
        }
    }
}
