using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly applicationContext _context;

        public IndexModel(ILogger<IndexModel> logger, applicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public SearchModel Search { get; set; }
        public IList<PlacowkaLista> PlacowkaLista { get; set; }
        //public IList<Nowosc> Nowosc { get; set; }

        [TempData]
        public string SearchMessage { get; set; }
        public class SearchModel
        {
            public string? Nazwa { get; set; }
            public string? Miejscowosc { get; set; }
            public string? Dzielnica { get; set; }
            public string? Ulica { get; set; }
            public string? Typ { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id = -1)
        {
            if (_context.PlacowkaLista == null)
            {
                return NotFound();
            }

            var placowkaLista = await _context.PlacowkaLista.Where(w => w.PlacowkaTypID == id || id == -1).OrderByDescending(m => m.ID).ToListAsync();

            if (placowkaLista == null )
            {
                return NotFound();
            }
            else
            {
                PlacowkaLista = placowkaLista;
            }

            //Nowosc = await _context.Nowosc.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var nazwa = "*";
            var typ = "*";
            var miejscowosc = "*";
            var dzielnica = "*";
            var ulica = "*";

            if (Search != null)
            {
                nazwa = Search.Nazwa == null ? "*" : Search.Nazwa;
                typ = Search.Typ == null ? "*" : Search.Typ;
                miejscowosc = Search.Miejscowosc == null ? "*" : Search.Miejscowosc;
                dzielnica = Search.Dzielnica == null ? "*" : Search.Dzielnica;
                ulica = Search.Ulica == null ? "*" : Search.Ulica;
            }

            var placowkaLista = await _context.PlacowkaLista
                .Where(w => (w.Nazwa == nazwa || nazwa == "*") 
                    && (w.PlacowkaTyp == typ || typ == "*")
                    && (w.Miejscowosc == miejscowosc || miejscowosc == "*")
                    && (w.Dzielnica == dzielnica || dzielnica == "*")                    
                    && (w.Ulica == ulica || ulica == "*"))
                .ToListAsync();

            if (placowkaLista == null)
            {
                return NotFound();
            }
            else
            {
                PlacowkaLista = placowkaLista;
            }

            return Page();
        }
    }
}