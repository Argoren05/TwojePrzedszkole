using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities
{
    public class PlacowkaOddzial
    {
        public int ID { get; set; }
        public int PlacowkaID { get; set; }
        public int Rok { get; set; }
        public string? Nazwa { get; set; }
        public int Limit { get; set; }

        public virtual Placowka? Placowka { get; set; }

        public virtual ICollection<PlacowkaRekrutacja>? PlacowkaRekrutacja { get; } = new List<PlacowkaRekrutacja>();
    }
}
