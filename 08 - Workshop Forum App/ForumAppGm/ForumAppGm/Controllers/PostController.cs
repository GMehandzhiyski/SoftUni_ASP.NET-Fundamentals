using ForumApp.Infrastructure.Data.Models;
using ForumAppGm.Core.Contract;
using ForumAppGm.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumAppGm.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PostModel> allPosts = await postService.GetAllPostsAsync();

            return View(allPosts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new PostModel());
        }


        [HttpPost]
        public async Task<IActionResult> Add(PostModel model)
        {
            PostModel post = new PostModel()
            {
                Title = model.Title,
                Content = model.Content,
            };

            await postService.CreateAsync(post);
            return RedirectToAction(nameof(Index));
        }
    }
}
