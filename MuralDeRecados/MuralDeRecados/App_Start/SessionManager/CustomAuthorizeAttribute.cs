using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MuralDeRecados.App_Start.SessionManager
{
    public struct SessionKeys
    {
        public const string Usuario = "Usuario";
    }

    public class ResearchAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionManager.CheckSession(SessionKeys.Usuario) == true)
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (SessionManager.CheckSession(SessionKeys.Usuario) == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                        {
                        { "action", "Login" },
                        { "controller", "Home" }
                        });
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }
}