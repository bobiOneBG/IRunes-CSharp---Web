﻿namespace SIS.MvcFramework.Result
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Responses;

    public abstract class ActionResult : HttpResponse, IActionResult
    {
        protected ActionResult(HttpResponseStatusCode responseStatusCode)
            : base(responseStatusCode)
        {

        }
    }
}