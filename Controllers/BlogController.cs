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
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
    private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

    [HttpGet]

   public ActionResult<List<Blog>> GetAll()
   {
     try
     {
        return Ok(_blogService.GetAll());
     }
     catch (System.Exception e)
     {
        return BadRequest(e.Message);
     }
   }

     [HttpGet("{blogId}")]

    public ActionResult<Blog> GetById(int blogId)
    {
      try
      {
          return Ok(_blogService.GetById(blogId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Blog>> Post([FromBody] Blog blogData)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          // for node reference - req.body.creatorId = req.userInfo.id
          // FIXME NEVER TRUST THE CLIENT
          blogData.CreatorId = userInfo.Id;
          Blog createdBlog = _blogService.Post(blogData);
          return createdBlog;
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }


    [Authorize]
    [HttpDelete("{blogId}")]

    public async Task<ActionResult<string>> RemoveBLog(int blogId)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          _blogService.RemoveBlog(blogId, userInfo.Id);
          return Ok("Your blog was deleted successfully");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

     [HttpPut("{blogId}")]
    public ActionResult<Blog> EditBlog(int blogId, [FromBody] Blog blogData)
    {
      try
      {
        var blog = _blogService.Edit(blogId, blogData);
        return Ok(blog);
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    }
}