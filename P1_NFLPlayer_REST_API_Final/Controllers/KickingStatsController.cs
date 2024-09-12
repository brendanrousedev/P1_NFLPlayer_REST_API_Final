using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1_NFLPlayer_REST_API_Final.POCO;

namespace P1_NFLPlayer_REST_API_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KickingStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public KickingStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/KickingStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KickingStat>>> GetKickingStats()
        {
            return await _context.KickingStats.ToListAsync();
        }

        // GET: api/KickingStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KickingStat>> GetKickingStat(int id)
        {
            var kickingStat = await _context.KickingStats.FindAsync(id);

            if (kickingStat == null)
            {
                return NotFound();
            }

            return kickingStat;
        }

        // PUT: api/KickingStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKickingStat(int id, KickingStat kickingStat)
        {
            if (id != kickingStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(kickingStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KickingStatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KickingStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KickingStat>> PostKickingStat(KickingStat kickingStat)
        {
            _context.KickingStats.Add(kickingStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKickingStat", new { id = kickingStat.StatId }, kickingStat);
        }

        // DELETE: api/KickingStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKickingStat(int id)
        {
            var kickingStat = await _context.KickingStats.FindAsync(id);
            if (kickingStat == null)
            {
                return NotFound();
            }

            _context.KickingStats.Remove(kickingStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KickingStatExists(int id)
        {
            return _context.KickingStats.Any(e => e.StatId == id);
        }
    }
}
