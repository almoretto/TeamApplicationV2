using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;

namespace TeamApplication
{
    public class DeleteCity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteCity(SementesApplicationContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            City = await _context.City.FindAsync(id);

            if (City != null)
            {
                _context.City.Remove(City);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
