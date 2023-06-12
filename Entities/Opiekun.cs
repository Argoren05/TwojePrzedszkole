using kindergartenAPP.ViewModels;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace kindergartenAPP.Entities;

public partial class Opiekun
{
    [Key]
    public int ID { get; set; }
    public String UzytkownikID { get; set; }
    //[Required(ErrorMessage = "Imię jest wymagane")]
    [Display(Name = "Imię")] 
    public string Imie { get; set; }
    //[Required(ErrorMessage = "Nazwisko jest wymagane")]
    [Display(Name = "Nazwisko")]
    public string Nazwisko { get; set; }
    [Required(ErrorMessage = "Miejscowość jest wymagana")]
    [Display(Name = "Miejscowość")]
    public int MiejscowoscID { get; set; }
    public string? Ulica { get; set; }
    public string? Dom { get; set; }
    public string? Lokal { get; set; }
    //[Required(ErrorMessage = "Numer telefonu jest wymagana")]
    public string? Telefon { get; set; }
    public string? Komorka { get; set; }
    public string? Email { get; set; }

    public virtual MiejscowoscLista? Miejscowosc { get; set; }
    public virtual ICollection<Dziecko> Dziecko { get; } = new List<Dziecko>();
}
