using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System;
using System.Collections.Generic;

namespace BL
{
    public class IllnessLogic : IIllnessLogic
    {
        private readonly IIllnessRepository illnessRepository;

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
