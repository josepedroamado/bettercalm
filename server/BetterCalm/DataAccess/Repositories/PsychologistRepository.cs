using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Psychologist> GetAll()
        {
            if (this.psychologists.Count() <= 0)
                throw new CollectionEmptyException("Psychologists");
            else
                return this.psychologists.Include(x => x.Rate).Include(p => p.Illnesses);
        }

        public Psychologist Get(int id)
        {
            Psychologist psychologist = this.psychologists
                .Include(psychologist => psychologist.Illnesses)
                .Include(x => x.Rate)
                .Include(s => s.ScheduleDays)
                .ThenInclude(s => s.Appointments)
                .FirstOrDefault(psychologist => psychologist.Id == id);
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
            
            if (psychologist.Validate())
            {
                this.psychologists.Add(psychologist);
                this.context.SaveChanges();
            }
        }

		public Psychologist Get(Illness illness, DateTime until, int appointmentLimitPerDay)
		{
            if (this.psychologists.Count() == 0)
                throw new CollectionEmptyException("Psychologists");

            Psychologist candidate = this.psychologists
                .Include(psychologist => psychologist.ScheduleDays).ThenInclude(scheduleDays => scheduleDays.Appointments)
                .Include(psychologist => psychologist.Rate)
                .Where(psychologist =>
                    psychologist.Illnesses.Contains(illness) &&
                    (psychologist.ScheduleDays.Count() == 0 ||
                    psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date < until ||
                    (psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date == until &&
                    psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Appointments.Count() < appointmentLimitPerDay
                    )))
                .OrderBy(psychologist => psychologist.CreatedDate)
                .FirstOrDefault();

            if (candidate == null)
            {
                candidate = this.psychologists
                   .Include(psychologist => psychologist.ScheduleDays).ThenInclude(scheduleDays => scheduleDays.Appointments)
                   .Include(psychologist => psychologist.Rate)
                   .Where(psychologist =>
                       psychologist.ScheduleDays.Count() == 0 ||
                       (psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date < until ||
                       (psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Date == until &&
                       psychologist.ScheduleDays.OrderBy(schedule => schedule.Date).Last().Appointments.Count() < appointmentLimitPerDay
                       )))
                   .OrderBy(psychologist => psychologist.CreatedDate)
                   .FirstOrDefault();
            }

            return candidate;
        }

		public void Update(Psychologist psychologist)
		{
            if (psychologist.Validate())
			{
                this.psychologists.Update(psychologist);
                this.context.SaveChanges();
            }
		}

        public void Delete(int psychologistId)
        {
            try
            {
                Psychologist psychologistToDelete = this.Get(psychologistId);
                if (psychologistToDelete != null)
                {
                    foreach(Schedule schedule in psychologistToDelete.ScheduleDays)
					{                             
                        foreach(Appointment appointment in schedule.Appointments)
						{
                            this.context.Remove(appointment);
                        }
                        this.context.Remove(schedule);
                    }
                    this.psychologists.Remove(psychologistToDelete);
                    this.context.SaveChanges();
                }
            }
            catch (NotFoundException) { }    
        }
    }
}
