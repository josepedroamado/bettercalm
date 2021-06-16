using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
	[Route("api/sessions")]
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
		public IActionResult Delete([FromBody] SessionInfoModel expectedToken)
        {
			this.sessionLogic.Logout(expectedToken.Token);
			return NoContent();
		}
    }
}
