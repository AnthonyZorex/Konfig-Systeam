﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models
{
    public class Aussenzylinder_Rundzylinder
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public string? Artikelnummer { get; set; }
        public int? Count { get; set; }
        public decimal? Cost { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Options>? Options { get; set; }
        public Aussenzylinder_Rundzylinder()
        {
            Options = new List<Options>();
        }
    }
}