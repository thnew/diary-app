using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers.Diary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Diary
{
    /// <summary>
    /// Handles interactions woth the diary of the current user
    /// </summary>
    [ApiController]
    [Route("/api/diary")]
    public class DiaryController : ControllerBase
    {
        private readonly DiaryBusinessService _diaryBusinessService;

        public DiaryController(DiaryBusinessService diaryBusinessService)
        {
            _diaryBusinessService = diaryBusinessService;
        }

        /// <summary>
        /// Returns all diary entries of the current user
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<DiaryEntryViewModel>> GetAll()
        {
            return await _diaryBusinessService.GetAllEntriesForCurrentUser();
        }

        /// <summary>
        /// Used to create new diary entries
        /// </summary>
        [HttpPost]
        public async Task<DiaryEntryViewModel> CreateNewEntry(DiaryEntryCreateModel createModel)
        {
            return await _diaryBusinessService.CreateNewEntry(createModel);
        }
    }
}
