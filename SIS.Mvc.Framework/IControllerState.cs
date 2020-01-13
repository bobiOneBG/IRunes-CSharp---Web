namespace SIS.Mvc.Framework
{
    using SIS.Mvc.Framework.Validation;
    using SIS.MvcFramework;

    public interface IControllerState
    {
        ModelStateDictionary ModelState { get; set; }

        void Reset();

        void Initialize(Controller controller);

        void SetState(Controller controller);
    }
}