using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
	[Route("api/importers")]
	[ApiController]
	[AuthorizationFilterAttribute("Administrator")]
	public class ImportersController : ControllerBase
	{
		private readonly IImporterLogic importerLogic;
        public ImportersController(IImporterLogic importerLogic)
        {
			this.importerLogic = importerLogic;
        }

		[HttpPost]
		public IActionResult Post([FromBody] ImporterInputModel  importerModel)
		{
			importerLogic.Import(importerModel.Type, importerModel.FilePath);
			return NoContent();
		}

		[HttpGet("types")]
		public IActionResult GetTypes()
		{
			ImporterTypesModel result = new ImporterTypesModel()
			{
				Types = importerLogic.GetTypes()
			};
			return Ok(result);
		}
	}
}
