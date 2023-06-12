using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.News.allNews
{
    public class DeleteModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DeleteModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Aktualnosci Aktualnosci { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Aktualnosci == null)
            {
                return NotFound();
            }

            var aktualnosci = await _context.Aktualnosci.FirstOrDefaultAsync(m => m.ID == id);

            if (aktualnosci == null)
            {
                return NotFound();
            }
            else 
            {
                Aktualnosci = aktualnosci;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Aktualnosci == null)
            {
                return NotFound();
            }
            var aktualnosci = await _context.Aktualnosci.FindAsync(id);

            if (aktualnosci != null)
            {
                Aktualnosci = aktualnosci;
                _context.Aktualnosci.Remove(Aktualnosci);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
