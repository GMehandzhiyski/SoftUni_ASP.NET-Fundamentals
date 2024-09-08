using ForumApp.Infrastructure.Data;
using ForumAppGm.Core.Contract;
using ForumAppGm.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumAppGm.Core.Service
{
    public class PostService : IPostService
    {
        private readonly ForumAppDbContext context;

        public PostService(ForumAppDbContext _context)
        {
            context = _context; 
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
        {
          return await context.Posts
                .Select (p => new PostModel
                { 
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,    
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
