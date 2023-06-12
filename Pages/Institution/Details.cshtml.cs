using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using kindergartenAPP.Model;

namespace kindergartenAPP.Pages.Institution
{
    public class DetailsModel : PageModel
    {
        private readonly applicationContext _context;

        public DetailsModel(applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlacowkaDetail PlacowkaDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var placowkaDetail = new PlacowkaDetail();

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
                placowkaDetail.PlacowkaLista = placowkaLista;
            }

            var placowkaPlik = await _context.PlacowkaPlik.Where(w => w.PlacowkaID == id).ToListAsync();
            placowkaDetail.PlacowkaPlik = placowkaPlik;

            PlacowkaDetail = placowkaDetail;

            return Page();
        }
    }
}
