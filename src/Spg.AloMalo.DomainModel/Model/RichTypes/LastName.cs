using Spg.AloMalo.DomainModel.Validators.RichTypes;

namespace Spg.AloMalo.DomainModel.Model.RichTypes
{
    public record LastName 
        : RichTypeBase<LastName, string>, IValidateable<LastName>
    {
        public LastName()
        { }
        public LastName(string value) 
            : base(value)
        { }

        public override (bool, string?) IsValid()
        {
            if (Value?.ToLower()?.Contains("homer") ?? false)
            {
                return (false, "No Homer constraint");
            }
            return (true, null!);
        }
    }
}
