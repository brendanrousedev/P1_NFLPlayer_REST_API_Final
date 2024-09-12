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
    public class ReceivingPlayerStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public ReceivingPlayerStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceivingPlayerStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceivingPlayerStat>>> GetReceivingPlayerStats()
        {
            return await _context.ReceivingPlayerStats.ToListAsync();
        }

        // GET: api/ReceivingPlayerStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceivingPlayerStat>> GetReceivingPlayerStat(int id)
        {
            var receivingPlayerStat = await _context.ReceivingPlayerStats.FindAsync(id);

            if (receivingPlayerStat == null)
            {
                return NotFound();
            }

            return receivingPlayerStat;
        }

        // PUT: api/ReceivingPlayerStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceivingPlayerStat(int id, ReceivingPlayerStat receivingPlayerStat)
        {
            if (id != receivingPlayerStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(receivingPlayerStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceivingPlayerStatExists(id))
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

        // POST: api/ReceivingPlayerStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceivingPlayerStat>> PostReceivingPlayerStat(ReceivingPlayerStat receivingPlayerStat)
        {
            _context.ReceivingPlayerStats.Add(receivingPlayerStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceivingPlayerStat", new { id = receivingPlayerStat.StatId }, receivingPlayerStat);
        }

        // DELETE: api/ReceivingPlayerStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceivingPlayerStat(int id)
        {
            var receivingPlayerStat = await _context.ReceivingPlayerStats.FindAsync(id);
            if (receivingPlayerStat == null)
            {
                return NotFound();
            }

            _context.ReceivingPlayerStats.Remove(receivingPlayerStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceivingPlayerStatExists(int id)
        {
            return _context.ReceivingPlayerStats.Any(e => e.StatId == id);
        }
    }
}
