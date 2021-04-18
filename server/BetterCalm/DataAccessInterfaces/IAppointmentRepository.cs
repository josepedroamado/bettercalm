using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IAppointmentRepository
	{
		void Add(Appointment appointment);
		Appointment Get(int id);
		IEnumerable<Appointment> GetAll();
	}
}
