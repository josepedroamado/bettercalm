using System;

namespace Domain.Exceptions
{
    public class ExceedingNumberOfIllnessesException : Exception
    {
        public ExceedingNumberOfIllnessesException() : base("The amount of illnesses entered exceeds the allowed amount for which a psychologist can be expert at a time.") { }
    }
}
