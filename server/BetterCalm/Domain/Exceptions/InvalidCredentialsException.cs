﻿using System;

namespace Domain.Exceptions
{
	public class InvalidCredentialsException : Exception
	{
		public InvalidCredentialsException() : base("Invalid credentials") {}
	}
}
