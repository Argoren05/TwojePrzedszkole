using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;

namespace kindergartenAPP.Pages.News.allNews
{
    public class CreateModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public CreateModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Aktualnosci Aktualnosci { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Aktualnosci == null || Aktualnosci == null)
          {
                return Page();
          }


            Aktualnosci.Data = DateTime.Now;

            _context.Aktualnosci.Add(Aktualnosci);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
