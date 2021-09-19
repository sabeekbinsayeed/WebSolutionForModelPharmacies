using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSolutionForModelPharmacies.Models;

namespace Helper
{
    public class Database : IDisposable
    {
        private static DatabaseContext _context;
        static Database()
        {
            _context = new DatabaseContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        internal static DatabaseContext getContext()
        {
            //_context = new DatabaseContext();
            return _context;
        }

    }


}