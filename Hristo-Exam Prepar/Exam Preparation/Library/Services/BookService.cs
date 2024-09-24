namespace Library.Services
{
    using Library.Contracts;
    using Library.Data;
    using Library.Data.Models;
    using Library.Models.Book;
    using Microsoft.EntityFrameworkCore;
    using static Extensions.FormattingMethods;


    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddBookToUserCollectionAsync(string userId, int bookId)
        {
            IdentityUserBook userBook = new IdentityUserBook()
            {
                CollectorId = userId,
                BookId = bookId
            };

            await dbContext.IdentitiesUsers
                         .AddAsync(userBook);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveBookFromUserCollectionAsync(string userId, int bookId)
        {
            IdentityUserBook? bookToRemove = await dbContext.IdentitiesUsers
                                                  .FirstOrDefaultAsync(b => b.CollectorId == userId && b.BookId == bookId);

            if (bookToRemove != null)
            {
                dbContext.IdentitiesUsers.Remove(bookToRemove);
                await dbContext.SaveChangesAsync();
            }

        }
        public async Task<bool> DoesUserHaveBookAsync(string userId, int bookId)
        {
            return await dbContext.IdentitiesUsers
                         .AnyAsync(b => b.CollectorId == userId && b.BookId == bookId);
        }

        public async Task<ICollection<AllViewBookModel>> GetAllAsync()
        {
            return await dbContext.Books
                         .AsNoTracking()
                         .Select(b => new AllViewBookModel
                         {
                             Id = b.Id,
                             Author = b.Author,
                             Category = b.Category.Name,
                             Title = b.Title,
                             Rating = b.Rating.ToString(),
                             ImageUrl = b.ImageUrl,
                         })
                         .ToListAsync();
        }

        public async Task<ICollection<UserBookViewModel>> GetUserBooksAsync(string userId)
        {
            return await dbContext.IdentitiesUsers
                         .AsNoTracking()
                         .Where(u => u.CollectorId == userId)
                         .Select(b => new UserBookViewModel
                         {
                             Id = b.Book.Id,
                             Author = b.Book.Author,
                             Category = b.Book.Category.Name,
                             Title = b.Book.Title,
                             ImageUrl = b.Book.ImageUrl,
                             Description = b.Book.Description
                         })
                         .ToListAsync();

        }

        public async Task AddBookAsync(BookFormViewModel model)
        {
            Book bookToAdd = new Book()
            {
                Author = model.Author,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Description = model.Description,
                Rating = DecimalToGlobalStandard(model.Rating),
                ImageUrl = model.Url
            };

            await dbContext.AddAsync(bookToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(int id,BookFormViewModel model)
        {
            Book bookToEdit = await dbContext.Books
                              .FirstAsync(b => b.Id == id);

            bookToEdit.Author = model.Author;
            bookToEdit.Rating = DecimalToGlobalStandard(model.Rating);
            bookToEdit.CategoryId = model.CategoryId;
            bookToEdit.Description = model.Description;
            bookToEdit.ImageUrl = model.Url;
            bookToEdit.Title = model.Title;

            dbContext.SaveChangesAsync();
        }

        public async Task<BookFormViewModel> GetByIdAync(int id)
        {
            return await dbContext.Books
                         .Where(u => u.Id == id)
                         .Select(u => new BookFormViewModel
                         {
                             Author = u.Author,
                             CategoryId = u.CategoryId,
                             Title = u.Title,
                             Description = u.Description,
                             Rating = u.Rating.ToString(),
                             Url = u.ImageUrl
                            
                         })
                         .FirstAsync();
        }
    }
}
