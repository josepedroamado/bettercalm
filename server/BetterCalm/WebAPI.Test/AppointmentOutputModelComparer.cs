using Model;
using System.Collections;

namespace WebAPI.Test
{
	class AppointmentOutputModelComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			AppointmentOutputModel xOutputModel = x as AppointmentOutputModel;
			AppointmentOutputModel yOutputModel = y as AppointmentOutputModel;

			if (Equals(xOutputModel.Address, yOutputModel.Address) &&
				Equals(xOutputModel.Date, yOutputModel.Date) &&
				Equals(xOutputModel.Format, yOutputModel.Format) &&
				Equals(xOutputModel.PsychologistName, yOutputModel.PsychologistName))
				return 0;
			return -1;
		}
	}
}
