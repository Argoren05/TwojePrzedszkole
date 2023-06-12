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

namespace kindergartenAPP.Pages.LegalGuardian
{
    public class Index1Model : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public Index1Model(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<Opiekun> Opiekun { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Opiekun != null)
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Mail = User.FindFirstValue(ClaimTypes.Email);

                Opiekun = await _context.Opiekun
                .Include(o => o.Miejscowosc)
                .Where(m => m.Email == Mail)
                .Where(w => w.UzytkownikID == userID).ToListAsync();
            }
        }
    }
}
