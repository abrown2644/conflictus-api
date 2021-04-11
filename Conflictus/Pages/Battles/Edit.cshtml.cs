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
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Battle Battle { get; set; }                
        public Location Location { get; set; }
        public War War { get; set; }
        public SideA SideA { get; set; }
        public SideB SideB { get; set; }
        public Participant ParticipantA { get; set; }
        public Participant ParticipantB { get; set; }

        public IList<War> Wars { get; set; }
        public IList<Participant> Participants { get; set; }
        public IList<Participant> ParticipantsA { get; set; }
        public IList<Participant> ParticipantsB { get; set; }

        [BindProperty]
        public int[] PartAIds { get; set; }
        [BindProperty]
        public int[] PartBIds { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {            
            Battle = await _db.Battle.FindAsync(id);

            ViewData["WarId"] = new SelectList(_db.War, "Id", "Name");
            Wars = await _db.War.ToListAsync();
            ViewData["ParticipantId"] = new SelectList(_db.Participant, "Id", "Name");
            Participants = await _db.Participant.ToListAsync();
            

            Battle battleWithLocation = await _db.Battle.Include(b => b.Location).FirstOrDefaultAsync(l => l.Id == id);
            if(battleWithLocation != null && battleWithLocation.Location != null)
                Location = await _db.Location.FindAsync(battleWithLocation.Location.Id);
            
            Battle battleWithSideA = await _db.Battle.Include(b => b.SideA).FirstOrDefaultAsync(a => a.Id == id);
            if (battleWithSideA != null && battleWithSideA.SideA != null)
                SideA = await _db.SideA.FindAsync(battleWithSideA.SideA.Id);

            Battle battleWithSideB = await _db.Battle.Include(b => b.SideB).FirstOrDefaultAsync(b => b.Id == id);
            if (battleWithSideB != null && battleWithSideB.SideB != null)
                SideB = await _db.SideB.FindAsync(battleWithSideA.SideB.Id);

            Battle battleWithsideAParticipants = await _db.Battle.Include(s => s.SideA.Participants).FirstOrDefaultAsync(pa => pa.Id == id);
            if (battleWithsideAParticipants != null && battleWithsideAParticipants.SideA.Participants != null)
                ParticipantsA = battleWithsideAParticipants.SideA.Participants.ToList();

            Battle battleWithsideBParticipants = await _db.Battle.Include(s => s.SideB.Participants).FirstOrDefaultAsync(pb => pb.Id == id);
            if (battleWithsideBParticipants != null && battleWithsideBParticipants.SideB.Participants != null)
                ParticipantsB = battleWithsideBParticipants.SideB.Participants.ToList();

            //Participant sidaAParticpants = _db.SideA.Where(pa => pa.Id == id)
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {         
            if (ModelState.IsValid)
            {                
                //Battle
                var BattleFromDb = await _db.Battle.FindAsync(id);
                BattleFromDb.Title = Battle.Title;
                BattleFromDb.Date = Battle.Date;
                BattleFromDb.Year = Battle.Year;
                BattleFromDb.Summary = Battle.Summary;
                BattleFromDb.Type = Battle.Type;
                BattleFromDb.ImageUrl = Battle.ImageUrl;
                BattleFromDb.Outcome = Battle.Outcome;
                BattleFromDb.Url = Battle.Url;
                BattleFromDb.WarId = Battle.WarId;
                
                //Location
                Battle BattleFromDbLocation = await _db.Battle.Include(b => b.Location).FirstOrDefaultAsync(l => l.Id == id);
                if (BattleFromDbLocation != null && BattleFromDbLocation.Location != null)
                    Location = await _db.Location.FindAsync(BattleFromDbLocation.Location.Id);

                Location.Description = Battle.Location.Description;
                Location.HasCoordinates = Battle.Location.HasCoordinates;
                Location.Latitude = Battle.Location.Latitude;
                Location.Longitude = Battle.Location.Longitude;
                
                //SideA
                Battle battleWithSideA = await _db.Battle.Include(b => b.SideA).FirstOrDefaultAsync(a => a.Id == id);
                if (battleWithSideA != null && battleWithSideA.SideA != null)
                    SideA = await _db.SideA.FindAsync(battleWithSideA.SideA.Id);

                SideA.Commanders = Battle.SideA.Commanders;
                SideA.Strength = Battle.SideA.Strength;
                SideA.Losses = Battle.SideA.Losses;
                SideA.Victory = Battle.SideA.Victory;
                
                //SideB
                Battle battleWithSideB = await _db.Battle.Include(b => b.SideB).FirstOrDefaultAsync(b => b.Id == id);
                if (battleWithSideB != null && battleWithSideB.SideB != null)
                    SideB = await _db.SideB.FindAsync(battleWithSideA.SideB.Id);

                SideB.Commanders = Battle.SideB.Commanders;
                SideB.Strength = Battle.SideB.Strength;
                SideB.Losses = Battle.SideB.Losses;
                SideB.Victory = Battle.SideB.Victory;
                
                //Participants
                Battle battleWithsideAParticipants = await _db.Battle.Include(s => s.SideA.Participants).FirstOrDefaultAsync(pa => pa.Id == id);
                if (battleWithsideAParticipants != null && battleWithsideAParticipants.SideA.Participants != null)
                    ParticipantsA = battleWithsideAParticipants.SideA.Participants.ToList();

                Battle battleWithsideBParticipants = await _db.Battle.Include(s => s.SideB.Participants).FirstOrDefaultAsync(pb => pb.Id == id);
                if (battleWithsideBParticipants != null && battleWithsideBParticipants.SideB.Participants != null)
                    ParticipantsB = battleWithsideBParticipants.SideB.Participants.ToList();

               
                SideA.Participants.Clear();                
                foreach (var partA in PartAIds)
                {
                    ParticipantA = await _db.Participant.FindAsync(partA);   
                    
                    SideA.Participants.Add(ParticipantA);
                }

                SideB.Participants.Clear();                
                foreach (var partB in PartBIds)
                {
                    ParticipantB = await _db.Participant.FindAsync(partB);
                    SideB.Participants.Add(ParticipantB);
                }
               

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
