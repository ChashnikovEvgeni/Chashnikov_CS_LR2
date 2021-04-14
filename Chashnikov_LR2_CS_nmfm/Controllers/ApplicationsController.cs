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
        private readonly InterfaceofDb _logic;

        public ApplicationsController(MySystemContext context, InterfaceofDb logic)
        {
            _logic = logic;
            _context = context;
        }
       [Authorize]
        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetApplications()
        {
            return  _logic.ShowAllApps(_context.Applications.Include(a=>a.UsersApps).ToList()); 
        }


       [Authorize]
        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppsInfo>> GetApplication(long id)
        {
            var application =  await _context.Applications.Include(a => a.UsersApps).FirstAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }
           
            return application.GetAppsInfo();
        }







         [Authorize]
        [HttpGet("CompanyApp/{name}")]
        public CreatedAtActionResult GetCompanyApp(string name)

        {
           var t= _logic.SelectCompany(_context.Developers.Include(d => d.Applications).ToList(), name);
            return CreatedAtAction("GetCompanyApp", new
            {
               
                result = t

            }); ;



        }

        [Authorize]
        //GET: api/Applications/DevsApp/5
        [HttpGet("DevsApp/{id}")]
        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetDevelopersApp(long id)
        {
            return  _logic.DevelopersApplications(_context.Applications.Include(a => a.UsersApps).ToList(), id);
           }

       // [Authorize]
        //GET: api/Applications/Appointment/Social
        [HttpGet("appointment/{appointment}")]
        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetAppwithAppointment(string appointment)
        {
            
            return _logic.SelectbyAppointment(_context.Applications.Include(a => a.UsersApps).ToList(), appointment);
        }


        //sort "true" means sorting in ascending order, "false" means sorting in descending order 
         [Authorize]
        //GET: api/Applications/ByNOF
        [HttpGet("ByNof/{sorting}")]

        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetAppByNOFD(bool sorting)
        {

            return _logic.AppsbyNOF(_context.Applications.Include(a=>a.UsersApps).ToList(), sorting);
        }



        //NOF -Nummber Of User
        //sort "true" means sorting in ascending order, "false" means sorting in descending order 
        [Authorize]
        //GET: api/Applications/TopbyNOF/appointment/sorting
        [HttpGet("TopByNof/{appointment}/{sorting}")]

        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetAppTopByNof(string appointment, bool sorting)
        {

            return _logic.Top5AppsbyNOF(_context.Applications.Include(a => a.UsersApps).ToList(), appointment, sorting);
        }




        //sort "true" means sorting in ascending order, "false" means sorting in descending order 
        // [Authorize]
        //GET: api/Applications/ByRating/true
        [HttpGet("ByRating/{sorting}")]

        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetAppByRating(bool sorting)
        {

            return _logic.AppsbyRating(_context.Applications.Include(a => a.UsersApps).ToList(), sorting);
        }



        //sort "true" means sorting in ascending order, "false" means sorting in descending order 
         [Authorize]
        //GET: api/Applications/Top5byRating/appointment/sorting
        [HttpGet("Top5ByRating/{appointment}/{sorting}")]

        public async Task<ActionResult<IEnumerable<AppsInfo>>> GetAppTopByRating(string appointment, bool sorting)
        {

            return _logic.Top5AppsbyRating(_context.Applications.Include(a => a.UsersApps).ToList(), appointment, sorting);
        }







        [Authorize(Roles = "admin")]
        // PUT: api/Applications/5
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
