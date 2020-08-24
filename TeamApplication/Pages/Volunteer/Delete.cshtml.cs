using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DeleteVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
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

            var volunteerToRemove = await _context.Volunteer.FindAsync(id);
            //Volunteer = await _context.Volunteer.FindAsync(id);
            if (volunteerToRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.Volunteer.Remove(volunteerToRemove);
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
