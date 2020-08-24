using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;
using TeamApplication.Classes;

namespace TeamApplication
{
    public class IndexAddress : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexAddress(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string CitySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Address> Addresses { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CitySort = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "city_asc";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Address> addressesIQ = from s in _context.Address
                                              .Include(c => c.City)
                                              .ThenInclude(d=>d.State) 
                                              select s;
            // IEnumerable<City> cities = await _context.City.Include(c => c.State).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                addressesIQ = addressesIQ.Where(s => s.Designation.Contains(searchString)
                                       || s.City.CityName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    addressesIQ = addressesIQ.OrderByDescending(s => s.Designation);
                    break;
                case "city_desc":
                    addressesIQ = addressesIQ.OrderByDescending(s => s.City.CityName);
                    break;
                case "city_asc":
                    addressesIQ = addressesIQ.OrderBy(s => s.City.CityName);
                    break;
                default:
                    addressesIQ = addressesIQ.OrderBy(s => s.Designation);
                    break;
            }

            int pageSize = 6;
            //cities = citiesIQ;
            Addresses = await PaginatedList<Address>.CreateAsync(
                addressesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            /*
            Address = await _context.Address
                .Include(a => a.City).ToListAsync();*/
        }
    }
}
