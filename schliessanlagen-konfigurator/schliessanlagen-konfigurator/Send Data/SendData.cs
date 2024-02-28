using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using System.Diagnostics.Metrics;
using System.Security.AccessControl;

namespace schliessanlagen_konfigurator.Send_Data
{
    public class SendData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new schliessanlagen_konfiguratorContext(
                serviceProvider.GetRequiredService <
                    DbContextOptions<schliessanlagen_konfiguratorContext>>()))
                {
                if (context.Schliessanlagen.Any())
                {
                    return;
                }
                if (context.Profil_Doppelzylinder.Any())
                {
                    return;
                }
                if (context.Profil_Doppelzylinder_Options.Any())
                {
                    return;
                }
                if (context.Hebelzylinder.Any())
                {
                    return;
                }
                if (context.Profil_Knaufzylinder.Any())
                {
                    return;
                }
                if (context.Vorhangschloss.Any())
                {
                    return;
                }
                if (context.Aussenzylinder_Rundzylinder.Any())
                {
                    return;
                }
                if (context.Profil_Halbzylinder.Any())
                {
                    return;
                }
               
                if (context.Orders.Any())
                {
                    return;
                }
                context.Schliessanlagen.AddRange(
                   new Schliessanlagen
                   {
                      nameType = "Profil-Doppelzylinder"
                   },
                   new Schliessanlagen
                   {
                       nameType = "Profil-Halbzylinder"
                   },
                   new Schliessanlagen
                   {
                       nameType = "Profil-Knaufzylinder"
                   },
                   new Schliessanlagen
                   {
                       nameType = "Hebelzylinder"
                   },
                   new Schliessanlagen
                   {
                       nameType = "Vorhangschloss"
                   },
                   new Schliessanlagen
                   {
                       nameType = "Aussenzylinder_Rundzylinder"
                   }
                );         
                context.SaveChanges();
            }
        }
    }
}
