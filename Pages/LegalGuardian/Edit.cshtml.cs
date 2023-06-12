using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Parent
{
    [Authorize(Roles = "Opiekunowie")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Opiekun Opiekun { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Opiekun == null)
            {
                return NotFound();
            }

            var opiekun =  await _context.Opiekun.FirstOrDefaultAsync(m => m.ID == id);
            if (opiekun == null)
            {
                return NotFound();
            }

            Opiekun = opiekun;
            ViewData["MiejscowoscID"] = new SelectList(_context.Set<MiejscowoscLista>(), "ID", "Miejscowosc");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Opiekun).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpiekunExists(Opiekun.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OpiekunExists(int id)
        {
          return (_context.Opiekun?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
