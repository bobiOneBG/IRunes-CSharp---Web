namespace SIS.MvcFramework.Result
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using System.Text;

    public class JsonResult : ActionResult
    {
        public JsonResult(string jsonContent,
            HttpResponseStatusCode responseStatusCode = HttpResponseStatusCode.Ok)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/json"));
            this.Content = Encoding.UTF8.GetBytes(jsonContent);
        }
    }
}