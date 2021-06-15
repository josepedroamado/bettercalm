using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/playlists")]
	[ApiController]
	public class PlaylistsController : ControllerBase
	{
		private readonly IPlaylistLogic playlistLogic;
		private readonly IContentLogic contentLogic;

		public PlaylistsController(IPlaylistLogic playlistLogic, IContentLogic contentLogic)
		{
			this.playlistLogic = playlistLogic;
			this.contentLogic = contentLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<PlaylistBasicInfo> playlists = this.playlistLogic.GetPlaylists()
				.Select(playlist => new PlaylistBasicInfo(playlist));
			return Ok(playlists);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok(this.playlistLogic.GetPlaylist(id));
		}

		[HttpGet("{id}/contents")]
		public IActionResult GetContents(int id)
		{
			IEnumerable<ContentBasicInfo> contents = this.contentLogic
				.GetContents(playlistLogic.GetPlaylist(id))
				.Select(content => new ContentBasicInfo(content));
			return Ok(contents);
		}
	}
}
