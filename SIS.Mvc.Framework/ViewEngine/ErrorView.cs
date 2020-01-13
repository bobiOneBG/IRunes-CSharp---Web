namespace SIS.Mvc.Framework.ViewEngine
{
    using SIS.Mvc.Framework.Validation;
    using SIS.MvcFramework.Identity;
    using SIS.MvcFramework.ViewEngine;

    public class ErrorView : IView
    {
        private readonly string errors;

        public ErrorView(string errors)
        {
            this.errors = errors;
        }

        public string GetHtml(object model,ModelStateDictionary modelState, Principal user)
        {
            return errors;
        }
    }
}