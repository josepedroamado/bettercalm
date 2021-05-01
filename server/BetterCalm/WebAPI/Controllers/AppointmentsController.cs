using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentsController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post([FromBody] AppointmentInputModel model)
		{
			throw new NotImplementedException();
		}
	}
}
