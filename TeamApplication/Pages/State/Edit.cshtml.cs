using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class EditState : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditState(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public State State { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State = await _context.State.FindAsync(id);

            if (State == null)
            {
                return NotFound();
            }
            return Page();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var stateToUpdate = await _context.State.FindAsync(id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(State).State = EntityState.Modified;

            try
            {
                if (await TryUpdateModelAsync<State>(
                    stateToUpdate,
                    "State",
                    s => s.UFName, 
                    s => s.UFAbreviation))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(State.StateId))
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

        private bool StateExists(int id)
        {
            return _context.State.Any(e => e.StateId == id);
        }
    }
}
