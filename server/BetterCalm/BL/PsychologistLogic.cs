using BLInterfaces;
using DataAccessInterfaces;
using Domain;

namespace BL
{
    public class PsychologistLogic : IPsychologistLogic
    {
        private IPsychologistRepository psychologistRepository;
        private IIllnessRepository illnessRepository;

        public PsychologistLogic(IPsychologistRepository psychologistRepository, IIllnessRepository illnessRepository)
        {
            this.psychologistRepository = psychologistRepository;
            this.illnessRepository = illnessRepository;
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
