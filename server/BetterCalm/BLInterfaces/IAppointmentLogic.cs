using Domain;

namespace BLInterfaces
{
	public interface IAppointmentLogic
	{
		Appointment CreateAppointment(Patient patient, Illness illness);
	}
}
