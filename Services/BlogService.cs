using System;
using System.Collections.Generic;
using BloggerDotNet.Repositories;

namespace BloggerDotNet.Services
{
    public class BlogService
    {
    private readonly BlogRepository _blogRepository;

        public BlogService(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
          
        }

    public List<Blog> GetAll()
    {
      return _blogRepository.GetAll();
    }
    public List<Blog> GetAllByAccount(string userId)
    {
      return _blogRepository.GetAllByAccount(userId);
    }

     public Blog GetById(int blogId)
    {
      Blog foundBlog = _blogRepository.GetById(blogId);
      if(foundBlog == null)
      {
        throw new Exception("Sorry there is no blog post by that Id :(");
      }
      return foundBlog;
    }


    public Blog Post(Blog blogData)
    {
      return _blogRepository.Post(blogData);
    }

    public void RemoveBlog(int blogId, string userId)
    {
      Blog foundBlog = GetById(blogId);
      if(foundBlog.CreatorId != userId)
      {
        throw new Exception("That is not your blog");
      }
      _blogRepository.RemoveBlog(blogId);
    }

    public Blog Edit(int blogId, Blog blogData)
    {
      var blog = GetById(blogId);

      blog.Title = blogData.Title?? blog.Title;
      blog.Body = blogData.Body ?? blog.Body;
      blog.ImgUrl = blogData.ImgUrl ?? blog.ImgUrl;
      blog.Published = blogData.Published;
      // something here aka save
      _blogRepository.Edit(blogId, blogData);
      return blog;
    }

        internal object GetAllByAccount(object id)
        {
            throw new NotImplementedException();
        }
    }
}