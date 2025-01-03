﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Hebel;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Models.Vorhan;

namespace schliessanlagen_konfigurator.Data
{
    public class schliessanlagen_konfiguratorContext : IdentityDbContext<User>
    {
        public schliessanlagen_konfiguratorContext(DbContextOptions<schliessanlagen_konfiguratorContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminId = Guid.NewGuid().ToString();
            var clientId = Guid.NewGuid().ToString();

            var adminRole = new IdentityRole
            {
                Id = adminId,
                Name = "admin",
                NormalizedName = "admin"
            };

            var clientRole = new IdentityRole
            {
                Id = clientId,
                Name = "client",
                NormalizedName = "client"
            };

            builder.Entity<IdentityRole>().HasData(adminRole, clientRole);
            base.OnModelCreating(builder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            builder.Entity<IdentityRole>().HasData(admin, client);
        }
        public DbSet<Page> Page { get; set; }
        public DbSet<Blog> Blogs { get; set; }      
        public DbSet<SystemOptionValue> SystemOptionValue { get; set; }
        public DbSet<SystemOptionen> SystemOptionen { get; set; }
        public DbSet<SystemOptionInfo> SystemOptionInfo { get; set; }
        public DbSet<SystemScheker> SystemScheker {  get; set; }
        public DbSet<ProductGalery> ProductGalery { get; set; }
        public DbSet<Rehnungs> Rehnungs { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserOrdersShop> UserOrdersShop { get; set; }
        public DbSet<ProductSysteam> ProductSysteam { get; set; }

        public DbSet<Schliessanlagen> Schliessanlagen { get; set; }

        public DbSet<Profil_Doppelzylinder> Profil_Doppelzylinder { get; set; }
        public DbSet<Profil_Doppelzylinder_Options> Profil_Doppelzylinder_Options { get; set; }
        public DbSet<Aussen_Innen> Aussen_Innen { get; set; }
        public DbSet<Doppel_Innen_klein> Doppel_Innen_klein { get; set; }
        public DbSet<NGF> NGF { get; set; }
        public DbSet<NGF_Value> NGF_Value { get; set; }


        public DbSet<Guest> Guest { get; set; }

        public DbSet<Profil_Halbzylinder> Profil_Halbzylinder { get; set; }
        public DbSet<Profil_Halbzylinder_Options> Profil_Halbzylinder_Options { get; set; }
        public DbSet<Halbzylinder_Options> Halbzylinder_Options { get; set; }
        public DbSet<Halbzylinder_Options_value> Halbzylinder_Options_value { get; set; }
        public DbSet<Aussen_Innen_Halbzylinder> Aussen_Innen_Halbzylinder { get; set; }


        public DbSet<Hebel> Hebelzylinder { get; set; }
        public DbSet<Hebelzylinder_Options> Hebelzylinder_Options { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Options_value> Options_value { get; set; }


        public DbSet<Vorhangschloss> Vorhangschloss { get; set; }
        public DbSet<Models.Vorhan.Size> Size { get; set; }
        public DbSet<Vorhan_Options> Vorhan_Options { get; set; }
        public DbSet<OptionsVorhan> OptionsVorhan { get; set; }
        public DbSet<OptionsVorhan_value> OptionsVorhan_value { get; set; }


        public DbSet<Aussenzylinder_Rundzylinder> Aussenzylinder_Rundzylinder { get; set; }
        public DbSet<Aussen_Rouns_all_value> Aussen_Rouns_all_value { get; set; }
        public DbSet<Aussen_Rund_options> Aussen_Rund_options { get; set; }
        public DbSet<Aussen_Rund_all> Aussen_Rund_all { get; set; }


        public DbSet<Orders> Orders { get; set; }
        public DbSet<isOpen_Order> isOpen_Order { get; set; }
        public DbSet<isOpen_value> isOpen_value { get; set; }
        public DbSet<KeyValue> KeyValue { get; set; }
        public DbSet<SysteamPriceKey> SysteamPriceKey {  get; set; }

        public DbSet<Profil_Knaufzylinder> Profil_Knaufzylinder { get; set; }
        public DbSet<Aussen_Innen_Knauf> Aussen_Innen_Knauf { get; set; }
        public DbSet<Knayf_Options> Knayf_Options { get; set; }
        public DbSet<Knayf_Options_value> Knayf_Options_value { get; set; }
        public DbSet<Profil_Knaufzylinder_Options> Profil_Knaufzylinder_Options { get; set; }
        public DbSet<Aussen_Innen_Knauf_klein> Aussen_Innen_Knauf_klein { get; set; }

    }


}

