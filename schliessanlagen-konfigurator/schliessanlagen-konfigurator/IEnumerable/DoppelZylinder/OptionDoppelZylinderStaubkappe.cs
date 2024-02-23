using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace schliessanlagen_konfigurator.IEnumerable.DoppelZylinder
{
    [Flags]
    public enum OptionDoppelZylinderStaubkappe:int
    {
        [Display(Name = "ohne Staubkappe")]
        ohne__Staubkappe =0 ,

        [Display(Name = "mit Staubkappe einseitig")]
        mitS__taubkappe__beinseitig = 1,

        [Display(Name = "mit Staubkappe beidseitig")]
        mit__Staubkappe__beidseitig = 2
    }
}
