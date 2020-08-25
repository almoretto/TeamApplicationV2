using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DeleteSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamSchedule = await _context.TeamSchedule.FindAsync(id);

            if (TeamSchedule != null)
            {
                _context.TeamSchedule.Remove(TeamSchedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
