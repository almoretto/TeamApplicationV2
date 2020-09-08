using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class Schedule_Volunteer_ViewModel : PageModel
    {
        private readonly SementesApplicationContext _context;

        public Schedule_Volunteer_ViewModel(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Designation");
            return Page();
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; }

         
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Volunteer.Add(Volunteer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
