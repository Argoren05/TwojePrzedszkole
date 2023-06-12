using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class OcenaPrzedszkole
{
    [Key]
    public int ID { get; set; }
    public int? PrzedszkoleID { get; set; }
    public string? UzytkownikID { get; set; }
    public byte? Ocena { get; set; }
}
