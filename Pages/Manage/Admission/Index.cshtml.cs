using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using kindergartenAPP.Data;

namespace kindergartenAPP.Pages.Manage.Admission
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class IndexModel : PageModel
    {
        private readonly applicationContext _context;

        public IndexModel(applicationContext context)
        {
            _context = context;
        }

        public IList<PlacowkaRekrutacjaLista> PlacowkaRekrutacjaLista { get; set; } = default!;
        public int placowkaID { get; set; }

        public async Task<IActionResult> OnGetAsync(Placowka placowka)
        {
            placowkaID = placowka.ID;

            PlacowkaRekrutacjaLista = await _context.PlacowkaRekrutacjaLista.Where(p => p.PlacowkaID == placowkaID).ToListAsync();

            return Page();
        }
    }
}
