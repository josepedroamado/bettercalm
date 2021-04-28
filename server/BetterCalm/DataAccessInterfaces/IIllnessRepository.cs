using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IIllnessRepository
    {
        IEnumerable<Illness> GetAll();
    }
}
