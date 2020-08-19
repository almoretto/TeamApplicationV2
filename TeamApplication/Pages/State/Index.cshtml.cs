using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;
using System.Linq;
using System;

namespace TeamApplication
{
    public class IndexState : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexState(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public IList<State> States { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

           
            IQueryable<State> stateIQ = from s in _context.State
                                        select s;

            switch (sortOrder)
            {
                case "name_desc":
                    stateIQ = stateIQ.OrderByDescending(s => s.UFName);
                    break;
                default:
                    stateIQ = stateIQ.OrderBy(s => s.UFName);
                    break;
            }

            States = await stateIQ.AsNoTracking().ToListAsync();
        }
    }
}
//