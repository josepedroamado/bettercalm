using Domain;
using System;

namespace DataAccessInterfaces
{
	public interface IPsychologistRepository
    {
        Psychologist Get(int id);

        void Add(Psychologist psychologist);
        Psychologist Get(Illness illness, DateTime until, int appointmentLimitPerDay);
    }
}
