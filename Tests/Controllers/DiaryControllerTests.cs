using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.BusinessServices;
using Api.Controllers.Diary;
using Api.Controllers.Diary.Models;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    [TestClass]
    public class DiaryControllerTests : TestBase
    {
        private readonly DiaryController _controller;

        public DiaryControllerTests()
        {
            var diaryBusinessService = (DiaryBusinessService)ServiceProvider.GetService(typeof(DiaryBusinessService));
            _controller = new DiaryController(diaryBusinessService);
        }

        [TestMethod]
        public async Task Should_Save_Diary_Entry_And_Get_Again()
        {
            // Arrange
            await DatabaseContext.Database.EnsureDeletedAsync();
            var diaryEntry = await _controller.CreateNewEntry(new DiaryEntryCreateModel
            {
                Description = "Testdescription",
                EventAt = new DateTime(2021, 1, 16, 10, 0, 0),
                Images = new List<DiaryImageCreateModel>
                {
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages One",
                        ImageFile = new byte[] { 1, 2, 3 }
                    },
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages Two",
                        ImageFile = new byte[] { 4, 5, 6, 7 }
                    },
                }
            });

            var userBusinessService = (UserBusinessService)ServiceProvider.GetService(typeof(UserBusinessService));

            // Act
            var result = await _controller.GetById(diaryEntry.Id) as OkObjectResult;
            var savedDiaryEntry = result.Value as DiaryEntryViewModel;

            // Assert
            Assert.AreEqual(diaryEntry.Description, savedDiaryEntry.Description);
            Assert.AreEqual(diaryEntry.EventAt, savedDiaryEntry.EventAt);
            Assert.AreEqual(diaryEntry.Images.Count(), savedDiaryEntry.Images.Count());

            foreach (var savedImage in diaryEntry.Images)
            {
                var foundImage = savedDiaryEntry.Images.First(x => x.ImageFileName == savedImage.ImageFileName);
                Assert.IsTrue(savedImage.ImageFile.SequenceEqual(foundImage.ImageFile));
            }

            var dbEntry = DatabaseContext
                .Set<DiaryEntry>()
                .FirstOrDefault(x => x.Id == savedDiaryEntry.Id);
            Assert.AreEqual(DateTime.Now.Year, dbEntry.CreatedAt.Year);
            Assert.AreEqual(DateTime.Now.Month, dbEntry.CreatedAt.Month);
            Assert.AreEqual(DateTime.Now.Day, dbEntry.CreatedAt.Day);
            Assert.AreEqual(userBusinessService.CurrentUserName, dbEntry.CreatedBy);
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public async Task Should_Modify_Diary_Entry(bool shouldUpdateImages)
        {
            // Arrange
            await DatabaseContext.Database.EnsureDeletedAsync();
            var diaryEntry = await _controller.CreateNewEntry(new DiaryEntryCreateModel
            {
                Description = "Testdescription",
                EventAt = new DateTime(2021, 1, 16, 10, 0, 0),
                Images = new List<DiaryImageCreateModel>
                {
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages One",
                        ImageFile = new byte[] { 1, 2, 3 }
                    },
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages Two",
                        ImageFile = new byte[] { 4, 5, 6, 7 }
                    }
                }
            });

            var modifyModel = new DiaryEntryModifyModel
            {
                Id = diaryEntry.Id,
                Description = "New Description",
                EventAt = new DateTime(2021, 1, 16, 15, 15, 15),
                ShouldUpdateImages = shouldUpdateImages,
                Images = new List<DiaryImageModifyModel>
                {
                    new DiaryImageModifyModel
                    {
                        ImageFileName = "New image",
                        ImageFile = new byte[] { 1, 2, 3 }
                    },
                    new DiaryImageModifyModel
                    {
                        ImageFileName = "Modified image",
                        ImageFile = new byte[] { 4, 5, 6, 7 }
                    },
                    new DiaryImageModifyModel
                    {
                        ImageFileName = "Removed image",
                        ImageFile = new byte[] { 8, 9 }
                    }
                }
            };

            var userBusinessService = (UserBusinessService)ServiceProvider.GetService(typeof(UserBusinessService));

            // Act
            var result = (OkObjectResult)await _controller.ModifyEntry(modifyModel);
            var savedDiaryEntry = result.Value as DiaryEntryViewModel;

            // Assert
            Assert.AreEqual(modifyModel.Description, modifyModel.Description);
            Assert.AreEqual(modifyModel.EventAt, modifyModel.EventAt);
            var expectedmImageCount = shouldUpdateImages ?
                modifyModel.Images.Count()
                : savedDiaryEntry.Images.Count();
            Assert.AreEqual(expectedmImageCount, modifyModel.Images.Count(), savedDiaryEntry.Images.Count());

            if (shouldUpdateImages)
            {
                foreach (var savedImage in modifyModel.Images)
                {
                    var foundImage = savedDiaryEntry.Images.First(x => x.ImageFileName == savedImage.ImageFileName);
                    Assert.IsTrue(savedImage.ImageFile.SequenceEqual(foundImage.ImageFile));
                }
            }
            else
            {
                foreach (var savedImage in diaryEntry.Images)
                {
                    var foundImage = savedDiaryEntry.Images.First(x => x.ImageFileName == savedImage.ImageFileName);
                    Assert.IsTrue(savedImage.ImageFile.SequenceEqual(foundImage.ImageFile));
                }
            }

            var dbEntry = DatabaseContext
                .Set<DiaryEntry>()
                .FirstOrDefault(x => x.Id == savedDiaryEntry.Id);
            Assert.AreEqual(DateTime.Now.Year, dbEntry.CreatedAt.Year);
            Assert.AreEqual(DateTime.Now.Month, dbEntry.CreatedAt.Month);
            Assert.AreEqual(DateTime.Now.Day, dbEntry.CreatedAt.Day);
            Assert.AreEqual(userBusinessService.CurrentUserName, dbEntry.CreatedBy);

            Assert.AreEqual(DateTime.Now.Year, dbEntry.ModifiedAt.Year);
            Assert.AreEqual(DateTime.Now.Month, dbEntry.ModifiedAt.Month);
            Assert.AreEqual(DateTime.Now.Day, dbEntry.ModifiedAt.Day);
            Assert.AreEqual(userBusinessService.CurrentUserName, dbEntry.ModifiedBy);
        }

        [TestMethod]
        public async Task Should_Not_Get_Or_Modify_Diary_Entry_If_Different_User()
        {
            // Arrange
            await DatabaseContext.Database.EnsureDeletedAsync();

            var userBusinessService = (UserBusinessService)ServiceProvider.GetService(typeof(UserBusinessService));
            var currentUser = await DatabaseContext
                .Set<User>()
                .AddAsync(new User { UserName = "Max Muster" });
            userBusinessService.SetCurrentUser(currentUser.Entity);

            var diaryEntry = await _controller.CreateNewEntry(new DiaryEntryCreateModel
            {
                Description = "Testdescription",
                EventAt = new DateTime(2021, 1, 16, 10, 0, 0),
                Images = new List<DiaryImageCreateModel>
                {
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages One",
                        ImageFile = new byte[] { 1, 2, 3 }
                    },
                    new DiaryImageCreateModel
                    {
                        ImageFileName = "Testimages Two",
                        ImageFile = new byte[] { 4, 5, 6, 7 }
                    }
                }
            });

            // Imitate that the diary entry belongs to someone else
            var otherUser = await DatabaseContext
                .Set<User>()
                .AddAsync(new User { UserName = "Paul Pete" });
            var dbEntry = await DatabaseContext.Set<DiaryEntry>().FirstOrDefaultAsync(x => x.Id == diaryEntry.Id);
            dbEntry.UserId = otherUser.Entity.Id;
            await DatabaseContext.SaveChangesAsync();

            var modifyModel = new DiaryEntryModifyModel
            {
                Id = diaryEntry.Id,
                Description = "New Description",
                EventAt = new DateTime(2021, 1, 16, 15, 15, 15)
            };

            // Act
            var resultA = (BadRequestObjectResult)await _controller.GetById(diaryEntry.Id);
            var resultB = (BadRequestObjectResult)await _controller.ModifyEntry(modifyModel);

            // Assert
            Assert.IsNotNull(resultA);
            Assert.AreEqual("ERROR_DIARY_ENTRY_NOT_FOUND", resultA.Value);

            Assert.IsNotNull(resultB);
            Assert.AreEqual("ERROR_DIARY_ENTRY_NOT_FOUND", resultB.Value);
        }
    }
}
