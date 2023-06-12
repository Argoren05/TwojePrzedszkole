using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kindergartenAPP.Data
{
    public class applicationContext : IdentityDbContext<Uzytkownik>
    {
        public applicationContext(DbContextOptions<applicationContext> options) : base(options)
        {

        }

        // Entities
        public virtual DbSet<Dziecko> Dziecko { get; set; }
        public virtual DbSet<Placowka> Placowka { get; set; }
        public virtual DbSet<PlacowkaOddzial> PlacowkaOddzial { get; set; }
        public virtual DbSet<PlacowkaPlik> PlacowkaPlik { get; set; }
        public virtual DbSet<PlacowkaRekrutacja> PlacowkaRekrutacja { get; set; }
        public virtual DbSet<Opiekun> Opiekun { get; set; }
        public virtual DbSet<Narodowosc> Narodowosc { get; set; }
        public virtual DbSet<Obywatelstwo> Obywatelstwo { get; set; }
        public DbSet<Aktualnosci> Aktualnosci { get; set; } = default!;
        public DbSet<OcenaPlacowka> OcenaPlacowka { get; set; } = default!;

        //ViewModel
        public virtual DbSet<PlacowkaLista> PlacowkaLista { get; set; }
        public virtual DbSet<MiejscowoscLista> MiejscowoscLista { get; set; }
        public virtual DbSet<PlacowkaRekrutacjaLista> PlacowkaRekrutacjaLista { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=dbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dziecko>().ToTable(tb => tb.HasTrigger("utrAiDziecko"));
            //modelBuilder.Entity<Placowka>().ToTable(tb => tb.HasTrigger("utrAiPlacowka"));
            modelBuilder.Entity<PlacowkaPlik>().ToTable(tb => tb.HasTrigger("utrAiuPlacowkaPlik"));
            //modelBuilder.Entity<Opiekun>().ToTable(tb => tb.HasTrigger("utrAiOpiekun"));
        }
    }
}
