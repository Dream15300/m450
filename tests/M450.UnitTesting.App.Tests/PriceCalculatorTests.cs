using System;
using System.Collections.Generic;
using System.Text;

namespace M450.UnitTesting.App.Tests
{
    [TestClass]
    public class PriceCalculatorTests
    {
        [TestMethod]
        public void PriceCalculatorNoDiscountTest()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var result = priceCalculator.CalculateTotal(10m, 2);

            // Assert
            Assert.AreEqual(20m, result);

        }

        [TestMethod]
        public void PriceCalculatorWithDiscount()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var result = priceCalculator.CalculateTotal(10m, 2, 10m);

            // Assert
            Assert.AreEqual(18m, result);
        }

        [TestMethod]
        public void PriceCalculatorQuantityZero()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var result = priceCalculator.CalculateTotal(10m, 0);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void PriceCalculatorFullDiscount()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var result = priceCalculator.CalculateTotal(10, 5, 100);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void PriceCalculatorNegativeUnitPrice()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();

            // Act + Assert
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => priceCalculator.CalculateTotal(-10, 2));
        }

        [TestMethod]
        public void PriceCalculatorNegativeQuantity()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();
            // Act + Assert
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => priceCalculator.CalculateTotal(10, -2));
        }

        [TestMethod]
        public void PriceCalculatorInvalidDiscountPercentage()
        {
            // Arrange
            PriceCalculator priceCalculator = new PriceCalculator();
            // Act + Assert
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => priceCalculator.CalculateTotal(10, 2, -5));
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => priceCalculator.CalculateTotal(10, 2, 105));
        }
    }
}
