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
        private readonly InterfaceofDb _logic;


        public DevelopersController(MySystemContext context, InterfaceofDb logic)
        {
            _context = context;
            _logic = logic;
            if (!_context.Developers.Any())
            {
         
                _context.SaveChanges();
            }
     
      
        }

       [Authorize]
        // GET: api/Developers
        [HttpGet]
     
        public CreatedAtActionResult GetDevelopers()
        {
            return CreatedAtAction("GetDevelopers", new
            {
              
                result =_context.Developers.Select(p => new { Id = p.Id, Name = p.Name, Age = p.Age, Company = p.Company, Applications = p.Applications.Select(t => t.Name) })
            });

        }

        // GET: api/developers/Company
         [Authorize]
        [HttpGet("Company")]
        public CreatedAtActionResult GetCompany()
        { return CreatedAtAction("GetCompany", new
            {
                result = _logic.ShowAllCompany(_context.Developers.ToList())
               
            });
          
        }

       [Authorize]
        // GET: api/Developers/5
        [HttpGet("{id}")]


        public CreatedAtActionResult GetDeveloper(long id)
        {

            var developer = _context.Developers.Select(p => new { Id = p.Id, Name = p.Name, Age = p.Age, Company = p.Company, Applications = p.Applications.Select(t => t.Name) }).FirstOrDefault(a => a.Id == id);
            if (developer == null)

                return CreatedAtAction("GetDeveloper", new
                {
                    result = "Данный разработчик не найден"

                });

            else
                return CreatedAtAction("GetDeveloper", new
                {
                    result = developer

                }) ;

        }
    

         [Authorize(Roles = "admin")]

        // PUT: api/Developers/5
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
