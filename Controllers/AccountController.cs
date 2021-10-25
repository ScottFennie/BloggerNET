using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggerDotNet.Models;
using BloggerDotNet.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggerDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly BlogService _blogService;

        private readonly CommentsService _commentsService;

        public AccountController(AccountService accountService, BlogService blogService, CommentsService commentsService)
        {
            _accountService = accountService;
            _blogService = blogService;
            _commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    [HttpGet("blogs")]
    public async Task<ActionResult<List<Blog>>> GetAllByAccountAsync()
   {
     try
     {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_blogService.GetAllByAccount(userInfo.Id));
     }
     catch (System.Exception e)
     {
        return BadRequest(e.Message);
     }
   }
    [HttpGet("comments")]
    public async Task<ActionResult<List<Comment>>> GetAllCommentsByAccountAsync()
   {
     try
     {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_commentsService.GetAllCommentsByAccount(userInfo.Id));
     }
     catch (System.Exception e)
     {
        return BadRequest(e.Message);
     }
   }


    }

}