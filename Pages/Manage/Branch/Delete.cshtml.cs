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

namespace kindergartenAPP.Pages.Manage.Branch
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class DeleteModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DeleteModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PlacowkaOddzial placowkaOddzial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaOddzial == null)
            {
                return NotFound();
            }

            var placowkaoddzial = await _context.PlacowkaOddzial.FirstOrDefaultAsync(m => m.ID == id);

            if (placowkaoddzial == null)
            {
                return NotFound();
            }
            else 
            {
                placowkaOddzial = placowkaoddzial;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int placowkaID = 0;

            if (id == null || _context.PlacowkaOddzial == null)
            {
                return NotFound();
            }
            var placowkaoddzial = await _context.PlacowkaOddzial.FindAsync(id);

            if (placowkaoddzial != null)
            {
                placowkaID = placowkaoddzial.PlacowkaID;

                placowkaOddzial = placowkaoddzial;
                _context.PlacowkaOddzial.Remove(placowkaOddzial);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index", new { id = placowkaID });
        }
    }
}
