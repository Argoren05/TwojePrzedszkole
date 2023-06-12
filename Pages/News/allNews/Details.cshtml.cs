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
    public class DetailsModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DetailsModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

      public Aktualnosci Aktualnosci { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Aktualnosci == null)
            {
                return NotFound();
            }

            var aktualnosci = await _context.Aktualnosci.FirstOrDefaultAsync(m => m.ID == id);
            if (aktualnosci == null)
            {
                return NotFound();
            }
            else 
            {
                Aktualnosci = aktualnosci;
            }
            return Page();
        }
    }
}
