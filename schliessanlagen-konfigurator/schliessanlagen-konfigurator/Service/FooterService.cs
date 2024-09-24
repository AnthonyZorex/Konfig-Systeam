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
        private readonly IMemoryCache _cache;

        public FooterService(schliessanlagen_konfiguratorContext context, IMemoryCache cache)
        {
            db = context;
            _cache = cache;
        }

        private List<T> GetCachedDataAsync<T>(string cacheKey, Func<List<T>> getDataFunc)
        {
            if (!_cache.TryGetValue(cacheKey, out List<T> cachedData))
            {
                cachedData =  getDataFunc();
                _cache.Set(cacheKey, cachedData, TimeSpan.FromHours(24));
            }
            return cachedData;
        }

        public List<SysteamPriceKey> SystemAsync(string companyName, string cacheKey)
        {
            return GetCachedDataAsync(cacheKey, () =>
            {
                var doppelZylinder = db.Profil_Doppelzylinder
                    .AsNoTracking()
                    .Where(x => x.companyName == companyName)
                    .Select(x => x.NameSystem)
                    .Distinct()
                    .ToList();

                return  db.SysteamPriceKey
                    .AsNoTracking()
                    .Where(x => doppelZylinder.Contains(x.NameSysteam))
                    .ToList();
            });
        }

        public List<SysteamPriceKey> SystemCes() => SystemAsync("CES", "SystemCes");
        public List<SysteamPriceKey> SystemBasi() => SystemAsync("BASI", "SystemBasi");
        public List<SysteamPriceKey> SystemABUS() => SystemAsync("ABUS", "SystemABUS");
        public List<SysteamPriceKey> SystemEVVA() => SystemAsync("EVVA", "SystemEVVA");
    }
}
