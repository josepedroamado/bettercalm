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
			string token = this.userManager.Login(movieModel.EMail, movieModel.Password);
			SessionInfoModel sessionInfo = new SessionInfoModel()
			{
				Token = token
			};
			return Ok(sessionInfo);
		}

        public void Delete(string expectedToken)
        {
            throw new NotImplementedException();
        }
    }
}
