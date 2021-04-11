using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Upload
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _context;

        public IndexModel(Conflictus.Model.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Battle> Battle { get;set; }

        public async Task OnGetAsync()
        {
            Battle = await _context.Battle
                .Include(b => b.War).ToListAsync();
        }
    }
}
