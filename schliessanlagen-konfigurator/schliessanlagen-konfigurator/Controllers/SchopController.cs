using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Models.Vorhan;
using System.Diagnostics;
using System.Security.Claims;
using System.Drawing;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using schliessanlagen_konfigurator.Models.Hebel;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Schop_models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Linq.Dynamic.Core;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore.Query.Internal;
using schliessanlagen_konfigurator.Migrations;
using OpenAI_API.Chat;
using OpenAI_API;
using System.Drawing;
using System.Security.Policy;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using Microsoft.AspNetCore.OutputCaching;
namespace schliessanlagen_konfigurator.Controllers
{
    public class SchopController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public SchopController(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        async void ZylinderHerschteller(int? page, float? priceVon, float? priceBis, string? Herschteller, string? Sort_string, ZylinderViewModel model)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            var zylinder = db.Profil_Doppelzylinder.Select(x => x.companyName).ToList();

            if (Sort_string!=null)
            {
                if (Herschteller != null || priceBis != null && priceVon != null)
                {
                    if (priceBis != null && priceVon != null && Herschteller == null)
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Name.Contains(Sort_string) && x.Price > priceVon && priceBis > x.Price).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        int totalItems = count_zylinder;
                        model.PriceBis = priceBis;
                        model.PriceVon = priceVon;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.Sort_string = Sort_string;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }
                    else if (priceBis == null && priceVon == null && Herschteller != null)
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        model.Sort_string = Sort_string;
                        int totalItems = count_zylinder;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }
                    else
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Name.Contains(Sort_string) && x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        model.Sort_string = Sort_string;
                        int totalItems = count_zylinder;
                        model.PriceBis = priceBis;
                        model.PriceVon = priceVon;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }

                }
                else
                {
                    model.Herschteller = null;
                    var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.Name.Contains(Sort_string)).ToList();
                    var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Name.Contains(Sort_string)).ToList();
                    var halbZylinder = db.Profil_Halbzylinder.Where(x => x.Name.Contains(Sort_string)).ToList();
                    var hebelZylinder = db.Hebelzylinder.Where(x => x.Name.Contains(Sort_string)).Distinct().ToList();
                    var vorhanZylinder = db.Vorhangschloss.Where(x => x.Name.Contains(Sort_string)).Distinct().ToList();
                    var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Name.Contains(Sort_string)).Distinct().ToList();

                    var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                    halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                    model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                    .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .OrderBy(x => x.Price)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList<dynamic>();

                    int totalItems = count_zylinder;
                    model.Sort_string = Sort_string;
                    model.page = pageNumber;
                    model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                }
            }
            else
            {
                if (Herschteller != null || priceBis != null && priceVon != null)
                {
                    if (priceBis != null && priceVon != null && Herschteller == null)
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Price > priceVon && priceBis > x.Price).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        int totalItems = count_zylinder;
                        model.PriceBis = priceBis;
                        model.PriceVon = priceVon;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }
                    else if (priceBis == null && priceVon == null && Herschteller != null)
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.companyName == Herschteller).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.companyName == Herschteller).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.companyName == Herschteller).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.companyName == Herschteller).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.companyName == Herschteller).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.companyName == Herschteller).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        int totalItems = count_zylinder;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }
                    else
                    {
                        var doppelZylinder = db.Profil_Doppelzylinder.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var knayfZylinder = db.Profil_Knaufzylinder.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var halbZylinder = db.Profil_Halbzylinder.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var hebelZylinder = db.Hebelzylinder.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var vorhanZylinder = db.Vorhangschloss.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();
                        var aussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.companyName == Herschteller && (x.Price > priceVon && priceBis > x.Price)).ToList();

                        var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                        halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                        model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                        .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                        .OrderBy(x => x.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .ToList<dynamic>();

                        int totalItems = count_zylinder;
                        model.PriceBis = priceBis;
                        model.PriceVon = priceVon;
                        model.Herschteller = Herschteller;
                        model.page = pageNumber;
                        model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    }

                }
                else
                {
                    model.Herschteller = null;
                    var doppelZylinder = db.Profil_Doppelzylinder.ToList();
                    var knayfZylinder = db.Profil_Knaufzylinder.ToList();
                    var halbZylinder = db.Profil_Halbzylinder.ToList();
                    var hebelZylinder = db.Hebelzylinder.Distinct().ToList();
                    var vorhanZylinder = db.Vorhangschloss.Distinct().ToList();
                    var aussenZylinder = db.Aussenzylinder_Rundzylinder.Distinct().ToList();

                    var count_zylinder = doppelZylinder.Distinct().Count() + hebelZylinder.Distinct().Count() + knayfZylinder.Distinct().Count() +
                    halbZylinder.Distinct().Count() + vorhanZylinder.Distinct().Count() + aussenZylinder.Distinct().Count();

                    model.ZylinderItems = doppelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price })
                    .Concat(knayfZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(halbZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(hebelZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(vorhanZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .Concat(aussenZylinder.Select(x => new { x.Name, x.description, x.ImageName, x.NameSystem, x.companyName, x.Price }))
                    .OrderBy(x => x.Price)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList<dynamic>();

                    int totalItems = count_zylinder;

                    model.page = pageNumber;
                    model.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                }
            }
          
            ViewBag.Zylinder = zylinder.Distinct();
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? page,ZylinderViewModel model)
        {
            ZylinderHerschteller(model.page, model.PriceVon, model.PriceBis, model.Herschteller, model.Sort_string, model);
            return View("../Schop/Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ZylinderViewModel filter)
        {
            int page = filter.page;
            string herschteller = filter.Herschteller;
            string sort = filter.Sort_string;
            float? priceVon = filter.PriceVon;
            float? priceBis = filter.PriceBis;
            return RedirectToAction("Index", "Schop", new { page = page, Herschteller = herschteller, priceVon = priceVon, priceBis = priceBis , Sort_string = sort });
        }

        public async Task<IActionResult> zylinder_page(string product_Name)
        {
            string productName = product_Name.Replace("%", " ");

            var doppel = db.Profil_Doppelzylinder.Include(x=>x.Profil_Doppelzylinder_Options).ThenInclude(x=>x.NGF).ThenInclude(x=>x.NGF_Value).Where(x=>x.Name == productName).ToList();
            var halb = db.Profil_Halbzylinder.Include(x=>x.Profil_Halbzylinder_Options).ThenInclude(x=>x.Halbzylinder_Options).ThenInclude(x=>x.Halbzylinder_Options_value).Where(x => x.Name == productName).ToList();
            var knayf = db.Profil_Knaufzylinder.Include(x=>x.Profil_Knaufzylinder_Options).Where(x => x.Name == productName).ToList();
            var hebel = db.Hebelzylinder.Include(x=>x.Hebelzylinder_Options).ThenInclude(x=>x.Options).ThenInclude(x=>x.Options_value).Where(x => x.Name == productName).ToList();
            var vorhan = db.Vorhangschloss.Include(x=>x.Vorhan_Options).ThenInclude(x=>x.Options).ThenInclude(x=>x.Options_value).Where(x => x.Name == productName).ToList();
            var aussen = db.Aussenzylinder_Rundzylinder.Include(x=>x.Aussen_Rund_options).ThenInclude(x=>x.Aussen_Rund_all).ThenInclude(x=>x.Aussen_Rouns_all_value).Where(x => x.Name == productName).ToList();

            if (doppel.Count()>0)
            {
                ViewBag.Item = doppel;
                var size = db.Aussen_Innen.Include(x=>x.Doppel_Innen_klein).Where(x => x.Profil_DoppelzylinderId == doppel.First().Id).ToList();

                ViewBag.Aussen = size.Select(x => x.aussen).ToList();
                ViewBag.Intern = size.Select(x => x.Intern).ToList();

                var options = doppel.SelectMany(x=>x.Profil_Doppelzylinder_Options).ToList();
                var optionsItem = doppel.SelectMany(x => x.Profil_Doppelzylinder_Options).SelectMany(x => x.NGF).ToList();
                var optionsValue = doppel.SelectMany(x=> x.Profil_Doppelzylinder_Options).SelectMany(x=>x.NGF).SelectMany(x=>x.NGF_Value).ToList();

                var listCountHalb = new List<int>();
                
                 listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var list = new List<int>();

                foreach (var fs in optionsItem)
                {
                    list.Add(fs.NGF_Value.Count());
                }

                ViewBag.countOptionsList = list;
                ViewBag.optionsValue = optionsValue.Select(x=>x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x=>x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());

                var doppelKleinSize = size.First().Doppel_Innen_klein;


                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(size.Select(x => x.costSizeIntern).ToList());
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(size.Select(x => x.costSizeAussen).ToList());

                ViewBag.DopelzylinderIntern = doppelKleinSize.Select(x => x.Intern).ToList();
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(size.Select(x => x.costSizeIntern).Skip(1).ToList());

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(size.Where(x => x.Intern > 0).Select(x => x.Intern).ToList());
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(doppelKleinSize.Where(x => x.Intern > 0).Select(x => x.Intern).ToList());
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(doppelKleinSize.Select(x => x.costSizeIntern).ToList());

            }
            if (halb.Count() > 0)
            {
                ViewBag.Item = halb;

                var size = db.Aussen_Innen_Halbzylinder.Where(x=>x.Profil_HalbzylinderId==halb.First().Id).ToList();

                ViewBag.Aussen = size.Select(x => x.aussen).ToList();
                ViewBag.Intern = null;

                var options = halb.SelectMany(x => x.Profil_Halbzylinder_Options).ToList();
                var optionsItem = halb.SelectMany(x => x.Profil_Halbzylinder_Options).SelectMany(x => x.Halbzylinder_Options).ToList();
                var optionsValue = halb.SelectMany(x => x.Profil_Halbzylinder_Options).SelectMany(x => x.Halbzylinder_Options).SelectMany(x => x.Halbzylinder_Options_value).ToList();

                var listCountHalb = new List<int>();

                listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var list = new List<int>();

                foreach (var fs in optionsItem)
                {
                    list.Add(fs.Halbzylinder_Options_value.Count());
                }


                string[] emptyArray = new string[] { };

                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = optionsValue.Select(x => x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());


                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(size.Select(x => x.costAussen).ToList());

                ViewBag.DopelzylinderIntern = null;
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(emptyArray);

                ViewBag.countOptionsQuery = options.Count();


            }
            if (knayf.Count() > 0)
            {
                ViewBag.Item = knayf;

                var size = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == knayf.First().Id).ToList();

                ViewBag.Aussen = size.Select(x => x.aussen).ToList();
                ViewBag.Intern = size.Select(x => x.Intern).ToList();

                var options = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == knayf.First().Id).ToList();
                var optionsItem = new List<Knayf_Options>();
                var optionsValue = new List<Knayf_Options_value>();
                
                foreach (var option in options)
                {
                    var items = db.Knayf_Options.Where(x => x.OptionsId == option.Id).ToList();

                    foreach(var list in items)
                    {
                        optionsItem.Add(list);
                    }
                }

                foreach (var option in optionsItem)
                {
                    var items = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == option.Id).ToList();

                    foreach (var list in items)
                    {
                        optionsValue.Add(list);
                    }
                }

                var listCountHalb = new List<int>();

                listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var lists = new List<int>();

                foreach (var fs in optionsItem)
                {
                    lists.Add(fs.Knayf_Options_value.Count());
                }

                ViewBag.countOptionsList = lists;
                ViewBag.optionsValue = optionsValue.Select(x => x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());

                var doppelKleinSize = size.First().Aussen_Innen_Knauf_klein;

                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(size.Select(x => x.costSizeIntern).ToList());
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(size.Select(x => x.costSizeAussen).ToList());

                ViewBag.DopelzylinderIntern = doppelKleinSize.Select(x => x.Intern).ToList();
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(size.Select(x => x.costSizeIntern).Skip(1).ToList());

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(size.Where(x => x.Intern > 0).Select(x => x.Intern).ToList());
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(doppelKleinSize.Where(x => x.Intern > 0).Select(x => x.Intern).ToList());
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(doppelKleinSize.Select(x => x.costSizeIntern).ToList());

            }
            if (hebel.Count() > 0)
            {
                ViewBag.Item = hebel;

                ViewBag.Aussen = null;
                ViewBag.Intern = null;

                var options = hebel.SelectMany(x => x.Hebelzylinder_Options).ToList();
                var optionsItem = hebel.SelectMany(x => x.Hebelzylinder_Options).SelectMany(x => x.Options).ToList();
                var optionsValue = hebel.SelectMany(x => x.Hebelzylinder_Options).SelectMany(x => x.Options).SelectMany(x => x.Options_value).ToList();

                var listCountHalb = new List<int>();

                listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var list = new List<int>();

                foreach (var fs in optionsItem)
                {
                    list.Add(fs.Options_value.Count());
                }

                string[] emptyArray = new string[] { };

                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = optionsValue.Select(x => x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());


                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderIntern = null;
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(emptyArray);

                ViewBag.countOptionsQuery = options.Count();

            }
            if (vorhan.Count() > 0)
            {
                ViewBag.Item = vorhan;
                var size = db.Size.Where(x => x.VorhangschlossId == vorhan.First().Id).ToList();

                ViewBag.Aussen = size.Select(x => x.sizeVorhangschloss).ToList();
                ViewBag.Intern = null;

                var options = hebel.SelectMany(x => x.Hebelzylinder_Options).ToList();
                var optionsItem = hebel.SelectMany(x => x.Hebelzylinder_Options).SelectMany(x => x.Options).ToList();
                var optionsValue = hebel.SelectMany(x => x.Hebelzylinder_Options).SelectMany(x => x.Options).SelectMany(x => x.Options_value).ToList();

                var listCountHalb = new List<int>();

                listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var list = new List<int>();

                foreach (var fs in optionsItem)
                {
                    list.Add(fs.Options_value.Count());
                }

                string[] emptyArray = new string[] { };

                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = optionsValue.Select(x => x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());


                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderIntern = null;
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(emptyArray);

                ViewBag.countOptionsQuery = options.Count();
            }
            if (aussen.Count() > 0)
            {
                ViewBag.Item = aussen;

                ViewBag.Aussen = null;
                ViewBag.Intern = null;
                var options = aussen.SelectMany(x => x.Aussen_Rund_options).ToList();
                var optionsItem = aussen.SelectMany(x => x.Aussen_Rund_options).SelectMany(x => x.Aussen_Rund_all).ToList();
                var optionsValue = aussen.SelectMany(x => x.Aussen_Rund_options).SelectMany(x => x.Aussen_Rund_all).SelectMany(x => x.Aussen_Rouns_all_value).ToList();

                var listCountHalb = new List<int>();

                listCountHalb.Add(optionsItem.Count());

                ViewBag.countOptionsQuery = options.Count();
                ViewBag.optionsName = optionsItem;

                var list = new List<int>();

                foreach (var fs in optionsItem)
                {
                    list.Add(fs.Aussen_Rouns_all_value.Count());
                }

                string[] emptyArray = new string[] { };

                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = optionsValue.Select(x => x.Value).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(optionsItem.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(optionsValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(optionsValue.Select(x => x.Cost).ToList());


                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);
                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderIntern = null;
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(emptyArray);

                ViewBag.DopelzylinderInternNormal = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKlein = JsonConvert.SerializeObject(emptyArray);
                ViewBag.DopelzylinderInternKleinPreis = JsonConvert.SerializeObject(emptyArray);

                ViewBag.countOptionsQuery = options.Count();
            }

            return View("../Schop/zylinder_page");
        }
        [HttpPost]
        public async Task<IActionResult> Order_item(OrderZylinder zylinder)
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            float cost = float.Parse(zylinder.Cost.Replace("€", "").Trim());

            string loginInform = ident.Claims.Select(x => x.Value).First();

            var users = db.Users.FirstOrDefault(x => x.Id == loginInform);

            var UserOrderSchop = new UserOrdersShop
            {
                OrderSum = cost,
                ProductName = zylinder.Name,
                UserId = users.Id,
                createData = DateTime.Now,
                OrderStatus = "Nicht bezahlt",
                count = 1,
                KeyCount = 3,
                KeyCost = 0,
                UserOrderKey = "",
            };

            db.UserOrdersShop.Add(UserOrderSchop);
            db.SaveChanges();

            var ProductSysteam = new ProductSysteam
            {
                Name = zylinder.Name,
                Aussen = zylinder.Aussen,
                Intern = zylinder.Intern,
                Option = zylinder.Options,
                Count = zylinder.Count,
                Price = cost,
                UserOrdersShopId = UserOrderSchop.Id
            };

            db.ProductSysteam.Add(ProductSysteam);
            db.SaveChanges();

            return Redirect("/Identity/Account/Manage/PagePersonalOrders");
        }
    }
}
