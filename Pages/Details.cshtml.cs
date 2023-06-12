using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kindergartenAPP.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DetailsModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public PlacowkaLista PlacowkaLista { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaLista == null)
            {
                return NotFound();
            }

            var placowkaLista = await _context.PlacowkaLista.FirstOrDefaultAsync(m => m.ID == id);
            if (placowkaLista == null)
            {
                return NotFound();
            }
            else
            {
                PlacowkaLista = placowkaLista;
            }

            return Page();
        }
    }
}
