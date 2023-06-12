using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Dziecko
{
    [Key]
    public int ID { get; set; }
    public int? OpiekunID { get; set; }
    [Display(Name = "Placówka")]
    public int? PlacowkaID { get; set; }
    [Required(ErrorMessage = "Oddział jest wymagany")]
    [Display(Name = "Oddział")]
    public int? PlacowkaOddzialID { get; set; }
    [Required(ErrorMessage = "Imię jest wymagane")]
    [Display(Name = "Imię")]
    public string? Imie { get; set; }
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [Display(Name = "Nazwisko")]
    public string? Nazwisko { get; set; }
    [Required(ErrorMessage = "PESEL jest wymagany")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Numer PESEL musi zawierać 11 znaków")]
    [RegularExpression("[^0-9]", ErrorMessage = "Numer PESEL może zawierać tylko cyfry")]
    [Display(Name = "PESEL")]
    public string? Pesel { get; set; }
    [Required(ErrorMessage = "Data urodzenia jest wymagana")]
    [Display(Name = "Data urodzenia")]
    [DataType(DataType.Date)]
    public DateTime? DataUrodzenie { get; set; }
    [Required(ErrorMessage = "Płeć dziecka jest wymagana")]
    [Display(Name = "Płeć")]
    public string? PlecKOD { get; set; }
    [Display(Name = "Narodowość")]
    public int? NarodowoscID { get; set; }
    [Display(Name = "Obywatelstwo")]
    public int? ObywatelstwoID { get; set; }

    public virtual Opiekun? Opiekun { get; set; }
    public virtual Placowka? Placowka { get; set; }
    public virtual PlacowkaOddzial? PlacowkaOddzial { get; set; }
    public virtual Plec? Plec { get; set; }
    public virtual Narodowosc? Narodowosc { get; set; }
    public virtual Obywatelstwo? Obywatelstwo { get; set; }
    public virtual PlacowkaRekrutacja? PlacowkaRekrutacja { get; set; }

}
