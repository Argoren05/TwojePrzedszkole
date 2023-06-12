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
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class AddModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public AddModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PlacowkaTypID"] = new SelectList(_context.Set<PlacowkaTyp>(), "ID", "Opis");
            ViewData["MiejscowoscID"] = new SelectList(_context.Set<MiejscowoscLista>(), "ID", "Miejscowosc");

            return Page();
        }

        [BindProperty]
        public Placowka Placowka { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Placowka == null || Placowka == null)
            {
                return Page();
            }

            Placowka.UzytkownikID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Placowka.Add(Placowka);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
