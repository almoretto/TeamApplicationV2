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


            var ListJobs = from j in _context.Job
                            .Include(k => k.Entity)
                            .OrderBy(k => k.Entity.EntityName)
                           select j;

            ViewData["JobId"] = new SelectList(ListJobs, "JobId", "JobDay", "Entity.EntityName", "Entity.EntityName");
           
            var ListVolunteers = from v in _context.Volunteer
                                 join s in _context.Schedule on v.VolunteerId equals s.VolunteerId
                                 join j in _context.Job on s.TSDate equals j.JobDay
                                 select new
                                 {
                                     v.VolunteerId,
                                     v.VName
                                 };
            ViewData["VolunteerId"] = new MultiSelectList(ListVolunteers, "VolunteerId", "VName");


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Team.Add(Team);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}

