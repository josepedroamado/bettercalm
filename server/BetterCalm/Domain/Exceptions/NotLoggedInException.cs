using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class NotLoggedInException : Exception
    {
        public NotLoggedInException() : base("User not logged in.") { }
    }
}
