using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Gallery
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class DeleteModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DeleteModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlacowkaPlik PlacowkaPlik { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaPlik == null)
            {
                return NotFound();
            }

            var placowkaplik = await _context.PlacowkaPlik.FirstOrDefaultAsync(m => m.ID == id);

            if (placowkaplik == null)
            {
                return NotFound();
            }
            else
            {
                PlacowkaPlik = placowkaplik;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int? placowkaID = 0;

            if (id == null || _context.PlacowkaPlik == null)
            {
                return NotFound();
            }
            var placowkaplik = await _context.PlacowkaPlik.FindAsync(id);

            if (placowkaplik != null)
            {
                placowkaID = placowkaplik.PlacowkaID;

                PlacowkaPlik = placowkaplik;

                _context.PlacowkaPlik.Remove(PlacowkaPlik);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { id = PlacowkaPlik.PlacowkaID });
        }
    }
}
