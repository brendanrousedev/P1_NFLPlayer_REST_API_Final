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
    public class RushingPlayerStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public RushingPlayerStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/RushingPlayerStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RushingPlayerStat>>> GetRushingPlayerStats()
        {
            return await _context.RushingPlayerStats.ToListAsync();
        }

        // GET: api/RushingPlayerStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RushingPlayerStat>> GetRushingPlayerStat(int id)
        {
            var rushingPlayerStat = await _context.RushingPlayerStats.FindAsync(id);

            if (rushingPlayerStat == null)
            {
                return NotFound();
            }

            return rushingPlayerStat;
        }

        // PUT: api/RushingPlayerStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRushingPlayerStat(int id, RushingPlayerStat rushingPlayerStat)
        {
            if (id != rushingPlayerStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(rushingPlayerStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RushingPlayerStatExists(id))
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

        // POST: api/RushingPlayerStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RushingPlayerStat>> PostRushingPlayerStat(RushingPlayerStat rushingPlayerStat)
        {
            _context.RushingPlayerStats.Add(rushingPlayerStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRushingPlayerStat", new { id = rushingPlayerStat.StatId }, rushingPlayerStat);
        }

        // DELETE: api/RushingPlayerStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRushingPlayerStat(int id)
        {
            var rushingPlayerStat = await _context.RushingPlayerStats.FindAsync(id);
            if (rushingPlayerStat == null)
            {
                return NotFound();
            }

            _context.RushingPlayerStats.Remove(rushingPlayerStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RushingPlayerStatExists(int id)
        {
            return _context.RushingPlayerStats.Any(e => e.StatId == id);
        }
    }
}
