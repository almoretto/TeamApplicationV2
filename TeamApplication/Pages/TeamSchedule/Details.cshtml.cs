using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DetailsSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        public TeamSchedule TeamSchedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamSchedule = await _context.TeamSchedule
                .Include(t => t.Volunteer).FirstOrDefaultAsync(m => m.TeamScheduleId == id);

            if (TeamSchedule == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
