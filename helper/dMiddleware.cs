using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    public class dMiddleware
    {
        public static bool check()
        {

            return true;
        }
        public static object Authentication(string authString,string redirectUrl)
        {
            if (HttpContext.Current.Session[authString] != null)
            {
                return HttpContext.Current.Session[authString];
            }

            HttpContext.Current.Response.Redirect(redirectUrl);
            return null;
        }
    }
}