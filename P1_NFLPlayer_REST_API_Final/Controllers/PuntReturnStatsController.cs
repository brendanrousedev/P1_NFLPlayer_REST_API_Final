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
    public class PuntReturnStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public PuntReturnStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/PuntReturnStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntReturnStat>>> GetPuntReturnStats()
        {
            return await _context.PuntReturnStats.ToListAsync();
        }

        // GET: api/PuntReturnStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PuntReturnStat>> GetPuntReturnStat(int id)
        {
            var puntReturnStat = await _context.PuntReturnStats.FindAsync(id);

            if (puntReturnStat == null)
            {
                return NotFound();
            }

            return puntReturnStat;
        }

        // PUT: api/PuntReturnStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntReturnStat(int id, PuntReturnStat puntReturnStat)
        {
            if (id != puntReturnStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(puntReturnStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntReturnStatExists(id))
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

        // POST: api/PuntReturnStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PuntReturnStat>> PostPuntReturnStat(PuntReturnStat puntReturnStat)
        {
            _context.PuntReturnStats.Add(puntReturnStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuntReturnStat", new { id = puntReturnStat.StatId }, puntReturnStat);
        }

        // DELETE: api/PuntReturnStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntReturnStat(int id)
        {
            var puntReturnStat = await _context.PuntReturnStats.FindAsync(id);
            if (puntReturnStat == null)
            {
                return NotFound();
            }

            _context.PuntReturnStats.Remove(puntReturnStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntReturnStatExists(int id)
        {
            return _context.PuntReturnStats.Any(e => e.StatId == id);
        }
    }
}
