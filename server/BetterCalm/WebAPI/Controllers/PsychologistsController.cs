using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model;
using BLInterfaces;
using WebAPI.Filters;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
			var psychologist = psychologistModel.ToEntity();
			this.psychologistLogic.Add(psychologist);
			return Ok();
		}

		// PUT api/<PsychologistsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<PsychologistsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
