using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;
using SVContractTestingAPI.Models;
using System.Threading.Tasks;

namespace SVContractTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly SintVincentiusContext _context;

        public VolunteerController(SintVincentiusContext context)
        {
            _context = context;
        }

        // GET: api/Volunteer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteers()
        {
            return await _context.Volunteers.ToListAsync();
        }

        // GET: api/Volunteer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Volunteer>> GetVolunteer(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return volunteer;
        }

        // POST: api/Volunteer
        [HttpPost]
        public async Task<ActionResult<Volunteer>> CreateVolunteer(Volunteer volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVolunteer), new { id = volunteer.Id }, volunteer);
        }

        // GET: api/Volunteer/ByEmail
        [HttpGet("ByEmail")]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteersByEmail([FromQuery] string email)
        {
            var volunteers = await _context.Volunteers
                .Where(v => v.Email.Contains(email))
                .ToListAsync();

            return Ok(volunteers);
        }

        // GET: api/Volunteer/ByName
        [HttpGet("ByName")]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteersByName([FromQuery] string name)
        {
            var volunteers = await _context.Volunteers
                .Where(v => v.Name.Contains(name))
                .ToListAsync();

            return Ok(volunteers);
        }
    }
}