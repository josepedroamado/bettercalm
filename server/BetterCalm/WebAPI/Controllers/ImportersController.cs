using BLInterfaces;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ImportersController : ControllerBase
	{
		private readonly IImporterLogic importerLogic;
        public ImportersController(IImporterLogic importerLogic)
        {
			this.importerLogic = importerLogic;
        }

		[HttpGet]
		public IActionResult Post([FromBody] ImporterInputModel  importerModel)
		{
			importerLogic.Import(importerModel.Type, importerModel.FilePath);
			return NoContent();
		}
	}
}
