using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.LegalGuardian
{
    [Authorize(Roles = "Opiekunowie")]
    public class CreateModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public CreateModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            int placowkaID = id;

            Dziecko = new Dziecko();
            Dziecko.PlacowkaID = placowkaID;
            Dziecko.OpiekunID = Convert.ToInt32(User.FindFirst("opiekunID").Value);
            Dziecko.ObywatelstwoID = -1;
            Dziecko.NarodowoscID = -1;

            ViewData["NarodowoscID"] = new SelectList(_context.Narodowosc, "ID", "Opis");
            ViewData["ObywatelstwoID"] = new SelectList(_context.Obywatelstwo, "ID", "Opis");
            ViewData["PlacowkaOddzialID"] = new SelectList(_context.PlacowkaOddzial.Where(w => w.PlacowkaID == id), "ID", "Nazwa");
            ViewData["PlecKOD"] = new SelectList(_context.Set<Plec>(), "KOD", "Opis");

            return Page();
        }

        [BindProperty]
        public Dziecko Dziecko { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Dziecko.Any(a => a.Pesel == Dziecko.Pesel && a.PlacowkaID == Dziecko.PlacowkaID))
            {
                ModelState.AddModelError("Dziecko", "Dziecko już jest zarejestrowane w tej placówce.");
            }

            if (!ModelState.IsValid || _context.Dziecko == null || Dziecko == null)
            {
                return Page();
            }

            _context.Dziecko.Add(Dziecko);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
