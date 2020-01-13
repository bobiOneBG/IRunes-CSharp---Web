namespace SIS.Mvc.Framework.Validation
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class ModelStateDictionary
    {
        private readonly IDictionary<string, List<string>> errorMessages;

        public ModelStateDictionary()
        {
            errorMessages = new Dictionary<string, List<string>>();
        }

        public bool IsValid => this.errorMessages.Count == 0;

        public IReadOnlyDictionary<string, List<string>> ErrorMessages =>
            errorMessages.ToImmutableDictionary();

        public void Add(string name, string errorMessage)
        {
            if (!this.errorMessages.ContainsKey(name))
            {
                errorMessages.Add(name, new List<string>());
            }

            errorMessages[name].Add(errorMessage );
        }
    }
}