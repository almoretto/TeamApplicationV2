using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;

namespace TeamApplication
{
    public class DetailsEntity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsEntity(SementesApplicationContext context)
        {
            _context = context;
        }

        public Entity Entity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            return Page();
        }
    }
}
