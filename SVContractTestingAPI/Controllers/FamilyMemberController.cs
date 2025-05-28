using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;
using SVContractTestingAPI.Models;
using System.Threading.Tasks;

namespace SVContractTestingAPI.Controllers
{
    [Route("api/Family/[controller]")]
    [ApiController]
    public class FamilyMemberController : ControllerBase
    {
        private readonly SintVincentiusContext _context;

        public FamilyMemberController(SintVincentiusContext context)
        {
            _context = context;
        }

        // GET: api/FamilyMember
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetFamilyMembers()
        {
            return await _context.FamilyMembers
                .Include(fm => fm.Family)
                .ToListAsync();
        }

        // GET: api/FamilyMember/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyMember>> GetFamilyMember(int id)
        {
            var familyMember = await _context.FamilyMembers
                .Include(fm => fm.Family)
                .FirstOrDefaultAsync(fm => fm.Id == id);

            if (familyMember == null)
            {
                return NotFound();
            }

            return familyMember;
        }

        // POST: api/FamilyMember
        [HttpPost]
        public async Task<ActionResult<FamilyMember>> CreateFamilyMember(FamilyMember familyMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Families.AnyAsync(f => f.Id == familyMember.FamilyId))
            {
                return BadRequest("Ongeldige FamilyId.");
            }

            _context.FamilyMembers.Add(familyMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFamilyMember), new { id = familyMember.Id }, familyMember);
        }

        // GET: api/FamilyMember/ByFamily/5
        [HttpGet("ByFamily/{familyId}")]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetFamilyMembersByFamily(int familyId)
        {
            var familyMembers = await _context.FamilyMembers
                .Where(fm => fm.FamilyId == familyId)
                .Include(fm => fm.Family)
                .ToListAsync();

            return Ok(familyMembers);
        }

        // GET: api/FamilyMember/Minors
        [HttpGet("Minors")]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetMinors()
        {
            var minors = await _context.FamilyMembers
                .Where(fm => fm.DateOfBirth > DateTime.Now.AddYears(-18))
                .Include(fm => fm.Family)
                .ToListAsync();

            return Ok(minors);
        }
    }
}