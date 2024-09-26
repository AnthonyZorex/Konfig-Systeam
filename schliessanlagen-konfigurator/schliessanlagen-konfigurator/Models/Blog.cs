namespace schliessanlagen_konfigurator.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Data { get; set; }
        
        //public int? Count_click {  get; set; } 
    }
}
