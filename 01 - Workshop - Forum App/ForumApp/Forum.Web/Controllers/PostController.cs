using Forum.Core.Contracts;
using Forum.Core.Models.Post;
using Forum.Core.Services;
using Forum.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var allPosts = await _postService.GetAllAsync();

                return View(allPosts);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult Add()
        {
            return View(new PostFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            try
            {
                await _postService.CreateAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }

        public async Task<IActionResult> Edit(string id)
        {
            Post model = await _postService.GetByIdAsync(id);

            PostFormModel formModel = new PostFormModel()
            {
                Title = model.Title,
                Content = model.Content
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            try
            {
                bool success = await _postService.UpdateAsync(id, post);
                if (success)
                {
                    TempData["Message"] = "Post edited successfully";
                    return View(post);
                }
                else
                {
                    TempData["Message"] = "Post not found";
                    return View(post);
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(ex.Message);
            }      
        }

        public async Task<IActionResult> Delete(string id)
        {
            Post model = await _postService.GetByIdAsync(id);

            PostFormModel formModel = new PostFormModel()
            {
                Title = model.Title,
                Content = model.Content
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, PostFormModel model)
        {

            try
            {
                var  success = await _postService.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
