using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.LegalGuardian.Registration
{
    [Authorize(Roles = "Opiekunowie")]
    public class DeleteModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DeleteModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Dziecko Dziecko { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dziecko == null)
            {
                return NotFound();
            }

            var dziecko = await _context.Dziecko
                .Include(p => p.Plec)
                .Include(o => o.Obywatelstwo)
                .Include(n => n.Narodowosc)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (dziecko == null)
            {
                return NotFound();
            }
            else 
            {
                Dziecko = dziecko;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Dziecko == null)
            {
                return NotFound();
            }
            var dziecko = await _context.Dziecko.FindAsync(id);

            if (dziecko != null)
            {
                Dziecko = dziecko;
                _context.Dziecko.Remove(Dziecko);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { id = Dziecko.OpiekunID });
        }
    }
}
