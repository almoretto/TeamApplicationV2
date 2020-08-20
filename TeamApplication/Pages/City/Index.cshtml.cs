using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class IndexCity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexCity(SementesApplicationContext context)
        {
            _context = context;
        }

        public IList<City> Cities { get;set; }

        public async Task OnGetAsync()
        {
            Cities = await _context.City
                .Include(c => c.State).ToListAsync();
        }
    }
}
