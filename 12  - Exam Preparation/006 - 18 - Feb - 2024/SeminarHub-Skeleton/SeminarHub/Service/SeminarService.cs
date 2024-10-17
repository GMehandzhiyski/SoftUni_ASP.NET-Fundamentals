using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using SeminarHub.Service.Contract;
using static SeminarHub.Common.DateConstants;

namespace SeminarHub.Service
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext context;

        public SeminarService(SeminarHubDbContext _context)
        {
            context = _context;
        }

        public async Task DeleteSeminarAsync(int currSeminarId)
        {
            Seminar? deleteSeminar = context.Seminars
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Id == currSeminarId)
                .FirstOrDefault();

            if (deleteSeminar != null)
            {
                deleteSeminar.IsDeleted = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<SeminarDeleteVIewModel?> GetSeminarForDeleting(int seminarId)
        {
            return await context.Seminars
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Id == seminarId)
                .Select(s => new SeminarDeleteVIewModel
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime,
                    OrganizerId = s.OrganizerId
                })
                .FirstOrDefaultAsync();

        }


        public async Task<SeminarDetailsViewModel?> GetDetailsAsync(int seminarId)
        {
            return await context.Seminars
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Id == seminarId)
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(DateFormatType),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditSeminarAsync(int seminarId,SeminarAddFormModel model, DateTime dateAndTime)
        {
            Seminar? currSeminar = await context.Seminars
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Id == seminarId)
                .FirstOrDefaultAsync();

            if(currSeminar != null)
            {
                currSeminar.Topic = model.Topic;
                currSeminar.Lecturer = model.Lecturer;
                currSeminar.Details = model.Lecturer;
                currSeminar.DateAndTime = dateAndTime;
                currSeminar.Duration = model.Duration;
                currSeminar.CategoryId = model.CategoryId;

                await context.SaveChangesAsync();
            }

        }

        public async Task AddSeminarAsync(SeminarAddFormModel model,DateTime dateAndTime,string creatorId)
        {
            Seminar newSeminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = creatorId,
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
            };

            await context.Seminars.AddAsync(newSeminar);
            await context.SaveChangesAsync();
        }

        public async Task<SeminarAddFormModel?> GetSeminarAsync(int seminarId)
        {
            return await context.Seminars
                .Where(s => s.IsDeleted == false)
                .Where(s => s.Id == seminarId)
                .Select(s => new SeminarAddFormModel()
                {
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    DateAndTime = s.DateAndTime.ToString(DateFormatType),
                    Duration = s.Duration,
                    CategoryId = s.CategoryId,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync()
        {
            return await context.Seminars
                .Where(s => s.IsDeleted == false)
                .Select(s => new SeminarAllViewModel
                {
                    Id= s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName,
                    DateAndTime = s.DateAndTime.ToString(DateFormatType),
                    Category = s.Category.Name
                })
                .ToListAsync();
        }

        public async Task<bool> IsUserOwnerAsync(int seminarId, string organizerId)
        {
            return await context.Seminars
                .AnyAsync(s => s.Id == seminarId
                               && s.OrganizerId == organizerId);
        }

        public async Task<ICollection<SeminarCategoryViewModel>> GetCategoriesAsync() 
        {
            return await context.Categories
                .Select(c => new SeminarCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,  
                })
                .ToListAsync();
        }

        public async Task<bool> IsCategoryValid(int categoryId)
        {
            return await context.Categories
                .AnyAsync(s => s.Id == categoryId);
        }

    }
}
