using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergartenAPP.ViewModels
{
    [Table("vPlacowkaRekrutacjaLista", Schema = "dbo")]
    public class PlacowkaRekrutacjaLista
    {
        public int? ID { get; set; }
        public int? DzieckoID { get; set; }
        public int? OpiekunID { get; set; }
        public int? PlacowkaID { get; set; }
        [Display(Name = "Imię")]
        public string? Imie { get; set; }
        [Display(Name = "Nazwisko")]
        public string? Nazwisko { get; set; }
        [Display(Name = "PESEL")]
        public string? Pesel { get; set; }
        [Display(Name = "Urodzony")]
        [DataType(DataType.Date)]
        public DateTime? DataUrodzenie { get; set; }
        [Display(Name = "Płeć")]
        public string? PlecKOD { get; set; }
        [Display(Name = "Narodowość")]
        public string? Narodowsc { get; set; }
        public string? Obywatelstwo { get; set; }
        public string? Plec { get; set; }
        [Display(Name = "Imię")]
        public string? ImieOpiekun { get; set; }
        [Display(Name = "Nazwisko")]
        public string? NazwiskoOpiekun { get; set; }
        [Display(Name = "Komórka")]
        public string? KomorkaOpiekun { get; set; }
        [Display(Name = "Telefon")]
        public string? TelefonOpiekun { get; set; }
        [Display(Name = "E-mail")]
        public string? EmailOpiekun { get; set; }
        [Display(Name = "Miejscoswość")]
        public int? MiejscowoscOpiekun { get; set; }
        [Display(Name = "Ulica")]
        public string? UlicaOpiekun { get; set; }
        [Display(Name = "Numer domu")]
        public string? DomOpiekun { get; set; }
        [Display(Name = "Numer lokalu")]
        public string? LokalOpiekun { get; set; }
        [Display(Name = "Nazwa oddziału")]
        public string? Nazwa { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }
        public bool? Akceptacja { get; set; }
    }
}
