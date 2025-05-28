using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;
using SVContractTestingAPI.Models;
using System.Threading.Tasks;

namespace SVContractTestingAPI.Controllers
{
    [Route("api/Family/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly SintVincentiusContext _context;

        public CertificateController(SintVincentiusContext context)
        {
            _context = context;
        }

        // GET: api/Certificate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificates()
        {
            return await _context.Certificates
                .Include(c => c.Family)
                .ToListAsync();
        }

        // GET: api/Certificate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certificate>> GetCertificate(int id)
        {
            var certificate = await _context.Certificates
                .Include(c => c.Family)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (certificate == null)
            {
                return NotFound();
            }

            return certificate;
        }

        // POST: api/Certificate
        [HttpPost]
        public async Task<ActionResult<Certificate>> CreateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Families.AnyAsync(f => f.Id == certificate.FamilyId))
            {
                return BadRequest("Ongeldige FamilyId.");
            }

            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCertificate), new { id = certificate.Id }, certificate);
        }

        // GET: api/Certificate/ByFamily/5
        [HttpGet("ByFamily/{familyId}")]
        public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificatesByFamily(int familyId)
        {
            var certificates = await _context.Certificates
                .Where(c => c.FamilyId == familyId)
                .Include(c => c.Family)
                .ToListAsync();

            return Ok(certificates);
        }

        // GET: api/Certificate/Valid
        [HttpGet("Valid")]
        public async Task<ActionResult<IEnumerable<Certificate>>> GetValidCertificates()
        {
            var certificates = await _context.Certificates
                .Where(c => c.ExpiryDate == null || c.ExpiryDate >= DateTime.Now)
                .Include(c => c.Family)
                .ToListAsync();

            return Ok(certificates);
        }

        // DELETE: api/Certificate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}