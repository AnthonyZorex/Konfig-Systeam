using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Models.Users;
using Microsoft.Extensions.Caching.Memory;
namespace schliessanlagen_konfigurator.Service
{
    public class FooterService
    {
        private readonly schliessanlagen_konfiguratorContext db;
        private readonly IWebHostEnvironment Environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMemoryCache _cache;

        public FooterService(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment environment, IMemoryCache cache)
        {
            db = context;
            Environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
            _cache = cache;
        }

        private List<T> GetCachedData<T>(string cacheKey, Func<List<T>> getDataFunc)
        {
            if (!_cache.TryGetValue(cacheKey, out List<T> cachedData))
            {
                // Данные не найдены в кэше, получаем из базы данных
                cachedData = getDataFunc();

                // Устанавливаем кэш с истечением через 24 часа
                _cache.Set(cacheKey, cachedData, TimeSpan.FromHours(24));
            }
            return cachedData;
        }

        public List<ProductGalery> SystemGalery()
        {
            return db.ProductGalery.AsNoTracking().ToList();
        }

        public List<SysteamPriceKey> SystemInfo()
        {
            return db.SysteamPriceKey.AsNoTracking().ToList();
        }

        public List<SysteamPriceKey> SystemCes()
        {
            return GetCachedData("SystemCes", () =>
            {
                var System = SystemInfo();
                var DoppelZylinder = db.Profil_Doppelzylinder.AsNoTracking().Where(x => x.companyName == "CES").ToList();
                var Ces = new List<SysteamPriceKey>();

                foreach (var item in DoppelZylinder)
                {
                    var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();
                    Ces.AddRange(sortItem);
                }
                return Ces;
            });
        }

        public List<ProductGalery> System_CES_Galery()
        {
            return GetCachedData("SystemCesGallery", () =>
            {
                var Ces = SystemCes();
                var CesGalerry = new List<ProductGalery>();

                foreach (var item in Ces)
                {
                    var gallery = db.ProductGalery.AsNoTracking().Where(x => x.SysteamPriceKeyId == item.Id).ToList();
                    CesGalerry.AddRange(gallery);
                }
                return CesGalerry;
            });
        }

        public List<SysteamPriceKey> SystemBasi()
        {
            return GetCachedData("SystemBasi", () =>
            {
                var System = SystemInfo();
                var DoppelZylinder = db.Profil_Doppelzylinder.AsNoTracking().Where(x => x.companyName == "BASI").ToList();
                var Basi = new List<SysteamPriceKey>();

                foreach (var item in DoppelZylinder)
                {
                    var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();
                    Basi.AddRange(sortItem);
                }
                return Basi;
            });
        }

        public List<ProductGalery> System_Basi_Galery()
        {
            return GetCachedData("SystemBasiGallery", () =>
            {
                var Basi = SystemBasi();
                var BasiGalerry = new List<ProductGalery>();

                foreach (var item in Basi)
                {
                    var gallery = db.ProductGalery.AsNoTracking().Where(x => x.SysteamPriceKeyId == item.Id).ToList();
                    BasiGalerry.AddRange(gallery);
                }
                return BasiGalerry;
            });
        }

        public List<SysteamPriceKey> SystemABUS()
        {
            return GetCachedData("SystemABUS", () =>
            {
                var System = SystemInfo();
                var DoppelZylinder = db.Profil_Doppelzylinder.AsNoTracking().Where(x => x.companyName == "ABUS").ToList();
                var ABUS = new List<SysteamPriceKey>();

                foreach (var item in DoppelZylinder)
                {
                    var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();
                    ABUS.AddRange(sortItem);
                }
                return ABUS;
            });
        }

        public List<ProductGalery> System_ABUS_Galery()
        {
            return GetCachedData("SystemABUSGallery", () =>
            {
                var ABUS = SystemABUS();
                var ABUS_Galerry = new List<ProductGalery>();

                foreach (var item in ABUS)
                {
                    var gallery = db.ProductGalery.AsNoTracking().Where(x => x.SysteamPriceKeyId == item.Id).ToList();
                    ABUS_Galerry.AddRange(gallery);
                }
                return ABUS_Galerry;
            });
        }

        public List<SysteamPriceKey> SystemEVVA()
        {
            return GetCachedData("SystemEVVA", () =>
            {
                var System = SystemInfo();
                var DoppelZylinder = db.Profil_Doppelzylinder.AsNoTracking().Where(x => x.companyName == "EVVA").ToList();
                var EVVA = new List<SysteamPriceKey>();

                foreach (var item in DoppelZylinder)
                {
                    var sortItem = System.Where(x => x.NameSysteam == item.NameSystem).ToList();
                    EVVA.AddRange(sortItem);
                }
                return EVVA;
            });
        }

        public List<ProductGalery> System_EVVA_Galery()
        {
            return GetCachedData("SystemEVVAGallery", () =>
            {
                var EVVA = SystemEVVA();
                var EVVA_Galerry = new List<ProductGalery>();

                foreach (var item in EVVA)
                {
                    var gallery = db.ProductGalery.AsNoTracking().Where(x => x.SysteamPriceKeyId == item.Id).ToList();
                    EVVA_Galerry.AddRange(gallery);
                }
                return EVVA_Galerry;
            });
        }
    }
}
