using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class CreateSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["VolunteerId"] = new SelectList(_context.Volunteer, "VolunteerId", "VName");
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; }

         
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var emptySchedule = new Schedule();

            if (await TryUpdateModelAsync<Schedule>(
                emptySchedule,
                "Schedule",   // Prefix for form value.
                    s => s.TSDate,
                    s => s.TSPeriod,
                    s => s.VolunteerId))
            {
                _context.Schedule.Add(emptySchedule);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
