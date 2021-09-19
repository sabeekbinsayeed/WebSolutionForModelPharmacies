using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    public class dRedirect
    {
        public static void Redirect(string url)
        {
            System.Web.HttpContext.Current.Server.ClearError();
            System.Web.HttpContext.Current.Response.Redirect(url, false);

        }
    }
}