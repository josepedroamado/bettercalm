using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
	[Route("api/appointments")]
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
			return Ok(new AppointmentOutputModel(
				this.appointmentLogic.CreateAppointment(model.ToEntity())
				));
		}
	}
}
