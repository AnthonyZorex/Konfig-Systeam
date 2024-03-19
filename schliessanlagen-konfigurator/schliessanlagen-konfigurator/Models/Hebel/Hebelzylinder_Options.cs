using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models;

namespace schliessanlagen_konfigurator.Models.Hebelzylinder
{
    public class Hebelzylinder_Options
    {
        public int Id { get; set; }
        public int? HebelzylinderId { get; set; }
        public Hebelzylinder Hebelzylinder { get; set; }
        public ICollection<Options> Options { get; set; }

        public Hebelzylinder_Options()
        {
            Options = new List<Options>();
        }
    }
}
