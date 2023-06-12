using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Placowka
{
    [Key]
    public int ID { get; set; }
    public string? UzytkownikID { get; set; }
    [Required(ErrorMessage = "Typ placówki jest wymagany")]
    [Display(Name = "Typ placówki")]
    public int? PlacowkaTypID { get; set; }
    [Required(ErrorMessage = "Nazwa placówki jest wymagana")]
    [Display(Name = "Nazwa")] 
    public string? Nazwa { get; set; }
    [Display(Name = "Opis")]
    public string? Opis { get; set; }
    [Display(Name = "Udogodnienia")]
    public string? Udogodnienia { get; set; }
    [Display(Name = "Program")]
    public string? Program { get; set; }
    [Required(ErrorMessage = "Miejscowość placówki jest wymagana")]
    [Display(Name = "Miejscowość")]
    public int MiejscowoscID { get; set; }
    public string? Dzielnica { get; set; }
    [Required(ErrorMessage = "Ulica placówki jest wymagana")]
    public string? Ulica { get; set; }
    [Required(ErrorMessage = "Numer domu placówki jest wymagany")]
    public string? Dom { get; set; }
    public string? Lokal { get; set; }
    public string? Telefon { get; set; }
    [Display(Name = "Komórka")]
    public string? Komorka { get; set; }
    [Display(Name = "E-mail")]
    public string? Email { get; set; }
    [Display(Name = "Strona internetowa")]
    public string? Strona { get; set; }

    public virtual ICollection<PlacowkaPlik> PlacowkaPlik { get; } = new List<PlacowkaPlik>();
    public virtual ICollection<PlacowkaOddzial> PlacowkaOddzial { get; } = new List<PlacowkaOddzial>();
    public virtual PlacowkaTyp? PlacowkaTyp { get; set; }
}
