using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await _productRepository.GetProducts();
            return Ok(result);
        }

        [HttpGet("GetProdcutById")]
        public async Task<ActionResult<Product>> GetProdcutById(string id)
        {
            var result = await _productRepository.GetProduct(id);
            if(result == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetProductByName")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var result = await _productRepository.GetProductByName(name);
            return Ok(result);
        }

        [HttpGet("GetProductByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var result = await _productRepository.GetProductByCatagory(category);
            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return Ok(product);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await  _productRepository.UpdateProduct(product));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
