using BigECommerce.Domain.Entities;

namespace BigECommerce.Domain.Tests.Entities
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_ShouldSetProperties()
        {
            // Arrange & Act
            var product = new Product("Notebook", 2500, 10);

            // Assert
            Assert.Equal("Notebook", product.Name);
            Assert.Equal(2500m, product.Price);
            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public void SubtractStock_ShouldDecreaseStock()
        {
            // Arrange
            var product = new Product("Notebook", 2500, 10);

            // Act
            product.SubtractStock(3);

            // Assert
            Assert.Equal(7, product.Stock);
        }

        [Fact]
        public void Update_ShouldUpdateValues()
        {
            // Arrange
            var product = new Product("Notebook", 2500, 10);

            // Act
            product.Update("Notebook Gamer", 3000, 8);

            // Assert
            Assert.Equal("Notebook Gamer", product.Name);
            Assert.Equal(3000m, product.Price);
            Assert.Equal(8, product.Stock);
        }
    }
}
