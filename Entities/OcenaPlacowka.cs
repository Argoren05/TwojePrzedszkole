using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class OcenaPlacowka
{
    [Key]
    public int ID { get; set; }
    public int? PlacowkaID { get; set; }
    public string? UzytkownikID { get; set; }
    public byte? Ocena { get; set; }
    public DateTime? Data { get; set; }
}
