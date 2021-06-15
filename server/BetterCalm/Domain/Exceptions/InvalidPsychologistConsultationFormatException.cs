using System;

namespace Domain.Exceptions
{
    public class InvalidPsychologistConsultationFormatException : Exception
    {
        public InvalidPsychologistConsultationFormatException() : base("The entered consultation format is invalid.") { }
    }
}
