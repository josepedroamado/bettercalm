using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IPsychologistLogic
    {
        IEnumerable<Psychologist> GetAll();

        Psychologist Get(int id);

        void Add(Psychologist psychologist);

        void Update(Psychologist psychologist);      
    }
}
