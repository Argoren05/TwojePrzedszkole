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
using System.Collections.Immutable;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Parent
{
    [Authorize(Roles = "Opiekunowie")]
    public class IndexModel : PageModel
    {
        private readonly applicationContext _context;

        public IndexModel(applicationContext context)
        {
            _context = context;
        }

        public IList<Opiekun> opiekun { get;set; } = default!;


        //public async Task OnGetAsync()
        //{
        //    var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);


        //    if (_context.Opiekun != null)
        //    {
        //        opiekun = _context.Opiekun
        //          .Include(d => d.Miejscowosc)
        //          .Where(w => w.UzytkownikID == userID).ToList();
        //    }
        //}
        public async Task<IActionResult> OnGetAsync()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                opiekun = _context.Opiekun
                    .Include(d => d.Miejscowosc)
                    .Where(w => w.UzytkownikID == userID).ToList();

            return Page();
        }
    }
}
