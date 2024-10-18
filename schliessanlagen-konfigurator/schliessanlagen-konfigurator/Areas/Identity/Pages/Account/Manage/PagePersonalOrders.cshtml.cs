using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using schliessanlagen_konfigurator.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Vorhan;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using schliessanlagen_konfigurator.Models.Users;
using System.Net.Http.Headers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace schliessanlagen_konfigurator.Areas.Identity.Pages.Account.Manage
{
    public class PagePersonalOrdersModel : PageModel
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public PagePersonalOrdersModel(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment
        , IHttpContextAccessor httpContextAccessor, UserManager<User> userManager,SignInManager<User> signInManager)
        {
            db = context;
            Environment = _environment;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public List<User> Users { get; set; } = new List<User>();
        public async Task<IActionResult> OnGet()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            string loginInform = ident.Claims.Select(x => x.Value).FirstOrDefault();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == loginInform);

            Users.Add(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var userOrder = db.UserOrdersShop.Where(x => x.UserId == user.Id).ToList();

            if (userOrder.Count()>0)
            {
                var ListItem = new List<UserOrdersShop>();

                var OrderList = db.UserOrdersShop.Where(x => x.UserId == user.Id && x.OrderStatus == "Nicht bezahlt").Distinct().ToList();

                foreach (var list in OrderList)
                {
                    ListItem.Add(list);
                }

                var ListItemProduct = new List<Models.Users.ProductSysteam>();
                List<float> gramm = new List<float>();
                List<string> images = new List<string>();
                List<string> descriptions = new List<string>();

                List<float> SumGramm = new List<float>();
                var countIterationProduct = new List<int>();

                for (int f = 0; f < ListItem.Count(); f++)
                {
                    var ProductList = db.ProductSysteam.Where(x => x.UserOrdersShopId == ListItem[f].Id).Distinct().ToList();

                    foreach (var list in ProductList)
                    {
                        ListItemProduct.Add(list);
                        var doppel = db.Profil_Doppelzylinder.FirstOrDefault(x => x.Name == list.Name);
                        var knayf = db.Profil_Knaufzylinder.FirstOrDefault(x => x.Name == list.Name);
                        var halb = db.Profil_Halbzylinder.FirstOrDefault(x => x.Name == list.Name);
                        var hebel = db.Hebelzylinder.FirstOrDefault(x => x.Name == list.Name);
                        var Vorhan = db.Vorhangschloss.FirstOrDefault(x => x.Name == list.Name);
                        var Aussen = db.Aussenzylinder_Rundzylinder.FirstOrDefault(x => x.Name == list.Name);

                        if (doppel != null)
                        {
                            gramm.Add(doppel.Gramm ?? 0);
                            images.Add(doppel.ImageName);
                            descriptions.Add(doppel.description);
                        }
                        if (knayf != null)
                        {
                            gramm.Add(knayf.Gramm ?? 0);
                            images.Add(knayf.ImageName);
                            descriptions.Add(knayf.description);
                        }
                        if (halb != null)
                        {
                            gramm.Add(halb.Gramm ?? 0);
                            images.Add(halb.ImageName);
                            descriptions.Add(halb.description);
                        }
                        if (hebel != null)
                        {
                            gramm.Add(hebel.Gramm ?? 0);
                            images.Add(hebel.ImageName);
                            descriptions.Add(hebel.description);
                        }
                        if (Vorhan != null)
                        {
                            gramm.Add(Vorhan.Gramm ?? 0);
                            images.Add(Vorhan.ImageName);
                            descriptions.Add(Vorhan.description);
                        }
                        if (Aussen != null)
                        {
                            gramm.Add(Aussen.Gramm ?? 0);
                            images.Add(Aussen.ImageName);
                            descriptions.Add(Aussen.description);
                        }
                    }
                    countIterationProduct.Add(ProductList.Count);
                    SumGramm.Add(gramm.Sum());
                    gramm = new List<float>();
                }


                ViewData["Image"] = images;
                ViewData["Descriptions"] = descriptions;

                ViewData["OrderLis"] = ListItem;
                ViewData["OrderItem"] = ListItemProduct.OrderBy(x => x.UserOrdersShopId).ToList();

                ViewData["CounterProduct"] = countIterationProduct;
                ViewData["Gewicht"] = SumGramm;
            }

            return Page();

        }
        public async Task<IActionResult> OnPost(string SendVorname, string SendNachname,string SendFirma,
        string SendVat,string SendStrasse, string SendZip, string SendStadt, string SendLand, string SendTelefon,string NettoSum,
        string DhlSend,string Pay,string Steuer, string Versand, string OrderSum,string Rehnung, string RechnungAdresse,string RechnungVorname,
        string RechnungNachname, string RechnungFirma, string RechnungVat, string RechnungStrasse, string RechnungZip,
        string RechnungStadt, string RechnungLand, string RechnungTelefon, string userName,string procent,bool aufRechnung)
        {

            var model = new
            {
                SendVorname = SendVorname,
                SendNachname = SendNachname,
                SendFirma = SendFirma,
                SendVat = SendVat,
                SendStrasse = SendStrasse,

                SendZip = SendZip,
                SendStadt = SendStadt,
                SendLand = SendLand,
                Steuer = Steuer,
                Versand = Versand,
                OrderSum = OrderSum,
                NettoSum = NettoSum,
                SendTelefon = SendTelefon,
                DhlSend = DhlSend,
                Rehnung = Rehnung,
                Pay = Pay,
                procent = procent,

                RechnungVorname = RechnungVorname,
                RechnungNachname = RechnungNachname,
                RechnungFirma = RechnungFirma,
                RechnungVat = RechnungVat,
                RechnungStrasse = RechnungStrasse,
                RechnungZip = RechnungZip,
                RechnungStadt = RechnungStadt,
                RechnungLand = RechnungLand,
                RechnungTelefon = RechnungTelefon,

            };

            var JsonObject = JsonConvert.SerializeObject(model, Formatting.Indented);
            return RedirectToAction("SendRehnung","Konfigurator", new { info = JsonObject, userName = userName , OrderSum = OrderSum, aufRechnung= aufRechnung });
        }
    }
}
