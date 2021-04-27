using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryLogic categoryLogic;
		private readonly IContentLogic contentLogic;

		public CategoriesController(ICategoryLogic categoryLogic, IContentLogic contentLogic)
		{
			this.categoryLogic = categoryLogic;
			this.contentLogic = contentLogic;
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

		[HttpGet("{id}/contents/")]
		public IActionResult GetContents(int id)
		{
			IEnumerable<ContentBasicInfo> contents =
				this.contentLogic.GetContents(categoryLogic.GetCategory(id)).
				Select(content => new ContentBasicInfo(content));

			return Ok(contents);
		}
	}
}
