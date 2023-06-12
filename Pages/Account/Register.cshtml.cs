#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using kindergartenAPP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using System.Security.Claims;

namespace kindergartenAPP.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Uzytkownik> _signInManager;
        private readonly UserManager<Uzytkownik> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<Uzytkownik> _userStore;
        private readonly IUserEmailStore<Uzytkownik> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly applicationContext _context;

        public RegisterModel(
            SignInManager<Uzytkownik> signInManager,
            UserManager<Uzytkownik> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<Uzytkownik> userStore,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            applicationContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
            [EmailAddress]
            [Display(Name = "Adres E-mail")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Has≥o jest wymagane.")]
            [StringLength(100, ErrorMessage = "Has≥o {0} musi mieÊ minimum {2} a maksymalnie {1} znakÛw.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Has≥o")]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Potwierdü has≥o")]
            [Compare("Password", ErrorMessage = "Podane has≥a nie sπ zgodne.")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Naleøy wybraÊ rodzaj konta")]
            [Display(Name = "Typ konta")]
            public byte AccountType { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    switch (Input.AccountType)
                    {
                        case 1:
                            Opiekun opiekun = new Opiekun();
                            opiekun.UzytkownikID = user.Id;
                            opiekun.Imie = "brak";
                            opiekun.Nazwisko = "brak";
                            opiekun.Ulica = "brak";
                            opiekun.Dom = "brak";
                            opiekun.Lokal = "brak";
                            opiekun.Telefon = "brak";
                            opiekun.Komorka = "brak";
                            opiekun.Email = user.Email;

                            _context.Opiekun.Add(opiekun);
                            await _context.SaveChangesAsync();

                            var claim1 = new Claim("opiekunID", opiekun.ID.ToString());
                            if (claim1 != null)
                            {
                                await _userManager.AddClaimAsync(user, claim1);
                            }

                            await _userManager.AddToRoleAsync(user, "Opiekunowie");

                            break;
                        case 2:
                            Placowka placowka = new Placowka();
                            placowka.UzytkownikID = user.Id;
                            placowka.PlacowkaTypID = 0;
                            placowka.Nazwa = "brak";

                            _context.Placowka.Add(placowka);
                            await _context.SaveChangesAsync();

                            var claim2 = new Claim("placowkaID", placowka.ID.ToString());
                            if (claim2 != null)
                            {
                                await _userManager.AddClaimAsync(user, claim2);
                            }

                            await _userManager.AddToRoleAsync(user, "Zarzπdzanie placÛwkπ");

                            break;
                    }


                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (Input.AccountType == 1)
                    {
                        await _emailSender.SendEmailAsync(Input.Email, "Potwierdü adres e-mail",
                        $"Potwierdü swoje konto w portalu twojeprzedszkole.pl <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikajπc w link</a>.");
                    }
                    else
                    {
                        await _emailSender.SendEmailAsync("admin@softcell.pl", "Potwierdü konto zarzπdcy",
                        $"Uøytkownik {Input.Email} zarejestrowa≥ konto w portalu twojeprzedszkole.pl, potwierdü je <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klikajπc w link</a>. i powiadom uøytkownika" );

                        await _emailSender.SendEmailAsync(Input.Email, "Potwierdü adres e-mail",
                            $"Twoje konto w portalu twojeprzedszkole.pl zosta≥o utworzone i czeka na potwierdzenie przez administratora");
                    }

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        if (Input.AccountType == 1)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            return RedirectToPage("RegisterConfirmation2", new { email = Input.Email, returnUrl = returnUrl });
                        }
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private Uzytkownik CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Uzytkownik>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Uzytkownik)}'. " +
                    $"Ensure that '{nameof(Uzytkownik)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Uzytkownik> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Uzytkownik>)_userStore;
        }
    }
}
