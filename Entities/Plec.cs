using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Plec
{
    [Key]
    public string KOD { get; set; } = null!;
    public string? Opis { get; set; }

    public virtual ICollection<Dziecko> Dziecko { get; } = new List<Dziecko>();
}
