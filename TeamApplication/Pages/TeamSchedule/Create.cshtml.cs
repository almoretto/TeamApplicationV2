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
    public class CreateSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["VolunteerId"] = new SelectList(_context.Volunteer, "VolunteerId", "VDocCPF");
            return Page();
        }

        [BindProperty]
        public TeamSchedule TeamSchedule { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TeamSchedule.Add(TeamSchedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
