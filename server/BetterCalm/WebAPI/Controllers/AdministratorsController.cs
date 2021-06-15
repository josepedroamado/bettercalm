using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
	[Route("api/administrators")]
	[ApiController]
	[AuthorizationFilter("Administrator")]
	public class AdministratorsController : ControllerBase
	{
		private readonly IUserLogic userLogic;
		private const string AdministratorRole = "Administrator";

		public AdministratorsController(IUserLogic userLogic)
		{
			this.userLogic = userLogic;
		}

		[HttpPost]
		public IActionResult Post([FromBody] AdministratorInputModel model)
		{
			this.userLogic.CreateUser(model.ToEntityWithRole());
			return NoContent();
		}

		[HttpPatch]
		public IActionResult Patch([FromBody] AdministratorInputModel model)
		{
			this.userLogic.UpdateUser(model.ToEntity());
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			this.userLogic.DeleteUser(id);
			return NoContent();
		}

		[HttpGet]
		public IActionResult Get()
		{
			return new OkObjectResult(
				this.userLogic.GetUsersByRole(AdministratorRole).
					Select(user => new AdministratorOutputModel(user)).
					ToList());
		}

		[HttpGet("{email}")]
		public IActionResult Get(string email)
		{
			return Ok(new AdministratorOutputModel(this.userLogic.GetUser(email)));
		}
	}
}
