﻿using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace schliessanlagen_konfigurator.Models.OrdersOpen
{
    public class isOpen_value
    {
        public int Id { get; set; }
        public int? isOpen_OrderId { get; set; }
        public isOpen_Order isOpen_Order { get; set; }
        public int CountKey { get; set; }
        public string? NameKey { get; set; }

        public ICollection<KeyValue> KeyValue { get; set; }
        public isOpen_value()
        {
            KeyValue = new List<KeyValue>();
        }
    }
}