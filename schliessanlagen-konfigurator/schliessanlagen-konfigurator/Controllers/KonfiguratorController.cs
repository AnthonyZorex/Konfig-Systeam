using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Hebelzylinder;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Vorhan;
using schliessanlagen_konfigurator.Models.Users;
using User = schliessanlagen_konfigurator.Models.Users.User;
using System.IO;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.Options;
using schliessanlagenkonfigurator.Migrations;
using OfficeOpenXml.Core;
using NuGet.Protocol;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Immutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

namespace schliessanlagen_konfigurator.Controllers
{
    public class KonfiguratorController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public KonfiguratorController(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment
        , IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            Environment = _environment;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<ActionResult> UserSave(string Name, string VorName, string Login,string Password,string Email,string Telefonnumer,string Status)
        {
            var newUser = new Models.Users.User
            {
               Name = Name,
               Email = Email,
               Password = Password,
               Login = Login,
               Status = Status,
               Sername = VorName,
               PhoneNumber = Telefonnumer
            };
            
            db.User.Add(newUser);
            db.SaveChanges();

            return RedirectToAction("AdminConnect", newUser);
        }
        public async Task<ActionResult> AdminConnect(User UserLogin , int UserId)
        {
            if (UserLogin.Id != 0)
            {
                var UserItem = db.User.FirstOrDefault(x => x.Id == UserLogin.Id);

                var ListItem = new List<UserOrdersShop>();


                var OrderList = db.UserOrdersShop.Where(x => x.UserId == UserItem.Id).Distinct().ToList();

                foreach (var list in OrderList)
                {
                    ListItem.Add(list);
                }


                ViewBag.CountOrders = ListItem.Count();

                var ListItemProduct = new List<Models.Users.ProductSysteam>();

                for (int f = 0; f < ListItem.Count(); f++)
                {
                    var ProductList = db.ProductSysteam.Where(x => x.UserOrdersShopId == ListItem[f].Id).Distinct().ToList();

                    foreach (var list in ProductList)
                    {
                        ListItemProduct.Add(list);
                    }
                }

                ViewBag.OrderList = ListItem;
                ViewBag.OrderItem = ListItemProduct.OrderBy(x => x.UserOrdersShopId).ToList();

                var countIterationProduct = new List<int>();

                foreach(var list in ListItem)
                {
                    var p = ListItemProduct.OrderBy(x => x.UserOrdersShopId).Where(x => x.UserOrdersShopId == list.Id).ToList();
                    countIterationProduct.Add(p.Count());
                }

                ViewBag.CounterProduct = countIterationProduct.ToList();

                return View(UserItem);
            }
            if (UserId != 0)
            {
                var UserItemID = db.User.FirstOrDefault(x => x.Id == UserId);

                var ListItemId = new List<UserOrdersShop>();


                var OrderListId = db.UserOrdersShop.Where(x => x.UserId == UserId).Distinct().ToList();

                foreach (var list in OrderListId)
                {
                    ListItemId.Add(list);
                }


                ViewBag.CountOrders = ListItemId.Count();

                var ListItemProductId = new List<Models.Users.ProductSysteam>();

                for (int f = 0; f < ListItemId.Count(); f++)
                {
                    var ProductList = db.ProductSysteam.Where(x => x.UserOrdersShopId == ListItemId[f].Id).Distinct().ToList();

                    foreach (var list in ProductList)
                    {
                        ListItemProductId.Add(list);
                    }
                }

                ViewBag.OrderList = ListItemId.ToList();
                ViewBag.OrderItem = ListItemProductId;

                var countIterationProduct = new List<int>();

                foreach (var list in ListItemId)
                {
                    var p = ListItemProductId.OrderBy(x => x.UserOrdersShopId).Where(x => x.UserOrdersShopId == list.Id).ToList();
                    countIterationProduct.Add(p.Count());
                }

                ViewBag.CounterProduct = countIterationProduct.ToList();

                return View(UserItemID);
            }
            return View();
        }
        public async Task<ActionResult> LoginPerson(string Login,string Password,string url)
        {
            var UserLogin = db.User.FirstOrDefault(x => x.Login == Login && x.Password == Password);

            if (UserLogin != null)
            {
                var ListItemId = new List<UserOrdersShop>();


                var OrderListId = db.UserOrdersShop.Where(x => x.UserId == UserLogin.Id).ToList();

                foreach (var list in OrderListId)
                {
                    ListItemId.Add(list);
                }


                ViewBag.CountOrders = ListItemId.Count();

                var UserLoginList = db.User.Where(x => x.Login == Login && x.Password == Password).ToList();

                var queryOrderId = from t1 in UserLoginList

                                   select new
                                   {
                                       t1.Name,
                                       t1.Sername,
                                      t1.Email,
                                       t1.PhoneNumber,
                                       t1.Password,
                                        t1.Status,
                                        t1.Login,
                                        t1.Adress,
                                       countOrder = ListItemId.Count(),
                                       t1.Id,
                                   };

                var UserLoginP = queryOrderId.ToList();
               
                if (url == null)
                {
                    return RedirectToAction("IndexKonfigurator", new { Auser = UserLoginP });
                }
                else
                {
                    var UserName_UserSername = UserLoginList.Select(x=>x.Name).First() + " " + UserLoginList.Select(x=>x.Sername).First();
                    ViewBag.UserNameItem = UserName_UserSername;
                    ViewBag.UserInformStatus = UserLoginList.Select(x => x.Status).First();
                    ViewBag.UserId = UserLoginList.Select(x => x.Id).First();
                    ViewBag.CountOrders = db.UserOrdersShop.Where(x=>x.UserId == UserLoginList.Select(x=>x.Id).First());

                    return RedirectPermanent($"~/{url}");
                }
               
            }
            else
            {
                ViewBag.CountOrders = 0;
                TempData["AlertMessage"] = "Sie haben einen falschen Benutzernamen oder ein falsches Passwort eingegeben!";
                return RedirectPermanent($"~/{url}");
            }
            
        }
        public ActionResult IndexKonfigurator(string Status,string Auser)
        {
            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();

            if (Auser != null)
            {
                string[] fruits = Auser.Split(',');

                for (int i = 0; i < fruits.Length; i++)
                {
                    int index = fruits[i].IndexOf("=");
                    if (index != -1)
                    {
                        string result = fruits[i].Substring(index + 1);
                        fruits[i] = result;
                    }
                    
                }
                for (int i = 0; i < fruits.Length; i++)
                {
                    int index = fruits[i].IndexOf("}");
                    if (index != -1)
                    {
                        string result = fruits[i].Substring(0, index);
                        fruits[i] = result;
                    }

                }
                for (int i = 0; i < fruits.Length; i++)
                {
                    int index = fruits[i].IndexOf(" ");
                    if (index != -1)
                    {
                        string result = fruits[i].Substring(index + 1);
                        fruits[i] = result;
                    }

                }
                var UserName_UserSername = fruits[0] + " " + fruits[1];
                ViewBag.UserInformStatus = fruits[5];
                ViewBag.UserId = fruits[9];
                ViewBag.UserNameItem = UserName_UserSername;
                ViewBag.CountOrders = fruits[8];
            }
            else
            {
                ViewBag.CountOrders = 0;
            }

            
            #region allParametrsDoppel

            var a = db.Aussen_Innen.Select(x => x.aussen).Distinct().OrderBy(x=>x).ToList();
            var d = db.Aussen_Innen.Select(x => x.Intern).Distinct().OrderBy(x => x).ToList();

            var DoppeloptionsName = db.NGF.Select(x => x.Name).ToList();

            var listAllInnen = new List<float>();

            var ListAussenDopple = new List<float>();

            for (int i = 0; i < a.Count(); i++)
                ListAussenDopple.Add(a[i]);

            ViewBag.DoppelAussen = ListAussenDopple.Distinct();

            var ListInternDopple = new List<float>();
            for (int i = 0; i < d.Count(); i++)
                ListInternDopple.Add(d[i]);

            ViewBag.DoppelIntern = ListInternDopple.Distinct();

            var DoppelListOptions = new List<string>();

            for (int i = 0; i < DoppeloptionsName.Count; i++)
                DoppelListOptions.Add(DoppeloptionsName[i]);

            ViewBag.OptionsName = DoppelListOptions.Distinct();
 
            #endregion
            #region knayf allParametrsKnayf

            var b = db.Aussen_Innen_Knauf.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();
            var ba = db.Aussen_Innen_Knauf.Select(x => x.Intern).Distinct().OrderBy(x => x).ToList();
            var KnayfOptions = db.Knayf_Options.Select(x => x.Name).ToList();

            var listKnayfAussen = new List<float>();
            var listKnayfIntern = new List<float>();
            var KnayfListOptions = new List<string>();

            for (int i = 0; i < b.Count(); i++)
                listKnayfAussen.Add(b[i]);

            for (int i = 0; i < ba.Count(); i++)
                listKnayfIntern.Add(ba[i]);

            for (int i = 0; i < KnayfOptions.Count(); i++)
                KnayfListOptions.Add(KnayfOptions[i]);

            #endregion
            #region allPrametrsHalbzylinder
            var c = db.Aussen_Innen_Halbzylinder.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();

            var HalbzylinderAussen = new List<float>();

            for (int i = 0; i < c.Count(); i++)
                HalbzylinderAussen.Add(c[i]);

            var HalbzylinderOptions = db.Halbzylinder_Options.Select(x => x.Name).ToList();
            var HalbzylunderAllOptions = new List<string>();

            for (int i = 0; i < HalbzylinderOptions.Count(); i++)
                HalbzylunderAllOptions.Add(HalbzylinderOptions[i]);

          
            #endregion
            #region VorhanSchloss

            var size = db.Size.Select(x => x.sizeVorhangschloss).ToList();
            var VorhanSchlossSize = new List<float>();

            for (int i = 0; i < size.Count(); i++)
                VorhanSchlossSize.Add(size[i]);

            var VorhanSchloss = db.OptionsVorhan.Select(x => x.Name).ToList();
            var VorhanSchlossOptions = new List<string>();

            for (int i = 0; i < VorhanSchloss.Count(); i++)
                VorhanSchlossOptions.Add(VorhanSchloss[i]);
            #endregion

            #region Hebel

            var HebelOptions = db.Options.Select(x => x.Name).ToList();
            var HebelAllOptions = new List<string>();

            for (int i = 0; i < HebelOptions.Count(); i++)
                HebelAllOptions.Add(HebelOptions[i]);

            #endregion

            #region Aussen

            var AussenOptions = db.Aussen_Rund_all.Select(x => x.Name).ToList();
            var AussenAllOptions = new List<string>();
            for (int i = 0; i < AussenOptions.Count(); i++)
                AussenAllOptions.Add(AussenOptions[i]);

            #endregion

            var session = _contextAccessor.HttpContext.Session;
            var UserKey = session.Id;
            Orders user = new Orders();
            user.userKey = UserKey;
            ViewBag.isOpen = db.KeyValue.Select(x => x.isOpen);



            ViewBag.KnayfAussen = listKnayfAussen.Distinct();
            ViewBag.KnayfInter = listKnayfIntern.Distinct();

            ViewBag.JsonDoppelOption = JsonConvert.SerializeObject((DoppelListOptions.Distinct()));
            ViewBag.JsonKnayf = JsonConvert.SerializeObject(KnayfOptions.Distinct());
            ViewBag.JsonHabl = JsonConvert.SerializeObject(HalbzylunderAllOptions.Distinct());
            ViewBag.JsonHebel = JsonConvert.SerializeObject(HebelAllOptions);
            ViewBag.JsonVorhan = JsonConvert.SerializeObject(VorhanSchlossOptions.Distinct());
            ViewBag.JsonAussen = JsonConvert.SerializeObject(AussenAllOptions.Distinct());

            ViewBag.SizeDoppelAussen = JsonConvert.SerializeObject(ListAussenDopple.Distinct());
            ViewBag.SizeDoppelIntern = JsonConvert.SerializeObject(ListInternDopple.Distinct());


            ViewBag.SizeKnayfAussen = JsonConvert.SerializeObject(listKnayfAussen.Distinct());
            ViewBag.SizeKnayfIntern = JsonConvert.SerializeObject(listKnayfIntern.Distinct());

            ViewBag.SizeHalb = JsonConvert.SerializeObject(HalbzylinderAussen.Distinct());

            ViewBag.SizeVorhan = JsonConvert.SerializeObject(VorhanSchlossSize.Distinct());

            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult> System_Auswählen(Orders userKey, string userName)
        {
            char[] separators = { ' ', '\n', '\t', '\r' };
            
            if (userName != null)
            {
                string[] words = userName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var UserLogin = db.User.FirstOrDefault(x => x.Name == words[0] & x.Sername == words[1]);
                var UserName_UserSername = UserLogin.Name + " " + UserLogin.Sername;
                ViewBag.UserInform = UserLogin.Status;
                ViewBag.UserId = UserLogin.Id;
                ViewBag.UserNameItem = UserName_UserSername;
            }


            var orders = await db.Orders.ToListAsync();
            var keyUser = orders.Last();
            var allUserListOrder = await db.Orders.Where(x => x.userKey == keyUser.userKey).ToListAsync();

            ViewBag.Zylinder_Typ = await db.Schliessanlagen.ToListAsync();

            var profilD = await db.Profil_Doppelzylinder.ToListAsync();

            var profilH = await db.Profil_Halbzylinder.ToListAsync();
            var profilK = await db.Profil_Knaufzylinder.ToListAsync();
            var hebel = await db.Hebelzylinder.ToListAsync();
            var Vorhangschloss = await db.Vorhangschloss.ToListAsync();
            var Aussenzylinder = await db.Aussenzylinder_Rundzylinder.ToListAsync();
            var Zylinder_Typ = await db.Schliessanlagen.ToListAsync();

            var cheked = new List<Profil_Doppelzylinder>();
            var cheked2 = new List<Profil_Knaufzylinder>();
            var cheked3 = new List<Profil_Halbzylinder>();
            var cheked4 = new List<Hebel>(); 
            var cheked5 = new List<Vorhangschloss>();
            var cheked6 = new List<Aussenzylinder_Rundzylinder>(); 

            var dopelType = profilD.Select(x => x.schliessanlagenId).First();
            var KnayfType = profilK.Select(x => x.schliessanlagenId).First();
            var HebelType = hebel.Select(x => x.schliessanlagenId).First();
            var HalbType = profilH.Select(x => x.schliessanlagenId).First();
            var VorhanType = Vorhangschloss.Select(x => x.schliessanlagenId).First();
            var AussenType = Aussenzylinder.Select(x => x.schliessanlagenId).First();
    
            var allOderDopelSyze = allUserListOrder.Where(x => x.ZylinderId == dopelType).ToList();

                if (allOderDopelSyze.Count()>0)
                {
                    var maxInnenParameter = allOderDopelSyze.Max(x => x.innen);
                    var maxAussenParameter = allOderDopelSyze.Max(x => x.aussen);

                    var dopelProduct = new List<Profil_Doppelzylinder>();

                    var products = await db.Aussen_Innen.ToListAsync();

                    var items = products.Where(x => x.aussen >= maxInnenParameter & x.Intern >= maxAussenParameter).Select(x=>x.Profil_DoppelzylinderId).Distinct().ToList();

                    var safeDoppelItem = new List<Profil_Doppelzylinder>();

                    for (int i = 0; i < items.Count(); i++)
                    {
                        var chekedItem = db.Profil_Doppelzylinder.Where(x => x.Id == items[i]).ToList();

                        for (int g = 0; g < chekedItem.Count(); g++)
                            safeDoppelItem.Add(chekedItem[g]);

                    }

                    for (int j = 0; j < safeDoppelItem.Count(); j++)
                    {
                        dopelProduct.Add(safeDoppelItem[j]);
                    }

                    var allDopelOption = allUserListOrder.Where(x => x.ZylinderId == dopelType).ToList();

                    var DopelOption = from t1 in allUserListOrder
                                      join t2 in dopelProduct
                                          on t1.ZylinderId equals t2.schliessanlagenId
                                      select new
                                      {
                                          Option = t1.Options
                                      };


                    var OptionDoppel = DopelOption.Where(x => x.Option != null).Count();


                if (OptionDoppel>0)
                {
                        var OptionsList = new List<Profil_Doppelzylinder_Options>();

                        var results = new List<NGF>();

                        var opt = new List<Profil_Doppelzylinder_Options>();

                        for (int i = 0; i < dopelProduct.Count(); i++)
                        {
                            var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == dopelProduct[i].Id).GroupBy(x=>x.DoppelzylinderId).ToList();
                            foreach(var ts in options)
                            {
                                var optionItem = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == ts.Key).ToList();
                                for (int fs = 0; fs < optionItem.Count(); fs++)
                                {
                                    opt.Add(optionItem[fs]);
                                }
                            }
 
                        }
                        
                        var resultList = new List<NGF>();

                        var OptionsName = db.NGF.Select(x=>x).ToList();

                        var queryOrder = from t1 in OptionsName
                                         join t2 in opt
                                         on t1.OptionsId equals t2.Id
                                         select new
                                         {
                                             Id = t1.Id,
                                             Name = t1.Name,
                                             OptionsId = t1.OptionsId,
                                             Description = t1.Description,
                                             DoppelzylinderId = t2.DoppelzylinderId
                                         };


                        var detateils = queryOrder.Distinct().ToList();

                        var queryOrder2 = from t1 in detateils
                                          join t2 in allDopelOption
                                          on t1.Name equals t2.Options
                                          select new
                                          {
                                              Id = t1.Id,
                                              Name = t1.Name,
                                              OptionsId = t1.OptionsId,
                                              Description = t1.Description,
                                              DoppelzylinderId = t1.DoppelzylinderId
                                          };

                        var groupedByDoppelId = queryOrder2.GroupBy(x => x.DoppelzylinderId);

                        foreach (var group in groupedByDoppelId)
                        {
                            int countInGroup = group.Count();
                            int orderOptionCount = allDopelOption.Where(x => x.Options != null).Count();
                          
                            if (countInGroup == orderOptionCount)
                            
                            {
                                foreach (var item in group)
                                {
                                    var resultt = new NGF
                                    {
                                        Name = item.Name,
                                        OptionsId = item.OptionsId,
                                        Description =item.Description,
                                    };
                                    resultList.Add(resultt);
                                }
                                
                           }
                        }
                     

                       var NeOptionList = new List<Profil_Doppelzylinder_Options>();

                        foreach (var g in resultList)
                        {
                          var optX = opt.Where(x => x.Id == g.OptionsId).Distinct().ToList();
                            foreach(var mh in optX)
                            {
                                NeOptionList.Add(mh);
                            }
                            
                        }
                        foreach (var gf in NeOptionList)
                        {
                            var SortProduct = dopelProduct.Where(x => x.Id == gf.DoppelzylinderId).Distinct().ToList();
                            foreach (var j in SortProduct)
                            {
                                cheked.Add(j);
                            }
                        }

                    }
                    else
                    {
                        foreach(var g in dopelProduct.Distinct())
                        {
                            cheked.Add(g);
                        }
                        
                    }

                }

                var allOderKnayf = allUserListOrder.Where(x => x.ZylinderId == KnayfType).ToList();

                if (allOderKnayf.Count()>0)
                {

                    var maxInnenParameter = allOderKnayf.Max(x => x.innen);
                    var maxAussenParameter = allOderKnayf.Max(x => x.aussen);

                    var KnayfProduct = new List<Profil_Knaufzylinder>();

                    var products = await db.Aussen_Innen_Knauf.ToListAsync();

                    var item = products.Where(x => x.aussen >= maxInnenParameter & x.Intern >= maxAussenParameter).Select(x => x.Profil_KnaufzylinderId).Distinct().ToList();

                    var safeDoppelItem = new List<Profil_Knaufzylinder>();

                    for (int i = 0; i < item.Count(); i++)
                    {
                        var chekedItem = db.Profil_Knaufzylinder.Where(x => x.Id == item[i]).ToList();

                        for (int g = 0; g < chekedItem.Count(); g++)
                            safeDoppelItem.Add(chekedItem[g]);

                    }

                    for (int j = 0; j < safeDoppelItem.Distinct().Count(); j++)
                    {
                        KnayfProduct.Add(safeDoppelItem[j]);
                    }

                    var allDopelOptionKnayf = allUserListOrder.Where(x => x.ZylinderId == KnayfType).ToList();
                

                    var KnayfOption = from t1 in allUserListOrder
                                      join t2 in KnayfProduct
                                          on t1.ZylinderId equals t2.schliessanlagenId
                                          select new
                                          {
                                             Option = t1.Options
                                          };


                    var OptionKnayf = KnayfOption.Where(x=>x.Option != null).Count();


                if (OptionKnayf>0)
                {

                        var OptionsList = new List<Profil_Knaufzylinder_Options>();

                        var results = new List<Knayf_Options>();

                        var opt = new List<Profil_Knaufzylinder_Options>();

                        for (int i = 0; i < KnayfProduct.Count(); i++)
                        {
                            var options = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == KnayfProduct[i].Id).GroupBy(x => x.Profil_KnaufzylinderId).ToList();

                            foreach (var ts in options)
                            {
                                var optionItem = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == ts.Key).ToList();
                                
                                for (int fs = 0; fs < optionItem.Count(); fs++)
                                {
                                    opt.Add(optionItem[fs]);
                                }
                            }
                        }
                        var resultList = new List<Knayf_Options>();

                        var allDopelOption = allUserListOrder.Where(x => x.ZylinderId == KnayfType).ToList();

                        var OptionsName = db.Knayf_Options.Select(x => x).ToList();

                        var queryOrderKnayf = from t1 in OptionsName
                                         join t2 in opt
                                         on t1.OptionsId equals t2.Id
                                         select new
                                         {
                                             Id = t1.Id,
                                             Name = t1.Name,
                                             OptionsId = t1.OptionsId,
                                             Description = t1.Description,
                                             KnaufzylinderId = t2.Profil_KnaufzylinderId
                                         };


                        var detateilsKnayf = queryOrderKnayf.ToList();

                        var queryOrderKnayf2 = from t1 in detateilsKnayf
                                          join t2 in allDopelOptionKnayf
                                          on t1.Name equals t2.Options
                                          select new
                                          {
                                              Id = t1.Id,
                                              Name = t1.Name,
                                              OptionsId = t1.OptionsId,
                                              Description = t1.Description,
                                              KnaufzylinderId = t1.KnaufzylinderId
                                          };

                        var groupedByDoppelId = queryOrderKnayf2.GroupBy(x => x.KnaufzylinderId);

                        foreach (var group in groupedByDoppelId)
                        {
                            int countInGroup = group.Count();
                            int orderOptionCount = allDopelOption.Where(x => x.Options != null).Count();

                            if (countInGroup == orderOptionCount)

                            {
                                foreach (var itemKnayf in group)
                                {
                                    var resultt = new Knayf_Options
                                    {
                                        Name = itemKnayf.Name,
                                        OptionsId = itemKnayf.OptionsId,
                                        Description = itemKnayf.Description,
                                    };
                                    resultList.Add(resultt);
                                }

                            }
                        }

                        var NeOptionList = new List<Profil_Knaufzylinder_Options>();

                        foreach (var g in resultList)
                        {
                            var optX = opt.Where(x => x.Id == g.OptionsId).ToList();
                            foreach (var mh in optX)
                            {
                                NeOptionList.Add(mh);
                            }

                        }
                        foreach (var gf in NeOptionList)
                        {
                            var SortProduct = KnayfProduct.Where(x => x.Id == gf.Profil_KnaufzylinderId).ToList();
                            foreach (var j in SortProduct)
                            {
                                cheked2.Add(j);
                            }
                        }

                    }
                    else
                    {
                        foreach (var g in KnayfProduct)
                        {
                            cheked2.Add(g);
                        }

                    }
                }

            var allOderHalb = allUserListOrder.Where(x => x.ZylinderId == HalbType).ToList();
           
            
            if (allOderHalb.Count()>0)
            {
                var maxAussenParameter = allOderHalb.Max(x => x.aussen);

                var HalbProduct = new List<Profil_Halbzylinder>();

                var products = await db.Aussen_Innen_Halbzylinder.ToListAsync();

                var items = products.Where(x => x.aussen >= maxAussenParameter).Select(x => x.Profil_HalbzylinderId).Distinct().ToList();

                var safeDoppelItem = new List<Profil_Halbzylinder>();

                for (int i = 0; i < items.Count(); i++)
                {
                    var chekedItem = db.Profil_Halbzylinder.Where(x => x.Id == items[i]).ToList();

                    for (int g = 0; g < chekedItem.Count(); g++)
                        safeDoppelItem.Add(chekedItem[g]);
                }

                for (int j = 0; j < safeDoppelItem.Distinct().Count(); j++)
                {
                    HalbProduct.Add(safeDoppelItem[j]);
                }
               
                var allHalbOption = allUserListOrder.Where(x => x.ZylinderId == HalbType).ToList();
                
                var HalbOption = from t1 in allUserListOrder
                                  join t2 in HalbProduct
                                      on t1.ZylinderId equals t2.schliessanlagenId
                                  select new
                                  {
                                      Option = t1.Options
                                  };


                var OptionKnayf = HalbOption.Where(x => x.Option != null).Count();

                if (OptionKnayf>0)
                {
                    var OptionsList = new List<Profil_Halbzylinder_Options>();

                    var results = new List<Halbzylinder_Options>();

                    var opt = new List<Profil_Halbzylinder_Options>();

                    for (int i = 0; i < HalbProduct.Count(); i++)
                    {
                        var options = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == HalbProduct[i].Id).GroupBy(x => x.Profil_HalbzylinderId).ToList();

                        foreach (var ts in options)
                        {
                            var optionItem = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == ts.Key).ToList();

                            for (int fs = 0; fs < optionItem.Count(); fs++)
                            {
                                opt.Add(optionItem[fs]);
                            }
                        }
                    }

                    var resultList = new List<Halbzylinder_Options>();

                    var OptionsName = db.Halbzylinder_Options.Select(x => x).ToList();

                    var queryOrder = from t1 in OptionsName
                                     join t2 in opt
                                     on t1.OptionsId equals t2.Id
                                     select new
                                     {
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         OptionsId = t1.OptionsId,
                                         Description = t1.Description,
                                         DoppelzylinderId = t2.Profil_HalbzylinderId
                                     };


                    var detateils = queryOrder.Distinct().ToList();

                    var queryOrder2 = from t1 in detateils
                                      join t2 in allHalbOption
                                      on t1.Name equals t2.Options
                                      select new
                                      {
                                          Id = t1.Id,
                                          Name = t1.Name,
                                          OptionsId = t1.OptionsId,
                                          Description = t1.Description,
                                          DoppelzylinderId = t1.DoppelzylinderId
                                      };

                    var groupedByDoppelId = queryOrder2.GroupBy(x => x.DoppelzylinderId);

                    var NeOptionList = new List<Profil_Halbzylinder_Options>();

                    foreach (var group in groupedByDoppelId)
                    {
                        int countInGroup = group.Count();
                        int orderOptionCount = allHalbOption.Where(x => x.Options != null).Count();

                        if (countInGroup == orderOptionCount)

                        {
                            foreach (var item in group)
                            {
                                var resultt = new Halbzylinder_Options
                                {
                                    Name = item.Name,
                                    OptionsId = item.OptionsId,
                                    Description = item.Description,
                                };
                                resultList.Add(resultt);
                            }

                        }
                    }

                    foreach (var g in resultList)
                    {
                        var optX = opt.Where(x => x.Id == g.OptionsId).ToList();
                        foreach (var mh in optX)
                        {
                            NeOptionList.Add(mh);
                        }

                    }
                    foreach (var gf in NeOptionList)
                    {
                        var SortProduct = HalbProduct.Where(x => x.Id == gf.Profil_HalbzylinderId).ToList();
                        foreach (var j in SortProduct)
                        {
                            cheked3.Add(j);
                        }
                    }

                }
                else
                {
                    foreach (var g in HalbProduct)
                    {
                        cheked3.Add(g);
                    }

                }

            }
            var allOderHebel = allUserListOrder.Where(x => x.ZylinderId == HebelType).ToList();

            if (allOderHebel.Count() > 0)
            {
                var allHebelOption = allUserListOrder.Where(x => x.ZylinderId == HebelType).ToList();
                
                var HebelOption = from t1 in allUserListOrder
                                  join t2 in hebel
                                  on t1.ZylinderId equals t2.schliessanlagenId
                                  select new
                                  {
                                      Option = t1.Options
                                  };


                var OptionHebel = HebelOption.Where(x => x.Option != null).Count();

                if (OptionHebel>0)
                {
                    var OptionsList = new List<Hebelzylinder_Options>();

                    var results = new List<Options>();

                    var opt = new List<Hebelzylinder_Options>();

                    for (int i = 0; i < hebel.Count(); i++)
                    {
                        var options = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel[i].Id).GroupBy(x => x.HebelzylinderId).ToList();
                        
                        foreach (var ts in options)
                        {
                            var optionItem = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == ts.Key).ToList();
                            for (int fs = 0; fs < optionItem.Count(); fs++)
                            {
                                opt.Add(optionItem[fs]);
                            }
                        }
                    }
                    var resultList = new List<Options>();

                    var OptionsName = db.Options.Select(x => x).ToList();

                    var queryOrder = from t1 in OptionsName
                                     join t2 in opt
                                     on t1.OptionId equals t2.Id
                                     select new
                                     {
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         OptionsId = t1.OptionId,
                                         Description = t1.Description,
                                         DoppelzylinderId = t2.HebelzylinderId
                                     };


                    var detateils = queryOrder.Distinct().ToList();

                    var queryOrder2 = from t1 in detateils
                                      join t2 in allHebelOption
                                      on t1.Name equals t2.Options
                                      select new
                                      {
                                          Id = t1.Id,
                                          Name = t1.Name,
                                          OptionsId = t1.OptionsId,
                                          Description = t1.Description,
                                          DoppelzylinderId = t1.DoppelzylinderId
                                      };

                    var groupedByDoppelId = queryOrder2.GroupBy(x => x.DoppelzylinderId);

                    foreach (var group in groupedByDoppelId)
                    {
                        int countInGroup = group.Count();

                        int orderOptionCount = allHebelOption.Where(x => x.Options != null).Count();

                        if (countInGroup == orderOptionCount)

                        {
                            foreach (var item in group)
                            {
                                var resultt = new Options
                                {
                                    Name = item.Name,
                                    OptionId = item.OptionsId,
                                    Description = item.Description,
                                };
                                resultList.Add(resultt);
                            }

                        }
                    }

                    var NeOptionList = new List<Hebelzylinder_Options>();

                    foreach (var g in resultList)
                    {
                        var optX = opt.Where(x => x.Id == g.OptionId).ToList();
                        foreach (var mh in optX)
                        {
                            NeOptionList.Add(mh);
                        }

                    }
                    foreach (var gf in NeOptionList)
                    {
                        var SortProduct = hebel.Where(x => x.Id == gf.HebelzylinderId).ToList();
                        foreach (var j in SortProduct)
                        {
                            cheked4.Add(j);
                        }
                    }

                }
                else
                {
                    foreach (var g in hebel)
                    {
                        cheked4.Add(g);
                    }

                }

            }

            var allOderVorhan = allUserListOrder.Where(x => x.ZylinderId == VorhanType).ToList();
            
            var VorhangProduct = new List<Vorhangschloss>();

            if (allOderVorhan.Count() > 0)
            {
                var maxAussenParameter = allOderVorhan.Max(x => x.aussen);

                var products = await db.Size.ToListAsync();

                var items = products.Where(x =>x.sizeVorhangschloss >= maxAussenParameter).Select(x => x.VorhangschlossId).Distinct().ToList();

                var safeDoppelItem = new List<Vorhangschloss>();

                for (int i = 0; i < items.Count(); i++)
                {
                    var chekedItem = db.Vorhangschloss.Where(x => x.Id == items[i]).ToList();

                    for (int g = 0; g < chekedItem.Count(); g++)
                        safeDoppelItem.Add(chekedItem[g]);

                }

                for (int j = 0; j < safeDoppelItem.Count(); j++)
                {
                    VorhangProduct.Add(safeDoppelItem[j]);
                }


                var allDopelOption = allUserListOrder.Where(x => x.ZylinderId == dopelType).ToList();

                var VorhanOption = from t1 in allUserListOrder
                                  join t2 in VorhangProduct
                                      on t1.ZylinderId equals t2.schliessanlagenId
                                  select new
                                  {
                                      Option = t1.Options
                                  };


                var OptionKnayf = VorhanOption.Where(x => x.Option != null).Count();

                if (OptionKnayf > 0)
                {
                    var OptionsList = new List<Vorhan_Options>();

                    var results = new List<OptionsVorhan>();

                    var opt = new List<Vorhan_Options>();

                    for (int i = 0; i < VorhangProduct.Count(); i++)
                    {
                        var options = db.Vorhan_Options.Where(x => x.VorhangschlossId == VorhangProduct[i].Id).GroupBy(x => x.VorhangschlossId).ToList();

                        foreach (var ts in options)
                        {
                            var optionItem = db.Vorhan_Options.Where(x => x.VorhangschlossId == ts.Key).ToList();
                            for (int fs = 0; fs < optionItem.Count(); fs++)
                            {
                                opt.Add(optionItem[fs]);
                            }
                        }

                    }

                    var resultList = new List<OptionsVorhan>();



                    var OptionsName = db.OptionsVorhan.Select(x => x).ToList();

                    var queryOrder = from t1 in OptionsName
                                     join t2 in opt
                                     on t1.OptioId equals t2.Id
                                     select new
                                     {
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         OptionsId = t1.OptioId,
                                         Description = t1.Description,
                                         DoppelzylinderId = t2.VorhangschlossId
                                     };


                    var detateils = queryOrder.Distinct().ToList();

                    var queryOrder2 = from t1 in detateils
                                      join t2 in allDopelOption
                                      on t1.Name equals t2.Options
                                      select new
                                      {
                                          Id = t1.Id,
                                          Name = t1.Name,
                                          OptionsId = t1.OptionsId,
                                          Description = t1.Description,
                                          DoppelzylinderId = t1.DoppelzylinderId
                                      };

                    var groupedByDoppelId = queryOrder2.GroupBy(x => x.DoppelzylinderId);

                    foreach (var group in groupedByDoppelId)
                    {
                        int countInGroup = group.Count();
                        int orderOptionCount = allDopelOption.Where(x => x.Options != null).Count();

                        if (countInGroup == orderOptionCount)

                        {
                            foreach (var item in group)
                            {
                                var resultt = new OptionsVorhan
                                {
                                    Name = item.Name,
                                    OptioId = item.OptionsId,
                                    Description = item.Description,
                                };
                                resultList.Add(resultt);
                            }

                        }
                    }


                    var NeOptionList = new List<Vorhan_Options>();

                    foreach (var g in resultList)
                    {
                        var optX = opt.Where(x => x.Id == g.OptioId).Distinct().ToList();
                        foreach (var mh in optX)
                        {
                            NeOptionList.Add(mh);
                        }

                    }
                    foreach (var gf in NeOptionList)
                    {
                        var SortProduct = VorhangProduct.Where(x => x.Id == gf.VorhangschlossId).Distinct().ToList();
                        foreach (var j in SortProduct)
                        {
                            cheked5.Add(j);
                        }
                    }
                }
                else
                {
                    foreach (var g in VorhangProduct.Distinct())
                    {
                        cheked5.Add(g);
                    }

                }

            }
           


            var allOderAussen = allUserListOrder.Where(x => x.ZylinderId == AussenType).ToList();

            if (allOderAussen.Count() > 0)
            {
                var allAussenOption = allUserListOrder.Where(x => x.ZylinderId == AussenType).ToList();

                var boolOptionAussen = allUserListOrder.FirstOrDefault(x => x.ZylinderId == AussenType);

                if (boolOptionAussen != null)
                {
                    var OptionsList = new List<Aussen_Rund_options>();

                    var results = new List<Aussen_Rund_all>();

                    var opt = new List<Aussen_Rund_options>();

                    for (int i = 0; i < Aussenzylinder.Count(); i++)
                    {
                        var options = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == hebel[i].Id).GroupBy(x => x.Aussenzylinder_RundzylinderId).ToList();

                        foreach (var ts in options)
                        {
                            var optionItem = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == ts.Key).ToList();
                            
                            for (int fs = 0; fs < optionItem.Count(); fs++)
                            {
                                opt.Add(optionItem[fs]);
                            }
                        }

                    }
                    var resultList = new List<Aussen_Rund_all>();

                    var OptionsName = db.Aussen_Rund_all.Select(x => x).ToList();

                    var queryOrder = from t1 in OptionsName
                                     join t2 in opt
                                     on t1.Aussen_Rund_optionsId equals t2.Id
                                     select new
                                     {
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         OptionsId = t1.Aussen_Rund_optionsId,
                                         Description = t1.Description,
                                         DoppelzylinderId = t2.Aussenzylinder_RundzylinderId
                                     };


                    var detateils = queryOrder.Distinct().ToList();

                    var queryOrder2 = from t1 in detateils
                                      join t2 in allAussenOption
                                      on t1.Name equals t2.Options
                                      select new
                                      {
                                          Id = t1.Id,
                                          Name = t1.Name,
                                          OptionsId = t1.OptionsId,
                                          Description = t1.Description,
                                          DoppelzylinderId = t1.DoppelzylinderId
                                      };

                    var groupedByDoppelId = queryOrder2.GroupBy(x => x.DoppelzylinderId);

                    foreach (var group in groupedByDoppelId)
                    {
                        int countInGroup = group.Count();

                        int orderOptionCount = allAussenOption.Where(x => x.Options != null).Count();

                        if (countInGroup == orderOptionCount)

                        {
                            foreach (var item in group)
                            {
                                var resultt = new Aussen_Rund_all
                                {
                                    Name = item.Name,
                                    Aussen_Rund_optionsId = item.OptionsId,
                                    Description = item.Description,
                                };
                                resultList.Add(resultt);
                            }

                        }
                    }
                    var NeOptionList = new List<Aussen_Rund_options>();

                    foreach (var g in resultList)
                    {
                        var optX = opt.Where(x => x.Id == g.Aussen_Rund_optionsId).ToList();

                        foreach (var mh in optX)
                        {
                            NeOptionList.Add(mh);
                        }

                    }
                    foreach (var gf in NeOptionList)
                    {
                        var SortProduct = Aussenzylinder.Where(x => x.Id == gf.Aussenzylinder_RundzylinderId).ToList();
                        foreach (var j in SortProduct)
                        {
                            cheked6.Add(j);
                        }
                    }
                }
                else
                {
                    foreach (var g in Aussenzylinder)
                    {
                        cheked6.Add(g);
                    }

                }


            }

            if (cheked.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in allUserListOrder
                                 on t1.schliessanlagenId equals t2.ZylinderId
                                 select new
                                 {
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var ListOrder = queryOrder.Distinct().ToList();
                ViewBag.Doppel = ListOrder.ToList();

            }

            if (cheked2.Count() > 0)
            {
                var queryOrder = from t1 in cheked2
                                 join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                 select new
                                 {
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Knaufzylinder = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Knaufzylinder;
            }
            if (cheked3.Count() > 0)
            {
                var query = from t1 in cheked3
                            join t2 in allUserListOrder
                            on t1.schliessanlagenId equals t2.ZylinderId
                            select new
                            {
                                Id = t1.Id,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t1.description,
                                NameSystem = t1.NameSystem,
                                Cost = t1.Cost,
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.Halb = rl.ToList();
            }
            if (cheked4.Count() > 0)
            {
                var query = from t1 in cheked4
                            join t2 in allUserListOrder
                            on t1.schliessanlagenId equals t2.ZylinderId
                            select new
                            {
                                Id = t1.Id,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t1.description,
                                NameSystem = t1.NameSystem,
                                Cost = t1.Cost,
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.Hebel = rl.ToList();
            }
            if (cheked5.Count() > 0)
            {
                var query = from t1 in cheked5
                            join t2 in allUserListOrder
                            on t1.schliessanlagenId equals t2.ZylinderId
                            select new
                            {
                                Id = t1.Id,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t1.description,
                                NameSystem = t1.NameSystem,
                                Cost = t1.Cost,
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.VorhanSchloss = rl.ToList();
            }
            if (cheked6.Count() > 0)
            {
                var query = from t1 in cheked5
                            join t2 in allUserListOrder
                            on t1.schliessanlagenId equals t2.ZylinderId
                            select new
                            {
                                Id = t1.Id,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t1.description,
                                NameSystem = t1.NameSystem,
                                Cost = t1.Cost,
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.Aussen = rl.ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked2
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join;
                ViewBag.Knaufzylinder = Join;
            }

            if (cheked.Count() > 0 && cheked3.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked3
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked3 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();
                ViewBag.a = Join;
                ViewBag.DH = Join;
            }
            if (cheked.Count() > 0 && cheked4.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked4
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked4 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();
                ViewBag.a = Join;
                ViewBag.DHE = Join;
            }
            if (cheked.Count() > 0 && cheked5.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked5
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked5 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();
                ViewBag.a = Join;
                ViewBag.DV = Join;
            }

            if (cheked.Count() > 0 && cheked6.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked6
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked6 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();
                ViewBag.a = Join;
                ViewBag.DA = Join;
            }

            if (cheked2.Count() > 0 && cheked3.Count() > 0)
            {
                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3
                                 on t1.NameSystem equals t2.NameSystem
                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.b = Join;
                ViewBag.KH = Join;
            }

            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     cheked3 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost + t3.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
                ViewBag.Knaufzylinder = "";
                ViewBag.Halb = "";
               
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0)
            {
                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem


                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost + t3.Cost + t4.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();


                ViewBag.a = Join;
                ViewBag.b = Join;
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0)
            {


                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked5 on t4.NameSystem equals t5.NameSystem

                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = t4.Id,
                                     cheked5 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost + t3.Cost + t4.Cost + t5.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();


                ViewBag.a = Join;
                ViewBag.b = Join;
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0)
            {
                var queryOrder = from t1 in cheked.Distinct()
                                 join t2 in cheked2.Distinct() on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3.Distinct() on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4.Distinct() on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked5.Distinct() on t4.NameSystem equals t5.NameSystem
                                 join t6 in cheked6.Distinct() on t5.NameSystem equals t6.NameSystem
                                 select new
                                 {
                                     cheked2 = t2.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = t4.Id,
                                     cheked5 = t5.Id,
                                     cheked6 = t6.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t1.description,
                                     NameSystem = t1.NameSystem,
                                     Cost = t1.Cost + t2.Cost + t3.Cost + t4.Cost + t5.Cost + t6.Cost,
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();


                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
                ViewBag.Knaufzylinder = "";
                ViewBag.Halb = "";
                ViewBag.Hebel = "";
                ViewBag.VorhanSchloss = "";
                ViewBag.Aussen = "";
            }

            return View("System_Auswählen", keyUser);
        }
       
        [HttpGet]
        public async Task<IActionResult> OrdersKey(int DopelId, List<string> dopelOption, string param2,int KnayfID, int Halb,int Hebel,int Aussen,int Vorhan,string userInfo)
        {

            char[] separators = { ' ', '\n', '\t', '\r' };

            if (userInfo != null)
            {
                string[] words = userInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var UserLogin = db.User.Where(x => x.Name == words[0] & x.Sername == words[1]).ToList();

                var UserName_UserSername = UserLogin.Select(x=>x.Name).First() + " " + UserLogin.Select(x => x.Sername).First();

                ViewBag.UserInformStatus = UserLogin.Select(x=>x.Status).First();
                ViewBag.UserId = UserLogin.Select(x => x.Id).First();
                ViewBag.UserNameItem = UserName_UserSername; 
            }

            var key = await db.Orders.Where(x => x.userKey == param2).Distinct().ToListAsync();

            var DopelOrderlist = new List<Profil_Doppelzylinder>();

            var OrderList = await db.Profil_Doppelzylinder.Where(x => x.Id == Convert.ToInt32(DopelId)).ToListAsync();

            var AussenInen = await db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == Convert.ToInt32(DopelId)).ToListAsync();

            var Halbzylinder = new List<Profil_Halbzylinder>();

            var SelectHalbzylinder = await db.Profil_Halbzylinder.Where(x => x.Id == Halb).ToListAsync();

            var halbAussen_Inter = await db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halb).ToListAsync();

            ViewBag.AussenHalb = halbAussen_Inter.Select(x=>x.aussen).ToList();

            var KnaufZelinder = await db.Profil_Knaufzylinder.Where(x => x.Id == KnayfID).ToListAsync();

            var Kanyf_AussenInen = await db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == Convert.ToInt32(KnayfID)).ToListAsync();



            var Vorhanschlos = new List<Vorhangschloss>();

            var SelectVorhanschlos = await db.Vorhangschloss.Where(x => x.Id == Vorhan).ToListAsync();

            var SizeVorhanschloss = await db.Size.Where(x=>x.VorhangschlossId == Vorhan).Select(x=>x.sizeVorhangschloss).ToListAsync();

           

            var listVorHanOptions = new List<Vorhan_Options>();

            foreach (var list in SelectVorhanschlos)
            {
                var VorhanOptions = await db.Vorhan_Options.Where(x => x.VorhangschlossId == list.Id).ToListAsync();
                
                foreach(var s in VorhanOptions)
                {
                    listVorHanOptions.Add(s);
                }
            }

            ViewBag.VorhanschlossCount = listVorHanOptions.Count();
           

            var listVorHanOptionsValueName = new List<OptionsVorhan>();

            foreach (var ls in listVorHanOptions)
            {
                var listOptionVorhanValue = await db.OptionsVorhan.Where(x => x.OptioId == ls.Id).ToListAsync();
                foreach (var lst in listOptionVorhanValue)
                {
                    listVorHanOptionsValueName.Add(lst);
                }
            }
            ViewBag.VorhanschlossOptionName = listVorHanOptionsValueName.Select(x => x.Name).ToList();

            var VorhanOptionValue = new List<OptionsVorhan_value>();
            
            foreach (var ls in listVorHanOptionsValueName)
            {
                var listOptionVorhanInfoValue = await db.OptionsVorhan_value.Where(x => x.OptionsId == ls.Id).ToListAsync();
                
                foreach (var lst in listOptionVorhanInfoValue)
                {
                    VorhanOptionValue.Add(lst);
                }
            }
            ViewBag.VorhanOptionCount = VorhanOptionValue.Count();
            ViewBag.VorhanValue = VorhanOptionValue.Select(x=>x.Value).ToList();
            ViewBag.VorhanSize = SizeVorhanschloss;

            float KhaufAussenCost = 0f;
            float DoppelAussenCost = 0f;
            float halbAussenCost = 0f;
            float VorhangschlossCost = 0f;

            var Aussenzylinder = new List<Aussenzylinder_Rundzylinder>();

            var SelectAussenzylinder = await db.Aussenzylinder_Rundzylinder.Where(x => x.Id == Aussen).ToListAsync();

            var AussenOption = new List<Aussen_Rund_options>();

            foreach(var x in SelectAussenzylinder)
            {
                var listOptionsAussenZylinder = await db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == x.Id).ToListAsync();
                
                foreach(var f in listOptionsAussenZylinder)
                {
                    AussenOption.Add(f);
                }
                ViewBag.AussenCountOption = listOptionsAussenZylinder.Count();

            }

            var AussenListRundAll = new List<Aussen_Rund_all>();
            
            foreach(var ls in AussenOption)
            {
                var list = await db.Aussen_Rund_all.Where(x=>x.Aussen_Rund_optionsId == ls.Id).ToListAsync();
                foreach(var l in list)
                {
                    AussenListRundAll.Add(l);
                }
            }

            ViewBag.AussenName = AussenListRundAll.Select(x => x.Name).ToList();

            var AussenListvalue = new List<Aussen_Rouns_all_value>();

            foreach(var listValueAussen in AussenListRundAll)
            {
                var valueList = await db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == listValueAussen.Id).ToListAsync();

                foreach(var f in valueList)
                {
                    AussenListvalue.Add(f);
                }
            }

            ViewBag.AussenValue = AussenListvalue.Select(x=>x.Value).ToList();

            var HelbZ = new List<Hebel>();
            var HebelZylinder = await db.Hebelzylinder.Where(x=>x.Id == Hebel).ToListAsync();
            var HebelOption = new List<Hebelzylinder_Options>();

            foreach (var list in HebelZylinder)
            {
                var HebelOptions = await db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == list.Id).ToListAsync();
                
                foreach (var Optionslist in HebelOptions)
                {
                    HebelOption.Add(Optionslist);
                }
               
            }

            ViewBag.CountOptionsHebel = HebelOption.Count();

            var HebelOptionListAll = new List<Options>();

            foreach (var list in HebelOption)
            {
                var HebelOptionsList = await db.Options.Where(x => x.OptionId == list.Id).ToListAsync();
                
                foreach (var Optionslist in HebelOptionsList)
                {
                    HebelOptionListAll.Add(Optionslist);
                }

            }

            ViewBag.OptionHebelName = HebelOptionListAll.Select(x => x.Name).Distinct().ToList();

            var HebelOptionValueList = new List<Options_value>();

            foreach (var listValue in HebelOptionListAll)
            {
                var list = await db.Options_value.Where(x => x.OptionsId == listValue.Id).ToListAsync();
                foreach(var l in list)
                {
                    HebelOptionValueList.Add(l);
                }
            }

            var listAllValueHebel = new List<int>();

           listAllValueHebel.Add(HebelOptionValueList.Count());

            ViewBag.CountValueHebel = listAllValueHebel;
            ViewBag.ValueHebel = HebelOptionValueList.Select(x=>x.Value).Distinct().ToList();

            var queryableOptionsHalb = await db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Convert.ToInt32(Halb)).Select(x => x.Id).ToListAsync();

            var OptionsHalb = new List<Halbzylinder_Options>();
            
                for(int f =0;f< queryableOptionsHalb.Count(); f++)
                {
                    var optionsHabel = await db.Halbzylinder_Options.Where(x => x.OptionsId == queryableOptionsHalb[f]).ToListAsync();
                   
                    foreach(var listHalb in optionsHabel)
                    {
                        OptionsHalb.Add(listHalb);
                    }
                }
            var OptionsValueHalb = new List<Halbzylinder_Options_value>();
            
            for(int t=0;t<OptionsHalb.Count(); t++)
            {
                var listValueOptionsHalb = await db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == OptionsHalb[t].Id).ToListAsync();
                foreach (var listvalue in listValueOptionsHalb)
                    OptionsValueHalb.Add(listvalue);
            }

            var listCountHalb = new List<int>();
            foreach (var f in OptionsHalb)
                listCountHalb.Add(f.Halbzylinder_Options_value.Count());  

            ViewBag.countOptionsQueryHalb = queryableOptionsHalb.Count();
            ViewBag.HalbOptionsName = OptionsHalb.Select(x=>x.Name).ToList();
   
            ViewBag.HalbOptionsValue = OptionsValueHalb.Select(x=>x.Value).ToList();
            ViewBag.HalbOptionsValueCount = listCountHalb.ToList();

            var KnayfOrderlist = new List<Profil_Knaufzylinder>();

            for (var i = 0; i < key.Count(); i++)
            {
                if (OrderList.Count() > 0)
                {
                    if (key[i].ZylinderId == OrderList.Select(x=>x.schliessanlagenId).First())
                        DopelOrderlist.Add(OrderList.Last());
                }
               
                if (KnaufZelinder.Count() > 0)
                {
                    if (key[i].ZylinderId == KnaufZelinder.Select(x => x.schliessanlagenId).First())
                        KnayfOrderlist.Add(KnaufZelinder.Last());
                }
                if (SelectHalbzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectHalbzylinder.Select(x => x.schliessanlagenId).First())
                        Halbzylinder.Add(SelectHalbzylinder.Last());
                }
               
                if(HebelZylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == HebelZylinder.Select(x => x.schliessanlagenId).First())
                        HelbZ.Add(HebelZylinder.Last());
                }
                if (SelectVorhanschlos.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectVorhanschlos.Select(x => x.schliessanlagenId).First())
                        Vorhanschlos.Add(SelectVorhanschlos.Last());
                }

                if (SelectAussenzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectAussenzylinder.Select(x => x.schliessanlagenId).First())
                        Aussenzylinder.Add(SelectAussenzylinder.Last());
                }


            }

            var queryableOptions = await db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Convert.ToInt32(DopelId)).Select(x => x.Id).ToListAsync();
            ViewBag.countOptionsQuery = queryableOptions.Count();
            
            if (queryableOptions.Count() > 0)
            {
                List<NGF> ngf = new List<NGF>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = await db.NGF.Where(x => x.OptionsId == queryableOptions[z]).ToListAsync();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = dopelOption.ToList();

                List<NGF_Value> ngfList = new List<NGF_Value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = await db.NGF_Value.Where(x => x.NGFId == ngf[s].Id).ToListAsync();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                foreach (var order in key)
                {
                    var optionDopel = ngf.Where(x => x.Name == order.Options).ToList();
                    foreach(var option in optionDopel)
                    {
                        var CostOption = ngfList.Where(x => x.NGFId == option.Id).Select(x => x.Cost).Sum();
                        
                        DoppelAussenCost = DoppelAussenCost + Convert.ToInt32(CostOption);

                    }
                }
                    var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.NGF_Value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }

            var queryableOptionsKnayf = await db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == Convert.ToInt32(KnayfID)).Select(x => x.Id).ToListAsync();
            ViewBag.countOptionsQueryKnayf = queryableOptionsKnayf.Count();
            if (queryableOptionsKnayf.Count() > 0)
            {

                List<Knayf_Options> ngf = new List<Knayf_Options>();

                for (int z = 0; z < queryableOptionsKnayf.Count(); z++)
                {
                    var allOptions = db.Knayf_Options.Where(x => x.OptionsId == queryableOptionsKnayf[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsNameKnayf = ngf.Select(x => x.Name).ToList();

                List<Knayf_Options_value> ngfList = new List<Knayf_Options_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = await db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == ngf[s].Id).ToListAsync();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCountKnayf = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Knayf_Options_value.Count());
                }


                ViewBag.countOptionsListKnayf = list;

                ViewBag.optionsValueKnayf = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }

            var keyOpenOrder = new List<isOpen_Order>();

            foreach (var order in key)
            {
                var isOpen = await db.isOpen_Order.Where(x => x.OrdersId == order.id).ToListAsync();

                foreach (var list in isOpen)
                    keyOpenOrder.Add(list);
            }
            var IsOpenValue = new List<isOpen_value>();

            foreach (var order in keyOpenOrder)
            {
                var opens = await db.isOpen_value.Where(x => x.isOpen_OrderId == order.Id).ToListAsync();
                foreach (var cheked in opens)
                    IsOpenValue.Add(cheked);
            }
            var ValueKeyOpen = new List<KeyValue>();

            foreach (var tl in IsOpenValue)
            {
                var listValueOpen = await db.KeyValue.Where(x => x.OpenKeyId == tl.Id).OrderBy(x=>x.OpenKeyId).ToListAsync();
                var id = listValueOpen.Select(x => x.OpenKeyId).ToList();
                foreach (var tlr in listValueOpen)
                    ValueKeyOpen.Add(tlr);
            }
            ViewBag.Order = IsOpenValue.Distinct().ToList();

            foreach (var order in key)
            {
                if (order.ZylinderId == 1)
                {
                  
                    for (var i = 30; i < order.aussen;)
                    {
                        DoppelAussenCost = DoppelAussenCost + 5;
                        i = i + 5;

                    }
                    for (var j = 30; j < order.innen;)
                    {
                        DoppelAussenCost = DoppelAussenCost + 5;
                        j = j + 5;

                    }

                }
                if (order.ZylinderId == 2)
                {
                    ViewBag.HalbAussen = order.aussen;

                    for (var i = 30; i < order.aussen;)
                    {
                        halbAussenCost = halbAussenCost + 5;
                        i = i + 5;
                    }

                }
                if (order.ZylinderId == 3)
                {

                    for (var i = 30; i < order.aussen;)
                    {
                        KhaufAussenCost = KhaufAussenCost + 5;
                        i = i + 5;

                    }
                    for (var j = 30; j < order.innen;)
                    {
                        KhaufAussenCost = KhaufAussenCost + 5;
                        j = j + 5;

                    }
                }
                if (order.ZylinderId == 5)
                {
                    ViewBag.Vorhangschloss = order.aussen;

                    var NameSysteam = SelectVorhanschlos.Select(x => x.NameSystem).ToList();
                    foreach(var list in NameSysteam)
                    {
                        if (list == "Vitess.4000")
                        {
                            if (order.aussen == 24)
                            {
                                VorhangschlossCost = 0;
                            }
                            if (order.aussen == 48)
                            {
                                VorhangschlossCost = 7;
                            }
                            if (order.aussen == 64)
                            {
                                VorhangschlossCost = 30;
                            }
                            if (order.aussen == 80)
                            {
                                VorhangschlossCost = 45;
                            }
                        }
                        if (list == "Bravus.2000")
                        {
                            if (order.aussen == 24)
                            {
                                VorhangschlossCost = 0;
                            }
                            if (order.aussen == 48)
                            {
                                VorhangschlossCost = 7;
                            }
                            if (order.aussen == 64)
                            {
                                VorhangschlossCost = 30;
                            }
                            if (order.aussen == 80)
                            {
                                VorhangschlossCost = 44;
                            }
                        }
                        if (list == "Zolit.1000")
                        {
                            if (order.aussen == 24)
                            {
                                VorhangschlossCost = 0;
                            }
                            if (order.aussen == 48)
                            {
                                VorhangschlossCost = 26.5f;
                            }
                            if (order.aussen == 64)
                            {
                                VorhangschlossCost = 26.5f;
                            }
                            if (order.aussen == 80)
                            {
                                VorhangschlossCost = 44.7f;
                            }
                        }
                    }
                   
                }


            }

            var HablAussen = await db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Convert.ToInt32(Halb)).Select(x=>x.aussen).ToListAsync();

            ViewBag.SelectHalb = key.Where(x => x.ZylinderId == 2).Select(x => x.aussen).Distinct().ToList();

            ViewBag.Halb = Halbzylinder.ToList();
            ViewBag.HalbItem = Halbzylinder.Select(x => x.Id).ToList();
            ViewBag.HalbAussenList = HablAussen.Distinct().ToList();
            ViewBag.HalbOrderAussen = key.Where(x => x.ZylinderId == 2).Select(x => x.aussen).ToList();

            ViewBag.KnayfZelinder = KnayfOrderlist.ToList();
            ViewBag.KnayfItemId = KnayfOrderlist.Select(x => x.Id).ToList();
            ViewBag.KnayfZelinderAussen = Kanyf_AussenInen.Select(x => x.aussen).Distinct().ToList();
            ViewBag.KnayfZelinderIntern = Kanyf_AussenInen.Select(x => x.Intern).Distinct().ToList();
            ViewBag.KAussen = key.Where(x => x.ZylinderId == 3).Select(x => x.aussen).ToList();
            ViewBag.KInter = key.Where(x => x.ZylinderId == 3).Select(x => x.innen).ToList();

            ViewBag.DopelzylinderIdList = DopelOrderlist.Select(x => x.Id).ToList();
            ViewBag.Dopelzylinderaussen = AussenInen.Select(x => x.aussen).ToList();
            ViewBag.DopelzylinderIntern = AussenInen.Select(x => x.Intern).ToList();
            ViewBag.Dopelzylinder = DopelOrderlist.ToList();
            ViewBag.DAussen = key.Where(x => x.ZylinderId == 1).Select(x => x.aussen).ToList();
            ViewBag.DInter = key.Where(x => x.ZylinderId == 1).Select(x => x.innen).ToList();

            ViewBag.HelbZ = HelbZ.ToList();
            ViewBag.HalbItem = HelbZ.Select(x => x.Id).ToList();

            ViewBag.Vorhanschlos = Vorhanschlos.ToList();
            ViewBag.VorhanschlosItem = Vorhanschlos.Select(x=>x.Id).ToList();
            ViewBag.VorhanOrderAussen = key.Where(x => x.ZylinderId == 5).Select(x => x.aussen).ToList();

            ViewBag.Aussenzylinder = Aussenzylinder.ToList();
            ViewBag.AussenzylinderItem = Aussenzylinder.Select(x=>x.Id).ToList();


            ViewBag.KeyValue = ValueKeyOpen.Select(x=>x.isOpen).ToList();
            ViewBag.DorName = key.Select(x => x.DorName).Distinct().ToList();
            ViewBag.User = key.Select(x => x.userKey).Distinct().ToList();
            
            var SumCost = DopelOrderlist.Select(x => x.Cost).Sum() + KnaufZelinder.Select(x => x.Cost).Sum() + Halbzylinder.Select(x => x.Cost).Sum() +
                HelbZ.Select(x => x.Cost).Sum() + Vorhanschlos.Select(x => x.Cost).Sum() + Aussenzylinder.Select(x => x.Cost).Sum() + DoppelAussenCost
                + KhaufAussenCost + halbAussenCost + VorhangschlossCost;

            int precision = 2; // количество знаков после запятой

            double Costed = Math.Round(SumCost, precision);

            ViewBag.Cost = Costed;

            return View("Finisher", key.Last() );
        }
        
        [HttpPost]
        public ActionResult RemoveOrder(int data,int UserId)
        {
            var RemoveOrder = db.UserOrdersShop.Where(x=>x.Id == data).ToList();
            
            var OrderProduct = db.ProductSysteam.Where(x=>x.UserOrdersShopId ==  data).ToList();
            
            foreach (var listProduct in OrderProduct)
            {
                db.ProductSysteam.Remove(listProduct);
               
                foreach (var listOrder in RemoveOrder)
                {
                    db.UserOrdersShop.Remove(listOrder);
                   
                }
               
            }

            db.SaveChanges();

            var UserLogin = db.User.FirstOrDefault(x => x.Id == UserId);
            return RedirectToAction("AdminConnect", UserLogin);
        }

        [HttpGet]
        public ActionResult SaveUserOrders(string userInfo,List<string> nameKey, List<string> TurName,List<string> DopelName,List<float> DoppelAussen,List<float> DoppelIntern
            , List<string> DoppelOption, List<string> KnayfName, List<float> KnayfAussen, List<float> KnayfIntern, List<string> HalbName, List<float> HalbAussen, List<string> HelbName,
           List<string> VorhanName, List<float> VorhanAussen, List<string> AussenName,float cost,List<string> key, List<bool> keyIsOpen,List<int> countKey)
        {
            char[] separators = { ' ', '\n', '\t', '\r' };

            var Zylinder_Typ =  db.Schliessanlagen.ToList();
            var profilD = db.Profil_Doppelzylinder.ToList();
            var profilH = db.Profil_Halbzylinder.ToList();
            var profilK = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var Vorhangschloss = db.Vorhangschloss.ToList();
            var Aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();
            var Orders =  db.Orders.ToList();

            if (userInfo == null)
            {
                TempData["AlertMessage"] = "Bitte melden Sie sich an!";
            }
            else
            {
                string[] words = userInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var UserLogin = db.User.FirstOrDefault(x => x.Name == words[0] & x.Sername == words[1]);

                using (FileStream fstream = new FileStream(@$"wwwroot/Orders/{UserLogin.Name + " " + UserLogin.Sername} OrderFile.xlsx", FileMode.OpenOrCreate))
                {
                    fstream.Close();
                }

                string sourceFilePath = @"wwwroot/Orders/CES_schliessplan_DE_schliessanlagen.xltx";

                string destinationFilePath = @$"wwwroot/Orders/{UserLogin.Name + " " + UserLogin.Sername} OrderFile.xlsx";

                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))

                using (BinaryReader reader = new BinaryReader(sourceFileStream))
                {
                    using (FileStream destinationFileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    using (BinaryWriter writer = new BinaryWriter(destinationFileStream))
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            writer.Write(buffer, 0, bytesRead);

                        }
                        writer.Close();
                    }
                    reader.Close();
                    sourceFileStream.Close();
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                FileInfo fileInfo = new FileInfo(destinationFilePath);


                var UserOrder = new UserOrdersShop
                {
                    UserId = UserLogin.Id,
                    ProductName = DopelName.First(),
                    OrderSum = cost
                };

                db.UserOrdersShop.Add(UserOrder);
                db.SaveChanges();

                var CountAllItem = VorhanName.Count() + AussenName.Count() + DopelName.Count() + KnayfName.Count() + HalbName.Count() + HelbName.Count();

                int Rowcheked = 17;
                int row = 19;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Schließplan"];

                    for (int i = 0; i < CountAllItem; i++)
                    {

                        for (int z = 0; z < countKey.Count(); z++)
                        {
                            if (z < countKey.Count() / TurName.Count())
                            {
                                if (keyIsOpen[z] == true)
                                {
                                    worksheet.Cells[$"S{Rowcheked + z + i}"].Value = "X";
                                }
                                else
                                {
                                    worksheet.Cells[$"S{Rowcheked + z + i}"].Value = "O";
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        worksheet.Cells[$"A{Rowcheked}"].Value = i + 1;
                        worksheet.Cells[$"B{Rowcheked}"].Value = i + 1;
                        worksheet.Cells[$"C{Rowcheked}"].Value = TurName[i];
                        worksheet.Cells[$"E{Rowcheked}"].Value = "Ssl";

                        if (i < countKey.Count())
                        {
                            worksheet.Cells[$"O{Rowcheked}"].Value = countKey[i];
                        }
                        if (i < key.Count())
                        {
                            worksheet.Cells[1, row + i].Value = key[i];
                        }

                        if (i < DopelName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Profil-Doppelzylinder";

                            string Option = "";

                            if (i < DoppelOption.Count())
                            {
                                Option = DoppelOption[i];
                            }
                            else
                            {
                                Option = "";
                            }

                            worksheet.Cells[$"J{Rowcheked}"].Value = Option;
                            worksheet.Cells[$"M{Rowcheked}"].Value = DoppelAussen[i];
                            worksheet.Cells[$"N{Rowcheked}"].Value = DoppelIntern[i];

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = DopelName[i],
                                Aussen = DoppelAussen[i],
                                Intern = DoppelIntern[i],
                                Option = Option
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        else if (i < KnayfName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Profil-Knaufzylinder";

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = KnayfName[i],
                                Aussen = KnayfAussen[i],
                                Intern = KnayfIntern[i]
                                //Option = DoppelOption[i]
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        else if (i < HalbName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Profil-Halbzylinder";

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = HalbName[i],
                                Aussen = HalbAussen[i]
                                //Option = DoppelOption[i]
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        else if (i < HelbName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Hebelzylinder";

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = HelbName[i],
                                //Option = DoppelOption[i]
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        else if (i < VorhanName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Vorhangschloss";

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = VorhanName[i],
                                Aussen = VorhanAussen[i],
                                //Option = DoppelOption[i]
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        else if (i < AussenName.Count())
                        {
                            worksheet.Cells[$"I{Rowcheked}"].Value = "Aussenzylinder_Rundzylinder";

                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = AussenName[i]
                                //Option = DoppelOption[i]
                            };

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();
                        }

                        Rowcheked++;
                    }

                    package.Save();
                }

                var UserName_UserSername = UserLogin.Name + " " + UserLogin.Sername;

                ViewBag.UserInformStatus = UserLogin.Status;
                ViewBag.UserId = UserLogin.Id;
                ViewBag.UserNameItem = UserName_UserSername;
                return RedirectToAction("AdminConnect", UserLogin);
            }
           


            return View();
        }
        [HttpGet]
        public ActionResult FinischerProductSelect(string userInfo, float SumCosted,List<string> DopelOption, List<int> DopelItem,  List<float> DAussen, List<float> DIntern,List<int> Knayf, string user, List<int> Halb,
            List<int> KnayfIntern, List<int> KnayfAusse, List<int> Helb, List<int> Vorhan,List<int> Aussen, List<float> VorhanAussen, List<float> HablAussen)
        {

            char[] separators = { ' ', '\n', '\t', '\r' };

            if (userInfo != null)
            {
                string[] words = userInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var UserLogin = db.User.Where(x => x.Name == words[0] & x.Sername == words[1]).ToList();

                var UserName_UserSername = UserLogin.Select(x => x.Name).First() + " " + UserLogin.Select(x => x.Sername).First();

                ViewBag.UserInformStatus = UserLogin.Select(x => x.Status).First();
                ViewBag.UserId = UserLogin.Select(x => x.Id).First();
                ViewBag.UserNameItem = UserName_UserSername;
            }



            var UserOrder = db.Orders.FirstOrDefault(x => x.userKey == user);
            var DoppelSylinder = new List<Profil_Doppelzylinder>();
            var KnayfSylinder = new List<Profil_Knaufzylinder>();
            var HalbSylinder = new List<Profil_Halbzylinder>();
            var HelbSylinder = new List<Hebel>();
            var VorhanSylinder = new List<Vorhangschloss>();
            var AussenSylinder = new List<Aussenzylinder_Rundzylinder>();

            var OpenLis = new List<isOpen_value>();
            var Order = db.Orders.Where(x => x.userKey == user).Distinct().ToList();

            var OrderChekedKey = db.Orders.Where(x => x.userKey == user).ToList();

            var KeyValue = new List<KeyValue>();
            var isopen = new List<isOpen_Order>();

            ViewBag.SumCosted = SumCosted;

            foreach (var list in Order)
            {
                var IsOpen = db.isOpen_Order.Where(x => x.OrdersId == list.id).Distinct().ToList();
                foreach (var OpenList in IsOpen)
                {
                    isopen.Add(OpenList);
                   
                }
               
            }
            foreach (var OpenList in isopen.Distinct())
            {
                var OpenValue = db.isOpen_value.Where(x => x.isOpen_OrderId == OpenList.Id).Distinct().ToList();
                
                foreach (var allList in OpenValue.Distinct())
                    OpenLis.Add(allList);
            }
           
            foreach (var OpenList in OpenLis.Distinct().ToList())
            {
                var opened = db.KeyValue.Where(x => x.OpenKeyId == OpenList.Id).Distinct().ToList();
                foreach (var allList in opened.Distinct())
                    KeyValue.Add(allList);
               
            }

            if (DopelItem.Count() > 0)
            {
                for (int i =0;i< DopelItem.Count();i++)
                {
                    var DopelZylinder = db.Profil_Doppelzylinder.Where(x => x.Id == DopelItem.First()).ToList();
                     foreach(var list in DopelZylinder)
                        DoppelSylinder.Add(list);
                }
                
            }
            if (Knayf.Count() > 0)
            {
                for (int i = 0; i < Knayf.Count(); i++)
                {
                    var KnayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Id == Knayf.First()).ToList();
                    foreach (var list in KnayfZylinder)
                        KnayfSylinder.Add(list);
                }

            }
            if (Halb.Count() > 0)
            {
                for (int i = 0; i < Halb.Count(); i++)
                {
                    var HalbZylinder = db.Profil_Halbzylinder.Where(x => x.Id == Halb.First()).ToList();
                    foreach (var list in HalbZylinder)
                        HalbSylinder.Add(list);
                }

            }
            
            if (Helb.Count() > 0)
            {
                for (int i = 0; i < Helb.Count(); i++)
                {
                    var HelbZylinder = db.Hebelzylinder.Where(x => x.Id == Helb.First()).ToList();
                    foreach (var list in HelbZylinder)
                        HelbSylinder.Add(list);
                }

            }
            if (Vorhan.Count() > 0)
            {
                for (int i = 0; i < Vorhan.Count(); i++)
                {
                    var VorhanZylinder = db.Vorhangschloss.Where(x => x.Id == Vorhan.First()).ToList();
                    foreach (var list in VorhanZylinder)
                        VorhanSylinder.Add(list);
                }

            }
            if (Aussen.Count() > 0)
            {
                for (int i = 0; i < Aussen.Count(); i++)
                {
                    var AussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == Aussen.First()).ToList();
                    foreach (var list in AussenZylinder)
                        AussenSylinder.Add(list);
                }

            }

            ViewBag.Tur = Order.Select(x => x.DorName).ToList();
            
            var countRow = KeyValue.Distinct().ToList().Count()/OpenLis.Distinct().ToList().Count();
            ViewBag.EinRow = countRow;

            var Doppelname = DoppelSylinder.Select(x => x.Name).ToList();

            ViewBag.DoppelAussen = DAussen.ToList();
            ViewBag.DoppelIntern = DIntern.ToList();
            ViewBag.DoppelOption = DopelOption;
            ViewBag.DoppelItem = DoppelSylinder.Count();
            ViewBag.DopelZylinder = Doppelname.ToList();

            ViewBag.KnayfAussen = KnayfAusse.ToList();
            ViewBag.KnayfName = KnayfSylinder.Select(x => x.Name).ToList();
            ViewBag.KnayflIntern = KnayfIntern.ToList();
            ViewBag.KnayfItem = KnayfSylinder.Count;

            ViewBag.HalbName = HalbSylinder.Select(x => x.Name).ToList();
            ViewBag.HalbAussen = HablAussen.ToList();
            ViewBag.HalbCount = HalbSylinder.Count();
            
            ViewBag.HelbName = HelbSylinder.Select(x => x.Name).ToList();
            ViewBag.HelbCount = HelbSylinder.Count();

            ViewBag.VorhanName = VorhanSylinder.Select(x => x.Name).ToList();
            ViewBag.VorhanAussen = VorhanAussen.ToList();
            ViewBag.VorhanCount = VorhanSylinder.Count();

            ViewBag.AussenName = AussenSylinder.Select(x => x.Name).ToList();
            ViewBag.AussenCount = AussenSylinder.Count();

            ViewBag.Key = OpenLis.Distinct().ToList();
            ViewBag.KeyValueFT = KeyValue.ToList();

            ViewBag.IsOpen = KeyValue.Select(x => x.isOpen).ToList();
            ViewBag.NameKey = OpenLis.Select(x=>x.NameKey).Distinct().ToList();
            ViewBag.CountKey = OpenLis.Select(x => x.CountKey).ToList();


            return View("FinischerProductSelect", UserOrder );
        }
        [HttpPost]
        public ActionResult SaveOrder(string userName, Orders Key, List<string> Turname, List<string> ZylinderId, List<float> aussen, List<float> innen, List<int> Count, List<string> NameKey, List<int> CountKey, List<string> IsOppen, List<string> Options, List<int> ItemCount,List<string>Ssl)
        {
            int CountOrders = Turname.Count();

            string zylinderTyp;

            for (int i = 0; i < CountOrders; i++)
            {

                int idZylinder=0;

                if (ZylinderId.Count() <= i)
                {
                    zylinderTyp = ZylinderId.Last();

                    if (zylinderTyp == "Profil-Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Profil-Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Profil-Knaufzylinder")
                    {
                        idZylinder = 3;
                    }
                    if (zylinderTyp == "Hebelzylinder")
                    {
                        idZylinder = 4;
                    }
                    if (zylinderTyp == "Vorhangschloss")
                    {
                        idZylinder = 5;
                    }
                    if (zylinderTyp == "Aussenzylinder_Rundzylinder")
                    {
                        idZylinder = 6;
                    }
                }
                else
                {
                    zylinderTyp = ZylinderId[i];

                    if (zylinderTyp == "Profil-Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Profil-Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Profil-Knaufzylinder")
                    {
                        idZylinder = 3;
                    }
                    if (zylinderTyp == "Hebelzylinder")
                    {
                        idZylinder = 4;
                    }
                    if (zylinderTyp == "Vorhangschloss")
                    {
                        idZylinder = 5;
                    }
                    if (zylinderTyp == "Aussenzylinder_Rundzylinder")
                    {
                        idZylinder = 6;
                    }
                }

                string TurnameValue;
                if (i >= Turname.Count())
                {
                    TurnameValue = Turname.Last();
                }
                else
                {
                    TurnameValue = Turname[i];
                }

                string artikul = "";
        
                if (i >= Ssl.Count())
                {
                    artikul = Ssl.Last();
                }
                else
                {
                    artikul = Ssl[i];
                }

                string optionValue;

                if (i >= Options.Count())
                {
                    if (Options.Last() == "Options")
                        optionValue = null;
                    else
                        optionValue = Options.Last();
                }
                else
                {
                    if (Options[i] == "Options")
                        optionValue = null;
                    else
                        optionValue = Options[i];
                }

                var orders = new Orders
                {
                    userKey = Key.userKey,
                    DorName = TurnameValue,
                    ZylinderId = idZylinder,
                    Options = optionValue,
                    Artikelnummer = artikul,
                    Created = DateTime.Now
                };


                if (innen.Count() > 0)
                {
                    if (i >= innen.Count())
                    {
                      
                    }
                    else
                    {
                        if(innen[i] == 0)
                        {
                            orders.innen = null;
                        }
                        else
                        {
                            orders.innen = innen[i];
                        }
                       
                    }

                    
                }
                if (aussen.Count() > 0)
                {
                    if (i >= aussen.Count())
                    {
                       
                    }
                    else
                    {
                        if (aussen[i] == 0)
                        {
                            orders.aussen = null;
                        }
                        else
                        {
                            orders.aussen = aussen[i];
                        }
                       
                    }
                  
                }
                db.Orders.Add(orders);
                db.SaveChanges();

                var x = db.Orders.Select(x => x.id).ToList();

                var Open = new isOpen_Order
                {
                    OrdersId = x.Last()
                };
                db.isOpen_Order.Add(Open);
                db.SaveChanges();

            }
            var order_open = db.isOpen_Order.Select(x => x.Id).ToList();
           
            var d = 0;
           
            if (ItemCount.Count() > 0)
            {
                var itemsCount = IsOppen.Count() / ItemCount.First();
                for (var s = 0; s < ItemCount.First(); s++)
                {
                    string NameKeyValue;

                    int CountkeyOrders;


                    if (s >= CountKey.Count())
                    {
                        CountkeyOrders = CountKey.Last();
                    }
                    else
                    {
                        CountkeyOrders = CountKey[s];
                    }
                    if (s >= NameKey.Count())
                    {
                        NameKeyValue = NameKey.Last();
                    }
                    else
                    {
                        NameKeyValue = NameKey[s];
                    }
                    var Open_value = new isOpen_value
                    {
                        isOpen_OrderId = order_open.Last(),
                        NameKey = NameKeyValue,
                        CountKey = CountkeyOrders,
                    };

                    db.isOpen_value.Add(Open_value);

                    db.SaveChanges();

                    for (var f = 0; f < itemsCount; f++)
                    {

                        bool valueOppen = false;
                        if (d >= IsOppen.Count())
                        {
                            if (f >= IsOppen.Count())
                            {
                                if (IsOppen.Last() == "true")
                                {
                                    valueOppen = true;
                                }
                                if (IsOppen.Last() == "false")
                                {
                                    valueOppen = false;
                                }
                            }
                            else
                            {
                                if (IsOppen[f] == "true")
                                {
                                    valueOppen = true;
                                }
                                if (IsOppen[f] == "false")
                                {
                                    valueOppen = false;
                                }
                            }

                        }
                        else
                        {
                            if (IsOppen[d] == "true")
                            {
                                valueOppen = true;
                            }
                            if (IsOppen[d] == "false")
                            {
                                valueOppen = false;
                            }
                        }

                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = valueOppen
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            else
            {
                for (var s = 0; s < Turname.Count(); s++)
                {
                    string NameKeyValue;
                    int CountkeyOrders;

                    if (s >= CountKey.Count())
                    {
                        CountkeyOrders = CountKey.Last();
                    }
                    else
                    {
                        CountkeyOrders = CountKey[s];
                    }
                    if (s >= NameKey.Count())
                    {
                        NameKeyValue = NameKey.Last();
                    }
                    else
                    {
                        NameKeyValue = NameKey[s];
                    }
                    var Open_value = new isOpen_value
                    {
                        isOpen_OrderId = order_open.Last(),
                        NameKey = NameKeyValue,
                        CountKey = CountkeyOrders,
                    };
                    db.isOpen_value.Add(Open_value);
                    db.SaveChanges();

                    for (var f = 0; f <= IsOppen.Count(); f++)
                    {
                        bool valueOppen = false;
                        if(d >= IsOppen.Count())
                        {
                            if(f >= IsOppen.Count())
                            {
                                if (IsOppen.Last() == "true")
                                {
                                    valueOppen = true;
                                }
                                if (IsOppen.Last() == "false")
                                {
                                    valueOppen = false;
                                }
                            }
                            else
                            {
                                if (IsOppen[f] == "true")
                                {
                                    valueOppen = true;
                                }
                                if (IsOppen[f] == "false")
                                {
                                    valueOppen = false;
                                }
                            }
                           
                        }
                        else
                        {
                            if (IsOppen[d] == "true")
                            {
                                valueOppen = true;
                            }
                            if (IsOppen[d] == "false")
                            {
                                valueOppen = false;
                            }
                        }
                       
                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = valueOppen
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            db.SaveChanges();
            return RedirectToAction("System_Auswählen", "Konfigurator", new { Key, userName } );
        }
        
    }
}
