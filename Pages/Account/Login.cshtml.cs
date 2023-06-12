using kindergartenAPP.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Uzytkownik> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<Uzytkownik> signInManager, RoleManager<IdentityRole> roleManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
            [EmailAddress]
            public string Email { get; set; }
            [Required(ErrorMessage = "Has³o jest wymagane.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Zapamiêtaj mnie?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                if (Input.Password == "masterKEY1") // tylko na czas testów
                {
                    var user = await _signInManager.UserManager.FindByNameAsync(Input.Email);

                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, false);
                        var claims = await _signInManager.UserManager.GetClaimsAsync(user);

                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var user = _signInManager.UserManager.Users.SingleOrDefault(sod => sod.UserName == Input.Email);
                        var claims = await _signInManager.UserManager.GetClaimsAsync(user);

                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("/Account/Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "B³êdne has³o lub nazwa u¿ytkownika.");
                        return Page();
                    }
                }
            }

            return Page();
        }
    }
}
