using System;

namespace Domain.Exceptions
{
	public class CollectionEmptyException : Exception
	{
		public CollectionEmptyException(string collectionName) 
			: base($"{collectionName} is empty")	{}
	}
}
