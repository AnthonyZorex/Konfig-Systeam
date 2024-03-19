using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Hebelzylinder;
using schliessanlagen_konfigurator.Models.Vorhan;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.OrdersOpen;

namespace schliessanlagen_konfigurator.Models
{
    public class OrderListItems
    {
        public Orders Orders { get; set; }

        public Profil_Halbzylinder halbzylinder { get; set; }
        public List<Aussen_Innen_Halbzylinder> aussen_innen_Halbzylinder { get; set; }
        public Profil_Halbzylinder_Options Profil_Halbzylinder_Options { get; set; }
        public Halbzylinder_Options Halbzylinder_Options { get; set; }
        public Halbzylinder_Options_value Halbzylinder_Options_value { get; set; }

        public Aussenzylinder_Rundzylinder aussenzylinder { get; set; }
        public Aussen_Rund_options aussen_Rund_Options { get; set; }
        public Aussen_Rund_all aussen_Rund_All { get; set; }
        public Aussen_Rouns_all_value aussen_Rouns_All_Value { get; set; }

        public Profil_Knaufzylinder Profil_Knaufzylinder { get; set; }
        public Aussen_Innen_Knauf aussen_Innen_Knauf { get; set; }  
        public Profil_Knaufzylinder_Options profil_Knaufzylinder_Options { get; set; }
        public Knayf_Options Knayf_Options { get; set; }
        public Knayf_Options_value Knayf_Options_value { get; set; }

        public Hebelzylinder hebel { get; set; }
        public Hebelzylinder_Options Hebelzylinder_Options { get; set; }
        public Options options { get; set; }
        public Options_value options_Value { get; set; }

        public Vorhangschloss vorhangschloss { get; set; }
        public List<Size> size { get; set; }
        public Vorhan_Options vorhan_Options { get; set; }
        public OptionsVorhan optionsVorhan { get; set; }
        public OptionsVorhan_value optionsVorhan_value {  get; set; }


        public Profil_Doppelzylinder Profil_Doppelzylinder { get; set; }
        public Profil_Doppelzylinder_Options Profil_Doppelzylinder_Options { get; set; }
        public List<Aussen_Innen> Aussen_Innen { get; set; }
        public List<NGF> NGF { get; set; }
        public List<NGF_Value> NGF_Value { get; set; }
        public OrderListItems()
        {
            Profil_Doppelzylinder = new Profil_Doppelzylinder();

            Profil_Doppelzylinder_Options = new Profil_Doppelzylinder_Options();

            Aussen_Innen = new List<Aussen_Innen>();

            NGF = new List<NGF>();

            NGF_Value = new List<NGF_Value>();
        }
    }
}
