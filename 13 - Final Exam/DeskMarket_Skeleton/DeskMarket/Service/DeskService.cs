using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using DeskMarket.Service.Contract;
using Microsoft.EntityFrameworkCore;
using static DeskMarket.Common.DateConstants;

namespace DeskMarket.Service
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext context;

        public DeskService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task RemoveProductFromCart(int productId, string userId)
        {
            ProductsClients? productsClients = await context.ProductsClients
                .Where(pc => pc.ProductId == productId
                             && pc.ClientId == userId)
                .FirstOrDefaultAsync();

            if (productsClients != null)
            {
                context.ProductsClients.Remove(productsClients);
                await context.SaveChangesAsync();   
            }
        }

        public async Task AddProductToCart(int productId, string userId)
        {
            ProductsClients newProductsClients = new ProductsClients()
            {
                ProductId = productId,
                ClientId = userId,
            };

            await context.ProductsClients.AddAsync(newProductsClients);
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<DeskCartViewModel>> GetCartAsync(string userId)
        {
            return await context.ProductsClients
                .Where(pc => pc.ClientId == userId)
                .Where(pc => pc.Product.IsDeleted == false)
                .Select(pc => new DeskCartViewModel()
                {
                    Id = pc.Product.Id,
                    ProductName = pc.Product.ProductName,
                    Price = pc.Product.Price,
                    ImageUrl = pc.Product.ImageUrl,
                })
                .ToListAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product? productDelete = await context.Products
                .Where(g => g.IsDeleted == false)
                .Where(s => s.Id == productId)
                .FirstOrDefaultAsync();

            if (productDelete != null)
            {
                productDelete.IsDeleted = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<DeskDeleteViewModel?> GetProductForDelete(int productId)
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == productId)
                .Select(p => new DeskDeleteViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Seller = p.Seller.UserName,
                    SellerId = p.SellerId,

                })
                .FirstOrDefaultAsync();

        }

        public async Task EditProductAsync(int productId, DeskAddFormModel model, DateTime addedOn)
        {
            Product? currProduct = await context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (currProduct != null)
            {
                currProduct.ProductName = model.ProductName;
                currProduct.Description = model.Description;
                currProduct.Price = decimal.Parse(model.Price);
                currProduct.ImageUrl = model.ImageUrl;
                currProduct.AddedOn = addedOn;
                currProduct.CategoryId = model.CategoryId;

                await context.SaveChangesAsync();
            }
        }

        public async Task<DeskAddFormModel?> GetProductEditAsync(int productId)
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == productId)
                .Select(p => new DeskAddFormModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price.ToString(),
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString(DateFormatType),
                    CategoryId = p.CategoryId,
                    SellerId = p.SellerId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeskDetailsViewModel?> GetDetailsAsync(int productId, string userName, string userId)
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == productId)
                .Select(p => new DeskDetailsViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString(DateFormatType),
                    CategoryName = p.Category.Name,
                    Seller = userName,
                    HasBought = p.ProductsClients.Any(pp => pp.ProductId == p.Id && pp.ClientId == userId),

                })
                .FirstOrDefaultAsync();
        }

        public async Task AddProductAsync(DeskAddFormModel model, DateTime addedOn, string userId)
        {
            Product newProduct = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = decimal.Parse(model.Price),
                ImageUrl = model.ImageUrl,
                SellerId = userId,
                AddedOn = addedOn,
                CategoryId = model.CategoryId,
            };

            await context.Products.AddAsync(newProduct);
            await context.SaveChangesAsync();
        }


        public async Task<ICollection<DeskIndexViewModel>> GetAllProductsAuthenticateAsync(string userId)
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new DeskIndexViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IsSeller = p.SellerId == userId,
                    HasBought = p.ProductsClients.Any(pp => pp.ProductId == p.Id && pp.ClientId == userId),
                })
                .ToListAsync();
        }


        public async Task<ICollection<DeskIndexViewModel>> GetAllProductsUnAuthenticateAsync()
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new DeskIndexViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IsSeller = false,
                    HasBought = false,
                })
                .ToListAsync();
        }

        public async Task<ICollection<DeskCategoryViewModel>> GetCategoryAsync()
        {
            return await context.Categories
                .Select(c => new DeskCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> IsCategoryValid(int categoryId)
        {
            return await context.Categories
                  .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> IsUserOwner(int productId, string userId)
        {
            return await context.Products
                .Where(p => p.IsDeleted == false)
                .AnyAsync(p => p.Id == productId
                            && p.SellerId == userId);
        }

        public async Task<bool> IsUserHaveProduct(int productId, string userId)
        {
            return await context.ProductsClients
                .AnyAsync(gg => gg.ProductId == productId
                                && gg.ClientId == userId);
        }
    }
}
