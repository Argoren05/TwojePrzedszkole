using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using kindergartenAPP.Data;
using kindergartenAPP.ViewModels;

namespace kindergartenAPP.Pages.Manage.Admission
{
    public class ListModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public ListModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<PlacowkaRekrutacjaLista> PlacowkaRekrutacjaLista { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PlacowkaRekrutacjaLista != null)
            {
                PlacowkaRekrutacjaLista = await _context.PlacowkaRekrutacjaLista.ToListAsync();
            }
        }
    }
}
