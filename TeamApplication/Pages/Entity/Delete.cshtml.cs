using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;
using System.Text;

namespace TeamApplication
{
    public class DeleteEntity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteEntity(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entity Entity { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entity = await _context.Entity
                .FirstOrDefaultAsync(m => m.EntityId == id);

            if (Entity == null)
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

            var entityToRemove = await _context.Entity.FindAsync(id);

            if (entityToRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entity.Remove(entityToRemove);
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
