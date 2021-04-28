using Domain;
using System;
using System.Collections.Generic;

namespace Model
{
    public class PsychologistModel
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public Format Format { get; set; }
		public IEnumerable<Illness> Illnesses { get; set; }

		public Psychologist ToEntity()
        {
			return new Psychologist
			{
				FirstName = this.FirstName,
				LastName = this.LastName,
				Address = this.Address,
				Format = this.Format,
				Illnesses = this.Illnesses,
				CreatedDate = DateTime.Today
			};
        }

        public override bool Equals(object obj)
        {
            return obj is PsychologistModel model &&
                   Id == model.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public PsychologistModel(Psychologist psychologist)
        {
			Id = psychologist.Id;
			FirstName = psychologist.FirstName;
			LastName = psychologist.LastName;
			Address = psychologist.Address;
			Format = psychologist.Format;
			Illnesses = psychologist.Illnesses;
		}

        public PsychologistModel()
        {

        }
    }
}
