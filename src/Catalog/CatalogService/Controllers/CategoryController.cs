using CatalogService.Entities;
using CatalogService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IProductRepository productRepository, ILogger<CategoryController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task <ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products =await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if(product == null)
            {
                _logger.LogError($"Product with id: {id}, not found");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> CreateProduct([FromBody]Products products)
        {
            await _productRepository.CreateProduct(products);
            return CreatedAtRoute("GetProduct", new { id = products.ID }, products);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody]Products products)
        {
            return Ok(await _productRepository.UpdateProduct(products));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string Id)
        {
            return Ok(await _productRepository.DeleteProduct(Id));
        }










    }
}
