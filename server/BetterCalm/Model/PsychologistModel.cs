using Domain;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class PsychologistModel
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Format { get; set; }
		public IEnumerable<IllnessModel> IllnessModels { get; set; }
		public int Rate { get; set; }

		public Psychologist ToEntity()
        {
			return new Psychologist
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				Address = this.Address,
				Format = ParseFormat(this.Format),
				Illnesses = this.IllnessModels?.Select(illnessModel => illnessModel.ToEntity()).ToList(),
				CreatedDate = DateTime.Now,
				Rate = new PsychologistRate()
                {
					Id = 1,
					HourlyRate = Rate
                }
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
			IllnessModels = psychologist.Illnesses?.Select(illness => new IllnessModel(illness)).ToList();
			Rate = psychologist.Rate.HourlyRate;
		}

        public PsychologistModel()
        {

        }
    }
}
