using Data;

namespace basketbackend.Service.BasketTotalCalculator
{
    public class BasketTotalGrossCalculator : IBasketTotalGrossCalculator
    {
        // We could write some retrieval logic for this value if needed
        // Could be system provided, or maybe we could obtain it based on region
        // The specifications did not say, so I took the liberty to just add it here as a const
        private const double _vatPercentage = 0.1;
        private readonly IBasketTotalNetCalculator _netCalculator;

        public BasketTotalGrossCalculator(IBasketTotalNetCalculator netCalculator)
        {
            _netCalculator = netCalculator;
        }

        public double Compute(Basket basket)
        {
            var totalAmmount = _netCalculator.Compute(basket);
            if(basket.PaysVAT)
            {
                var vatAmmount = _vatPercentage * totalAmmount;
                totalAmmount += vatAmmount;
            }

            return totalAmmount;
        }
    }
}
