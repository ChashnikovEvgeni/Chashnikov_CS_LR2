using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chashnikov_LR2_CS.Models;
using Microsoft.AspNetCore.Authorization;

namespace Chashnikov_LR2_CS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly MySystemContext _context;

        public ApplicationsController(MySystemContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user")]
        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }


        [Authorize(Roles = "user")]
        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(long id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }



        [Authorize(Roles = "user")]
        [HttpGet("developer/{name}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetCompanyApp(string name)
{
    return await _context.Applications.Where(b => b.Developer.Company == name).ToListAsync();
    }


        [Authorize(Roles = "user")]
        //GET: api/Applications/DevsApp/5
        [HttpGet("DevsApp/{id}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetDevelopersApp(long id)
        {
             return await _context.Applications.Where(u => u.DeveloperId == id).ToListAsync();
           }

        [Authorize(Roles = "user")]
        //GET: api/Applications/Appointment/Social
        [HttpGet("appointment/{appointment}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetAppwithAppointment(string appointment)
        { 
            return await _context.Applications.Where(u => u.Appointment == appointment).ToListAsync();
        }


        // PUT: api/Applications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(long id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Applications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(long id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return application;
        }

       

        private bool ApplicationExists(long id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
