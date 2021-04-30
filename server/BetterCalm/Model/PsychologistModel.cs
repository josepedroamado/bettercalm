using Domain;
using Domain.Exceptions;
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
		public string Format { get; set; }
		public IEnumerable<Illness> Illnesses { get; set; }

		public Psychologist ToEntity()
        {
			return new Psychologist
			{
				FirstName = this.FirstName,
				LastName = this.LastName,
				Address = this.Address,
				Format = ParseFormat(this.Format),
				Illnesses = this.Illnesses,
				CreatedDate = DateTime.Today
			};
        }

		private Format ParseFormat(string inputFormat)
		{
			if (Enum.TryParse<Format>(inputFormat, out Format parsedFormat))
			{
				return parsedFormat;
			}
			else
			{
				throw new InvalidPsychologistConsultationFormat();

			}
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
			Format = psychologist.Format.ToString();
			Illnesses = psychologist.Illnesses;
		}

        public PsychologistModel()
        {

        }
    }
}
