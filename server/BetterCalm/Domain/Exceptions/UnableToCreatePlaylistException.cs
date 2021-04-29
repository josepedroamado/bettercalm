using System;

namespace Domain.Exceptions
{
	public class UnableToCreatePlaylistException : Exception
	{
		public UnableToCreatePlaylistException() : base("Unable to create a new playlist, invalid input data.")
		{
		}
	}
}
