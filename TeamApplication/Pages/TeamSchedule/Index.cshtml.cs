using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;

namespace TeamApplication
{
    public class IndexSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        public IList<TeamSchedule> TeamSchedule { get;set; }

        public async Task OnGetAsync()
        {
            TeamSchedule = await _context.TeamSchedule
                .Include(t => t.Volunteer).ToListAsync();
        }
    }
}
