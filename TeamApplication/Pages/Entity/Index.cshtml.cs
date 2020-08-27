
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
    public class IndexEntity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexEntity(SementesApplicationContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Entity> Entities { get; set; }

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

            IQueryable<Entity> entitiesIQ = from s in _context.Entity
                                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                entitiesIQ = entitiesIQ.Where(s => s.EntityName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    entitiesIQ = entitiesIQ.OrderByDescending(s => s.EntityName);
                    break;
                default:
                    entitiesIQ = entitiesIQ.OrderBy(s => s.EntityName);
                    break;
            }

            int pageSize = 6;
            //cities = citiesIQ;
            Entities = await PaginatedList<Entity>.CreateAsync(
                entitiesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            //Entities = await _context.Entity.ToListAsync();
        }
    }
}
