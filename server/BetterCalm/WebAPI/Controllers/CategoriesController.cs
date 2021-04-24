using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly IContentPlayer contentPlayerLogic;

		public CategoriesController(IContentPlayer contentPlayerLogic)
		{
			this.contentPlayerLogic = contentPlayerLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Category> categories = this.contentPlayerLogic.GetCategories();
			return Ok(categories);
		}
	}
}
