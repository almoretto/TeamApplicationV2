using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamApplication.Models;
using TeamApplication.Data;

namespace TeamApplication
{
    public class CreateCity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateCity(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StateId"] = new SelectList(_context.State, "StateId", "UFAbreviation");
            return Page();
        }

        [BindProperty]
        public City City { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.City.Add(City);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
