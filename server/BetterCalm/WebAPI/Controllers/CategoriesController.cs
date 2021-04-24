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

		[HttpGet("{id}")]
		public IActionResult Get(int Id)
		{
			try
			{
				Category category = this.contentPlayerLogic.GetCategory(Id);
				return Ok(category);
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}
	}
}
