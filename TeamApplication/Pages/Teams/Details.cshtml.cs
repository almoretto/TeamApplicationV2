using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DetailsTeam : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        public Team Team { get; set; }
        public IQueryable<TeamVolunteer> TeamVolunteers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Team
                .Include(t => t.Job)
                .ThenInclude(u=>u.Entity)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            TeamVolunteers = from v in _context.TeamVolunteer
                             join x in _context.Volunteer on v.VolunteerId equals x.VolunteerId
                             join y in _context.Team on v.TeamId equals y.TeamId
                             select v;


            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
