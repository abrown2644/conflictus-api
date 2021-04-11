using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Wars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _context;

        public EditModel(Conflictus.Model.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(War).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarExists(War.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WarExists(int id)
        {
            return _context.War.Any(e => e.Id == id);
        }
    }
}
