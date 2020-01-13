namespace SIS.Mvc.Framework
{
    using SIS.Mvc.Framework.Validation;
    using SIS.MvcFramework;
    using System;

    public class ControllerState : IControllerState
    {
        public ControllerState()
        {
            this.Reset();
        }

        public ModelStateDictionary ModelState { get; set; }

        public void Initialize(Controller controller)
        {
            this.ModelState = controller.ModelState;
        }

        public void Reset()
        {
            this.ModelState = new ModelStateDictionary();
        }

        public void SetState(Controller controller)
        {
            controller.ModelState = this.ModelState;
        }
    }
}