using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;

namespace BL
{
    public class IllnessLogic : IIllnessLogic
    {
        private IIllnessRepository illnessRepository;

        public IllnessLogic(IIllnessRepository illnessRepository)
        {
            this.illnessRepository = illnessRepository;
        }
        public IEnumerable<Illness> GetIllnesses()
        {
            return this.illnessRepository.GetAll();
        }
    }
}
