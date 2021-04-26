using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionsController : ControllerBase
	{
		private readonly IUserManager userManager;

		public SessionsController(IUserManager userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost]
		[HttpPost]
		public IActionResult Post([FromBody] UserCredentialsModel movieModel)
		{
			throw new NotImplementedException();
		}
	}
}
