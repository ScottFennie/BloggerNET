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

    internal List<Comment> GetByBlogId(int blogId)
    {
      var sql = @"
      SELECT *
      FROM comments c
      WHERE c.blogId = @blogId
      ";
      return _db.Query<Comment>(sql, new { blogId }).ToList();
    }


    public Comment Post(Comment commentData)
    {

      var sql = @"
        INSERT INTO comments(
          body,
          creatorId
        )
        VALUES (
          @Body,
          @CreatorId
        );
        SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, commentData);
      commentData.Id = id;
      return commentData;
    }

    public Comment Edit(int id, Comment commentData)
    {
      commentData.Id = id;
      var sql = @"
        UPDATE comments
        SET
        body = @Body,
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