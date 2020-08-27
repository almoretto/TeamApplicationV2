using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DeleteJob : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteJob(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
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
            
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Could not perform Delete on record id: " + id;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobToRemove = await _context.Job.FindAsync(id);
            
            if (jobToRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.Job.Remove(jobToRemove);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ErrorMessage);
                sb.AppendLine(ex.ToString());

                ErrorMessage = sb.ToString();
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
