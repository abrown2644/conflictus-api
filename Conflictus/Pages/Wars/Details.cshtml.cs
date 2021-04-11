using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Wars
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _db;

        public DetailsModel(Conflictus.Model.ApplicationDbContext db)
        {
            _db = db;
        }

        public War War { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //War = await _db.War.FirstOrDefaultAsync(m => m.Id == id);
            War = await _db.War
                .Include(w => w.Battles)
                 .ThenInclude(w => w.SideA.Participants).Distinct()
                .Include(w => w.Battles)
                 .ThenInclude(w => w.SideB.Participants).Distinct()                 
                .FirstOrDefaultAsync(m => m.Id == id);
            //foreach (var b in War.Battles)
            //{
            //    foreach (var pa in b.SideA.Participants)
            //    {
                    
            //    }
            //}

            if (War == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
