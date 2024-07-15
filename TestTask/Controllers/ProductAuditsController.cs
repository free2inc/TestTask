using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAuditsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductAuditsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductAudits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAudit>>> GetProductAudit(DateTime? from, DateTime? to)
        {
            var query = _context.ProductAudit.AsQueryable();

            if (from.HasValue)
            {
                query = query.Where(a => a.ChangeDate >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(a => a.ChangeDate <= to.Value);
            }

            var auditTrail = await query.ToListAsync();
            return Ok(auditTrail);
        }
    }
}
