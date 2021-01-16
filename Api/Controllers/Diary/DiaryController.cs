using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers.Diary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Diary
{
    /// <summary>
    /// Handles interactions with the diary of the current user
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
        /// Returns the referenced diary entry
        /// </summary>
        /// <remarks>Throws error if it does not belong to the current user</remarks>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _diaryBusinessService.GetById(id));
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

        /// <summary>
        /// Used to modify an existing diary entry
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> ModifyEntry(DiaryEntryModifyModel modifyModel)
        {
            var result = await _diaryBusinessService.ModifyEntry(modifyModel);
            if(result.hasErrors)
            {
                return BadRequest(result.errorMessage);
            }

            return await GetById(modifyModel.Id);
        }
    }
}
