using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeamApplication.Data;
using TeamApplication.Models;

namespace TeamApplication.Classes
{
    public class MultSelectListControl : Controller
    {
        private readonly SementesApplicationContext _context;
        public IQueryable<Volunteer> ListOfVolunteers { get; set; }

        public MultSelectListControl(SementesApplicationContext context)
        {
            _context = context;
        }
        public ActionResult MultSelectList()
        {
            return View(ListOfVolunteers = from v in _context.Volunteer where(v.VActive==true)select v);
        }
    }
}