using Forum.Core.Contracts;
using Forum.Core.Models.Post;
using Forum.Infrastructure;
using Forum.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Forum.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext _context;
        public PostService(ForumDbContext context) 
        {
            this._context = context;
        }

    

        public async Task<ICollection<PostViewModel>> GetAllAsync()
        {
            var allPosts = await _context.Posts
                   .AsNoTracking()
                   .Select(p => new PostViewModel
                   {
                       Id = p.Id.ToString(),
                       Title = p.Title,
                       Content = p.Content
                   }
                   )
                   .ToListAsync();

            return allPosts;
        }

        public async Task CreateAsync(PostFormModel model)
        {
            Post postToAdd = new Post()
            {
                Content = model.Content,
                Title = model.Title,
            };

            await _context.Posts.AddAsync(postToAdd);
            await _context.SaveChangesAsync();

            
        }

        public async Task<Post> GetByIdAsync(string id)
        {
            
            var getPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id.ToString() == id);
            return getPost;
         
        }

        public async Task<bool> UpdateAsync(string id, PostFormModel model)
        {
            var postToEdit = await GetByIdAsync(id);

            if (postToEdit != null)
            {
                postToEdit.Title = model.Title;
                postToEdit.Content = model.Content;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;   
           
        }

        public async Task<bool> Delete(string id)
        {
            Post postToDelete = await GetByIdAsync(id);

            if (postToDelete != null)
            {
                _context.Posts.Remove(postToDelete);

                await _context.SaveChangesAsync();
                return true;
            }
           
            return false;
        }
    }
}


