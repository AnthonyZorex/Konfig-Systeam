using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models;

namespace schliessanlagen_konfigurator.Models.Hebelzylinder
{
    public class Hebelzylinder_Options
    {
        public int Id { get; set; }
        public int? HebelzylinderId { get; set; }
        public Hebel Hebelzylinder { get; set; }
        public ICollection<Halbzylinder_Options> Halbzylinder_Options { get; set; }

        public Hebelzylinder_Options()
        {
            Halbzylinder_Options = new List<Halbzylinder_Options>();
        }
    }
}
