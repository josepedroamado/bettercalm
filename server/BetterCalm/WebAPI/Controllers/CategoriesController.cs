using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
	[Route("api/categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryLogic categoryLogic;
		private readonly IContentLogic contentLogic;
		private readonly IPlaylistLogic playlistLogic;

		public CategoriesController(ICategoryLogic categoryLogic, IContentLogic contentLogic, IPlaylistLogic playlistLogic)
		{
			this.categoryLogic = categoryLogic;
			this.contentLogic = contentLogic;
			this.playlistLogic = playlistLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(this.categoryLogic.GetCategories());
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(this.categoryLogic.GetCategory(id));
		}

		[HttpGet("{categoryId}/contents")]
		public IActionResult GetContents(int categoryId)
		{
			IEnumerable<ContentBasicInfo> contents = this.contentLogic
				.GetContents(categoryLogic.GetCategory(categoryId))
				.Select(content => new ContentBasicInfo(content));
			return Ok(contents);
		}

		[HttpGet("{categoryId}/playlists")]
		public IActionResult GetPlaylists (int categoryId)
		{
			IEnumerable<PlaylistBasicInfo> playlists = this.playlistLogic
				.GetPlaylists(categoryLogic.GetCategory(categoryId))
				.Select(playlist => new PlaylistBasicInfo(playlist));
			return Ok(playlists);
		}
	}
}
