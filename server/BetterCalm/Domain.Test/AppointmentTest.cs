using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Test
{
	[TestClass]
	public class AppointmentTest
	{
		[TestMethod]
		public void GetDate_AppointmentHasDate_Fetched()
		{
			DateTime date = DateTime.Now;

			Appointment appointment = new Appointment()
			{
				Id = 1,
				Date = date
			};

			Assert.AreEqual(date.Date, appointment.GetDate());
		}
	}
}
