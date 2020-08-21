
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class IndexVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }

        public IList<Volunteer> Volunteer { get;set; }

        public async Task OnGetAsync()
        {
            Volunteer = await _context.Volunteer
                .Include(v => v.Address).ToListAsync();
        }
    }
}
