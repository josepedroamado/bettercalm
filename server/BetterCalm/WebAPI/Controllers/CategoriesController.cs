using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private IContentPlayer contentPlayerLogic;
		public CategoriesController(IContentPlayer contentPlayerLogic)
		{

		}

		[HttpGet]
		public IActionResult Get()
		{
			throw new NotImplementedException();
		}
	}
}
