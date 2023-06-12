using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Branch
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class CreateModel : PageModel
    {
        private readonly applicationContext _context;

        public CreateModel(applicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public PlacowkaOddzial placowkaOddzial { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            int placowkaID = id;

            placowkaOddzial = new PlacowkaOddzial();
            placowkaOddzial.PlacowkaID = placowkaID;
            placowkaOddzial.Rok = DateTime.Today.Year;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PlacowkaOddzial == null || placowkaOddzial == null)
            {
                return Page();
            }

            _context.PlacowkaOddzial.Add(placowkaOddzial);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index", new { id = placowkaOddzial.PlacowkaID });
        }
    }
}
