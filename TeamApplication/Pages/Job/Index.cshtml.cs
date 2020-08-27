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
    public class IndexJobs : PageModel
    {
        private readonly SementesApplicationContext _context;

        public IndexJobs(SementesApplicationContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Job> Jobs { get; set; }

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

            IQueryable<Job> jobsIQ = from s in _context.Job
                                            .Include(c => c.Entity)
                                     select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                jobsIQ = jobsIQ.Where(s => s.Entity.EntityName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.Entity.EntityName);
                    break;
                case "date_desc":
                    jobsIQ = jobsIQ.OrderByDescending(s => s.JobDay);
                    break;
                case "Date":
                    jobsIQ = jobsIQ.OrderBy(s => s.JobDay);
                    break;
                default:
                    jobsIQ = jobsIQ.OrderBy(s => s.Entity.EntityName);
                    break;
            }

            int pageSize = 6;
           
            Jobs = await PaginatedList<Job>.CreateAsync(
                jobsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
