﻿using Domain;
using System;
using System.Linq;

namespace BL.Utils
{
    public class DateCalculator
    {
		public static DateTime CalculateUntilDate(DateTime since)
		{
			int daysUntilFriday = (DayOfWeek.Friday - since.DayOfWeek + 7) % 7;
            if (daysUntilFriday == 0)
			{
				daysUntilFriday = 7;
			}
			since = since.AddDays(daysUntilFriday);
			return since.Date;
		}

		public static DateTime CalculateNextWorkDay(DateTime date)
		{
			if (date.DayOfWeek == DayOfWeek.Saturday)
			{
				date = date.AddDays(2);
			}
			else if (date.DayOfWeek == DayOfWeek.Sunday)
			{
				date = date.AddDays(1);
			}
			return date.Date;
		}

		public static DateTime CalculateAppointmentDate(Psychologist candidate, int limitOfAppointmentsPerDay)
		{
			DateTime today = DateTime.Now.Date;
			Schedule last = candidate.GetLast();
			if (last != null)
			{
				if (last.GetScheduleDate() <= DateTime.Now.Date)
				{
					return CalculateNextWorkDay(today.AddDays(1));
				}
				if (last.Appointments.Count() < limitOfAppointmentsPerDay)
				{
					return last.GetScheduleDate();
				}
				return CalculateNextWorkDay(last.GetScheduleDate().AddDays(1));
			}
			return CalculateNextWorkDay(today.AddDays(1));
		}
	}
}
