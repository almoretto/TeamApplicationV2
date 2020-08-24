
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;
using System.Linq;

namespace TeamApplication
{
    public class IndexVolunteer : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexVolunteer(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Volunteer> Volunteers { get; set; }
       // public IList<Volunteer> Volunteer { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Volunteer> volunteersIQ = from s in _context.Volunteer
                                              .Include(c => c.Address)
                                              .ThenInclude(d => d.City)
                                              .ThenInclude(e=>e.State)
                                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                volunteersIQ = volunteersIQ.Where(s => s.VName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    volunteersIQ = volunteersIQ.OrderByDescending(s => s.VName);
                    break;
                case "date_desc":
                    volunteersIQ = volunteersIQ.OrderByDescending(s => s.VBirthDate);
                    break;
                case "Date":
                    volunteersIQ = volunteersIQ.OrderBy(s => s.VBirthDate);
                    break;
                default:
                    volunteersIQ = volunteersIQ.OrderBy(s => s.VName);
                    break;
            }

            int pageSize = 6;
            //cities = citiesIQ;
            Volunteers = await PaginatedList<Volunteer>.CreateAsync(
                volunteersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
