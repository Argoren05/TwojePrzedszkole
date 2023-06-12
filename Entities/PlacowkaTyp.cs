using System.ComponentModel.DataAnnotations;

#nullable disable

namespace kindergartenAPP.Entities
{
    public class PlacowkaTyp
    {
        [Key]
        public int ID { get; set; }
        public string Opis { get; set; }
    }
}
