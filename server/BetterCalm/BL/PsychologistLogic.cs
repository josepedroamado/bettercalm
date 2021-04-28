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
            return this.psychologistRepository.Get(id);
        }

        public void Add(Psychologist psychologist)
        {
            this.psychologistRepository.Add(psychologist);
        }
    }
}
