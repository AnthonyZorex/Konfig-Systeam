using System.Numerics;

namespace schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions
{
    public class NGF_Value
    {
        public int Id { get; set; }
        public int? NGFId { get; set; }
        public NGF NGF { get; set; }
        public string Value { get; set; }
        public float? Cost { get; set; }

    }
}
