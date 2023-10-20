using Microsoft.AspNetCore.Http;
using System;
using System.Web;

namespace YB_AssessmentManagement.Common
{
    public class ValidatedURLExtensions
    {
        public  Uri GetAbsoluteUri(HttpContext context)
        {
            var request = context.Request;
            UriBuilder uriBuilder = new UriBuilder()
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return RemoveQueryStringByKey(uriBuilder.Uri, "_");
        }

        public Uri RemoveQueryStringByKey(Uri uri, string RemoveKey)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Remove(RemoveKey);
            var ub = new UriBuilder(uri)
            {
                Query = httpValueCollection.ToString()
            };
            return ub.Uri;
        }
    }
}
