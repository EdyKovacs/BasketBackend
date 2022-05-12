using AutoMapper;
using basketbackend.Presentation.Models;
using basketbackend.Service.BasketTotalCalculator;
using Data;

namespace basketbackend.Presentation.MappingProfiles
{
    public class BasketProfile : Profile
    {
        private readonly IBasketTotalNetCalculator _basketTotalNetCalculator;
        private readonly IBasketTotalGrossCalculator _basketTotalGrossCalculator;

        public BasketProfile(IBasketTotalNetCalculator basketTotalNetCalculator, IBasketTotalGrossCalculator basketTotalGrossCalculator)
        {
            _basketTotalNetCalculator = basketTotalNetCalculator;
            _basketTotalGrossCalculator = basketTotalGrossCalculator;

            CreateMap<Basket, BasketResponse>()
                .ForMember(basketResponse => basketResponse.TotalNet, opt => opt.MapFrom(basket => _basketTotalNetCalculator.Compute(basket)))
                .ForMember(basketResponse => basketResponse.TotalGross, opt => opt.MapFrom(basket => _basketTotalGrossCalculator.Compute(basket)));
        }
    }
}
