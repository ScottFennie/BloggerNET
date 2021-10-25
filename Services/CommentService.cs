using System;
using System.Collections.Generic;
using BloggerDotNet.Repositories;

namespace BloggerDotNet.Services
{
    public class CommentsService
    {
    private readonly CommentsRepository _commentsRepository;

        public CommentsService(CommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

    public List<Comment> GetAll()
    {
      return _commentsRepository.GetAll();
    }

     public Comment GetById(int commentId)
    {
      Comment foundComment = _commentsRepository.GetById(commentId);
      if(foundComment == null)
      {
        throw new Exception("Sorry there is no comment by that Id :(");
      }
      return foundComment;
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