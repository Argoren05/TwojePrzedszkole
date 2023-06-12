using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kindergartenAPP.Entities;

public partial class Forum
{
    [Key]
    public int ID { get; set; }
    public int? ForumID { get; set; }
    public string? UzytkownikID { get; set; }
    public DateTime? Data { get; set; }
    public string? Tresc { get; set; }

    public virtual Forum? ForumNavigation { get; set; }
    public virtual ICollection<Forum> InverseForumNavigation { get; } = new List<Forum>();
}
