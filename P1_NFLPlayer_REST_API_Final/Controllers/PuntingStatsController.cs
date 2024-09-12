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
    // The Route attribute defines the base URL for all routes in this controller
    // The [ApiController] attribute enables some API-specific behavior (like automatic model validation)
    // Automatic Model Validation: ASP.NET Core faeture that validates incoming request data against 
    // the data annotations (Such as Required, MaxLength, Range, etc.) defined in model classes
    // ASP.NET Core checks if the data meets the validation defined in the model before the action method
    // in the controller is executed
    [Route("api/[controller]")]
    [ApiController]
    public class PuntingStatsController : ControllerBase
    {
        // Dependency injection for the DbContect to interact with the database
        private readonly Nfl2024playerDbContext _context;

        // Constructor: Injects the DbContext into the controller, allowing it to access the database
        public PuntingStatsController(Nfl2024playerDbContext context)
        {
            _context = context;
        }

        // GET: api/PuntingStats
        // This action retrieves all PuntingStat records from the database
        // It returns an asynchronous Task of ActionResult with a collection of PuntingStat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntingStat>>> GetPuntingStats()
        {
            // Returns all records in the PuntingStats table as a list
            return await _context.PuntingStats.ToListAsync();
        }

        // GET: api/PuntingStats/5
        // This action retrieves a specific PuntingStat records from the database
        // it returns an asynchronous Task of ActioResult with a collection of PuntingStat
        [HttpGet("{id}")]
        public async Task<ActionResult<PuntingStat>> GetPuntingStat(int id)
        {
            // Fetches the record with the specified ID
            var puntingStat = await _context.PuntingStats.FindAsync(id);

            // If the record is not found, return a 404 (Not Found) status
            if (puntingStat == null)
            {
                return NotFound();
            }

            return puntingStat;

        }

        // PUT: api/PuntingStats/5
        // This action updates an existing PuntingStat based on its ID
        // The puntingStat object is passed in the body of the request
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntingStat(int id, PuntingStat puntingStat)
        {
            // Check if the ID in the URL matches the ID in the passed object
            if (id != puntingStat.StatId)
            {
                // Checks if the ID in the URL matches the ID in the passed object
                return BadRequest();
            }

            // Marks the entity as modified soo that EF knows to update it in the DB
            _context.Entry(puntingStat).State = EntityState.Modified;

            try
            {
                // Save the changes asynchronously to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the record no longer exists in the database, return a 404 (Not found) status
                if (!PuntingStatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    // Otherwise, rethrow the exception
                    throw;
                }
            }

            // Return 204 (No Content) if the update was successful
            return NoContent();
        }

        // POST: api/PuntingStats
        // This action creates a new PuntingStat entry
        // Te puntingStat object is passed in the body of the request
        [HttpPost]
        public async Task<ActionResult<PuntingStat>> PostPuntingStat(PuntingStat puntingStat)
        {
            // Adds the new PuntingStat to the database context
            _context.PuntingStats.Add(puntingStat);
            // Save the changes to the database asynchronously
            await _context.SaveChangesAsync();

            // Returns 201 (Created) status, along with the route to access the newly created stat
            // The CreatedAtAction method builds a URL pointing to the newly created resource (GetPuntingStat)
            return CreatedAtAction("GetPuntingStat", new { id = puntingStat.StatId }, puntingStat);
        }

        // DELETE: api/PuntingStats/5
        // This action deletes a PuntingStat, entry based on its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntingStat(int id)
        {
            // Tries to find the PuntingStat by ID
            var puntingStat = await _context.PuntingStats.FindAsync(id);

            // If the stat doesn't exist, return a 404 (Not Found) status
            if (puntingStat == null)
            {
                return NotFound();
            }

            // Removes the found PuntingStat from the database
            _context.PuntingStats.Remove(puntingStat);
            // Saves the changes asynchronously to the DB
            await _context.SaveChangesAsync();

            // Returns 204 (No Content) status to indicate the stat was successfully deleted
            return NoContent();
        }

        private bool PuntingStatExists(int id)
        {
            // Uses LINQ to check if any PuntingStat records exist with the given ID
            return _context.PuntingStats.Any(e => e.StatId == id);
        }

    }
}
