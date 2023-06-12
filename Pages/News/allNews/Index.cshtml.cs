using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.News.allNews
{
    public class IndexModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public IndexModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<Aktualnosci> Aktualnosci { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Aktualnosci != null)
            {
                Aktualnosci = await _context.Aktualnosci.OrderByDescending(n => n.ID).ToListAsync();
            }
        }
    }
}
