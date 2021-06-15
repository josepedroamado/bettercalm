using System;

namespace Domain.Exceptions
{
    public class IncorrectNumberOfIllnessesException : Exception
    {
        public IncorrectNumberOfIllnessesException() : base("The amount of illnesses for which a psychologist can be expert at a time must be between 1 and 3.") { }
    }
}
