using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Models;

namespace ServerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;

        public UsersController(UserContext userContext)
        {
            _userContext = userContext;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userContext.Users.ToListAsync();

            return users;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(x=>x.Id == id);

            if (user == null)
                return BadRequest();

            return user;
        }

        // POST api/users
        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            if (user == null || user.FirstName == null || user.LastName == null || user.Age == 0 || user.Email == null)
                return;

            _userContext.Users.Add(user);

            await _userContext.SaveChangesAsync();
        }


        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User user)
        {
            if (id == 0 || user == null || user.FirstName == null || user.LastName == null || user.Age == 0 || user.Email == null)
                return;

            var userDb = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDb == null)
                return;

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Age = user.Age;

            await _userContext.SaveChangesAsync();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var userDb = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDb == null)
                return;

            _userContext.Users.Remove(userDb);

            await _userContext.SaveChangesAsync();
        }
    }
}
