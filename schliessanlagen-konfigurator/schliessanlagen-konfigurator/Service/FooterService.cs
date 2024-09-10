using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public List<ProductGalery> SystemGalery()
        {
            var Gallery = db.ProductGalery.ToList();

            return Gallery;
        }
        public List<SysteamPriceKey> SystemInfo()
        {
            var System = db.SysteamPriceKey.ToList();

            return System;
        }
        public List<SysteamPriceKey> SystemCes()
        {
            var System = SystemInfo();

            var DoppelZylinder = db.Profil_Doppelzylinder.Where(x => x.companyName == "CES").ToList();

            var Ces = new List<SysteamPriceKey>();

            foreach (var item in DoppelZylinder)
            {
                var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();

                foreach (var x in sortItem)
                {

                    Ces.Add(x);
                }
            }

            return Ces;
        }

        public List<ProductGalery> System_CES_Galery()
        {
            var Ces = SystemCes();

            var CesGalerry = new List<ProductGalery>();

            foreach (var item in Ces)
            {
                var gallery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

                foreach (var x in gallery)
                {
                    CesGalerry.Add(x);
                }
            }

            return CesGalerry;
        }

        public List<SysteamPriceKey> SystemABUS()
        {
            var System = SystemInfo();

            var DoppelZylinder = db.Profil_Doppelzylinder.Where(x => x.companyName == "ABUS").ToList();

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
        public List<ProductGalery> System_ABUS_Galery()
        {
            var ABUS = SystemABUS();

            var ABUS_Galerry = new List<ProductGalery>();

            foreach (var item in ABUS)
            {
                var gallery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

                foreach (var x in gallery)
                {
                    ABUS_Galerry.Add(x);
                }
            }

            return ABUS_Galerry;
        }
        public List<SysteamPriceKey> SystemEVVA()
        {
            var System = SystemInfo();

            var DoppelZylinder = db.Profil_Doppelzylinder.Where(x => x.companyName == "EVVA").ToList();

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
        public List<ProductGalery> System_EVVA_Galery()
        {
            var EVVA = SystemEVVA();

            var EVVA_Galerry = new List<ProductGalery>();

            foreach (var item in EVVA)
            {
                var gallery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

                foreach (var x in gallery)
                {
                    EVVA_Galerry.Add(x);
                }
            }

            return EVVA_Galerry;
        }
    }
}
