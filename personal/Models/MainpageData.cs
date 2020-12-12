using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using personal.Models;

namespace personal.Models
{
    public class MainpageData
    {
        public IQueryable<Users> users;
        public IQueryable<Messages> messages;

    }
}