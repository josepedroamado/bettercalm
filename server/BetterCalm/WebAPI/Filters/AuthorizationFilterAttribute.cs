using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Filters
{
	public class AuthorizationFilterAttribute : Attribute, IActionFilter
    {
        private readonly string role;

        public AuthorizationFilterAttribute(string role)
        {
            this.role = role;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Not authorized"
                };
                return;
            }
            var sessions = GetSessions(context);
            if (!sessions.IsTokenValid(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Not authorized"
                };
                return;
            }
            if (!sessions.TokenHasRole(token, this.role))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "You don't have permission to access this resource"
                };
                return;
            }
        }

        private static ISessionLogic GetSessions(ActionExecutingContext context)
        {
            return (ISessionLogic)context.HttpContext.RequestServices.GetService(typeof(ISessionLogic));
        }

        public void OnActionExecuted(ActionExecutedContext context) {}
	}
}
