using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DetailsVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }

        public Volunteer Volunteer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Volunteer = await _context.Volunteer
                .Include(t => t.Address)
                .ThenInclude(u=>u.City)
                .ThenInclude(v=>v.State)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);

            if (Volunteer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
