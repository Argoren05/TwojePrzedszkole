#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace kindergartenAPP.ViewModels
{
    [Table("vMiejscowoscLista", Schema = "dbo")]
    public class MiejscowoscLista
    {
        public int ID { get; set; }
        public string Miejscowosc { get; set; }
        public string Gmina { get; set; }
        public string Powiat { get; set; }
        public string Wojewodztwo { get; set; }
        public string Opis { get; set; }
    }
}
