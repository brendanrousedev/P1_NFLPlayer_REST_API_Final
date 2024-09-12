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
    public class DefensePlayerStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public DefensePlayerStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/DefensePlayerStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefensePlayerStat>>> GetDefensePlayerStats()
        {
            return await _context.DefensePlayerStats.ToListAsync();
        }

        // GET: api/DefensePlayerStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DefensePlayerStat>> GetDefensePlayerStat(int id)
        {
            var defensePlayerStat = await _context.DefensePlayerStats.FindAsync(id);

            if (defensePlayerStat == null)
            {
                return NotFound();
            }

            return defensePlayerStat;
        }

        // PUT: api/DefensePlayerStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDefensePlayerStat(int id, DefensePlayerStat defensePlayerStat)
        {
            if (id != defensePlayerStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(defensePlayerStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefensePlayerStatExists(id))
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

        // POST: api/DefensePlayerStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DefensePlayerStat>> PostDefensePlayerStat(DefensePlayerStat defensePlayerStat)
        {
            _context.DefensePlayerStats.Add(defensePlayerStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDefensePlayerStat", new { id = defensePlayerStat.StatId }, defensePlayerStat);
        }

        // DELETE: api/DefensePlayerStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDefensePlayerStat(int id)
        {
            var defensePlayerStat = await _context.DefensePlayerStats.FindAsync(id);
            if (defensePlayerStat == null)
            {
                return NotFound();
            }

            _context.DefensePlayerStats.Remove(defensePlayerStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DefensePlayerStatExists(int id)
        {
            return _context.DefensePlayerStats.Any(e => e.StatId == id);
        }
    }
}
