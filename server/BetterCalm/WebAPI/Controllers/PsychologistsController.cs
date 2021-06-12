using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model;
using BLInterfaces;
using WebAPI.Filters;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[AuthorizationFilter("Administrator")]
	public class PsychologistsController : ControllerBase
	{

		private readonly IPsychologistLogic psychologistLogic;

        public PsychologistsController(IPsychologistLogic psychologistLogic)
        {
			this.psychologistLogic = psychologistLogic;
        }

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<PsychologistModel> models = this.psychologistLogic.GetAll().Select(psychologist => new PsychologistModel(psychologist));
			return Ok(models);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			PsychologistModel psychologistModel = new PsychologistModel(this.psychologistLogic.Get(id));
			return Ok(psychologistModel);
		}

		[HttpPost]
		public IActionResult Post([FromBody] PsychologistModel psychologistModel)
		{
			this.psychologistLogic.Add(psychologistModel.ToEntity());
			return Ok();
		}

		[HttpPatch]
		public void Patch([FromBody] PsychologistModel psychologistModel)
		{
			this.psychologistLogic.Update(psychologistModel.ToEntity());
		}

		[HttpDelete]
		public void Delete([FromBody] PsychologistDeleteModel psychologist)
		{
			this.psychologistLogic.Delete(psychologist.Id);
		}
	}
}
