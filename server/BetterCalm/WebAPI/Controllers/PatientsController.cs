using BLInterfaces;
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
        [HttpGet("/approvediscounts")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
