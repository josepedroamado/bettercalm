using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

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
			catch (CollectionEmptyException ex)
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
