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
    public class IndexTeam : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        public IList<Team> Teams { get; set; }
        public IList<TeamVolunteer> Volunteers { get; set; }

        public async Task OnGetAsync()
        {
            Teams = await _context.Team
                .Include(t => t.Job)
                .ThenInclude(u => u.Entity)
                .Include(v => v.TeamVolunteer)
                .ThenInclude(x => x.Volunteer)
                .ToListAsync();
            Volunteers = await _context.TeamVolunteer
                  .Include(a => a.Volunteer)
                  .Include(b => b.Team)
                  .Where(c=>c.TeamId==c.Team.TeamId)
                  .ToListAsync();


        }
    }
}
