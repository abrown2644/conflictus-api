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
    public class DetailsModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _context;

        public DetailsModel(Conflictus.Model.ApplicationDbContext context)
        {
            _context = context;
        }

        public Battle Battle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Battle = await _context.Battle
                .Include(b => b.War).FirstOrDefaultAsync(m => m.Id == id);

            if (Battle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
