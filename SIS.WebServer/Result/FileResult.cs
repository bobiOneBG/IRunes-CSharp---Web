namespace SIS.MvcFramework.Result
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;

    public class FileResult : ActionResult
    {
        public FileResult(byte[]content,
            HttpResponseStatusCode responseStatusCode=HttpResponseStatusCode.Ok) 
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, content.Length.ToString()));
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "attachment"));
            this.Content = content;
        }
    }
}