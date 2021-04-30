using Domain;
using System;

namespace Model
{
    public class IllnessModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IllnessModel()
        {

        }

        public IllnessModel(Illness illness)
        {
            this.Id = illness.Id;
            this.Name = illness.Name;
        }

        public Illness ToEntity()
        {
            return new Illness
            {
                Id = this.Id,
                Name = this.Name
            };
        }

        public override bool Equals(object obj)
        {
            return obj is IllnessModel model &&
                   Id == model.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
