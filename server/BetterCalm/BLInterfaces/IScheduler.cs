using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IScheduler
	{
		IEnumerable<Illness> GetIllnesses();
		void CreateAppointment(Appointment appointment);
		IEnumerable<Appointment> GetAppointments();
		Appointment GetAppointment(int id);
		void CreatePsychologist(Psychologist psychologist);
		void UpdatePsychologist(Psychologist psychologist);
		void DeletePsychologist(int id);
	}
}
