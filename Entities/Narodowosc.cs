using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Narodowosc
{
    [Key]
    public int ID { get; set; }
    public string? Opis { get; set; }
}
