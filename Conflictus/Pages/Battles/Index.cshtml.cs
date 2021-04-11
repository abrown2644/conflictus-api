using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Conflictus.Pages.Battles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Battle> Battles { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<SideA> SideAs { get; set; }
        public IEnumerable<SideB> SideBs { get; set; }

        public async Task OnGet()
        {
            Battles = await _db.Battle.ToListAsync();
            Locations = await _db.Location.ToListAsync();
            SideAs = await _db.SideA.ToListAsync();
            SideBs = await _db.SideB.ToListAsync();
        }


        public Location Location { get; set; }
        public SideA SideA { get; set; }
        public SideB SideB { get; set; }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            //Battle
            var Battle = await _db.Battle.FindAsync(id);

            //Location
            Battle battleWithLocation = await _db.Battle.Include(b => b.Location).FirstOrDefaultAsync(l => l.Id == id);
            if (battleWithLocation != null && battleWithLocation.Location != null)
                Location = await _db.Location.FindAsync(battleWithLocation.Location.Id);
   
            //SideA
            Battle battleWithSideA = await _db.Battle.Include(b => b.SideA).FirstOrDefaultAsync(a => a.Id == id);
            if (battleWithSideA != null && battleWithSideA.SideA != null)
                SideA = await _db.SideA.FindAsync(battleWithSideA.SideA.Id);

            //SideB
            Battle battleWithSideB = await _db.Battle.Include(b => b.SideB).FirstOrDefaultAsync(b => b.Id == id);
            if (battleWithSideB != null && battleWithSideB.SideB != null)
                SideB = await _db.SideB.FindAsync(battleWithSideA.SideB.Id);
            

            if (Battle == null)
            {
                return NotFound();
            }
            
            
           
            if (Location != null)
                _db.Location.Remove(Location);
            if (SideA != null)
                _db.SideA.Remove(SideA);
            if (SideB != null)
                _db.SideB.Remove(SideB);

            _db.Battle.Remove(Battle);

            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
