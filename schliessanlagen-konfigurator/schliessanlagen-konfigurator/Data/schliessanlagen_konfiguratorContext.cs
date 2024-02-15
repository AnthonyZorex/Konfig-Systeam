using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Models;

namespace schliessanlagen_konfigurator.Data
{
    public class schliessanlagen_konfiguratorContext : DbContext
    {
        public schliessanlagen_konfiguratorContext(DbContextOptions<schliessanlagen_konfiguratorContext> options)
            : base(options)
        {
             
        }
        public DbSet<Schliessanlagen> Schliessanlagen { get; set; }
        public DbSet<Profil_Doppelzylinder> Profil_Doppelzylinder { get; set; }
        public DbSet<Profil_Halbzylinder> Profil_Halbzylinder { get; set; }
        public DbSet<Profil_Knaufzylinder> Profil_Knaufzylinder { get; set; }
        public DbSet<Hebelzylinder> Hebelzylinder { get; set; }
        public DbSet<Vorhangschloss> Vorhangschloss { get; set; }
        public DbSet<Aussenzylinder_Rundzylinder> Aussenzylinder_Rundzylinder { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
