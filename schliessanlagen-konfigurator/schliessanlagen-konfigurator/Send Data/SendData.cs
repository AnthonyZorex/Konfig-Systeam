using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
namespace schliessanlagen_konfigurator.Send_Data
{
    public class SendData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new schliessanlagen_konfiguratorContext(
                serviceProvider.GetRequiredService<
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
                if (context.Aussen_Innen.Any())
                {
                    return;
                }
                if (context.Profil_Doppelzylinder_Options.Any())
                {
                    return;
                }
                if (context.NGF.Any())
                {
                    return;
                }
                if (context.NGF_Value.Any())
                {
                    return;
                }

              
                if (context.Hebelzylinder.Any())
                {
                    return;
                }
                if (context.Hebelzylinder_Options.Any())
                {
                    return;
                }
                if (context.Options.Any())
                {
                    return;
                }
                if (context.Options_value.Any())
                {
                    return;
                }


                if (context.Profil_Knaufzylinder.Any())
                {
                    return;
                }
                if (context.Aussen_Innen_Knauf.Any())
                {
                    return;
                }
                if (context.Profil_Knaufzylinder_Options.Any())
                {
                    return;
                }
                if (context.Knayf_Options.Any())
                {
                    return;
                }
                if (context.Knayf_Options_value.Any())
                {
                    return;
                }



                if (context.Vorhangschloss.Any())
                {
                    return;
                }
                if (context.Size.Any())
                {
                    return;
                }
                if (context.Vorhan_Options.Any())
                {
                    return;
                }
                if (context.OptionsVorhan.Any())
                {
                    return;
                }
                if (context.OptionsVorhan_value.Any())
                {
                    return;
                }


                if (context.Aussenzylinder_Rundzylinder.Any())
                {
                    return;
                }
                if (context.Aussen_Rund_options.Any())
                {
                    return;
                }
                if (context.Aussen_Rund_all.Any())
                {
                    return;
                }
                if (context.Aussen_Rouns_all_value.Any())
                {
                    return;
                }


                if (context.Profil_Halbzylinder.Any())
                {
                    return;
                }
                if (context.Profil_Halbzylinder_Options.Any())
                {
                    return;
                }
                if (context.Halbzylinder_Options.Any())
                {
                    return;
                }
                if (context.Halbzylinder_Options_value.Any())
                {
                    return;
                }
                if (context.Aussen_Innen_Halbzylinder.Any())
                {
                    return;
                }

                if (context.SysteamPriceKey.Any())
                {
                    return;
                }
              
                if (context.User.Any())
                {
                    return;
                }
             
                if (context.UserOrdersShop.Any())
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
