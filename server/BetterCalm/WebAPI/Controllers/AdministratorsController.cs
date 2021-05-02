using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using WebAPI.Filters;

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
		[ServiceFilter(typeof(AuthorizationAttributeFilter))]
		public IActionResult Post([FromBody] AdministratorInputModel model)
		{
			this.userLogic.CreateUser(model.ToEntity());
			return Ok();
		}
	}
}
