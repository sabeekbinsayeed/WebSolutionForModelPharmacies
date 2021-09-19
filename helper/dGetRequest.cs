using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helper
{
    public class dGetRequest
    {

        internal static string GetUrlParameter(HttpRequestBase request, string parName)
        {
            string result = string.Empty;

            var urlParameters = HttpUtility.ParseQueryString(request.Url.Query);
            if (urlParameters.AllKeys.Contains(parName))
            {
                result = urlParameters.Get(parName);
            }

            return result;
        }

        
    }
}