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
using System.Globalization;
using System.Linq;
using schliessanlagen_konfigurator.Models.Hebel;
using Microsoft.AspNetCore.Http;

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

                var SizeCost = new List<float>();

                var DoppelOptionenList = new List<NGF>();
                
                var CounterOption = new List<int>();
                
                var KayfOptionenList = new List<Knayf_Options>();

                var HalbOptionenList = new List<Halbzylinder_Options>();
                var HebelOptionenList = new List<Options>();
                var VorhengOptionenList = new List<OptionsVorhan>();
                var AussenOptionenList = new List<Aussen_Rund_all>();   

                var allOptionList = new List<(string name, float price)>();

                var AllNormalPrice = new List<float>(); 

                var itemList = new List<string>();

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

                            var isOption = db.Profil_Doppelzylinder_Options.Where(x=>x.DoppelzylinderId == doppel.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);


                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.NGF.Include(x => x.NGF_Value).Where(x => x.OptionsId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        DoppelOptionenList.Add(value);
                                    }

                                }
                                var x = DoppelOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();

                                DoppelOptionenList = x;

                                CounterOption.Add(x.Count());

                                var t = DoppelOptionenList.SelectMany(x => x.NGF_Value).Where(x => x.Cost > 0).Select(x=>x.Cost).ToList();
                                var s = 0;

                                foreach(var item in DoppelOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ??0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);
                            }

                        }

                        if (knayf != null)
                        {
                            gramm.Add(knayf.Gramm ?? 0);
                            images.Add(knayf.ImageName);
                            descriptions.Add(knayf.description);

                            var isOption = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == knayf.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);

                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.Knayf_Options.Include(x => x.Knayf_Options_value).Where(x => x.OptionsId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        KayfOptionenList.Add(value);
                                    }

                                }
                                var x = KayfOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();

                                CounterOption.Add(x.Count());

                                KayfOptionenList = x;

                                var t = KayfOptionenList.SelectMany(x => x.Knayf_Options_value).Where(x => x.Cost > 0).Select(x => x.Cost).ToList();
                                var s = 0;

                                foreach (var item in KayfOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ?? 0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);
                            }

                        }
                        if (halb != null)
                        {
                            gramm.Add(halb.Gramm ?? 0);
                            images.Add(halb.ImageName);
                            descriptions.Add(halb.description);

                            var isOption = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == halb.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);


                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.Halbzylinder_Options.Include(x => x.Halbzylinder_Options_value).Where(x => x.OptionsId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        HalbOptionenList.Add(value);
                                    }

                                }
                                var x = HalbOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();

                                CounterOption.Add(x.Count());

                                HalbOptionenList = x;

                                var PriceAusse_Innen =
                                list.Price
                                - halb.Price
                                - HalbOptionenList
                                    .SelectMany(x => x.Halbzylinder_Options_value) // Обработка null
                                    .Where(x => x.Cost != null) // Игнорируем null-значения Cost
                                    .Sum(x => x.Cost.Value); // Если Cost nullable, извлекаем значение

                                var t = HalbOptionenList.SelectMany(x => x.Halbzylinder_Options_value).Where(x => x.Cost > 0).Select(x => x.Cost).ToList();
                                var s = 0;

                                foreach (var item in HalbOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ?? 0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);
                            }

                        }
                        if (hebel != null)
                        {
                            gramm.Add(hebel.Gramm ?? 0);
                            images.Add(hebel.ImageName);
                            descriptions.Add(hebel.description);

                            var isOption = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);


                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.Options.Include(x => x.Options_value).Where(x => x.OptionId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        HebelOptionenList.Add(value);
                                    }

                                }
                                var x = HebelOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();
                                
                                CounterOption.Add(x.Count());

                                HebelOptionenList = x;
                              
                                var t = HebelOptionenList.SelectMany(x => x.Options_value).Where(x => x.Cost > 0).Select(x => x.Cost).ToList();
                                var s = 0;

                                foreach (var item in HebelOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ?? 0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);

                            }

                        }
                        if (Vorhan != null)
                        {
                            gramm.Add(Vorhan.Gramm ?? 0);
                            images.Add(Vorhan.ImageName);
                            descriptions.Add(Vorhan.description);

                            var isOption = db.Vorhan_Options.Where(x => x.VorhangschlossId == Vorhan.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);


                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.OptionsVorhan.Include(x => x.Options_value).Where(x => x.OptionId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        VorhengOptionenList.Add(value);
                                    }

                                }
                                var x = VorhengOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();

                                CounterOption.Add(x.Count());

                                VorhengOptionenList = x;

                                var t = VorhengOptionenList.SelectMany(x => x.Options_value).Where(x => x.Cost > 0).Select(x => x.Cost).ToList();
                                var s = 0;

                                foreach (var item in VorhengOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ?? 0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);
                            }
                        }
                        if (Aussen != null)
                        {
                            gramm.Add(Aussen.Gramm ?? 0);
                            images.Add(Aussen.ImageName);
                            descriptions.Add(Aussen.description);

                            var isOption = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == Vorhan.Id).ToList();

                            var input = list.Option;

                            if (input != null)
                            {
                                string pattern = @"\w+\s*=\s*[^:]+";

                                MatchCollection matches = Regex.Matches(input, pattern);


                                foreach (Match match in matches)
                                {
                                    itemList.Add(match.Value);
                                }

                                foreach (var option in isOption)
                                {
                                    var Options = db.Aussen_Rund_all.Include(x => x.Aussen_Rouns_all_value).Where(x => x.Aussen_Rund_optionsId == option.Id).ToList();

                                    foreach (var value in Options)
                                    {
                                        AussenOptionenList.Add(value);
                                    }

                                }
                                var x = AussenOptionenList.Where(opt => itemList.Any(item => opt.Name.Contains(item))).ToList();
                                CounterOption.Add(x.Count());

                                AussenOptionenList = x;

                                var t = AussenOptionenList.SelectMany(x => x.Aussen_Rouns_all_value).Where(x => x.Cost > 0).Select(x => x.Cost).ToList();
                                var s = 0;

                                foreach (var item in AussenOptionenList)
                                {
                                    allOptionList.Add((item.Name, t[s] ?? 0f));
                                    s++;
                                }
                            }
                            else
                            {
                                allOptionList.Add(("", 0f));
                                CounterOption.Add(0);

                            }
                        }
                    }
                    countIterationProduct.Add(ProductList.Count);
                    SumGramm.Add(gramm.Sum());
                    gramm = new List<float>();
                }

                ViewData["Options"] = allOptionList.ToList();
                ViewData["CountOptions"] = CounterOption;

                ViewData["Image"] = images;
                ViewData["Descriptions"] = descriptions;

                ViewData["OrderLis"] = ListItem;
                ViewData["OrderItem"] = ListItemProduct.OrderBy(x => x.UserOrdersShopId).ToList();

                ViewData["CounterProduct"] = countIterationProduct;
                ViewData["Gewicht"] = SumGramm;
            }

            return Page();

        }
        public async Task<IActionResult> OnPost(string SendVorname, string SendNachname, string SendFirma, string userKey, List<string> PreisProduct, string PreisKey,
        string SendVat,string SendStrasse, string SendZip, string SendStadt, string SendLand, string SendTelefon,string NettoSumOrder, List<int>  OptionCount,
        string DhlSend,string Pay,string Steuer, string Versand, string OrderSum,string Rehnung, string RechnungAdresse,string RechnungVorname,
        string RechnungNachname, string RechnungFirma, string RechnungVat, string RechnungStrasse, string RechnungZip,
        string RechnungStadt, string RechnungLand, string RechnungTelefon, string userName,string procent,bool aufRechnung)
        {
            var userOrder = db.UserOrdersShop.Where(x => x.UserId == userKey).ToList();

            userOrder.Last().KeyCost = float.Parse(PreisKey.Replace(".",","));

            // Обновляем заказ
            db.UserOrdersShop.Update(userOrder.Last());
            db.SaveChanges();
            // Получаем список продуктов для данного заказа
            var ProductList = db.ProductSysteam.Where(x => x.UserOrdersShopId == userOrder.Last().Id).ToList();

            // Убедитесь, что PreisProduct имеет нужное количество элементов
            if (ProductList.Count() > 0 && PreisProduct.Count() == ProductList.Count())
            {
                // Обновляем цены продуктов
                for (int i = 0; i < ProductList.Count(); i++)
                {
                    var product = ProductList[i];

                    product.Price = float.Parse(PreisProduct[i].Replace(".", ","));
                    db.ProductSysteam.Update(product);
                    db.SaveChanges();
                }
            }

            var model = new
            {
                SendVorname = SendVorname,
                SendNachname = SendNachname,
                SendFirma = SendFirma,
                SendVat = SendVat,
                SendStrasse = SendStrasse,

                NettoSum = NettoSumOrder,

                SendZip = SendZip,
                SendStadt = SendStadt,
                SendLand = SendLand,
                Steuer = Steuer,
                Versand = Versand,
                OrderSum = OrderSum,
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
