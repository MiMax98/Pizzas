using System;

namespace Pizzas
{
    [Flags]
    public enum PizzaTopping
    {
        Pepperoni = 1,
        Sausage = 2,
        Mushrooms = 4,
        Peppers = 8,
        Olives = 16
    }
}
