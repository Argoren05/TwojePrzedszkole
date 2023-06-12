using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace kindergartenAPP.Pages.Registration
{
    [Authorize(Roles = "Opiekunowie")]
    public class CreateModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;
        public static int? placowkaID { get; set; }

        public CreateModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Plec"] = new SelectList(_context.Set<Plec>(), "KOD", "Opis");
            ViewData["Narodowosc"] = new SelectList(_context.Set<Narodowosc>(), "ID", "Opis");
            ViewData["Obywatelstwo"] = new SelectList(_context.Set<Obywatelstwo>(), "ID", "Opis");
            placowkaID = id;

            return Page();
        }

        [BindProperty]        
        public Dziecko Dziecko { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Dziecko == null || Dziecko == null)
            {
                return Page();
            }

            int id = 0;

            if (User.FindFirst("opiekunID").Value != null)
            {
                id = Convert.ToInt32(User.FindFirst("opiekunID").Value);
            }

            if (id == 0)
            {
                return Page();
            }

            Dziecko.OpiekunID = id;
            Dziecko.PlacowkaID = placowkaID;

            _context.Dziecko.Add(Dziecko);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Registration/Index");
        }
    }
}
