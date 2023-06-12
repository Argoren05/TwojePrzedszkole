using kindergartenAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using kindergartenAPP.Extensions;
using kindergartenAPP.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null).AddRazorRuntimeCompilation();
services.AddDbContextPool<applicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
services.AddIdentity<Uzytkownik, IdentityRole>().AddEntityFrameworkStores<applicationContext>().AddDefaultTokenProviders();
services.AddTransient<IEmailSender, EmailSender>();

//Ustawienie wysy�ania maila z aktywacj� konta
services.Configure<EmailSenderOptions>(options =>
{
    options.HostAddress = configuration.GetSection("appSettings").GetValue<string>(key: "hostAddress");
    options.HostPort = configuration.GetSection("appSettings").GetValue<int>(key: "hostPort");
    options.HostUsername = configuration.GetSection("appSettings").GetValue<string>(key: "hostUsername");
    options.HostPassword = configuration.GetSection("appSettings").GetValue<string>(key: "hostPassword");
    options.SenderEMail = configuration.GetSection("appSettings").GetValue<string>(key: "senderEmail");
    options.SenderName = configuration.GetSection("appSettings").GetValue<string>(key: "senderName");
});

//Dodanie lokalizacji
services.AddLocalization();

//Ustawienia uwierzetylniania oraz ustawienia plik�w cookie
services.AddAuthentication(
     options =>
     {
         options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     })
     .AddCookie(
         options =>
         {
             options.LoginPath = "/Account/Login";
             options.LogoutPath = "/Acount/Logout";
             options.AccessDeniedPath = "/Account/AccessDenied";
             options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
         });

services.Configure<IdentityOptions>(options =>
{
    // Ustawienia has�a
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 5;

    // Ustawienia blokady
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20); // d�ugo�� zablokowania konta po wykorzystaniu wszystkich pr�b
    options.Lockout.MaxFailedAccessAttempts = 5; // ilo�� dozwolonych pr�b logowania
    options.Lockout.AllowedForNewUsers = true;

    // Ustawienia nazwy u�ytkownika
    options.User.AllowedUserNameCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]\{}|;':,.<>?";
    options.User.RequireUniqueEmail = true; // Nazwa u�ytkownika jako adres email

    // Akceptacja konta za pomoc� email
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
});

//Ustawienia autoryzacji
services.AddAuthorization();
services.AddAuthentication();

var cultureInfo = new CultureInfo("pl-PL");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
