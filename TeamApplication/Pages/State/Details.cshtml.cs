using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;


namespace TeamApplication
{
    public class DetailsState : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsState(SementesApplicationContext context)
        {
            _context = context;
        }

        public State State { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            State = await _context.State.FirstOrDefaultAsync(m => m.StateId == id);

            if (State == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
