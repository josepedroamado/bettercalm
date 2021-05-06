using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
    public interface IIllnessLogic
    {
        public IEnumerable<Illness> GetIllnesses();
    }
}
