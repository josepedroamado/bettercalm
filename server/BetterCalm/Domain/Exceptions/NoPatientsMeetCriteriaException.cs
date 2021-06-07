using System;

namespace Domain.Exceptions
{
    public class NoPatientsMeetCriteriaException : Exception
    {
        public NoPatientsMeetCriteriaException()
            : base("No patient that meets the requirements for a discount was found.") { }
    }
}
