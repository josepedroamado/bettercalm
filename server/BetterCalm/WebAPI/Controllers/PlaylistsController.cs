using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlaylistsController : ControllerBase
	{
		private readonly IContentPlayer contentPlayerLogic;

		public PlaylistsController(IContentPlayer contentPlayerLogic)
		{
			this.contentPlayerLogic = contentPlayerLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<PlaylistBasicInfo> playlists = 
				this.contentPlayerLogic.GetPlaylists().
				Select(playlist => new PlaylistBasicInfo(playlist));

			return Ok(playlists);
		}
	}
}
