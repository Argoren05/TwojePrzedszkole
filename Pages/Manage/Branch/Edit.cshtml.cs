using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Branch
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public PlacowkaOddzial placowkaOddzial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaOddzial == null)
            {
                return NotFound();
            }

            var placowkaoddzial =  await _context.PlacowkaOddzial.FirstOrDefaultAsync(m => m.ID == id);
            if (placowkaoddzial == null)
            {
                return NotFound();
            }

            placowkaOddzial = placowkaoddzial;
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(placowkaOddzial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacowkaOddzialExists(placowkaOddzial.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index", new { id = placowkaOddzial.PlacowkaID });
        }

        private bool PlacowkaOddzialExists(int id)
        {
          return (_context.PlacowkaOddzial?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
