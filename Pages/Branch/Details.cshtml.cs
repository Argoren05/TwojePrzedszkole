using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.Branch
{
    public class DetailsModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public DetailsModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

      public PlacowkaOddzial PlacowkaOddzial { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaOddzial == null)
            {
                return NotFound();
            }

            var placowkaoddzial = await _context.PlacowkaOddzial.FirstOrDefaultAsync(m => m.ID == id);
            if (placowkaoddzial == null)
            {
                return NotFound();
            }
            else 
            {
                PlacowkaOddzial = placowkaoddzial;
            }
            return Page();
        }
    }
}
