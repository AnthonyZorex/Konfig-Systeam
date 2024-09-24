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

        private async Task<List<T>> GetCachedDataAsync<T>(string cacheKey, Func<Task<List<T>>> getDataFunc)
        {
            if (!_cache.TryGetValue(cacheKey, out List<T> cachedData))
            {
                cachedData = await getDataFunc();
                _cache.Set(cacheKey, cachedData, TimeSpan.FromHours(24));
            }
            return cachedData;
        }

        public async Task<List<SysteamPriceKey>> SystemAsync(string companyName, string cacheKey)
        {
            return await GetCachedDataAsync(cacheKey, async () =>
            {
                var doppelZylinder = await db.Profil_Doppelzylinder
                    .AsNoTracking()
                    .Where(x => x.companyName == companyName)
                    .Select(x => x.NameSystem)
                    .Distinct()
                    .ToListAsync();

                return await db.SysteamPriceKey
                    .AsNoTracking()
                    .Where(x => doppelZylinder.Contains(x.NameSysteam))
                    .ToListAsync();
            });
        }

        public Task<List<SysteamPriceKey>> SystemCes() => SystemAsync("CES", "SystemCes");
        public Task<List<SysteamPriceKey>> SystemBasi() => SystemAsync("BASI", "SystemBasi");
        public Task<List<SysteamPriceKey>> SystemABUS() => SystemAsync("ABUS", "SystemABUS");
        public Task<List<SysteamPriceKey>> SystemEVVA() => SystemAsync("EVVA", "SystemEVVA");
    }
}
