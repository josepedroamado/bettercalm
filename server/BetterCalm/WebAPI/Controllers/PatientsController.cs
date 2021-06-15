using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("approvediscounts")]
        public IActionResult Get()
        {
            IEnumerable<PatientModel> patientModels = this.patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity().Select(patient => new PatientModel(patient));
            return Ok(patientModels);
        }

        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            return Ok(new PatientModel(this.patientLogic.Get(email)));
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] PatientModel patient)
        {
            this.patientLogic.Update(patient.ToEntity());
            return NoContent();
        }
    }
}
