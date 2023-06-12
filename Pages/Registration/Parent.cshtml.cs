using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace kindergartenAPP.Pages.Registration
{
    public class ParentModel : PageModel
    {
        private readonly applicationContext _context;

        public ParentModel(applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Opiekun Opiekun { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int id = 0;

            if (User.FindFirst("opiekunID").Value != null)
            {
                id = Convert.ToInt32(User.FindFirst("opiekunID").Value);
            }

            if (id == 0 || _context.Opiekun == null)
            {
                return NotFound();
            }

            var opiekun = await _context.Opiekun.FirstOrDefaultAsync(m => m.ID == id);

            if (opiekun == null)
            {
                return NotFound();
            }

            Opiekun = opiekun;

            return Page();
        }

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

            return RedirectToPage("/Registration/Index");
        }

        private bool OpiekunExists(int id)
        {
            return (_context.Opiekun?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
