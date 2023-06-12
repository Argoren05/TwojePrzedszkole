using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Nowosc
{
    [Key]
    public int ID { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Data { get; set; }
    [Display(Name = "Tytuł aktualności")]
    public string? Tytul { get; set; }
    public string? Opis { get; set; }
}
