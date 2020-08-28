using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;

namespace TeamApplication
{
    public class CreateTeam : PageModel
    {
        private readonly SementesApplicationContext _context;
       // public IEnumerable<ListItem> ListOfVolunteers { get; set; }

        public CreateTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            ViewData["JobId"] = new SelectList(_context.Job, "JobId", "JobDay");
           
           
            return Page();
        }

        [BindProperty]
        public Team Team { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Team.Add(Team);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
