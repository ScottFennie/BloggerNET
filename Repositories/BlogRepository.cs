using System.Data;

namespace BloggerDotNet.Repositories
{
    public class BlogRepository
    {
        private readonly IDbConnection _db;

        public BlogRepository(IDbConnection db)
        {
            _db = db;
        }

        


    }
}