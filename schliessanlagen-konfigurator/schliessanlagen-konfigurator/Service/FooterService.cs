using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Models.Users;

namespace schliessanlagen_konfigurator.Service
{
    public class FooterService
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public FooterService(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<ProductGalery>> SystemGalery()
        {
            var Gallery = await db.ProductGalery.ToListAsync();

            return  Gallery;
        }
        public async Task<List<SysteamPriceKey>> SystemInfo()
        {
            var System = await db.SysteamPriceKey.ToListAsync();

            return System;
        }
        public async Task<List<SysteamPriceKey>> SystemCes()
        {
            var System = await SystemInfo();

            var DoppelZylinder = await db.Profil_Doppelzylinder.Where(x => x.companyName == "CES").ToListAsync();

            var Ces = new List<SysteamPriceKey>();

            foreach (var item in DoppelZylinder)
            {
                var sortItem =  System.Where(x => x.NameSysteam == item.NameSystem).ToList();

                foreach (var x in sortItem)
                {

                    Ces.Add(x);
                }
            }

            return Ces;
        }

        public async Task<List<ProductGalery>> System_CES_Galery()
        {
            var Ces = await SystemCes();

            var CesGalerry = new List<ProductGalery>();

            foreach (var item in Ces)
            {
                var gallery = await db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToListAsync();

                foreach (var x in gallery)
                {
                    CesGalerry.Add(x);
                }
            }

            return CesGalerry;
        }
        public async Task<List<SysteamPriceKey>> SystemBasi()
        {
            var System = await SystemInfo();

            var DoppelZylinder = await db.Profil_Doppelzylinder.Where(x => x.companyName == "BASI").ToListAsync();

            var Basi = new List<SysteamPriceKey>();

            foreach (var item in DoppelZylinder)
            {
                var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();

                foreach (var x in sortItem)
                {
                    Basi.Add(x);
                }
            }

            return Basi;
        }

        public async Task<List<ProductGalery>> System_Basi_Galery()
        {
            var Basi = await SystemBasi();

            var BasiGalerry = new List<ProductGalery>();

            foreach (var item in Basi)
            {
                var gallery = await db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToListAsync();

                foreach (var x in gallery)
                {
                    BasiGalerry.Add(x);
                }
            }

            return BasiGalerry;
        }
        public async Task<List<SysteamPriceKey>> SystemABUS()
        {
            var System = await SystemInfo();

            var DoppelZylinder = await db.Profil_Doppelzylinder.Where(x => x.companyName == "ABUS").ToListAsync();

            var ABUS = new List<SysteamPriceKey>();

            foreach (var item in DoppelZylinder)
            {
                var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();

                foreach (var x in sortItem)
                {
                    ABUS.Add(x);
                }
            }

            return ABUS;
        }
        public async  Task<List<ProductGalery>> System_ABUS_Galery()
        {
            var ABUS = await SystemABUS();

            var ABUS_Galerry = new List<ProductGalery>();

            foreach (var item in ABUS)
            {
                var gallery = await db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToListAsync();

                foreach (var x in gallery)
                {
                    ABUS_Galerry.Add(x);
                }
            }

            return ABUS_Galerry;
        }
        public async Task<List<SysteamPriceKey>> SystemEVVA()
        {
            var System = await SystemInfo();

            var DoppelZylinder = await db.Profil_Doppelzylinder.Where(x => x.companyName == "EVVA").ToListAsync();

            var EVVA = new List<SysteamPriceKey>();

            foreach (var item in DoppelZylinder)
            {
                var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();

                foreach (var x in sortItem)
                {
                    EVVA.Add(x);
                }
            }

            return EVVA;
        }
        public async Task<List<ProductGalery>> System_EVVA_Galery()
        {
            var EVVA = await SystemEVVA();

            var EVVA_Galerry = new List<ProductGalery>();

            foreach (var item in EVVA)
            {
                var gallery = await db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToListAsync();

                foreach (var x in gallery)
                {
                    EVVA_Galerry.Add(x);
                }
            }

            return EVVA_Galerry;
        }
    }
}
