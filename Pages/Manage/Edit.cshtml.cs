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
using kindergartenAPP.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.NewFolder
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
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

            var placowka =  await _context.Placowka.FirstOrDefaultAsync(m => m.ID == id);
            if (placowka == null)
            {
                return NotFound();
            }
            Placowka = placowka;
            ViewData["PlacowkaTypID"] = new SelectList(_context.Set<PlacowkaTyp>(), "ID", "Opis");
            ViewData["MiejscowoscID"] = new SelectList(_context.Set<MiejscowoscLista>(), "ID", "Miejscowosc");
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

            

            _context.Attach(Placowka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacowkaExists(Placowka.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlacowkaExists(int id)
        {
          return (_context.Placowka?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
