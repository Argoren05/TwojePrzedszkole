using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Registration
{
    [Authorize(Roles = "Opiekunowie")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly applicationContext _context;

        public IndexModel(ILogger<IndexModel> logger, applicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Dziecko> Dziecko { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            int id = 0;

            if (User.FindFirst("opiekunID").Value != null)
            {
                id = Convert.ToInt32(User.FindFirst("opiekunID").Value);
            }

            if (_context.Dziecko == null)
            {
                return NotFound();
            }

            var dziecko = await _context.Dziecko
                .Include(p => p.Plec)
                .Include(n => n.Narodowosc)
                .Include(o => o.Obywatelstwo)
                .Where(w => w.OpiekunID == id).ToListAsync();

            if (dziecko == null)
            {
                return NotFound();
            }
            else
            {
                Dziecko = dziecko;
            }

            return Page();
        }
    }
}
