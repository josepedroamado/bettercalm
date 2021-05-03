using Domain;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IPsychologistRepository
    {
        IEnumerable<Psychologist> GetAll();
        Psychologist Get(int id);

        void Add(Psychologist psychologist);
        Psychologist Get(Illness illness, DateTime until, int appointmentLimitPerDay);
        void Update(Psychologist psychologist);
    }
}
