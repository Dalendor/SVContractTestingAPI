using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;
using SVContractTestingAPI.Models;

namespace SVContractTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly SintVincentiusContext _context;

        public FamilyController(SintVincentiusContext context)
        {
            _context = context;
        }

        // GET: api/Family
        [HttpGet]
        public async Task<IActionResult> GetFamilies()
        {
            var families = await _context.Families
                .Include(f => f.Members)
                .Include(f => f.Certificates)
                .ToListAsync();
            return Ok(new { items = families });
        }

        // GET: api/Family/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Family>> GetFamily(int id)
        {
            var family = await _context.Families
                .Include(f => f.Members)
                .Include(f => f.Certificates)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (family == null)
            {
                return NotFound();
            }
            return family;
        }

        // POST: api/Family
        [HttpPost]
        public async Task<ActionResult<Family>> CreateFamily(Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Families.Add(family);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFamily), new { id = family.Id }, family);
        }

        // PUT: api/Family/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamily(int id, Family family)
        {
            if (id != family.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingFamily = await _context.Families.FindAsync(id);
            if (existingFamily == null)
            {
                return NotFound();
            }

            existingFamily.Name = family.Name;
            existingFamily.Address = family.Address;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Family/ByAddress
        [HttpGet("ByAddress")]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamiliesByAddress([FromQuery] string address)
        {
            var families = await _context.Families
                .Where(f => f.Address.Contains(address))
                .Include(f => f.Members)
                .Include(f => f.Certificates)
                .ToListAsync();

            return Ok(new { items = families });
        }

        // GET: api/Family/WithValidCertificates
        [HttpGet("WithValidCertificates")]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamiliesWithValidCertificates()
        {
            var families = await _context.Families
                .Include(f => f.Certificates)
                .Where(f => f.Certificates.Any(c => c.ExpiryDate == null || c.ExpiryDate >= DateTime.Now))
                .Include(f => f.Members)
                .ToListAsync();

            return Ok(new { items = families });
        }
    }

}
