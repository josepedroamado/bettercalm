using System;

namespace Domain.Exceptions
{
    public class NoPatientsMeetCriteriaException : Exception
    {
        public NoPatientsMeetCriteriaException()
            : base("No patients meet the requirements for a discount.") { }
    }
}
