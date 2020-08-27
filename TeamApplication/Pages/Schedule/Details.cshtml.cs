
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;

namespace TeamApplication
{
    public class DetailsSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsSchedule(SementesApplicationContext context)
        {
            _context = context;
        }

        public PaginatedList<Schedule> Schedules { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? pageIndex)
        {
            
            IQueryable<Schedule> schedulesIQ = from s in _context.Schedule
                        .Include(t => t.Volunteer)
                        .Where(u => u.VolunteerId == id)
                        .OrderBy(v => v.Volunteer.VName)
                                               select s;


            int pageSize = 10;
            
            Schedules = await PaginatedList<Schedule>.CreateAsync(
                schedulesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            
            return Page();
        }
    }
}
