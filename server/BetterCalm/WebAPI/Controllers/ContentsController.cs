using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Route("api/contents")]
	[ApiController]
	public class ContentsController : ControllerBase
	{
		private readonly IContentLogic contentLogic;

		public ContentsController(IContentLogic contentLogic)
		{
			this.contentLogic = contentLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<ContentBasicInfo> contents = this.contentLogic.GetContents()
				.Select(content => new ContentBasicInfo(content));
			return Ok(contents);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(new ContentModel(this.contentLogic.GetContent(id)));
		}


		[HttpGet("contentType/{contentType}")]
		public IActionResult Get(string contentType)
		{
			IEnumerable<ContentBasicInfo> contents = this.contentLogic.GetContents(contentType)
				.Select(content => new ContentBasicInfo(content));
			return Ok(contents);
		}

		[HttpPost]
		[AuthorizationFilter("Administrator")]
		public IActionResult Post([FromBody] ContentModel contentModel)
		{
			this.contentLogic.CreateContent(contentModel.ToEntity());
			return NoContent();
		}

		[HttpDelete("{id}")]
		[AuthorizationFilter("Administrator")]
		public IActionResult Delete(int id)
		{
			this.contentLogic.DeleteContent(id);
			return NoContent();
		}

		[HttpPatch]
		public IActionResult Patch([FromBody] ContentModel contentModel)
		{
			this.contentLogic.UpdateContent(contentModel.ToEntity());
			return NoContent();
		}
	}
}
