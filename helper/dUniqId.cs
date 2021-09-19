using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    public class dUniqId
    {
        public static string get()
        {
            return String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000).ToString();
        }
    }
}