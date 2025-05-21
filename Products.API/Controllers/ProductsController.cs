using Products.Application.DTOs.Product;
using Products.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductservice _service;

        public ProductsController(IProductservice service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updated = await _service.UpdateAsync(id, input);
            return NoContent();
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
