using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProg.Data;
using TestProg.Data.Entities;

namespace ServerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _dataContext.Users.ToListAsync();

            return users;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x=>x.Id == id);

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

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();
        }


        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] User user)
        {
            if (id == 0 || user == null || user.FirstName == null || user.LastName == null || user.Age == 0 || user.Email == null)
                return;

            var userDb = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDb == null)
                return;

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Age = user.Age;

            await _dataContext.SaveChangesAsync();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var userDb = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDb == null)
                return;

            _dataContext.Users.Remove(userDb);

            await _dataContext.SaveChangesAsync();
        }
    }
}
