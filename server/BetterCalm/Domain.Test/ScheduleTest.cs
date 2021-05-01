using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Test
{
	[TestClass]
	public class ScheduleTest
	{
		[TestMethod]
		public void GetDateOk()
		{
			DateTime date = DateTime.Now;
			Schedule schedule = new Schedule()
			{
				Id = 1,
				Date = date
			};

			Assert.AreEqual(date.Date, schedule.GetScheduleDate());
		}
	}
}
