using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

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
			IEnumerable<IllnessModel> illnessModels = this.illnessLogic.GetIllnesses().Select(illness => new IllnessModel(illness)).ToList();
			return Ok(illnessModels);
		}
	}
}
