using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.ViewModels;

namespace kindergartenAPP.Pages.News
{
    public class _InstitutionPartialViewModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public _InstitutionPartialViewModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<PlacowkaLista> PlacowkaLista { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PlacowkaLista != null)
            {
                PlacowkaLista = await _context.PlacowkaLista.ToListAsync();
            }
        }
    }
}
