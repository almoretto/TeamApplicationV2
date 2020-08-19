using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class CreateState : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateState(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public State State { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyState = new State();

            if (!ModelState.IsValid)
            {
                return Page();
            } //Method avoiding overposting
            if (await TryUpdateModelAsync<State>(
               emptyState,
               "State",   // Prefix for form value.
               s => s.UFAbreviation,
               s => s.UFName))
            {
                _context.State.Add(emptyState);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
