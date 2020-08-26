using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication.Pages.Schedule
{
    public class DetailsModel : PageModel
    {
        private readonly TeamApplication.Data.SementesApplicationContext _context;

        public DetailsModel(TeamApplication.Data.SementesApplicationContext context)
        {
            _context = context;
        }

        public Schedule Schedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule = await _context.Schedule
                .Include(s => s.Volunteer).FirstOrDefaultAsync(m => m.TeamScheduleId == id);

            if (Schedule == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
