using BLInterfaces;
using DataAccessInterfaces;
using Domain;

namespace BL
{
    public class PsychologistLogic : IPsychologistLogic
    {
        private IPsychologistRepository psychologistRepository;

        public PsychologistLogic(IPsychologistRepository psychologistRepository)
        {
            this.psychologistRepository = psychologistRepository;
        }

        public Psychologist Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Psychologist psychologist)
        {
            throw new System.NotImplementedException();
        }
    }
}
