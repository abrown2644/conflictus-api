using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Conflictus.Pages.Battles
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Battle Battle { get; set; }
        public Location Location { get; set; }
        public SideA SideA { get; set; }
        public SideB SideB { get; set; }
        public Participant Participant { get; set; }
        public IList<Participant> Participants { get; set; }
        public IList<War> Wars { get; set; }

        [BindProperty]
        public int[] PartAIds { get; set; }
        [BindProperty]
        public int[] PartBIds { get; set; }

        
        public Participant ParticipantA { get; set; }
        public Participant ParticipantB { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["WarId"] = new SelectList(_db.War, "Id", "Name");
            ViewData["ParticipantId"] = new SelectList(_db.Participant, "Id", "Name");

            Wars = await _db.War.ToListAsync();
            Participants = await _db.Participant.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (var partA in PartAIds)
                {
                    ParticipantA = await _db.Participant.FindAsync(partA);
                    Battle.SideA.Participants.Add(ParticipantA);                    
                }
                foreach (var partB in PartBIds)
                {
                    ParticipantB = await _db.Participant.FindAsync(partB);
                    Battle.SideB.Participants.Add(ParticipantB);
                }

                
                await _db.Battle.AddAsync(Battle);                
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
