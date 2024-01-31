using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet] 
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var result = await _basketRepository.GetBasket(userName);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart shoppingCart)
        {
            var result = await _basketRepository.UpdateBasket(shoppingCart);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
