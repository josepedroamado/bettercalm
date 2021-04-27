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
		public IActionResult Post([FromBody] UserCredentialsModel userCredentialsModel)
		{
			string token = this.userManager.Login(userCredentialsModel.EMail, userCredentialsModel.Password);
			SessionInfoModel sessionInfo = new SessionInfoModel()
			{
				Token = token
			};
			return Ok(sessionInfo);
		}

		[HttpDelete]
		public void Delete([FromBody] string expectedToken)
        {
			this.userManager.Logout(expectedToken);
		}
    }
}
