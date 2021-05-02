using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdministratorsController : ControllerBase
	{
		// POST api/<AdministratorsController>
		[HttpPost]
		public void Post([FromBody] AdministratorInputModel model)
		{
			throw new NotImplementedException();
		}
	}
}
