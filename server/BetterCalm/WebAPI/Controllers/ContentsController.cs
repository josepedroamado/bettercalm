using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContentsController : ControllerBase
	{
		public ContentsController(IContentPlayer contentPlayerLogic)
		{

		}

		[HttpGet]
		public IActionResult Get()
		{
			throw new NotImplementedException();
		}
	}
}
