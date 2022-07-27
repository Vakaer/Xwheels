using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xwheels
{
    public class AuthorizeAdminAttribute:AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var username = filterContext.HttpContext.User.Identity.Name;
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Index" }));

            //if (username != "")
            //{
            //    base.HandleUnauthorizedRequest(filterContext);

            //}
            //else
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Index" }));
            //}
        }
    }
}