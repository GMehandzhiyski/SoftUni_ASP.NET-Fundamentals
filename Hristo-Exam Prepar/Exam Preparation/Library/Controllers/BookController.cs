namespace Library.Controllers
{
    using Library.Contracts;
    using Library.Extensions;
    using Library.Models.Book;
    using Library.Models.Category;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Common.ValidationConstants.Book;
    using static Extensions.FormattingMethods;

    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;

        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> All()
        {
            try
            {
                var books = await bookService.GetAllAsync();
                return View(books);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        public async Task<IActionResult> Mine()
        {
            try
            {
                var currUserId = User.GetId();

                var books = await bookService.GetUserBooksAsync(currUserId);

                return View(books);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
           
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            try
            {
                var currUserId = User.GetId();

                bool userHaveBook = await bookService.DoesUserHaveBookAsync(currUserId, id);

                if(userHaveBook)
                {
                    return RedirectToAction("All", "Book");
                }

                await bookService.AddBookToUserCollectionAsync(currUserId, id);

                return RedirectToAction("All", "Book");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            try
            {
                var currUserId = User.GetId();

                bool userHaveBook = await bookService.DoesUserHaveBookAsync(currUserId, id);

                if (!userHaveBook)
                {
                    return RedirectToAction("All", "Book");
                }

                await bookService.RemoveBookFromUserCollectionAsync(currUserId, id);

                return RedirectToAction("All", "Book");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                BookFormViewModel book = new BookFormViewModel();
                ICollection<CategoryViewModel> categories = await categoryService.GetAllAsync();
                book.Categories = categories;

                return View(book);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormViewModel bookModel)
        {
            try
            {
                var bookRating = DecimalToGlobalStandard(bookModel.Rating);

                if(bookRating < MinRating || bookRating > MaxRating)
                {
                    ICollection<CategoryViewModel> categories = await categoryService.GetAllAsync();
                    bookModel.Categories = categories;
                    ModelState.AddModelError("Rating", $"Rating must be between {MinRating} and {MaxRating}");
                    return View(bookModel);

                }

                if (!ModelState.IsValid)
                {
                    ICollection<CategoryViewModel> categories = await categoryService.GetAllAsync();
                    bookModel.Categories = categories;

                    return View(bookModel);
                }

                await bookService.AddBookAsync(bookModel);

                return RedirectToAction("All", "Book");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                BookFormViewModel bookModel = await bookService.GetByIdAync(id);
                ICollection<CategoryViewModel> categories = await categoryService.GetAllAsync();
                bookModel.Categories = categories;

                return View(bookModel);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormViewModel model)
        {
            try
            {
                await bookService.EditAsync(id,model);

                return RedirectToAction("All", "Book");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }


    }
}
