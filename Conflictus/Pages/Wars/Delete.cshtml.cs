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
    public class DeleteModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _context;

        public DeleteModel(Conflictus.Model.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public War War { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            War = await _context.War.FirstOrDefaultAsync(m => m.Id == id);

            if (War == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            War = await _context.War.FindAsync(id);

            if (War != null)
            {
                _context.War.Remove(War);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
