using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace WebAPI.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			try
			{
				throw context.Exception;
			}
			catch (NotFoundException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 404,
					Content = ex.Message
				};
			}
			catch (InvalidCredentialsException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (MissingCategoriesException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (UnableToCreatePlaylistException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (AlreadyExistsException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (CollectionEmptyException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 500,
					Content = ex.Message
				};
			}
			catch (IncorrectNumberOfIllnessesException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (InvalidPsychologistConsultationFormat ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (InvalidInputException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (FileNotFoundException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 400,
					Content = ex.Message
				};
			}
			catch (ImporterNotFoundException ex)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 500,
					Content = ex.Message
				};
			}
			catch (NoPatientsMeetCriteriaException ex)
            {
				context.Result = new ContentResult()
				{
					StatusCode = 500,
					Content = ex.Message
				};
            }
			catch (Exception)
			{
				context.Result = new ContentResult()
				{
					StatusCode = 500,
					Content = "Something went wrong."
				};
			}
		}
	}
}
