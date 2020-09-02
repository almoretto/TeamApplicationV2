using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication
{
    public class DeleteTeam : PageModel
    {
        private readonly SementesApplicationContext _context;

        public DeleteTeam(SementesApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Team
                .Include(t => t.Job)
                .ThenInclude(u=>u.Entity)
                .FirstOrDefaultAsync(m => m.TeamId == id);

            if (Team == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Could not perform Delete on record id: " + id;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamToRemove = await _context.Team.FindAsync(id);

            if (teamToRemove == null)
            {
                return NotFound();
            }
            try
            {
                _context.Team.Remove(teamToRemove);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ErrorMessage);
                sb.AppendLine(ex.ToString());

                ErrorMessage = sb.ToString();
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
