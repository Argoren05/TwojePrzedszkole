using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Gallery
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class IndexModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public IndexModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<PlacowkaPlik> placowkaPlik { get; set; } = default!;
        public int placowkaID { get; set; }

        public async Task OnGetAsync(int id)
        {
            placowkaID = id;

            if (_context.PlacowkaPlik != null)
            {
                placowkaPlik = await _context.PlacowkaPlik.
                    Where(w => w.PlacowkaID == placowkaID).ToListAsync();
            }
        }

    }
}
