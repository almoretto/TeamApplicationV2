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
    public class EditVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Volunteer = await _context.Volunteer
                .Include(v => v.Address)
                .FirstOrDefaultAsync(m => m.VolunteerId == id);

            if (Volunteer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Designation");
            return Page();
        }

         
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var volunteerToUpdate = await _context.Volunteer.FindAsync(id);
                

                if (volunteerToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Volunteer>(
                    volunteerToUpdate,
                    "Volunteer",
                    s => s.VDocCPF,
                    s => s.VDocRG,
                    s => s.VName,
                    s => s.VBirthDate,
                    s => s.VActive,
                    s => s.VEmail,
                    s => s.VMessagePhone,
                    s => s.VPhone,
                    s => s.VResumee,
                    s => s.VSocialMidiaProfile,
                    s => s.AddressId))
                {
                    volunteerToUpdate.AgeCalculator();
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(Volunteer.VolunteerId))
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

        private bool VolunteerExists(int id)
        {
            return _context.Volunteer.Any(e => e.VolunteerId == id);
        }
    }
}
