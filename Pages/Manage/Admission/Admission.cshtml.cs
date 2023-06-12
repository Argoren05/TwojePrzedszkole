using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace kindergartenAPP.Pages.Manage
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class AdmissionModel : PageModel
    {
        private readonly kindergartenAPP.Data.applicationContext _context;

        public AdmissionModel(kindergartenAPP.Data.applicationContext context)
        {
            _context = context;
        }

        public IList<PlacowkaRekrutacjaLista> PlacowkaRekrutacjaLista { get; set; } = default!;
        public int placowkaID { get; set; }

        public async Task<IActionResult> OnGetAsync(Placowka placowka)
        {
            placowkaID = placowka.ID;

            PlacowkaRekrutacjaLista = await _context.PlacowkaRekrutacjaLista.Where(p => p.PlacowkaID == placowkaID).ToListAsync();

            return Page();
        }

        //public async Task OnGetAsync()
        //{
        //    int id = 0;

        //    if (User.FindFirst("placowkaID").Value != null)
        //    {
        //        id = Convert.ToInt32(User.FindFirst("placowkaID").Value);
        //    }

        //    if (_context.PlacowkaRekrutacjaLista != null)
        //    {
        //        PlacowkaRekrutacjaLista = await _context.PlacowkaRekrutacjaLista.Where(w => w.PlacowkaID == id).ToListAsync();
        //    }
        //}
    }
}
