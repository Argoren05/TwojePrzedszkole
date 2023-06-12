using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Manage
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Placowka Placowka { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int id = 0;

            if (User.FindFirst("placowkaID").Value != null)
            {
                id = Convert.ToInt32(User.FindFirst("placowkaID").Value);
            }

            var placowka = await _context.Placowka.FirstOrDefaultAsync(m => m.ID == id);
            if (placowka == null)
            {
                return NotFound();
            }

            Placowka = placowka;

            ViewData["PlacowkaTypID"] = new SelectList(_context.Set<PlacowkaTyp>(), "ID", "Opis");

            return Page();
        }

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

            return RedirectToPage("/Manage/Index");
        }

        private bool PlacowkaExists(int id)
        {
            return (_context.Placowka?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
