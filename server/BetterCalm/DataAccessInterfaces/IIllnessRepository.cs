using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IIllnessRepository
    {
        Illness Get(int id);
        IEnumerable<Illness> GetAll();
    }
}
