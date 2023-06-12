using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.News
{
    public class _NewsPartialViewModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public _NewsPartialViewModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<Nowosc> Nowosc { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Nowosc != null)
            {
                Nowosc = await _context.Nowosc.ToListAsync();
            }
        }
    }
}
