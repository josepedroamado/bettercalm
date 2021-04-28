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
	}
}
