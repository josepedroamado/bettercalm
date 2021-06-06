using Domain;

namespace BL.Utils
{
    public class CostCalculator
    {
		public static double CalculateTotalCost(AppointmentDiscount discount, double hourlyRate, double hours)
		{
			double percentageToCharge = 1;
			if (discount != null)
			{
				percentageToCharge -= discount.Discount / 100;
			}
			double totalCost = hourlyRate * hours * percentageToCharge;
			return totalCost;
		}
	}
}
