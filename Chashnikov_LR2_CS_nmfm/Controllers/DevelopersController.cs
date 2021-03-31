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
    public class DevelopersController : ControllerBase
    {
  
        private readonly MySystemContext _context;

        public DevelopersController(MySystemContext context)
        {
            _context = context;
            if (!_context.Developers.Any())
            {
                Developer Tony= new Developer { Name = "Tony", Age = 28, Company = "Hub" };
               Application App1 = new Application { Name = "Triil", Appointment = "Social", Developer = Tony };

                _context.Developers.Add(Tony);
                _context.Applications.Add(App1);
                _context.SaveChanges();
            }
     
      
        }

        [Authorize(Roles = "user")]
        // GET: api/Developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            return await _context.Developers.ToListAsync();
        }


        [Authorize(Roles = "user")]
        // GET: api/Developers/findname/Tony
        [HttpGet("Name/{Name}")]
        public async Task<ActionResult<IEnumerable<Developer>>> GetName(string bname)
        {
            //return  _context.Developers.Include(u => u.Applications).ToList();
            return await _context.Developers.Where(u=>u.Name== bname).ToListAsync();
        }


        // GET: api/Developers/findname/Tony
        //[HttpGet("Sample/{Name}")]
        //public Task<ActionResult<IEnumerable<Developer>>> GetSample(string bname)
        //{
        //    //var query = await (from dev in _context.Developers
        //    //            join app in _context.Applications on dev.Id equals app.DeveloperId
        //    //            select new
        //    //            {
        //    //                Name = dev.Name,
        //    //                AppName = app.Name
        //    //            });

        //    //  return await Task.WhenAll(query);
        //    //           //return  _context.Developers.Include(u => u.Applications).ToList();

        //    //return await _context.Developers.Where(u => u.Name == bname).ToListAsync();

        //    var result = _context.Applications.Select((a) => new { Id = a.Id, Index = _context.Developers.Where((r) => r.Applications.Contains(a)).Select((r) => r.Id).FirstOrDefault() });
        //    return await Task.WhenAll(result);
        //}


        [Authorize(Roles = "user")]
        // GET: api/Developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(long id)
        {
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null)
            {
                return NotFound();
            }

            return developer;
        }

        [Authorize(Roles = "admin")]

        // PUT: api/Developers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(long id, Developer developer)
        {
            if (id != developer.Id)
            {
                return BadRequest();
            }

            _context.Entry(developer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
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


        [Authorize(Roles = "admin")]
        // POST: api/Developers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
        {
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.Id }, developer);
        }


        [Authorize(Roles = "admin")]
        // DELETE: api/Developers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Developer>> DeleteDeveloper(long id)
        {
            var developer = await _context.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return developer;
        }

 
        private bool DeveloperExists(long id)
        {
            return _context.Developers.Any(e => e.Id == id);
        }
    }
}
