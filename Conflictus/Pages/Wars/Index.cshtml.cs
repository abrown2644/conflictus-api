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
    public class IndexModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _db;

        public IndexModel(Conflictus.Model.ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<War> War { get;set; }
        //public ICollection<Battle> WarBattles { get; set; }
        public async Task OnGetAsync()
        {
            //War = await _db.War.ToListAsync();
            War = await _db.War
               .Include(w => w.Battles)
                .ThenInclude(w => w.SideA.Participants)
               .Include(w => w.Battles)
                .ThenInclude(w => w.SideB.Participants)
               .ToListAsync();
        }
    }
}
