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
		private readonly ISessionLogic sessionLogic;

		public SessionsController(ISessionLogic sessionLogic)
		{
			this.sessionLogic = sessionLogic;
		}

		[HttpPost]
		public IActionResult Post([FromBody] UserCredentialsModel userCredentialsModel)
		{
			string token = this.sessionLogic.Login(userCredentialsModel.EMail, userCredentialsModel.Password);
			SessionInfoModel sessionInfo = new SessionInfoModel()
			{
				Token = token
			};
			return Ok(sessionInfo);
		}

		[HttpDelete]
		public void Delete([FromBody] UserTokenModel expectedToken)
        {
			this.sessionLogic.Logout(expectedToken.Token);
		}
    }
}
