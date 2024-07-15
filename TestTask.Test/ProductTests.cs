using TestTask.Models;
using Xunit;

namespace TestTask.Test
{
    public class ProductTests
    {
        [Fact]
        public void TotalPriceWithVAT_ShouldCalculateCorrectly()
        {
            // Arrange
            var product = new Product { Quantity = 10, Price = 50m };
            var vatRate = 0.2m;

            // Act
            var result = product.TotalPriceWithVAT(vatRate);

            // Assert
            Assert.Equal(600m, result);
        }

    }
}
