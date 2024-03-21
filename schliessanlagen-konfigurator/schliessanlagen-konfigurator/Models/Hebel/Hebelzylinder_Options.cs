using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models;

namespace schliessanlagen_konfigurator.Models.Hebel
{
    public class Hebelzylinder_Options
    {
        public int Id { get; set; }
        public int? HebelzylinderId { get; set; }
        public Models.Hebelzylinder.Hebelzylinder Hebelzylinder { get; set; }
        public ICollection<Models.Hebelzylinder.Options> Options { get; set; }

        public Hebelzylinder_Options()
        {
            Options = new List<Models.Hebelzylinder.Options>();
        }
    }
}
