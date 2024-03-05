using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Coupon>> GetDiscount(string productId)
        {
            var coupon = await _repository.GetDiscount(productId);

            return Ok(coupon);  
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var result = await _repository.CreateDiscount(coupon);
            return Ok(coupon);
        }

        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            var result =  await _repository.UpdateDiscount(coupon);
            return Ok(coupon);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDiscount(string productId)
        {
            var result = await _repository.DeleteDiscount(productId);
            return Ok(result);
        }
    }
}
