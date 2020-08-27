using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class EditSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Schedule Schedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schedule = await _context.Schedule
                .Include(s => s.Volunteer)
                .FirstOrDefaultAsync(m => m.TeamScheduleId == id);

            if (Schedule == null)
            {
                return NotFound();
            }
           ViewData["VolunteerId"] = new SelectList(_context.Volunteer, "VolunteerId", "VName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var scheduleToUpdate = await _context.Schedule.FindAsync(id);


                if (scheduleToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Schedule>(
                    scheduleToUpdate,
                    "Schedule",
                    s => s.TSDate,
                    s => s.TSPeriod,
                    s => s.VolunteerId))
                {
                    
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(Schedule.TeamScheduleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.TeamScheduleId == id);
        }
    }
}
