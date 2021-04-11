using Conflictus.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conflictus.Controllers
{
    [Route("api/wars")]
    [ApiController]
    public class WarController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<War> Wars { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allWars = new List<War>();

            Wars = await _db.War
                .Include(w => w.Battles)
                 .ThenInclude(w => w.SideA.Participants).Distinct()
                .Include(w => w.Battles)
                 .ThenInclude(w => w.SideB.Participants).Distinct()
                .ToListAsync();


            return Json(new { data = Wars });
        }


        //Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var warFromDb = await _db.War.FirstOrDefaultAsync(w => w.Id == id);                     

            if (warFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.War.Remove(warFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "War deleted" });
        }
    }
}
