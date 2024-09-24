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
                cachedData = getDataFunc();
                _cache.Set(cacheKey, cachedData, TimeSpan.FromHours(24));
            }
            return cachedData;
        }


        public List<SysteamPriceKey> System(string companyName, string cacheKey)
        {
            return GetCachedData(cacheKey, () => 
            {
                var doppelZylinder = db.Profil_Doppelzylinder
               .AsNoTracking()
               .Where(x => x.companyName == companyName)
               .Select(x => x.NameSystem)
               .Distinct()
               .ToList();

                return db.SysteamPriceKey
                    .AsNoTracking()
                    .Where(x => doppelZylinder.Contains(x.NameSysteam))
                    .ToList();
              
            });
        }
        public List<SysteamPriceKey> SystemCes() => System("CES", "SystemCes");
        public List<SysteamPriceKey> SystemBasi() => System("BASI", "SystemBasi");
        public List<SysteamPriceKey> SystemABUS() => System("ABUS", "SystemABUS");
        public List<SysteamPriceKey> SystemEVVA() => System("EVVA", "SystemEVVA");

        //public async Task<List<ProductGalery>> System_CES_GaleryAsync()
        //{
        //    return await GetCachedData("SystemCesGallery", async () =>
        //    {
        //        var ces = await SystemCes();
        //        var cesGallery = await db.ProductGalery
        //            .AsNoTracking()
        //            .Where(x => ces.Select(c => c.Id).Contains(x.SysteamPriceKeyId))
        //            .ToListAsync();
        //        return cesGallery;
        //    });
        //}


    }
}
