// using System;
// using System.Collections.Generic;
// using BloggerDotNet.Repositories;

// namespace BloggerDotNet.Services
// {
//     public class BlogService
//     {
//     private readonly BlogRepository _blogRepository;

//         public BlogService(BlogRepository blogRepository)
//         {
//             _blogRepository = blogRepository;
//         }

//     public List<Blog> GetAll()
//     {
//       return _blogRepository.GetAll();
//     }

//      public Blog GetById(int blogId)
//     {
//       Blog foundBlog = _blogRepository.GetById(blogId);
//       if(foundBlog == null)
//       {
//         throw new Exception("Done didn't find no player by that id");
//       }
//       return foundBlog;
//     }

//     public Blog Post(Blog blogData)
//     {
//       return _blogRepository.Post(blogData);
//     }

//     public void RemoveBlog(int blogId, string userId)
//     {
//       Blog foundBlog = GetById(blogId);
//       if(foundBlog.CreatorId != userId)
//       {
//         throw new Exception("That aint your team");
//       }
//       _blogRepository.RemoveBlog(blogId);
//     }


//     }
// }