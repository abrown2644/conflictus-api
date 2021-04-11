using Conflictus.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Controllers
{
    [Route("api/battles")]
    [ApiController]
    public class BattleController : Controller
    {        
        private readonly ApplicationDbContext _db;

        public BattleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public Location Location { get; set; }
        public SideA SideA { get; set; }
        public SideB SideB { get; set; }

        //For Get
        public ICollection<Battle> Battles { get; set; }
        public List<Location> Locations { get; set; }        
        public List<SideA> SideAs { get; set; }
        public List<SideB> SideBs { get; set; }
        public List<Participant> ParticipantsA { get; set; }
        public List<Participant> ParticipantsB { get; set; }

        //All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _db.Battle
                .Include(b => b.Location)
                .Include(b => b.SideA)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.SideB)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.War).ToListAsync();

            //return Json(new { data = await _db.Battle.ToListAsync() });            
            //return Json(new { data = await results.ToListAsync() });
            return Json(new { data = results });
        }
        
        //By War
        //api/battles/war=2
        [HttpGet]
        [Route("war={warId}")]
        public async Task<IActionResult> GetByWar([FromRoute] int warId)
        {
            var results = await _db.Battle
                .Include(b => b.Location)
                .Include(b => b.SideA)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.SideB)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.War)
                .Where(b => b.War.Id == warId).ToListAsync();

            return Json(new { data = results });
        }

        //By Date Range
        //api/battles/date=1942
        [HttpGet("date={minDate},{maxDate}")]
        public async Task<IActionResult> GetByDate(int minDate, int maxDate)
        {
            var results = await _db.Battle
                .Include(b => b.Location)
                .Include(b => b.SideA)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.SideB)
                    .ThenInclude(b => b.Participants)
                .Include(b => b.War)
                .Where(b => b.Year >= minDate && b.Year <= maxDate).ToListAsync();

            return Json(new { data = results });
        }




        //Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var battleFromDb = await _db.Battle.FirstOrDefaultAsync(u => u.Id == id);
            
            //Location
            Battle battleWithLocation = await _db.Battle.Include(b => b.Location).FirstOrDefaultAsync(l => l.Id == id);
            if (battleWithLocation != null && battleWithLocation.Location != null)
                Location = await _db.Location.FindAsync(battleWithLocation.Location.Id);

            //SideA
            Battle battleWithSideA = await _db.Battle.Include(b => b.SideA).FirstOrDefaultAsync(a => a.Id == id);
            if (battleWithSideA != null && battleWithSideA.SideA != null)
                SideA = await _db.SideA.FindAsync(battleWithSideA.SideA.Id);

            //SideB
            Battle battleWithSideB = await _db.Battle.Include(b => b.SideB).FirstOrDefaultAsync(b => b.Id == id);
            if (battleWithSideB != null && battleWithSideB.SideB != null)
                SideB = await _db.SideB.FindAsync(battleWithSideA.SideB.Id);            



            if (Location != null)
                _db.Location.Remove(Location);
            if (SideA != null)
                _db.SideA.Remove(SideA);
            if (SideB != null)
                _db.SideB.Remove(SideB);            

            if (battleFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Battle.Remove(battleFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Battle deleted" });
        }
    }
}
