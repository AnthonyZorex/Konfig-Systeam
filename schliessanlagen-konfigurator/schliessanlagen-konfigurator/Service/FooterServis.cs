using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Models.Users;

namespace schliessanlagen_konfigurator.Service
{
    public class FooterServis
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public FooterServis(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        private async Task<List<SysteamPriceKey>> GetSystemsByCompanyNameAsync(string companyName)
        {
            // Получаем список дoppelzylinder для указанной компании
            var listDoppel = await db.Profil_Doppelzylinder
                .Where(x => x.companyName == companyName)
                .ToListAsync();

            // Получаем системы, соответствующие указанной компании
            var systems = await db.SysteamPriceKey
                .Where(s => listDoppel.Select(d => d.NameSystem).Contains(s.NameSysteam))
                .Select(s => new SysteamPriceKey
                {
                    Id = s.Id,
                    NameSysteam = s.NameSysteam,
                })
                .Distinct()
                .ToListAsync();

            return systems;
        }

        public Task<List<SysteamPriceKey>> Ces() => GetSystemsByCompanyNameAsync("CES");
        public Task<List<SysteamPriceKey>> Abus() => GetSystemsByCompanyNameAsync("ABUS");
        public Task<List<SysteamPriceKey>> Evva() => GetSystemsByCompanyNameAsync("EVVA");
        public Task<List<SysteamPriceKey>> Basi() => GetSystemsByCompanyNameAsync("BASI");
    }
}
