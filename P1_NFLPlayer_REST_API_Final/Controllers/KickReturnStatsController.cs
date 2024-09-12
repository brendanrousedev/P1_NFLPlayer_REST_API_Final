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
    public class KickReturnStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public KickReturnStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/KickReturnStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KickReturnStat>>> GetKickReturnStats()
        {
            return await _context.KickReturnStats.ToListAsync();
        }

        // GET: api/KickReturnStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KickReturnStat>> GetKickReturnStat(int id)
        {
            var kickReturnStat = await _context.KickReturnStats.FindAsync(id);

            if (kickReturnStat == null)
            {
                return NotFound();
            }

            return kickReturnStat;
        }

        // PUT: api/KickReturnStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKickReturnStat(int id, KickReturnStat kickReturnStat)
        {
            if (id != kickReturnStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(kickReturnStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KickReturnStatExists(id))
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

        // POST: api/KickReturnStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KickReturnStat>> PostKickReturnStat(KickReturnStat kickReturnStat)
        {
            _context.KickReturnStats.Add(kickReturnStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKickReturnStat", new { id = kickReturnStat.StatId }, kickReturnStat);
        }

        // DELETE: api/KickReturnStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKickReturnStat(int id)
        {
            var kickReturnStat = await _context.KickReturnStats.FindAsync(id);
            if (kickReturnStat == null)
            {
                return NotFound();
            }

            _context.KickReturnStats.Remove(kickReturnStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KickReturnStatExists(int id)
        {
            return _context.KickReturnStats.Any(e => e.StatId == id);
        }
    }
}
