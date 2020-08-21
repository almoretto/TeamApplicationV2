
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class CreateAddress : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateAddress(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityName");
            return Page();
        }

        [BindProperty]
        public Address Address { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyAddress = new Address();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Method avoiding overposting
            if (await TryUpdateModelAsync<Address>(
                    emptyAddress,
                    "Address",
                    s => s.AddressKind,
                    s => s.Designation,
                    s => s.District,
                    s => s.Complement,
                    s => s.ZipCode,
                    s => s.CityId))
            {
                _context.Address.Add(emptyAddress);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }
    }
}
