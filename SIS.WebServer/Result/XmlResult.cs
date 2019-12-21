namespace SIS.MvcFramework.Result
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using System.Text;

    public class XmlResult : ActionResult
    {
        public XmlResult(string xmlContent, 
            HttpResponseStatusCode responseStatusCode=HttpResponseStatusCode.Ok) 
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/xml"));
            this.Content = Encoding.UTF8.GetBytes(xmlContent);
        }
    }
}