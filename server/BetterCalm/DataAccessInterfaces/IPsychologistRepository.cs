using Domain;

namespace DataAccessInterfaces
{
    public interface IPsychologistRepository
    {
        Psychologist Get(int id);

        void Add(Psychologist psychologist);
    }
}
