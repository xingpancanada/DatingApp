using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        // private readonly IUserRepository _userRepository;
        // private readonly ILikesRepository _likesRepository;
        private readonly IUnitOfWork _unitOfWork;
        // public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        public LikesController(IUnitOfWork unitOfWork)
        {
            // _likesRepository = likesRepository;
            // _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            // var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            // var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);
             var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserId);

            if (likedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

            // var alreadyLiked = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);
            var alreadyLiked = await _unitOfWork.LikesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if (alreadyLiked != null) return BadRequest("You already like this user");

            alreadyLiked = new UserLike
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(alreadyLiked);

            // if (await _userRepository.SaveAllAsync()) return Ok();
            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like user");
        }

        [HttpGet]
        // public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
        {
            // var users = await _likesRepository.GetUserLikes(predicate, User.GetUserId());
            
            likesParams.UserId = User.GetUserId();
            // var users = await _likesRepository.GetUserLikes(likesParams);
            var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
    }
}