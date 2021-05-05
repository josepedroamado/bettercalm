using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Linq;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
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

		// POST api/<AdministratorsController>
		[HttpPost]
		public IActionResult Post([FromBody] AdministratorInputModel model)
		{
			this.userLogic.CreateUser(model.ToEntityWithRole());
			return Ok();
		}

		[HttpPatch]
		public IActionResult Patch([FromBody] AdministratorInputModel model)
		{
			this.userLogic.UpdateUser(model.ToEntity());
			return new StatusCodeResult(204);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			this.userLogic.DeleteUser(id);
			return new StatusCodeResult(204);
		}

		[HttpGet]
		public IActionResult Get()
		{
			return new OkObjectResult(
				this.userLogic.GetUsersByRole(AdministratorRole).
					Select(user => new AdministratorOutputModel(user)).
					ToList());
		}
	}
}
