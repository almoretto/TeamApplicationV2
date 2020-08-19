using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using System.Text;

namespace TeamApplication
{
    public class DeleteState : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteState(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public State State { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            State = await _context.State
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StateId == id);

            if (State == null)
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
            var stateToRemove = await _context.State.FindAsync(id);

            if (stateToRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.State.Remove(stateToRemove);
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
