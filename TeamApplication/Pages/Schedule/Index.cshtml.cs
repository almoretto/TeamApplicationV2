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
    public class IndexSchedule : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexSchedule(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Schedule> Schedules { get; set; }

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

            IQueryable<Schedule> schedulesIQ = from s in _context.Schedule
                                              .Include(c => c.Volunteer)
                                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                schedulesIQ = schedulesIQ.Where(s => s.Volunteer.VName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    schedulesIQ = schedulesIQ.OrderByDescending(s => s.Volunteer.VName);
                    break;
                case "date_desc":
                    schedulesIQ = schedulesIQ.OrderByDescending(s => s.TSDate);
                    break;
                case "Date":
                    schedulesIQ = schedulesIQ.OrderBy(s => s.TSDate);
                    break;
                default:
                    schedulesIQ = schedulesIQ.OrderBy(s => s.Volunteer.VName);
                    break;
            }

            int pageSize = 6;
            //cities = citiesIQ;
            Schedules = await PaginatedList<Schedule>.CreateAsync(
                schedulesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
