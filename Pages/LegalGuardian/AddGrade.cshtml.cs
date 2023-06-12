using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.LegalGuardian
{
    [Authorize(Roles = "Rodzice")]
    public class AddGradeModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public AddGradeModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            int placowkaID = id;

            OcenaPlacowka = new OcenaPlacowka();
            OcenaPlacowka.PlacowkaID = placowkaID;
            OcenaPlacowka.UzytkownikID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OcenaPlacowka.Ocena = 5;
            OcenaPlacowka.Data = DateTime.Now;

            return Page();
        }

        [BindProperty]
        public OcenaPlacowka OcenaPlacowka { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.OcenaPlacowka.Any(a => a.PlacowkaID == OcenaPlacowka.PlacowkaID && a.UzytkownikID == OcenaPlacowka.UzytkownikID))
            {
                ModelState.AddModelError("OcenaPlacowka", "Wystawiłeś już ocenę tej placówce.");
            }

            if (!ModelState.IsValid || _context.OcenaPlacowka == null || OcenaPlacowka == null)
            {
                return Page();
            }

            _context.OcenaPlacowka.Add(OcenaPlacowka);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Institution/Details", new { id = OcenaPlacowka.PlacowkaID });
        }
    }
}
