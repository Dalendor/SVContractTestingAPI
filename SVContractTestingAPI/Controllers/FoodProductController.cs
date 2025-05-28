using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;
using SVContractTestingAPI.Models;
using System.Threading.Tasks;

namespace SVContractTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodProductController : ControllerBase
    {
        private readonly SintVincentiusContext _context;

        public FoodProductController(SintVincentiusContext context)
        {
            _context = context;
        }

        // GET: api/FoodProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodProduct>>> GetFoodProducts()
        {
            return await _context.FoodProducts.ToListAsync();
        }

        // GET: api/FoodProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodProduct>> GetFoodProduct(int id)
        {
            var product = await _context.FoodProducts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/FoodProduct
        [HttpPost]
        public async Task<ActionResult<FoodProduct>> CreateFoodProduct(FoodProduct foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FoodProducts.Add(foodProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodProduct), new { id = foodProduct.Id }, foodProduct);
        }

        // GET: api/FoodProduct/NonExpired
        [HttpGet("NonExpired")]
        public async Task<ActionResult<IEnumerable<FoodProduct>>> GetNonExpiredFoodProducts()
        {
            var products = await _context.FoodProducts
                .Where(p => p.ExpiryDate >= DateTime.Now)
                .ToListAsync();

            return Ok(products);
        }
    }
}