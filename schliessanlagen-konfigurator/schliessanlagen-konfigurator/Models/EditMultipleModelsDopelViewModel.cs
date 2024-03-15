using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using System.Collections.Generic;

namespace schliessanlagen_konfigurator.Models
{
    public class EditMultipleModelsDopelViewModel
    {
        public  Profil_Doppelzylinder Profil_Doppelzylinder { get; set; }
        public Profil_Doppelzylinder_Options Profil_Doppelzylinder_Options { get; set; }
        public List<Aussen_Innen>  Aussen_Innen { get; set; }
        public List<NGF>  NGF { get; set; }
        public List<NGF_Value>  NGF_Value { get; set; }
        public EditMultipleModelsDopelViewModel()
        {
            Profil_Doppelzylinder = new Profil_Doppelzylinder();

            Profil_Doppelzylinder_Options = new Profil_Doppelzylinder_Options();

            Aussen_Innen = new List<Aussen_Innen>();

            NGF = new List<NGF>();

            NGF_Value = new List<NGF_Value>();
        }
    }
}
