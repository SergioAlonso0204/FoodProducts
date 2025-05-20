using FoodProducts.Application.DTOs.FoodProduct;
using FoodProducts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodProducts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodProductsController : ControllerBase
    {
        private readonly IFoodProductService _service;

        public FoodProductsController(IFoodProductService service)
        {
            _service = service;
        }

        // GET: api/FoodProducts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        // GET: api/FoodProducts/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/FoodProducts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FoodProductCreateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/FoodProducts/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FoodProductUpdateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updated = await _service.UpdateAsync(id, input);
            // Para PUT 204 NoContent es correcto si no se retorna entidad actualizada
            return NoContent();
        }

        // DELETE: api/FoodProducts/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
