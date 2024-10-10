using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeminarHub.Contract;
using SeminarHub.Controllers;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using static SeminarHub.Common.DateConstants;

namespace SeminarHub.Service
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext context;

        public SeminarService(SeminarHubDbContext _contex)
        {
            context = _contex;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoryAsync()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                   Id = c.Id,
                   Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<bool>IsCategoryValidAsync(int categoryId)
        {
            return await context.Categories
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task AddAsync(AddFormModel model, DateTime dateAndTime, string organizerId)
        {
            Seminar newSeminar = new Seminar()
            {
                //Id= model.Id,
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = organizerId,
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
            };

            await context.Seminars.AddAsync(newSeminar);    
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<AllViewModel>> GetAllSeminarsAsync()
        {
            return await context.Seminars
                .AsNoTracking()
                .Select(c => new AllViewModel()
                { 
                    Id = c.Id,
                    Topic = c.Topic,
                    Lecturer = c.Lecturer,
                    Category = c.Category.Name,
                    DateAndTime = c.DateAndTime.ToString(DateFormatConst), 
                    Organizer = c.Organizer.UserName
                })
                .ToListAsync();
        }

        public async Task JoinToCurrentSeminar(int seminarId, string organiserId)
        {
            SeminarParticipant newPracticant = new SeminarParticipant()
            {
                SeminarId = seminarId,
                ParticipantId = organiserId,
            };

            await context.AddRangeAsync(newPracticant);
            await context.SaveChangesAsync();   
        }

        public async Task<IEnumerable<JoinedVIewModel>> JoinedAsync(string userId)
        {
            return await context.SeminarParticipants
                .Where(sp => sp.ParticipantId == userId)
                .Select(sp => new JoinedVIewModel()
                {
                    Id = sp.Seminar.Id,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(DateFormatConst),
                    Organizer = sp.Seminar.Organizer.UserName
                })
                .ToListAsync();
        }

        public async Task<bool> IsHaveSeminar(int seminarId, string userId)
        {
            return await context.SeminarParticipants
                         .AnyAsync(sp => sp.SeminarId == seminarId
                                        && sp.ParticipantId == userId);
        }

        public async Task LeaveSeminarAsync(int seminarId, string userId)
        {

            var currSeminar = await context.SeminarParticipants
                .Where(sp => sp.SeminarId == seminarId
                              && sp.ParticipantId == userId)
                .FirstOrDefaultAsync();

            if (currSeminar != null)
            {
                context.SeminarParticipants.Remove(currSeminar);
                await context.SaveChangesAsync();   
            }
        
        }

        public async Task<bool> IsUserIsOwnerAsync(int seminarId, string userId)
        {
            return await context.Seminars
                .AnyAsync(s => s.Id == seminarId
                                && s.OrganizerId == userId);
        }

        public async Task<AddFormModel> EditGetSeminatAsync(int seminarId)
        {
            return await context.Seminars
                .AsNoTracking()
                .Where(s => s.Id == seminarId)
                .Select(s => new AddFormModel()
                {
                  Topic = s.Topic,
                  Lecturer = s.Lecturer,
                  Details = s.Details,
                  DateAndTime = s.DateAndTime.ToString(DateFormatConst),
                  Duration = s.Duration,
                  CategoryId = s.CategoryId,
                })
                .FirstOrDefaultAsync();
        
        }
    }
}
