using System.Collections.Generic;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // api/users
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users =  await _context.Users.ToListAsync();

            return users;
        }


         // api/users/3
      
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        // //Edit User by Id
        // [Authorize]
        [HttpPost("editUser/{id}")]
         public async Task<ActionResult<IEnumerable<AppUser>>> EditUser(int id, AppUser appuser)
        {
            var user = await _context.Users.FindAsync(id);

            if(user != null){
                user.Id = id;
                user.FirstName = appuser.FirstName;
                user.LastName = appuser.LastName;
                user.UserName = appuser.UserName;
                user.Active = appuser.Active;
            }

            var users =  await _context.Users.ToListAsync();
            return users;

        }

        // //Delete a user
        // [Authorize]
        // [HttpPost("deleteUser/{id}")]
        //  public async Task<ActionResult> DeleteUser(int id)
        // {
        //     var user = await _context.Users.FindAsync(id);

        //     await _context.Users.(user);

        //     return Ok();
        // }
    }
}