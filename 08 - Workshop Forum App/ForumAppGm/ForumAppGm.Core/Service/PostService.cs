using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
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
        public async Task CreateAsync(PostModel model)
        {
            Post post = new Post()
            {
                Title = model.Title,
                Content = model.Content,
            };

            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
        }


        public async Task<PostModel> GetPostModel(string id)
        {
           Post model = await GetPostByIdAsync(id);

            PostModel postModel = new PostModel()
            {
                Title = model.Title,
                Content = model.Content
            };

            return postModel;
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            Post currPost = await context.Posts.FirstOrDefaultAsync(p => p.Id.ToString() == id);
            return currPost;
        }

        public async Task<bool> Delete(string id)
        {
            Post postModel = await GetPostByIdAsync(id);

            if (postModel != null)
            {
                context.Posts.Remove(postModel);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
