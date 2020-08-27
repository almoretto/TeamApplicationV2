using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DetailsJob : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DetailsJob(SementesApplicationContext context)
        {
            _context = context;
        }

        public Job Job { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Job = await _context.Job
                .Include(j => j.Entity)
                .FirstOrDefaultAsync(m => m.JobId == id);

            if (Job == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
