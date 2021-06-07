using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    [AuthorizationFilter("Administrator")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientLogic patientLogic;

        public PatientsController(IPatientLogic patientLogic)
        {
            this.patientLogic = patientLogic;
        }

        [HttpGet("/approvediscounts")]
        public IActionResult Get()
        {       
            return Ok(this.patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity());
        }
    }
}
