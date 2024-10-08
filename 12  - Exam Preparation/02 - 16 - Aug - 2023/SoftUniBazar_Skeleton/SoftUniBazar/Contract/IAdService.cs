﻿using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;

namespace SoftUniBazar.Contract
{
    public interface IAdService
    {

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
        Task<IEnumerable<AdViewModel>> GetAllAdAsync();

        Task AddAsync(AdFormModel formModel, string creatorId);
        Task<bool> isCategoryValid(int categoryId);

        Task<bool> IsOwnerAdOwenAsync(int id, string userId);

        Task<AdFormModel> EditGetAsync(int id);

        Task EditPostAsync(AdFormModel model, int id);

        Task<IEnumerable<AdViewModel>> AllAdInMyCard(string userId);

        Task<bool> IsAdIsAlreadyAdToCart(string userId, int adId);

        Task AddAdToCartAsync(string userId, int adId);

        Task RemoveFromAdFromCart(int id, string userId);
    }
}
