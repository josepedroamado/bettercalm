using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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
			throw new NotImplementedException();
		}
	}
}
