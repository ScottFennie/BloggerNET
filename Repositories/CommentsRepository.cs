using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BloggerDotNet.Repositories
{
    public class CommentsRepository
    {
        private readonly IDbConnection _db;

        public CommentsRepository(IDbConnection db)
        {
            _db = db;
        }

 
    public Comment GetById(int id)
    {
      return _db
        .QueryFirstOrDefault<Comment>("SELECT * FROM comments WHERE id = @id", new { id });
    }

    internal List<Comment> GetByBlogId(int Blog)
    {
      var sql = @"
      SELECT *
      FROM comments c
      WHERE c.blog = @Blog
      ";
      return _db.Query<Comment>(sql, new { Blog }).ToList();
    }
    internal List<Comment> GetAllCommentsByAccount(string uId)
    {
      string sql = @"
      SELECT * FROM comments WHERE creatorId = @uId;
      ";
      return _db.Query<Comment>(sql, new {uId}).ToList();
    }


    public Comment Post(Comment commentData, int blogId)
    {

      var sql = @"
        INSERT INTO comments(
          body,
          creatorId,
          blog
        )
        VALUES (
          @Body,
          @CreatorId,
          @Blog
        );
        SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, commentData);
      commentData.Id = id;
      // commentData.Blog = blogId;
      return commentData;
    }

    public Comment Edit(int id, Comment commentData)
    {
      commentData.Id = id;
      var sql = @"
        UPDATE comments
        SET
        body = @Body
        WHERE id = @Id;
      ";

      _db.Execute(sql, commentData);
      return commentData;
    }

    public void RemoveComment(int id)
    {
      var rowsAffected = _db.Execute("DELETE FROM comments WHERE id = @id", new { id });
    }


    }
}