using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentsController : ControllerBase
	{
		private readonly IAppointmentLogic appointmentLogic;

		public AppointmentsController(IAppointmentLogic appointmentLogic)
		{
			this.appointmentLogic = appointmentLogic;
		}

		[HttpPost]
		public IActionResult Post([FromBody] AppointmentInputModel model)
		{
			throw new NotImplementedException();
		}
	}
}
