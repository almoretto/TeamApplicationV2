using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly TeamApplication.Data.SementesApplicationContext _context;

        public IndexModel(TeamApplication.Data.SementesApplicationContext context)
        {
            _context = context;
        }

        public IList<Schedule> Schedule { get;set; }

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedule
                .Include(s => s.Volunteer).ToListAsync();
        }
    }
}
