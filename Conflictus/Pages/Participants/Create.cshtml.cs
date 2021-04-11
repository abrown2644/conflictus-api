using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Conflictus.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Participants
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _db;

        public CreateModel(Conflictus.Model.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Participant Participant { get; set; }
        public War War { get; set; }
        public IList<War> Wars { get; set; }
     
        [BindProperty]
        public int[] WarIds { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["WarId"] = new SelectList(_db.Participant, "Id", "Name");
            //Wars = await _db.War.ToListAsync();
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //foreach (var id in WarIds)
            //{
            //    War = await _db.War.FindAsync(id);
            //    Participant.Wars.Add(War);
            //}

            _db.Participant.Add(Participant);
            await _db.SaveChangesAsync();

            //return RedirectToPage("./Index");
            return Page();
        }
    }
}
