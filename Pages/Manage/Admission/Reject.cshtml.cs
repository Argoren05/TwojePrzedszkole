using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Admission
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class RejectModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public RejectModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public PlacowkaRekrutacjaLista PlacowkaRekrutacjaLista { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaRekrutacjaLista == null)
            {
                return NotFound();
            }

            var placowkaRekrutacjaLista = await _context.PlacowkaRekrutacjaLista.FirstOrDefaultAsync(m => m.ID == id);
            if (placowkaRekrutacjaLista == null)
            {
                return NotFound();
            }
            else
            {
                PlacowkaRekrutacjaLista = placowkaRekrutacjaLista;
            }

            ViewData["MiejscowoscOpiekun"] = new SelectList(_context.Set<MiejscowoscLista>(), "ID", "Miejscowosc");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var placowkaRekrutacja = await _context.PlacowkaRekrutacja.FirstOrDefaultAsync(m => m.ID == PlacowkaRekrutacjaLista.ID);
            var placowkaID = PlacowkaRekrutacjaLista.PlacowkaID;

            if (placowkaRekrutacja == null)
            {
                placowkaRekrutacja = new PlacowkaRekrutacja();

                return NotFound();
            }
            else
            {
                placowkaRekrutacja.Akceptacja = false;
                placowkaRekrutacja.Data = DateTime.Now;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Manage/Admission/Index", new { id = placowkaID });
        }
    }
}
