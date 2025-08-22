namespace BigECommerce.Domain.Entities
{
    public class Product
    {
        protected Product()
        {
        }

        public Product(
            string name,
            decimal price,
            int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public decimal Price { get; internal set; }
        public int Stock { get; internal set; }

        public void Update(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void SubtractStock(int quantity) => Stock -= quantity;
    }
}
