namespace schliessanlagen_konfigurator.Models.Aussen_Rund
{
    public class Aussen_Rund_options
    {
        public int Id { get; set; }
        public int? Aussenzylinder_RundzylinderId { get; set; }
        public Aussenzylinder_Rundzylinder Aussenzylinder_Rundzylinder { get; set; }
        public ICollection<Aussen_Rund_all> Aussen_Rund_all { get; set; }

        public Aussen_Rund_options()
        {
            Aussen_Rund_all = new List<Aussen_Rund_all>();
        }
    }
}
