using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using System.Security.Claims;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Branch
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class IndexModel : PageModel
    {
        private readonly applicationContext _context;

        public IndexModel(applicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public IList<PlacowkaOddzial> PlacowkaOddzial { get; set; } = default!;
        public int placowkaID { get; set; }

        public async Task<IActionResult> OnGetAsync(Placowka placowka)
        {
            placowkaID = placowka.ID;

            PlacowkaOddzial = await _context.PlacowkaOddzial.Where(p => p.PlacowkaID == placowkaID).ToListAsync();

            return Page();
        }
    }
}
