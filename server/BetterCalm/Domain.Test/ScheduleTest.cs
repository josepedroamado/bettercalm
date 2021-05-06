using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Test
{
	[TestClass]
	public class ScheduleTest
	{
		[TestMethod]
		public void GetDate_ScheduleHasDate_Fetched()
		{
			DateTime date = DateTime.Now;
			Schedule schedule = new Schedule()
			{
				Id = 1,
				Date = date
			};

			Assert.AreEqual(date.Date, schedule.GetScheduleDate());
		}

		[TestMethod]
		public void GetAppointmentsCount_HasAppointments_Fetched()
		{
			DateTime date = DateTime.Now;
			Schedule schedule = new Schedule()
			{
				Id = 1,
				Date = date,
				Appointments = new List<Appointment>()
				{
					new Appointment()
					{
						Id = 1
					},
					new Appointment()
					{
						Id = 2
					}
				}
			};

			Assert.AreEqual(2, schedule.GetAppointmentsCount());
		}
	}
}
