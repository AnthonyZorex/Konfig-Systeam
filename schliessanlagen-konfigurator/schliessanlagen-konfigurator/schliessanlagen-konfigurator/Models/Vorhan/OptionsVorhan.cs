﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace schliessanlagen_konfigurator.Models.Vorhan
{
    public class OptionsVorhan
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public Vorhan_Options Option { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }

        public ICollection<OptionsVorhan_value> Options_value { get; set; }
        public OptionsVorhan()
        {
            Options_value = new List<OptionsVorhan_value>();
        }
    }
}