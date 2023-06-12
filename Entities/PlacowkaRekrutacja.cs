using System.ComponentModel.DataAnnotations;

#nullable disable

namespace kindergartenAPP.Entities
{
    public class PlacowkaRekrutacja
    {
        [Key]
        public int ID { get; set; }
        public int PlacowkaOddzialID { get; set; }
        public int DzieckoID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }
        public bool? Akceptacja { get; set; }

        public virtual PlacowkaOddzial PlacowkaOddzial { get; set; }
        public virtual Dziecko Dziecko { get; set; }
    }
}
