using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
	public class PsychologistRepository : IPsychologistRepository
    {
        private DbContext context;
        private DbSet<Psychologist> psychologists;

        public PsychologistRepository(DbContext context)
        {
            this.context = context;
            this.psychologists = context.Set<Psychologist>();
        }

        public Psychologist Get(int id)
        {
            Psychologist psychologist = this.psychologists.Include(psychologist => psychologist.Illnesses).
                FirstOrDefault(psychologist => psychologist.Id == id);
            if (psychologist == null)
                throw new NotFoundException(id.ToString());
            return psychologist;
        }

        public void Add(Psychologist psychologist)
        {
            Psychologist psychologistToAdd = this.psychologists.FirstOrDefault(psycho => psycho.Id == psychologist.Id);
            if (psychologistToAdd != null)
            {
                throw new AlreadyExistsException(psychologistToAdd.Id.ToString());
            }
            else
            {
                this.psychologists.Add(psychologist);
                this.context.SaveChanges();
            }
        }

		public Psychologist Get(Illness illness, DateTime until, int appointmentLimitPerDay)
		{
            Psychologist candidate = this.psychologists
                .Include("ScheduleDays")
                .Where(psychologist =>
                    psychologist.Illnesses.Contains(illness) &&
                    (psychologist.ScheduleDays.Count() == 0 ||
                    (psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date <= until &&
                    psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Appointments.Count() < appointmentLimitPerDay
                    )))
                .OrderBy(psychologist => psychologist.CreatedDate)
                .FirstOrDefault();

            if (candidate == null)
            {
                candidate = this.psychologists
                   .Include("ScheduleDays")
                   .Where(psychologist =>
                       psychologist.ScheduleDays.Count() == 0 ||
                       (psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date <= until &&
                       psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Appointments.Count() < appointmentLimitPerDay
                       ))
                   .OrderBy(psychologist => psychologist.CreatedDate)
                   .FirstOrDefault();
            }

            return candidate;
        }

		public void Update(Psychologist psychologist)
		{
			throw new NotImplementedException();
		}
	}
}
