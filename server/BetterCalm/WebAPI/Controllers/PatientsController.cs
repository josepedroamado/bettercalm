using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return Ok(this.patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string email)
        {
            return Ok(this.patientLogic.Get(email));
        }

        [HttpPatch]
        public void Patch([FromBody] Patient patient)
        {
            this.patientLogic.Update(patient);
        }
    }
}
