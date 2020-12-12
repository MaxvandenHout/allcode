using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace signalrtest
{
    public static class MySession
    {
        private static HttpSessionState session = null;

        public static HttpSessionState Current
        {
            get
            {
                if (session == null)
                {
                    session = HttpContext.Current.Session;
                }
                return session;
            }
        }
    }
}