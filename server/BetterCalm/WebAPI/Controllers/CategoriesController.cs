using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryLogic categoryLogic;

		public CategoriesController(ICategoryLogic categoryLogic)
		{
			this.categoryLogic = categoryLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<Category> categories = this.categoryLogic.GetCategories();
			return Ok(categories);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			Category category = this.categoryLogic.GetCategory(id);
			return Ok(category);
		}
	}
}
