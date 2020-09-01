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

        [BindProperty]
        public Team Team { get; set; }
        public CreateTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            
            var ListItems = from j in _context.Job
                            .Include(k => k.Entity)
                            .OrderBy(k => k.Entity.EntityName)
                            select j;
            ViewData["JobId"] = new SelectList(ListItems, "JobId", "JobDay", "Entity.EntityName", "Entity.EntityName");
            ViewData["VolunteerId"] = new MultiSelectList(_context.Volunteer, "VolunteerId", "VName");

            //_context.Job, "JobId", "JobDay"
            return Page();
        }


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
            foreach (int v in Team.Volunteers)
            {
                var emptyTeamVolunteer = new TeamVolunteer();
                emptyTeamVolunteer.VolunteerId = v;
                emptyTeamVolunteer.TeamId = Team.TeamId;

                _context.TeamVolunteer.Add(emptyTeamVolunteer);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}

