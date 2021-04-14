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
    public class UsersController : ControllerBase
    {
        private readonly MySystemContext _context;
        private readonly InterfaceofDb _logic;

        public UsersController(MySystemContext context, InterfaceofDb logic)
        {
            _context = context;
            _logic = logic;
        }

         [Authorize]
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


         [Authorize]
        // GET: api/Users/UsersApps/5
        [HttpGet("UsersApps/{id}")]

        public async Task<ActionResult<List<AppsInfo>>> GetUsersApps(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return _logic.UsersApp(_context.UsersApps.ToList(), id, _context.Applications.ToList());
            }

        }






        // PUT: api/Users/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        
        // POST: api/Users
        [HttpPost]
      [Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


         [Authorize(Roles = "admin")]
        [HttpPost("UsersAppsAdd")]

        public async Task<ActionResult<UsersApps>> AddUsersApps([FromForm] long UserId, [FromForm] List<long> AppsId)
        {

            foreach (long aid in AppsId)
            {
                UsersApps Buf = new UsersApps();
                Buf.User = _context.Users.FirstOrDefault(i => i.Id == UserId);
                Application application = _context.Applications.FirstOrDefault(i => i.Id == aid);
                if (Buf.User != null && application != null)
                {
                    Buf.UserId = UserId;
                    Buf.ApplicationId = aid;

                    _context.UsersApps.Add(Buf);
                    await _context.SaveChangesAsync();


                }
                else return BadRequest();
            }

            return NoContent();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("SvRemove")]

        public async Task<ActionResult<UsersApps>> RemoveUsersApps([FromForm] long UserId, [FromForm] List<long> AppsId)
        {

            foreach (long aid in AppsId)
            {

                var application = _context.Applications.FirstOrDefault(i => i.Id == aid);
                User user = _context.Users.FirstOrDefault(i => i.Id == UserId);
                if (application != null && user != null)
                {
                    var Bufer = _context.UsersApps.FirstOrDefault(u => u.UserId == UserId && u.ApplicationId == aid);
                    _context.UsersApps.Remove(Bufer);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("RemoveUsersApps", new { result = "Связь удалена" });



                }
                else return BadRequest();


            }

            return CreatedAtAction("RemoveUsersApps", new { result = "Ok" });

        }




        // DELETE: api/Users/5
        [HttpDelete("{id}")]
       [Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
