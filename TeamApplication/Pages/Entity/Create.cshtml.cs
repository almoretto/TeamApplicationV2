using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamApplication.Models;
using TeamApplication.Data;

namespace TeamApplication
{
    public class CreateEntity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateEntity(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Entity Entity { get; set; }

         
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyEntity = new Entity();

            if (await TryUpdateModelAsync<Entity>(
                emptyEntity,
                "Entity",   // Prefix for form value.
                    s => s.EntityName,
                    s => s.VisitDay,
                    s => s.VisitScale,
                    s => s.VisitDuration,
                    s => s.VisitTime,
                    s => s.Contact,
                    s => s.Phone,
                    s => s.Email,
                    s => s.MaxVolunteer))
            {
               
                _context.Entity.Add(emptyEntity);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
