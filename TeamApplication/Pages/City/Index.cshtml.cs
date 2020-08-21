using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;
using System;
using System.Linq;

namespace TeamApplication
{
    public class IndexCity : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexCity(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string StateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<City> Cities { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            StateSort = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "state_asc";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<City> citiesIQ = from s in _context.City
                                        .Include(c => c.State) 
                                        select s;
            // IEnumerable<City> cities = await _context.City.Include(c => c.State).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                citiesIQ = citiesIQ.Where(s => s.CityName.Contains(searchString)
                                       || s.State.UFName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    citiesIQ = citiesIQ.OrderByDescending(s => s.CityName);
                    break;
                case "state_desc":
                    citiesIQ = citiesIQ.OrderByDescending(s => s.State.UFName);
                    break;
                case "state_asc":
                    citiesIQ = citiesIQ.OrderBy(s => s.State.UFName);
                    break;
                default:
                    citiesIQ = citiesIQ.OrderBy(s => s.CityName);
                    break;
            }

            int pageSize = 6;
            //cities = citiesIQ;
            Cities = await PaginatedList<City>.CreateAsync(
                citiesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
