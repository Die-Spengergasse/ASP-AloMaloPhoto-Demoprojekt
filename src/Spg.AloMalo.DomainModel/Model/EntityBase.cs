namespace Spg.AloMalo.DomainModel.Model
{
    public class EntityBase<TEntity> 
        where TEntity : class
    {
        public bool IsValid { get; private set; } = true;

        public List<string> ErrorMessages { get; } = new();

        protected void Validate(List<Func<(bool validation, string error)>> valitations)
        {
            bool isValid = true;
            foreach (Func<(bool validation, string error)> item in valitations)
            {
                (bool validation, string error) = item();
                if (!validation)
                {
                    ErrorMessages.Add(error);
                    isValid = false;
                }
            }
            IsValid = isValid;
        }
    }
}
