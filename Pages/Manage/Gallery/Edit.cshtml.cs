﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace kindergartenAPP.Pages.Manage.Gallery
{
    [Authorize(Roles = "Zarządzanie placówką")]
    public class EditModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public EditModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PlacowkaPlik PlacowkaPlik { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PlacowkaPlik == null)
            {
                return NotFound();
            }

            var placowkaplik =  await _context.PlacowkaPlik.FirstOrDefaultAsync(m => m.ID == id);
            if (placowkaplik == null)
            {
                return NotFound();
            }
            PlacowkaPlik = placowkaplik;
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

            _context.Attach(PlacowkaPlik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacowkaPlikExists(PlacowkaPlik.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = PlacowkaPlik.PlacowkaID });
        }

        private bool PlacowkaPlikExists(int id)
        {
          return (_context.PlacowkaPlik?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
