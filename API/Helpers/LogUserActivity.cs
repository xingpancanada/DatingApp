using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            // // var username = resultContext.HttpContext.User.GetUserName();
            // var userId = resultContext.HttpContext.User.GetUserId();
            // var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            // // var user = await repo.GetUserByUsernameAsync(username);
            // var user = await repo.GetUserByIdAsync(userId);
            // user.LastActive = DateTime.UtcNow;
            // await repo.SaveAllAsync();

            var userId = resultContext.HttpContext.User.GetUserId();
            var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await uow.UserRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await uow.Complete();
        }
    }
}