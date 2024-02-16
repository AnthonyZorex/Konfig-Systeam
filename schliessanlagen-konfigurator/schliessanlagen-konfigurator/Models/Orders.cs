namespace schliessanlagen_konfigurator.Models
{
    public class Orders
    {
        public int id { get; set; }
        public string userKey { get; set; }
        public  string? Tur { get; set; }
        public int ZylinderId {  get; set; }
        public float? aussen { get; set; }
        public float? innen{ get; set; }
        public int Count { get; set; }
        public int CountKey { get; set; }
        public string? NameKey { get; set; }
        public bool IsOppen { get; set; }
        public bool IsOppen2 { get; set; }
        public bool IsOppen3 { get; set; }
        public bool IsOppen4 { get; set; }
        public bool IsOppen5 { get; set; } 
        public bool IsOppen6 { get; set; } 
        public bool IsOppen7 { get; set; } 
        public bool IsOppen8 { get; set; } 
        public bool IsOppen9 { get; set; }
        public bool IsOppen10 { get; set; }
        public bool IsOppen11 { get; set; }
        public bool IsOppen12 { get; set; }
        public bool IsOppen13 { get; set; }
        public bool IsOppen14 { get; set; } = false;
        public bool IsOppen15 { get; set; } = false;
        public bool IsOppen16 { get; set; } = false;
        public bool IsOppen17 { get; set; } = false;
        public bool IsOppen18 { get; set; } = false;
        public bool IsOppen19 { get; set; } = false;
        public bool IsOppen20 { get; set; } = false;


    }
}
