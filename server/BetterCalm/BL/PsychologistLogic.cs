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

        public IEnumerable<Psychologist> GetAll()
        {
            return this.psychologistRepository.GetAll();
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
            Psychologist toUpdate = this.psychologistRepository.Get(psychologist.Id);
            ICollection<Illness> illnesses = new List<Illness>() { };
                
            foreach (Illness illness in psychologist.Illnesses)
            {
                Illness obtainedIllness = this.illnessRepository.Get(illness.Id);
                if (obtainedIllness != null)
                {
                    illnesses.Add(obtainedIllness);
                }
            }
            psychologist.Illnesses = illnesses;
            toUpdate.UpdateData(psychologist);
            this.psychologistRepository.Update(toUpdate);
        }

        public void Delete(int psychologistId)
        {
            throw new System.NotImplementedException();
        }
    }
}
