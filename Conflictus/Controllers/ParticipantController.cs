using Conflictus.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ParticipantController(ApplicationDbContext db)
        {
            _db = db;
        }

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

            var results = await _db.Participant
                .Include(p => p.Wars)                
                .ToListAsync();

            return Json(new { data = results });
        }

        //Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var participantFromDb = await _db.Participant.FirstOrDefaultAsync(p => p.Id == id);

            if (participantFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Participant.Remove(participantFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Participant deleted" });
        }
    }
}
