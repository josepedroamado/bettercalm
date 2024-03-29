﻿using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
	public class Psychologist : Person
	{
		private ICollection<Schedule> scheduleDays = new List<Schedule>();
		public string Address { get; set; }
		public Format Format { get; set; }
		public IEnumerable<Illness> Illnesses { get; set; }
		public DateTime CreatedDate { get; set; }
		public ICollection<Schedule> ScheduleDays
		{
			get => scheduleDays;
			set => scheduleDays = value;
		}
		public PsychologistRate Rate { get; set; }

		public string GetFullName()
		{
			return string.Concat(this.FirstName, " ", this.LastName);
		}

		public override bool Validate()
		{
			if (string.IsNullOrWhiteSpace(this.Address))
			{
				throw new InvalidInputException("Address is required");
			}
			if (this.Rate == null)
			{
				throw new InvalidInputException("Rate is required");
			}
			return base.Validate();
		}

		public Schedule GetLast()
		{
			if (ScheduleDays.Count() > 0)
				return ScheduleDays.OrderBy(schedule => schedule.Date).Last();
			return null;
		}

		public void UpdateData(Psychologist psychologist)
		{
			if (!string.IsNullOrWhiteSpace(psychologist.FirstName))
			{
				this.FirstName = psychologist.FirstName;
			}
			if (!string.IsNullOrWhiteSpace(psychologist.LastName))
			{
				this.LastName = psychologist.LastName; 
			}
			if (!string.IsNullOrWhiteSpace(psychologist.Address))
            {
				this.Address = psychologist.Address;
			}
			this.Format = psychologist.Format;
			if (psychologist.Illnesses != null)
			{
				this.Illnesses = psychologist.Illnesses;
			}
            if (psychologist.Rate != null)
            {
				this.Rate = psychologist.Rate;
            }
		}

		public override bool Equals(object obj)
		{
			if (obj is Psychologist psychologist)
			{
				bool equalsIllnesses;
				if (this.Illnesses != null && psychologist.Illnesses != null)
					equalsIllnesses = this.Illnesses.SequenceEqual(psychologist.Illnesses);
				else
					equalsIllnesses = true;
				
				bool equalsScheduleDays;
				if (this.ScheduleDays != null && psychologist.ScheduleDays != null)
					equalsScheduleDays = this.ScheduleDays.SequenceEqual(psychologist.ScheduleDays);
				else
					equalsScheduleDays = true;

				return equalsIllnesses &&
					equalsScheduleDays &&
					Equals(this.Address, psychologist.Address) &&
					Equals(this.Format, psychologist.Format) &&
					Equals(this.CreatedDate, psychologist.CreatedDate) &&
					Equals(this.FirstName, psychologist.FirstName) &&
					Equals(this.LastName, psychologist.LastName);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Address,
				this.CreatedDate,
				this.FirstName,
				this.Format,
				this.Id,
				this.Illnesses,
				this.LastName,
				this.ScheduleDays);
		}
	}
}
