using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class PlacowkaPlik
{
    [Key]
    public int ID { get; set; }
    public int? PlacowkaID { get; set; }
    public string? Rozszerzenie { get; set; }
    public string? Nazwa { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Data { get; set; }
    public string? Folder { get; set; }
    [Display(Name = "Obraz")]
    public string? Link { get; set; }
    public string? Opis { get; set; }
    [Display(Name = "Główne zdjęcie")]
    public bool? Glowny { get; set; }
    public virtual Placowka? Placowka { get; set; }
}
