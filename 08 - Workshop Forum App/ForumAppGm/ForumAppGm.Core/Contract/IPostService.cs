using ForumAppGm.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAppGm.Core.Contract
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
    }
}
