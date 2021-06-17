using BL.Utils;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BL.Test.UtilsTest
{
    [TestClass]
    public class CostCalculatorTest
    {
        [TestMethod]
        public void CalculateTotalCost_DiscountIsFifteenPercent_Ok()
        {
            AppointmentDiscount discount = new AppointmentDiscount() 
            { 
                Id = 1, 
                Discount = 15 
            };
            double calculatedCost = CostCalculator.CalculateTotalCost(discount, 500, 2);
            Assert.AreEqual(850, calculatedCost);
        }

        [TestMethod]
        public void CalculateTotalCost_DiscountIsZeroPercent_Ok()
        {
            AppointmentDiscount discount = new AppointmentDiscount()
            {
                Id = 1,
                Discount = 0
            };
            double calculatedCost = CostCalculator.CalculateTotalCost(discount, 500, 2);
            Assert.AreEqual(1000, calculatedCost);
        }
    }
}
