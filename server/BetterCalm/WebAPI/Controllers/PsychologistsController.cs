using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model;
using BLInterfaces;
using WebAPI.Filters;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[ServiceFilter(typeof(AuthorizationAttributeFilter))]
	public class PsychologistsController : ControllerBase
	{

		private readonly IPsychologistLogic psychologistLogic;

        public PsychologistsController(IPsychologistLogic psychologistLogic)
        {
			this.psychologistLogic = psychologistLogic;
        }

		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
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
			throw new NotImplementedException();
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
