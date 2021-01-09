using System;
using System.Collections.Generic;
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
        public DiaryController()
        {
        }

        [HttpGet]
        public IEnumerable<DiaryEntryViewModel> GetAll()
        {
            return new List<DiaryEntryViewModel>
            {
                new DiaryEntryViewModel
                {
                    EventAt = DateTime.Now,
                    Description = "Testentry"
                }
            };
        }
    }
}
