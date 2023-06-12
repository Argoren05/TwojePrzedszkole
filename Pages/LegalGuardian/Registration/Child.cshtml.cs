using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.LegalGuardian
{
    public class ChildModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public ChildModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<Dziecko> Dziecko { get;set; } = default!;
        public int opiekunID { get; set; }

        public async Task OnGetAsync()
        {

            opiekunID = Convert.ToInt32(User.FindFirst("opiekunID").Value);

            if (_context.Dziecko != null)
            {
                
                Dziecko = await _context.Dziecko
                .Include(d => d.Narodowosc)
                .Include(d => d.Obywatelstwo)
                .Include(d => d.Placowka)
                .Include(d => d.PlacowkaOddzial)
                .Include(d => d.Plec)
                .Where(d => d.OpiekunID == opiekunID).ToListAsync();
            }
        }
    }
}
