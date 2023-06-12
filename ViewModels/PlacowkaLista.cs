using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kindergartenAPP.ViewModels
{
    [Table("vPlacowkaLista", Schema = "dbo")]
    public class PlacowkaLista
    {
        [Key]
        public int ID { get; set; }
        public string? Nazwa { get; set; }
        public string? Opis { get; set; }
        public int PlacowkaTypID { get; set; }
        public string? PlacowkaTyp { get; set; }
        public string? Miejscowosc { get; set; }
        public string? Gmina { get; set; }
        public string? Powiat { get; set; }
        public string? Wojewodztwo { get; set; }
        public string? Dzielnica { get; set; }
        public string? Ulica { get; set; }
        public string? Dom { get; set; }
        public string? Lokal { get; set; }
        [Display(Name = "Galeria")]
        public string? Zdjecie { get; set; }
        public Guid Kolejnosc { get; set; }
        public int? Ocena { get; set; }
        public int? Limit { get; set; }
        public int? Zgloszono { get; set; }
        public int? Wolne { get; set; }

        [Display(Name = "Udogodnienia")]
        public string? Udogodnienia { get; set; }
        [Display(Name = "Program")]
        public string? Program { get; set; }
        public string? Telefon { get; set; }
        public string? Komorka { get; set; }
        public string? Email { get; set; }
        public string? Strona { get; set; }
    }
}
