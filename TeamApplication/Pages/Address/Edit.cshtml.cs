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
    public class EditAddress : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditAddress(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Address = await _context.Address
                .Include(a => a.City).FirstOrDefaultAsync(m => m.AddressId == id);

            if (Address == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var addressToUpdate = await _context.Address.FindAsync(id);

                if (addressToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Address>(
                    addressToUpdate,
                    "Address",
                    s => s.AddressKind,
                    s => s.Designation,
                    s => s.District,
                    s => s.Complement,
                    s => s.ZipCode,
                    s => s.CityId))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(Address.AddressId))
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

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }
    }
}
