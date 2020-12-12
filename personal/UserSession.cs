using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace personal
{
    public static class UserSession
    {
        private static Dictionary<string, Dictionary<string, object>> sessionData = new Dictionary<string, Dictionary<string, object>>();
        private static object lockObject = new object();

        public static Dictionary<string, object> Current
        {
            get
            {
                string sessionId = null;
                if (
                    HttpContext.Current != null &&
                    HttpContext.Current.Request != null &&
                    HttpContext.Current.Request.Cookies != null &&
                    HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null
                )
                {
                    sessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
                }
                return GetSession(sessionId);
            }
        }

        public static Dictionary<string, object> CurrentFromContext(HubCallerContext context)
        {
            string sessionId = null;
            if (
                context != null &&
                context.Request != null &&
                context.Request.Cookies != null &&
                context.Request.Cookies["ASP.NET_SessionId"] != null
            )
            {
                sessionId = context.Request.Cookies["ASP.NET_SessionId"].Value;
            }
            return GetSession(sessionId);
        }

        public static void End(string sessionId)
        {
            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                lock (lockObject)
                {
                    if (sessionData.ContainsKey(sessionId))
                    {
                        var session = sessionData[sessionId];
                        sessionData.Remove(sessionId);
                        session.Clear();
                        session = null;
                    }
                }
            }
        }

        private static Dictionary<string, object> GetSession(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return null;
            }
            else
            {
                Dictionary<string, object> session = null;
                lock (lockObject)
                {
                    if (sessionData.ContainsKey(sessionId))
                    {
                        session = sessionData[sessionId];
                    }
                    else
                    {
                        session = new Dictionary<string, object>();
                        sessionData.Add(sessionId, session);
                    }
                }
                return session;
            }
        }
    }
}