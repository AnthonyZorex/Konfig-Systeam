﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Hebelzylinder;
namespace schliessanlagen_konfigurator.Models
{
    public class Hebel
    {
        public int Id { get; set; }
        public int schliessanlagenId { get; set; }
        public string Name { get; set; }
        public string? companyName { get; set; }
        public string? description { get; set; }
        public string? NameSystem { get; set; }
        public string? Artikelnummer { get; set; }
        public float Cost { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile ImageFile { get; set; }
        public Schliessanlagen Schliessanlagen { get; set; }
        public ICollection<Hebelzylinder_Options> Hebelzylinder_Options { get; set; }
        public Hebel()
        {
            Hebelzylinder_Options = new List<Hebelzylinder_Options>();
           
        }

    }
}