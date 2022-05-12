using basketbackend.Presentation.Models;
using basketbackend.Service.BasketService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace basketbackend.Presentation.Controllers
{
    // If this would've been more complex, I would have gone for MediatR + CQRS
    [Route("[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket([FromBody] CreateBasketModel createBasketModel)
            => Ok(await _basketService.CreateBasket(createBasketModel.Customer, createBasketModel.PaysVat));

        [HttpPut("{id}")]
        public async Task<IActionResult> AddItemToBasket([FromRoute] int id, [FromBody] AddItemToBasketModel addItemToBasketModel)
            => Ok(await _basketService.AddItemToBasket(id, addItemToBasketModel.Name, addItemToBasketModel.Price));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
            => Ok(await _basketService.Get(id));

        [HttpPatch("{id}")]
        public async Task<IActionResult> CloseBasket([FromRoute] int id, [FromBody] CloseBasketModel closeBasketModel)
            => Ok(await _basketService.CloseBasket(id, closeBasketModel.Paid));
    }
}
