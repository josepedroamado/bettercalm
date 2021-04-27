using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlaylistsController : ControllerBase
	{
		private readonly IPlaylistLogic playlistLogic;

		public PlaylistsController(IPlaylistLogic playlistLogic)
		{
			this.playlistLogic = playlistLogic;
		}

		[HttpGet]
		public IActionResult Get()
		{
			IEnumerable<PlaylistBasicInfo> playlists = 
				this.playlistLogic.GetPlaylists().
				Select(playlist => new PlaylistBasicInfo(playlist));

			return Ok(playlists);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			Playlist playlist = this.playlistLogic.GetPlaylist(id);
			return Ok(playlist);
		}
	}
}
