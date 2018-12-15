using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class RoleFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        if (filterContext.Result is ViewResultBase result)
        {
            var user = filterContext.HttpContext.User;
            if (user.IsInRole("Admin"))
            {
                result.ViewName = "About";
            }
            else if (user.IsInRole("Registered"))
            {
                result.ViewName = "Contact";
            }
            else if (user.IsInRole("Unregistered"))
            {
                result.ViewName = "Index";
            }
        }
    }
}