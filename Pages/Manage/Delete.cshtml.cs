using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class DeleteModel : PageModel
    {
        private readonly applicationContext _context;

        public DeleteModel(applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Placowka Placowka { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Placowka == null)
            {
                return NotFound();
            }

            var placowka = await _context.Placowka.Include(p => p.PlacowkaTyp).FirstOrDefaultAsync(m => m.ID == id);

            if (placowka == null)
            {
                return NotFound();
            }
            else 
            {
                Placowka = placowka;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Placowka == null)
            {
                return NotFound();
            }
            var placowka = await _context.Placowka.FindAsync(id);

            if (placowka != null)
            {
                Placowka = placowka;
                _context.Placowka.Remove(Placowka);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
