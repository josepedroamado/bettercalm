using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

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
            psychologist.Illnesses = GetStoredIlnesses(psychologist.Illnesses);
            this.psychologistRepository.Add(psychologist);
        }

        private List<Illness> GetStoredIlnesses(IEnumerable<Illness> inMemoryIlnesses)
        {
            if (inMemoryIlnesses == null)
            {
                return null;
            }

            if (inMemoryIlnesses.Count() > 3)
            {
                throw new ExceedingNumberOfIllnessesException();
            }

            List<Illness> storedIllnesses = inMemoryIlnesses.Select(illness => this.illnessRepository.Get(illness.Id)).ToList();

            return storedIllnesses;
        }

        public void Update(Psychologist psychologist)
        {
            this.psychologistRepository.Update(psychologist);
        }
    }
}
