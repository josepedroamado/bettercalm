using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class IllnessesController : ControllerBase
	{
		private readonly IIllnessLogic illnessLogic;
        public IllnessesController(IIllnessLogic illnessLogic)
        {
			this.illnessLogic = illnessLogic;
        }

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Illness> illnesses = this.illnessLogic.GetIllnesses();
			return Ok(illnesses);
		}
	}
}
