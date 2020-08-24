
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class CreateVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //used to populate a dropdown list
        ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Designation");
            return Page();
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyVolunteer = new Volunteer();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Method avoiding overposting
            if (await TryUpdateModelAsync<Volunteer>(
                    emptyVolunteer,
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
                _context.Volunteer.Add(emptyVolunteer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }
    }
}
