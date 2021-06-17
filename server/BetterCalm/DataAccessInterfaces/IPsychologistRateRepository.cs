using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IPsychologistRateRepository
    {
        IEnumerable<PsychologistRate> GetAll();

        PsychologistRate Get(int rate);
    }
}
