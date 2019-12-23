namespace SIS.HTTP.Headers
{
    using SIS.Common;
    using SIS.HTTP.Headers.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private Dictionary<string, HttpHeader> httpHeaders;

        public HttpHeaderCollection()
        {
            this.httpHeaders = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            ValidationExtensions.ThrowIfNull(header, nameof(header));
            this.httpHeaders.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            ValidationExtensions.ThrowIfNullOrEmpty(key, nameof(key));
            return this.httpHeaders.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            ValidationExtensions.ThrowIfNullOrEmpty(key, nameof(key));
            return this.httpHeaders[key];
        }

        public override string ToString() => string.Join("\r\n",
            this.httpHeaders.Values.Select(header => header.ToString()));
    }
}