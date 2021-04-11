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

namespace Conflictus.Pages.Upload
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
           ViewData["WarId"] = new SelectList(_context.War, "Id", "Name");
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

            _context.Attach(Battle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BattleExists(Battle.Id))
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

        private bool BattleExists(int id)
        {
            return _context.Battle.Any(e => e.Id == id);
        }
    }
}
