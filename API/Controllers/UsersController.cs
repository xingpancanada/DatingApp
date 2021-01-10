using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class UsersController : BaseApiController
    {
       

        // private readonly DataContext _context;

        // public UsersController(DataContext context)
        // {
        //     _context = context;
        // }

         public readonly IUserRepository _userRepository;

        public readonly IMapper _mapper;

        public UsersController(IUserRepository userRepoistory, IMapper mapper){
            _userRepository = userRepoistory;
            _mapper = mapper;
        }

        // api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // var users =  await _userRepository.GetUsersAsync();

            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            // return Ok(usersToReturn);

            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }


        //  // api/users/3
        // [HttpGet("{id}")]
        // public async Task<ActionResult<MemberDto>> GetUserById(int id)
        // {
        //     var user = await _userRepository.GetUserByIdAsync(id);

        //     return _mapper.Map<MemberDto>(user);
        // }

        // api/users/username/alma
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByName(string username)
        {
            // var user = await _userRepository.GetUserByUsernameAsync(username);

            // return _mapper.Map<MemberDto>(user);

            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user.");
        }
    }
}