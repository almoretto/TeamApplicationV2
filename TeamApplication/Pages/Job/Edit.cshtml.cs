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
    public class EditJob : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditJob(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Job
                .Include(j => j.Entity)
                .FirstOrDefaultAsync(m => m.JobId == id);

            if (Job == null)
            {
                return NotFound();
            }
            ViewData["EntityId"] = new SelectList(_context.Entity, "EntityId", "EntityName");

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

                var jobToUpdate = await _context.Job.FindAsync(id);


                if (jobToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Job>(
                    jobToUpdate,
                    "Job",
                    s => s.JobDay,
                    s => s.JobPeriod,
                    s => s.ActionKind,
                    s => s.EntityId,
                    s => s.MaxVolunteer))
                {

                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(Job.JobId))
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

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }
    }
}
