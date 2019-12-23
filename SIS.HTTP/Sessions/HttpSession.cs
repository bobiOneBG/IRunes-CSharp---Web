namespace SIS.HTTP.Sessions
{
    using SIS.Common;
    using SIS.HTTP.Sessions.Contracts;
    using System.Collections.Generic;

    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> sessionParameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.IsNew = true;
            this.sessionParameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public bool IsNew { get; set; }

        public object GetParameter(string parameterName)
        {
            ValidationExtensions.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

            // TODO: Validation for existing parameter (maybe throw exception)

            return this.sessionParameters[parameterName];
        }

        public bool ContainsParameter(string parameterName)
        {
            ValidationExtensions.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

            return this.sessionParameters.ContainsKey(parameterName);
        }

        public void AddParameter(string parameterName, object parameter)
        {
            ValidationExtensions.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));
            ValidationExtensions.ThrowIfNull(parameter, nameof(parameter));

            this.sessionParameters[parameterName] = parameter;
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }
    }
}