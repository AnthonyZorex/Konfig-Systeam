﻿namespace schliessanlagen_konfigurator.Models.Vorhan
{
    public class Size
    {
        public int Id { get; set; }
        public int VorhangschlossId { get; set; }
        public float sizeVorhangschloss { get; set; }
        public float Cost { get; set; }
        public Vorhangschloss Vorhangschloss { get; set; }
    }
}