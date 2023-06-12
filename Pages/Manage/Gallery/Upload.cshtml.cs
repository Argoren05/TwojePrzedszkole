using kindergartenAPP.Data;
using kindergartenAPP.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using static kindergartenAPP.Pages.IndexModel;

namespace kindergartenAPP.Pages.Manage.Gallery
{
    [Authorize(Roles = "Zarz¹dzanie placówk¹")]
    public class UploadModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly applicationContext _context;
        public static int placowkaID;

        public UploadModel(IWebHostEnvironment environment, applicationContext context)
        {
            _environment = environment;
            _context = context;
        }

        [BindProperty]
        public UploadFile File { get; set; }
        public int returnID;

        [TempData]
        public string UploadFileMessage { get; set; }
        public class UploadFile
        {
            public IFormFile Plik { get; set; }
            public string? Opis { get; set; }
            public bool Glowny { get; set; }
        }

        public IActionResult OnGet(int id)
        {
            placowkaID = id;
            returnID = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string guid = Guid.NewGuid().ToString().Replace("-", "");
            string folder = Path.Combine(_environment.WebRootPath, "photos", guid);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = Path.Combine(folder + @"\" + File.Plik.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await File.Plik.CopyToAsync(fileStream);
            }

            PlacowkaPlik placowkaPlik = new PlacowkaPlik();

            placowkaPlik.PlacowkaID = placowkaID;
            placowkaPlik.Nazwa = Path.GetFileName(File.Plik.FileName);
            placowkaPlik.Rozszerzenie = Path.GetExtension(File.Plik.FileName);
            placowkaPlik.Folder = @"\photos\" + guid + @"\";
            placowkaPlik.Link = @"\photos\" + guid + @"\" + File.Plik.FileName;
            placowkaPlik.Opis = File.Opis;
            placowkaPlik.Data = DateTime.Now;
            placowkaPlik.Glowny = File.Glowny;

            _context.PlacowkaPlik.Add(placowkaPlik);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = placowkaPlik.PlacowkaID });
        }
    }
}
