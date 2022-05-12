using Data;

namespace basketbackend.Service.BasketTotalCalculator
{
    public interface IBasketTotalCalculator
    {
        public double Compute(Basket basket);
    }
}
