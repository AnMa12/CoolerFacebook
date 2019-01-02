using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class RoleFilter : ActionFilterAttribute
{
   

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.Result is ViewResultBase result)
        {
            var user = filterContext.HttpContext.User;
            if (user.IsInRole("Registered"))
            {

                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Contact");
                redirectTargetDictionary.Add("controller", "Home");

                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

        }
    }
}