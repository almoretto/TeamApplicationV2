using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Models;
using TeamApplication.Data;
using TeamApplication.Classes;
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
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<State> States { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<State> statesIQ = from s in _context.State
                                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                statesIQ = statesIQ.Where(s => s.UFName.Contains(searchString)
                                       || s.UFAbreviation.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    statesIQ = statesIQ.OrderByDescending(s => s.UFName);
                    break;
                default:
                    statesIQ = statesIQ.OrderBy(s => s.UFName);
                    break;
            }

            int pageSize = 6;
            States = await PaginatedList<State>.CreateAsync(
                statesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            //States = await stateIQ.AsNoTracking().ToListAsync();
        }
    }
}