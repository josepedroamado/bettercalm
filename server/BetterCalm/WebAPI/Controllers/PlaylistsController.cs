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
		private readonly IMediaPlayer mediaPlayerLogic;

		public PlaylistsController(IMediaPlayer mediaPlayerLogic)
		{
			this.mediaPlayerLogic = mediaPlayerLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<PlaylistBasicInfo> playlists = 
				this.mediaPlayerLogic.GetPlaylists().
				Select(playlist => new PlaylistBasicInfo(playlist));

			return Ok(playlists);
		}
	}
}
