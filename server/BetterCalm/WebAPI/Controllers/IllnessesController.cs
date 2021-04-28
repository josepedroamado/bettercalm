using BLInterfaces;
using Microsoft.AspNetCore.Mvc;

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
			return Ok();
		}
	}
}
