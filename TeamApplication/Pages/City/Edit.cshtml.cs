using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;

namespace TeamApplication
{
    public class EditCity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditCity(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public City City { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City = await _context.City
                .Include(c => c.State).FirstOrDefaultAsync(m => m.CityId == id);

            if (City == null)
            {
                return NotFound();
            }
           ViewData["StateId"] = new SelectList(_context.State, "StateId", "UFAbreviation");
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

                var cityToUpdate = await _context.City.FindAsync(id);

                if (cityToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<City>(
                    cityToUpdate,
                    "City",
                    s => s.CityName,
                    s => s.StateId))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(City.CityId))
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

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.CityId == id);
        }
    }
}
