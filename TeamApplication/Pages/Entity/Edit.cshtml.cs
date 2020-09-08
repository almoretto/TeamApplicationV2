using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class EditEntity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditEntity(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entity Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await _context.Entity.FirstOrDefaultAsync(m => m.EntityId == id);

            if (Entity == null)
            {
                return NotFound();
            }
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
                var entityToUpdate = await _context.Entity.FindAsync(id);

                if (entityToUpdate == null)
                {
                    return NotFound();
                }
                if (await TryUpdateModelAsync<Entity>(
                                    entityToUpdate,
                                    "Entity",
                                    s => s.EntityName,
                                    s => s.VisitDay,
                                    s => s.VisitDuration,
                                    s => s.VisitScale,
                                    s => s.VisitTime,
                                    s => s.MaxVolunteer,
                                    s => s.Contact,
                                    s => s.Email,
                                    s => s.Phone))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(Entity.EntityId))
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

        private bool EntityExists(int id)
        {
            return _context.Entity.Any(e => e.EntityId == id);
        }
    }
}
