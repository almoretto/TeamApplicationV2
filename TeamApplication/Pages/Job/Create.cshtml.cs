using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class CreateJob : PageModel
    {
        private readonly SementesApplicationContext _context;

        public CreateJob(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }
        //public IQueryable<Entity> Entities { get; set; }

        public IActionResult OnGet()
        {
            ViewData["EntityId"] = new SelectList(_context.Entity, "EntityId", "EntityName");

            return Page();
        }



         
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyJob = new Job();

            if (await TryUpdateModelAsync<Job>(
                emptyJob,
                "Job",   // Prefix for form value.
                    s => s.JobDay,
                    s => s.JobPeriod,
                    s => s.ActionKind,
                    s => s.EntityId,
                    s => s.MaxVolunteer))
            {

                _context.Job.Add(emptyJob);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }

            return Page();
        }
    }
}
