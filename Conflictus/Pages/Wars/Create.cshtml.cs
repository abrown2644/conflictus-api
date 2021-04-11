using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Wars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _db;

        public CreateModel(Conflictus.Model.ApplicationDbContext db)
        {
            _db = db;
        }

       
        public IActionResult OnGet()
        {            
            return Page();
        }

        [BindProperty]
        public War War { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.War.Add(War);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
