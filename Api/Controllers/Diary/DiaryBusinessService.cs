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
        /// Returns the referenced diary entry
        /// </summary>
        /// <remarks>Throws error if it does not belong to the current user</remarks>
        public async Task<(bool hasErrors, string errorMessage, DiaryEntryViewModel result)> GetById(long id)
        {
            var entry = await _databaseContext
                .Set<DiaryEntry>()
                .Include(x => x.DiaryImages)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == _userBusinessService.CurrentUserId);
            if (entry == null)
            {
                return (true, "ERROR_DIARY_ENTRY_NOT_FOUND", null);
            }

            return (false, null, BuildViewModelFromDiaryEntry(entry));
        }

        /// <summary>
        /// Returns all diary entries of the current user
        /// </summary>
        public async Task<IEnumerable<DiaryEntryViewModel>> GetAllEntriesForCurrentUser()
        {
            return await _databaseContext
                .Set<DiaryEntry>()
                .Include(x => x.DiaryImages)
                .Where(x => x.UserId == _userBusinessService.CurrentUserId)
                .Select(x => BuildViewModelFromDiaryEntry(x))
                .ToListAsync();
        }

        /// <summary>
        /// Used to create new diary entries
        /// </summary>
        public async Task<DiaryEntryViewModel> CreateNewEntry(DiaryEntryCreateModel createModel)
        {
            var diaryEntry = new DiaryEntry()
            {
                EventAt = createModel.EventAt,
                Description = createModel.Description,
                UserId = _userBusinessService.CurrentUserId
            }.SetCreated<DiaryEntry>(_userBusinessService.CurrentUserName);

            await _databaseContext
                 .Set<DiaryEntry>()
                 .AddAsync(diaryEntry);

            var diaryImages = createModel.Images?.Select(x => new DiaryImage
            {
                ImageFileName = x.ImageFileName,
                ImageFile = x.ImageFile,
                DiaryEntryId = diaryEntry.Id
            }.SetCreated<DiaryImage>(_userBusinessService.CurrentUserName));

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

        /// <summary>
        /// Used to modify an existing diary entry
        /// </summary>
        public async Task<(bool hasErrors, string errorMessage, DiaryEntry result)> ModifyEntry(DiaryEntryModifyModel modifyModel)
        {
            var entry = await _databaseContext
                .Set<DiaryEntry>()
                .Include(x => x.DiaryImages)
                .FirstOrDefaultAsync(x => x.Id == modifyModel.Id && x.UserId == _userBusinessService.CurrentUserId);
            if (entry == null)
            {
                return (true, "ERROR_DIARY_ENTRY_NOT_FOUND", null);
            }

            entry.Description = modifyModel.Description;
            entry.EventAt = modifyModel.EventAt;
            entry.UpdateModified<DiaryEntry>(_userBusinessService.CurrentUserName);

            if (modifyModel.ShouldUpdateImages)
            {
                await UpdateDiaryImages(entry.DiaryImages, modifyModel.Images, entry.Id, _userBusinessService.CurrentUserName);
            }

            await _databaseContext.SaveChangesAsync();

            return (false, null, entry);
        }

        private async Task UpdateDiaryImages(
            ICollection<DiaryImage> diaryImages,
            IEnumerable<DiaryImageModifyModel> updatedImages,
            long diaryEntryId,
            string currentUserName)
        {
            updatedImages ??= new List<DiaryImageModifyModel>();

            var imagesToDelete = diaryImages.Where(x => !updatedImages.Any(y => y.Id == x.Id));
            var imagesToAdd = updatedImages.Where(x => !diaryImages.Any(y => y.Id == x.Id));
            var imagesToModify = diaryImages.Where(x => updatedImages.Any(y => y.Id == x.Id));

            var diaryImageSet = _databaseContext.Set<DiaryImage>();

            // Delete
            diaryImageSet.RemoveRange(imagesToDelete);

            // Add
            await diaryImageSet.AddRangeAsync(imagesToAdd.Select(x => new DiaryImage
            {
                ImageFileName = x.ImageFileName,
                ImageFile = x.ImageFile,
                DiaryEntryId = diaryEntryId
            }.SetCreated<DiaryImage>(currentUserName)));

            // Modify
            foreach (var image in imagesToModify)
            {
                var updatedImage = updatedImages.FirstOrDefault(x => x.Id == image.Id);
                image.ImageFileName = updatedImage.ImageFileName;
                image.ImageFile = updatedImage.ImageFile;
                image.UpdateModified<DiaryImage>(currentUserName);
            }
        }

        private static DiaryEntryViewModel BuildViewModelFromDiaryEntry(DiaryEntry x)
        {
            if (x == null) return null;

            return new DiaryEntryViewModel
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
            };
        }
    }
}
