using Domain;

namespace BLInterfaces
{
    public interface IPsychologistLogic
    {
        Psychologist Get(int id);

        void Add(Psychologist psychologist);

        void Update(Psychologist psychologist);      
    }
}
