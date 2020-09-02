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
        public IList<TeamVolunteer> Volunteers { get; set; }

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
            Volunteers = await _context.TeamVolunteer
                   .Include(a => a.Volunteer)
                   .Include(b => b.Team)
                   .Where(c => c.TeamId == c.Team.TeamId)
                   .OrderBy(d => d.Volunteer.VName)
                   .ToListAsync();


            if (Team == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
