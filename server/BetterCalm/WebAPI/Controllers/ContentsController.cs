using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContentsController : ControllerBase
	{
		private readonly IContentPlayer contentPlayerLogic;

		public ContentsController(IContentPlayer contentPlayerLogic)
		{
			this.contentPlayerLogic = contentPlayerLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<ContentBasicInfo> contents =
				this.contentPlayerLogic.GetContents().
				Select(content => new ContentBasicInfo(content));

			return Ok(contents);
		}
	}
}
