﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace schliessanlagen_konfigurator.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Address { get; set; } = "";

        public bool? Pruf_Firma = false;
        public string? Gender { get; set; } = "";
        public string? Firma { get; set; } = "";
        public string? USt_IdNr { get; set; } = "";
        public string? Rechnun_Land { get; set; } = "";
        public string? Rechnun_Straße { get; set; } = "";
        public string? Rechnun_Postleitzahl { get; set; } = "";
        public string? Rechnun_Stadt { get; set; } = "";
        public string? Liefer_Land { get; set; } = "";
        public string? Liefer_Straße { get; set; } = "";
        public string? Liefer_Postleitzahl { get; set; } = "";
        public string? Liefer_Stadt { get; set; } = "";

        public bool? isSend { get; set; } = false;
        public DateTime CreatedAT { get; set; }

        public ICollection<UserOrdersShop> UserOrdersShop { get; set; }
        public User()
        {
            UserOrdersShop = new List<UserOrdersShop>();
        }
    }
    public class UserOrdersShop
    {
        public int Id { get; set; }
        public float? Gramm { get; set; }
        public string? ProjektName { get; set; }
        public float OrderSum { get; set; }
        public string ProductName { get; set; }
        public int? KeyCount { get; set; }
        public float? KeyCost { get; set; }
        public string? E_PriceKey { get; set; }
        public string UserOrderKey { get; set; }
        public DateTime? createData { get; set; }
        public string? Lieferzeit { get; set; }
        public string? UserId { get; set; }
        public int? GuestId { get; set; }
        public Guest Guest { get; set; }
        public User User { get; set; }
        public string? Steur { get; set; }
        public string? SteurPrice { get; set; }
        public string? NettoPrice { get; set; }
        public string? VersandPrice { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? BezalenDate { get; set; }
        public string? ShippingStatus { get; set; }
        public int? UniKeyCount { get; set; } = 0;
        public string? Rehnung_E_Mail { get; set; }
        public string? Rehnung_Vorname { get; set; }
        public string? Rehnung_Nachname { get; set; }
        public string? Rehnung_Firma { get; set; }
        public string? Rehnung_Ust_Idnr { get; set; }
        public string? Rehnung_Strasse { get; set; }
        public string? Rehnung_Postleitzahl { get; set; }
        public string? Rehnung_Stadt { get; set; }
        public string? Rehnung_Land { get; set; }
        public string? Rehnung_TelefonNumber { get; set; }

        public string? Liefer_E_Mail { get; set; }
        public string? Liefer_Vorname { get; set; }
        public string? Liefer_Nachname { get; set; }
        public string? Liefer_Firma { get; set; }
        public string? Liefer_Ust_Idnr { get; set; }
        public string? Liefer_Strasse { get; set; }
        public string? Liefer_Postleitzahl { get; set; }
        public string? Liefer_Stadt { get; set; }
        public string? Liefer_Land { get; set; }
        public string? Liefer_TelefonNumber { get; set; }

        public int? count { get; set; }
        public ICollection<ProductSysteam> ProductSysteam { get; set; }
        public ICollection<Rehnungs> Rehnungs { get; set; }
        public UserOrdersShop()
        {
            ProductSysteam = new List<ProductSysteam>();
            Rehnungs = new List<Rehnungs>();
        }

    }
    public class ProductSysteam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Aussen { get; set; }
        public float? Intern { get; set; }
        public string? Option { get; set; }
        public int? UserOrdersShopId { get; set; }
        public float? E_Price { get; set; }
        public int? Count { get; set; }
        public int? TypeZylinder { get; set; }
        public float? Price { get; set; }   
        public UserOrdersShop UserOrdersShop { get; set; }

    }
    public class Rehnungs
    {
        public int Id { get; set; }
        public string RehnungsId { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [DisplayName("Upload your photo")]
        public List<IFormFile> File { get; set; }
        public int UserOrdersShopId { get; set; }
        public UserOrdersShop UserOrdersShop { get; set; }
    }
}
