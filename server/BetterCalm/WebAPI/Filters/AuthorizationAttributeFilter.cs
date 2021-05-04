using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Filters
{
	public class AuthorizationAttributeFilter : Attribute, IAuthorizationFilter
	{
		private readonly ISessionLogic sessionLogic;

		public AuthorizationAttributeFilter(ISessionLogic sessionLogic)
		{
			this.sessionLogic = sessionLogic;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string token = context.HttpContext.Request.Headers["Authorization"];

			if (string.IsNullOrEmpty(token))
			{
				context.Result = new ContentResult()
				{
					StatusCode = 401,
					Content = "Not authorized"
				};
			}
			else
			{
				if (!sessionLogic.IsTokenValid(token))
				{
					context.Result = new ContentResult()
					{
						StatusCode = 403,
						Content = "You don't have permission to access this resource"
					};
				}
			}
		}
	}
}
