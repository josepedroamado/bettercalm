using Domain;

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
    }
}
