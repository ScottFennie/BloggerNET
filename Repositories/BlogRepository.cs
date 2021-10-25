using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloggerDotNet.Models;
using Dapper;

namespace BloggerDotNet.Repositories
{
    public class BlogRepository
    {
        private readonly IDbConnection _db;

        public BlogRepository(IDbConnection db)
        {
            _db = db;
        }

    internal List<Blog> GetAll()
    {
      string sql = @"
      SELECT * FROM blogs;
      ";
      return _db.Query<Blog>(sql).ToList();
    }
    internal List<Blog> GetAllByAccount(string uId)
    {
      string sql = @"
      SELECT * FROM blogs WHERE creatorId = @uId;
      ";
      return _db.Query<Blog>(sql, new {uId}).ToList();
    }


   internal Blog GetById(int blogId)
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a on b.creatorId = a.id
      WHERE b.id = @blogId;
      ";
      return _db.Query<Blog, Account, Blog>(sql, (b, a) =>
      {
        b.Creator = a;
        return b;
      }, new{blogId}).FirstOrDefault();
    }

    public Blog Post(Blog blogData)
    {

      var sql = @"
        INSERT INTO blogs(
          title,
          body,
          imgUrl,
          published,
          creatorId
        )
        VALUES (
          @Title,
          @Body,
          @ImgUrl,
          @Published,
          @CreatorId
        );
        SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, blogData);
      blogData.Id = id;
      return blogData;
    }

    public Blog Edit(int id, Blog blogData)
    {
      blogData.Id = id;
      var sql = @"
        UPDATE blogs
        SET
          title = @Title,
          body = @Body,
          imgUrl = @ImgUrl,
          published = @Published
        WHERE id = @Id;
      ";

      _db.Execute(sql, blogData);
      return blogData;
    }

    public void RemoveBlog(int id)
    {
      var rowsAffected = _db.Execute("DELETE FROM blogs WHERE id = @id", new { id });
    }


    }
}