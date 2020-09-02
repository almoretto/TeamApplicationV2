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

namespace TeamApplication
{
    public class EditTeam : PageModel
    {
        private readonly SementesApplicationContext _context;

        public EditTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; }
        public IList<TeamVolunteer> TeamVolunteerToUpdate { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Team
                .Include(t => t.Job)
                .FirstOrDefaultAsync(m => m.TeamId == id);


            TeamVolunteerToUpdate = await _context.TeamVolunteer.Where(t => t.TeamId == id).ToListAsync();

            if (Team == null)
            {
                return NotFound();
            }
            var ListItems = from j in _context.Job
                             .Include(k => k.Entity)
                             .OrderBy(k => k.Entity.EntityName)
                            select j;
            ViewData["JobId"] = new SelectList(ListItems, "JobId", "JobDay", "Entity.EntityName", "Entity.EntityName");
            ViewData["VolunteerId"] = new MultiSelectList(_context.Volunteer, "VolunteerId", "VName");

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

                var TeamToUpdate = await _context.Team.FindAsync(id);

                TeamVolunteerToUpdate = await _context.TeamVolunteer.Where(t => t.TeamId == id).ToListAsync();

                if (TeamToUpdate == null)
                {
                    return NotFound();
                }

                await TryUpdateModelAsync<Team>(TeamToUpdate);
                await _context.SaveChangesAsync();

                int count = Team.Volunteers.Count();
                int i = 0;
                int contTv = TeamVolunteerToUpdate.Count();

                foreach (TeamVolunteer item in TeamVolunteerToUpdate)
                {
                    
                    if (i > count)
                    {
                        break;
                    }
                    item.VolunteerId = Team.Volunteers[i];
                    await TryUpdateModelAsync<TeamVolunteer>(item);
                    i++;

                }
                if (contTv < count)
                {
                    var emptyTeamVolunteer = new TeamVolunteer
                    {
                        VolunteerId = Team.Volunteers[i],
                        TeamId = Team.TeamId
                    };

                    _context.TeamVolunteer.Add(emptyTeamVolunteer);

                }
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(Team.TeamId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}
