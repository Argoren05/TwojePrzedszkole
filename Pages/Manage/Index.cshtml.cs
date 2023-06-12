using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace kindergartenAPP.Pages.Manage
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class IndexModel : PageModel
    {
        private readonly applicationContext _context;

        public IndexModel(applicationContext context)
        {
            _context = context;
        }

        public List<Placowka> placowka { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            placowka = _context.Placowka.Where(w => w.UzytkownikID == userID).ToList();

            return Page();
        }
    }
}

