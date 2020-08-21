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
        //create a dropdownlist
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
            var emptyCity = new City();

            if (!ModelState.IsValid)
            {
                return Page();
            } //Method avoiding overposting
            if (await TryUpdateModelAsync<City>(
               emptyCity,
               "City",   // Prefix for form value.
               s => s.CityName,
               s => s.StateId))
            {
                _context.City.Add(emptyCity);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
