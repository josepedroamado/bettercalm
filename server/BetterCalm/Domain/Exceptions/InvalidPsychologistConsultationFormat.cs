using System;

namespace Domain.Exceptions
{
    public class InvalidPsychologistConsultationFormat : Exception
    {
        public InvalidPsychologistConsultationFormat() : base("The entered consultation format is invalid.") { }
    }
}
