using ForumApp.Infrastructure.Data.Models;
using ForumAppGm.Core.Models;

namespace ForumAppGm.Core.Contract
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
        Task CreateAsync(PostModel model);
        Task<PostModel> GetPostModel(string id);

        Task<Post> GetPostByIdAsync(string id);
        Task<bool> Delete(string id);
    }
}
