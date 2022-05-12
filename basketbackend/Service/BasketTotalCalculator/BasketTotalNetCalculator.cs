using Data;
using System.Linq;

namespace basketbackend.Service.BasketTotalCalculator
{
    public class BasketTotalNetCalculator : IBasketTotalNetCalculator
    { 
        public double Compute(Basket basket) => basket.Items.Select(item => item.Price).Sum();
    }
}
