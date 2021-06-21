namespace Pizzas
{
    public class Order
    {
        public PizzaSize Size { get; set; }
        public PizzaTopping Toppings { get; set; }
        public string Name { get; set; }
        public int NumberOfPizzas { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
