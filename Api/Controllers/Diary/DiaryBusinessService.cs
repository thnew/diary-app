using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.BusinessServices;
using Api.Controllers.Diary.Models;
using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Diary
{
    public class DiaryBusinessService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserBusinessService _userBusinessService;

        public DiaryBusinessService(
            DatabaseContext databaseContext,
            UserBusinessService userBusinessService)
        {
            _databaseContext = databaseContext;
            _userBusinessService = userBusinessService;
        }

        /// <summary>
        /// Returns all diary entries of the current user
        /// </summary>
        public async Task<IEnumerable<DiaryEntryViewModel>> GetAllEntriesForCurrentUser()
        {
            var currentUserId = _userBusinessService.GetCurrentUserId();

            return await _databaseContext
                .Set<DiaryEntry>()
                .Include(x => x.DiaryImages)
                .Where(x => x.UserId == currentUserId)
                .Select(x => new DiaryEntryViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    EventAt = x.EventAt,
                    Images = x.DiaryImages.Select(image => new DiaryImageViewModel
                    {
                        Id = image.Id,
                        ImageFileName = image.ImageFileName,
                        ImageFile = image.ImageFile
                    })
                })
                .ToListAsync();
        }

        /// <summary>
        /// Used to create new diary entries
        /// </summary>
        public async Task<DiaryEntryViewModel> CreateNewEntry(DiaryEntryCreateModel createModel)
        {
            var currentUserId = _userBusinessService.GetCurrentUserId();
            var currentUserName = _userBusinessService.GetCurrentUserName();

            var diaryEntry = new DiaryEntry()
            {
                EventAt = createModel.EventAt,
                Description = createModel.Description,
                UserId = currentUserId
            }.SetCreated<DiaryEntry>(currentUserName);

            await _databaseContext
                 .Set<DiaryEntry>()
                 .AddAsync(diaryEntry);

            var diaryImages = createModel.Images?.Select(x => new DiaryImage
            {
                ImageFileName = x.ImageFileName,
                ImageFile = x.ImageFile,
                DiaryEntryId = diaryEntry.Id
            }.SetCreated<DiaryImage>(currentUserName));

            if (diaryImages != null)
            {
                await _databaseContext
                     .Set<DiaryImage>()
                     .AddRangeAsync(diaryImages);
            }

            await _databaseContext.SaveChangesAsync();

            return new DiaryEntryViewModel
            {
                Id = diaryEntry.Id,
                EventAt = diaryEntry.EventAt,
                Description = diaryEntry.Description,
                Images = diaryImages?.Select(x => new DiaryImageViewModel
                {
                    Id = x.Id,
                    ImageFileName = x.ImageFileName,
                    ImageFile = x.ImageFile
                })
            };
        }
    }
}
