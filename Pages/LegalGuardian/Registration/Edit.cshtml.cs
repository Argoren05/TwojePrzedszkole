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

namespace kindergartenAPP.Pages.LegalGuardian.Registration
{
    [Authorize(Roles = "Opiekunowie")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
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

            var dziecko =  await _context.Dziecko.FirstOrDefaultAsync(m => m.ID == id);
            if (dziecko == null)
            {
                return NotFound();
            }
            Dziecko = dziecko;
            ViewData["NarodowoscID"] = new SelectList(_context.Narodowosc, "ID", "Opis");
            ViewData["ObywatelstwoID"] = new SelectList(_context.Obywatelstwo, "ID", "Opis");
            ViewData["PlecKOD"] = new SelectList(_context.Set<Plec>(), "KOD", "Opis");
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

            _context.Attach(Dziecko).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DzieckoExists(Dziecko.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = Dziecko.OpiekunID });
        }

        private bool DzieckoExists(int id)
        {
          return (_context.Dziecko?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
