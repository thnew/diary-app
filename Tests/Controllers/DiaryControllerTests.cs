using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.Diary;
using Api.Controllers.Diary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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



            // Act
            var results = await _controller.GetAll();

            // Assert
            Assert.AreEqual(1, results.Count());

            var firstResult = results.First();
            Assert.AreEqual(diaryEntry.Description, firstResult.Description);
            Assert.AreEqual(diaryEntry.EventAt, firstResult.EventAt);
            Assert.AreEqual(diaryEntry.Images.Count(), firstResult.Images.Count());

            foreach(var expectedImage in diaryEntry.Images)
            {
                var foundImage = firstResult.Images.First(x => x.ImageFileName == expectedImage.ImageFileName);
                Assert.IsTrue(expectedImage.ImageFile.SequenceEqual(foundImage.ImageFile));
            }
        }
    }
}
