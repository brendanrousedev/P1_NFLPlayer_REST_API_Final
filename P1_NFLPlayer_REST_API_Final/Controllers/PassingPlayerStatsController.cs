using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1_NFLPlayer_REST_API_Final.POCO;
using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassingPlayerStatsController : ControllerBase
    {
        private readonly Nfl2024playerDbContext _context;

        public PassingPlayerStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassingPlayerStat>>> GetPassingPlayerStats()
        {
            return await _context.PassingPlayerStats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassingPlayerStat>> GetPassingPlayerStat(int id)
        {
            var passingPlayerStat = await _context.PassingPlayerStats.FindAsync(id);

            if (passingPlayerStat == null)
            {
                return NotFound();
            }

            return passingPlayerStat;
        }

        [HttpPut("{id")]
        public async Task<IActionResult> PutPassingPlayerStat(int id, PassingPlayerStat passingPlayerStat)
        {
            if (id != passingPlayerStat.StatId)
            {
                return BadRequest();
            }

            _context.Entry(passingPlayerStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassingPlayerStatExists(id))
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

        [HttpPost]
        public async Task<ActionResult<PassingPlayerStat>> PostPassingPlayerStat(PassingPlayerStat passingPlayerStat)
        {
            _context.PassingPlayerStats.Add(passingPlayerStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassingPlayerStat", new { id = passingPlayerStat.StatId }, passingPlayerStat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassingPlayerStat(int id)
        {
            var passingPlayerStat = await _context.PassingPlayerStats.FindAsync(id);
            if (passingPlayerStat == null)
            {
                return NotFound();
            }

            _context.PassingPlayerStats.Remove(passingPlayerStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassingPlayerStatExists(int id)
        {
            return _context.PassingPlayerStats.Any(e => e.StatId == id);
        }

    }
}
