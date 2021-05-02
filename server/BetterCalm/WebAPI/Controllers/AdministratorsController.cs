using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdministratorsController : ControllerBase
	{
		private readonly IUserLogic userLogic;

		public AdministratorsController(IUserLogic userLogic)
		{
			this.userLogic = userLogic;
		}

		// POST api/<AdministratorsController>
		[HttpPost]
		public IActionResult Post([FromBody] AdministratorInputModel model)
		{
			throw new NotImplementedException();
		}
	}
}
