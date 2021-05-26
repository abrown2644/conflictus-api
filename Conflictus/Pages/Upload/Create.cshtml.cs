using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Conflictus.Model;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Upload
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
        public Battle Battle { get; set; }
        public War War { get; set; }
        public Location Location { get; set; }
        public List<Battle> Battles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //var json = System.IO.File.ReadAllText("wwwroot/battles.json");

            ////Battle deserializedBattle = JsonConvert.DeserializeObject<Battle>(json);


            //var myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(json);


            //foreach (var item in myDeserializedClass)
            //{
            //    Battle battle;
            //    var yearArray = item.date.Split('-');
            //    string theYear;
            //    string theDate = item.date;
            //    string finalDate;
            //    if (yearArray.Length > 3)
            //    {
            //        theYear = "-" + yearArray[1];
            //        theDate = yearArray[1] + "-" + yearArray[2] + "-" + yearArray[3];
            //        var parsedDate = DateTime.Parse(theDate).ToString("dd MMMM yyyy");
            //        finalDate = parsedDate + " BC";
            //    }
            //    else
            //    {
            //        theYear = yearArray[0];
            //        finalDate = DateTime.Parse(theDate).ToString("dd MMMM yyyy");
            //    }

            //    var matchedWar = await _db.War.Where(w => w.Name == item.war).FirstOrDefaultAsync();
            //    //var matchedWar = await _db.War.FirstOrDefaultAsync(w => w.Name == item.war);

            //    //string warName;
            //    if (matchedWar == null)
            //    {
            //        battle = new Battle
            //        {

            //            Title = item.title,
            //            Date = finalDate,
            //            Year = int.Parse(theYear),
            //            Url = item.link,
            //            War = new War
            //            {
            //                Name = item.war
            //            },
            //            Location = new Location
            //            {
            //                Latitude = float.Parse(item.latitude),
            //                Longitude = float.Parse(item.longitude),
            //                HasCoordinates = true
            //            },
            //            SideA = new SideA(),
            //            SideB = new SideB()

            //        };
            //    }
            //    else
            //    {
            //        battle = new Battle
            //        {

            //            Title = item.title,
            //            Date = finalDate,
            //            Year = int.Parse(theYear),
            //            Url = item.link,
            //            War = await _db.War.FindAsync(matchedWar.Id),
            //            Location = new Location
            //            {
            //                Latitude = float.Parse(item.latitude),
            //                Longitude = float.Parse(item.longitude),
            //                HasCoordinates = true
            //            },
            //            SideA = new SideA(),
            //            SideB = new SideB()

            //        };
            //    }



            //    await _db.Battle.AddAsync(battle);
            //    await _db.SaveChangesAsync();
            //}




            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Battle.Add(Battle);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}


public class Root
{
    public string title { get; set; }
    public string war { get; set; }
    public string date { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string link { get; set; }
}