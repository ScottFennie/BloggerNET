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
    public class CommentsController : ControllerBase
    {
    private readonly CommentsService _commentsService;

        public CommentsController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]

  //  public ActionResult<List<Comment>> GetAll()
  //  {
  //    try
  //    {
  //       return Ok(_commentsService.GetAll());
  //    }
  //    catch (System.Exception e)
  //    {
  //       return BadRequest(e.Message);
  //    }
  //  }

     [HttpGet("{commentId}")]

    public ActionResult<Comment> GetById(int commentId)
    {
      try
      {
          return Ok(_commentsService.GetById(commentId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Comment>> Post([FromBody] Comment commentData)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          // for node reference - req.body.creatorId = req.userInfo.id
          // FIXME NEVER TRUST THE CLIENT
          commentData.CreatorId = userInfo.Id;
          Comment createdComment = _commentsService.Post(commentData);
          return createdComment;
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }


    [Authorize]
    [HttpDelete("{commentId}")]

    public async Task<ActionResult<string>> RemoveComment(int commentId)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          _commentsService.RemoveComment(commentId, userInfo.Id);
          return Ok("Your comment was deleted successfully");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
     [Authorize]
     [HttpPut("{commentId}")]
    public ActionResult<Comment> EditComment(int commentId, [FromBody] Comment commentData)
    {
      try
      {
        var comment = _commentsService.Edit(commentId, commentData);
        return Ok(comment);
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    }
}