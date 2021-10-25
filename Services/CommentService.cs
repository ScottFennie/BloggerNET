using System;
using System.Collections.Generic;
using BloggerDotNet.Repositories;

namespace BloggerDotNet.Services
{
    public class CommentsService
    {
    private readonly CommentsRepository _commentsRepository;
    private readonly BlogService _blogService;

        public CommentsService(CommentsRepository commentsRepository, BlogService blogService)
        {
            _commentsRepository = commentsRepository;
            _blogService = blogService;
        }

    // Gets all comments by the blogId
    public List<Comment> GetByBlogId(int blogId)
    {
      var blog = _blogService.GetById(blogId);
      return _commentsRepository.GetByBlogId(blogId);
    }
     public Comment GetById(int commentId)
    {
      return _commentsRepository.GetById(commentId);
    }

    public Comment Post(Comment commentData)
    {
      return _commentsRepository.Post(commentData);
    }

    public void RemoveComment(int commentId, string userId)
    {
      Comment foundComment = GetById(commentId);
      if(foundComment.CreatorId != userId)
      {
        throw new Exception("That is not your comment");
      }
      _commentsRepository.RemoveComment(commentId);
    }

    public Comment Edit(int commentId, Comment commentData)
    {
      var comment = GetById(commentId);

      comment.Body = commentData.Body?? comment.Body;
      // something here aka save
      _commentsRepository.Edit(commentId, commentData);
      return comment;
    }
    }
}