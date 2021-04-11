using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Conflictus.Model;
using Microsoft.AspNetCore.Authorization;

namespace Conflictus.Pages.Participants
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Conflictus.Model.ApplicationDbContext _context;

        public IndexModel(Conflictus.Model.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Participant> Participant { get;set; }

        public async Task OnGetAsync()
        {
            Participant = await _context.Participant.ToListAsync();
        }
    }
}
