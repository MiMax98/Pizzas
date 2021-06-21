using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Order _order;
        private readonly Dictionary<PizzaSize, decimal> SizePrices = new Dictionary<PizzaSize, decimal>
        {
            { PizzaSize.Small, 16m },
            { PizzaSize.Medium, 24m },
            { PizzaSize.Large, 32m }
        };

        private readonly Dictionary<PizzaTopping, decimal> ToppingPrices = new Dictionary<PizzaTopping, decimal>
        {
            {PizzaTopping.Mushrooms, 5m },
            {PizzaTopping.Olives, 3m },
            {PizzaTopping.Pepperoni, 7m },
            {PizzaTopping.Peppers, 2m },
            {PizzaTopping.Sausage, 5m },
        };

        public MainWindow()
        {
            InitializeComponent();
            _order = new Order();
        }

        private void btn_RecordTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (!PrepareOrder())
            {
                MessageBox.Show("Wystąpił błąd", "Error");
            }

        }

        private void rdo_Small_Checked(object sender, RoutedEventArgs e)
        {
            _order.Size = PizzaSize.Small;
        }

        private void rdo_Medium_Checked(object sender, RoutedEventArgs e)
        {
            _order.Size = PizzaSize.Medium;
        }

        private void rdo_Large_Checked(object sender, RoutedEventArgs e)
        {
            _order.Size = PizzaSize.Large;
        }

        private void chx_Pepperoni_Checked(object sender, RoutedEventArgs e)
        {
            _order.Toppings ^= PizzaTopping.Pepperoni;
        }

        private void chx_Sausage_Checked(object sender, RoutedEventArgs e)
        {
            _order.Toppings ^= PizzaTopping.Sausage;

        }

        private void chx_Mushrooms_Checked(object sender, RoutedEventArgs e)
        {
            _order.Toppings ^= PizzaTopping.Mushrooms;

        }

        private void chx_Peppers_Checked(object sender, RoutedEventArgs e)
        {
            _order.Toppings ^= PizzaTopping.Peppers;

        }

        private void chx_Olives_Checked(object sender, RoutedEventArgs e)
        {
            _order.Toppings ^= PizzaTopping.Olives;

        }

        private bool PrepareOrder()
        {
            _order.Name = txt_Name.Text;
            if(!int.TryParse(txt_Number.Text, out int number))
            {
                return false;
            }
            _order.NumberOfPizzas = number;

            _order.TotalAmount = _order.NumberOfPizzas * SizePrices[_order.Size];
            var toppings = _order.Toppings;
            for (int i = 1; i <= 32; i*=2)
            {
                if ((toppings & (PizzaTopping)i) != 0)
                {
                    _order.TotalAmount += ToppingPrices[(PizzaTopping)i] * _order.NumberOfPizzas;
                }
            }
            txt_Total.Text = _order.TotalAmount.ToString();
            return true;
        }

        private void btn_CompleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            var result = $@"
Name: {_order.Name}
Number of Pizzas: {_order.NumberOfPizzas}
Total amount: {_order.TotalAmount} PLN
Size: {_order.Size}
";
            var toppings = new List<PizzaTopping>();
            for (int i = 1; i <= 32; i *= 2)
            {
                if ((_order.Toppings & (PizzaTopping)i) != 0)
                {
                    toppings.Add((PizzaTopping)i);
                }
            }
            var toppingsTxt = String.Join("\n", toppings.Select(t => $"Toppings: {t}"));
            txt_Result.Text = $"{result}{toppingsTxt}";
        }
    }
}
