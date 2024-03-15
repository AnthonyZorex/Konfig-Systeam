using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagenkonfigurator.Migrations;
using SkiaSharp;
using Spire.Xls;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.PortableExecutable;

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

        public async Task<ActionResult> IndexKonfigurator()
        {
            ViewBag.Zylinder_Typ = await db.Schliessanlagen.ToListAsync();

            var a = await db.Aussen_Innen.Select(x=>x.aussen).ToListAsync();
            var b = await db.Aussen_Innen_Knauf.Select(x => x.aussen).ToListAsync();
            var c = await db.Aussen_Innen_Halbzylinder.Select(x => x.aussen).ToListAsync();

            var d = await db.Aussen_Innen.Select(x => x.Intern).ToListAsync();
            var e = await db.Aussen_Innen_Knauf.Select(x => x.Intern).ToListAsync();

            var optionsName = db.NGF.Select(x => x.Name).ToList();


            var listAllInnen = new List<float>();
            var listAllAussen = new List<float>();
            var ListOptions = new List<string>();

            for (int i = 0; i < optionsName.Count; i++)
                ListOptions.Add(optionsName[i]);


            for (int i = 0; i < d.Count; i++)
                listAllInnen.Add(d[i]);
            for (int i = 0; i < e.Count; i++)
                listAllInnen.Add(e[i]);



            for (int i = 0; i < a.Count; i++)
                listAllAussen.Add(a[i]);
            for (int i = 0; i < b.Count; i++)
                listAllAussen.Add(b[i]);
            for (int i = 0; i < c.Count; i++)
                listAllAussen.Add(c[i]);

            var orderedNumbers = from i in listAllAussen
                                 orderby i
                                 select i;

            var orderedInnen = from i in listAllInnen
                               orderby i
                                 select i;



            ViewBag.Innen = orderedInnen.Distinct();

            ViewBag.Aussen = orderedNumbers.Distinct();

            ViewBag.OptionsName = ListOptions.Distinct();

            var session = _contextAccessor.HttpContext.Session;

            var UserKey = session.Id;
            Orders user = new Orders();
            user.userKey = UserKey;
            ViewBag.isOpen = db.isOpen_value.Select(x=>x.isOpen);

            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult>System_Auswählen(Orders userKey)
        {

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

            for (var d = 0; d < allUserListOrder.Count(); d++)
            {
                int dopelType = 0;

                if (d >= profilD.Count())
                {
                    dopelType = profilD.Last().schliessanlagenId;
                }
                else
                {
                    dopelType = profilD[d].schliessanlagenId;
                }
                if (allUserListOrder[d].ZylinderId == dopelType)
                {
                   
                    var dopelProduct = new List<Profil_Doppelzylinder>();

                    var products = await db.Aussen_Innen.ToListAsync();

                    var item = products.Where(x => x.aussen <= allUserListOrder[d].aussen & x.Intern <= allUserListOrder[d].innen).ToList();
                    
                   

                    var safeDoppelItem = new List<Profil_Doppelzylinder>();

                    for (int i = 0;i < item.Count(); i++)
                    {
                        var chekedItem = db.Profil_Doppelzylinder.Where(x => x.Id == item[i].Profil_DoppelzylinderId).ToList();

                        for (int g = 0; g < chekedItem.Count(); g++)
                        safeDoppelItem.Add(chekedItem[g]);

                    }

                    for (int j = 0; j < safeDoppelItem.Count(); j++)
                    {
                        cheked.Add(safeDoppelItem[j]);
                    }

                    var OptionsList = new List<Profil_Doppelzylinder_Options>();

                    var results = new List<NGF>();

                    var opt = new List<Profil_Doppelzylinder_Options>();

                    for (int i = 0; i < cheked.Count();i++)
                    {
                        var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == cheked[i].Id).ToList();
                        for(int fs =0; fs < options.Count(); fs++)
                        {
                            opt.Add(options[fs]);
                        }

                    }
                    if (allUserListOrder[d].Options != "Options")
                {
                        if (opt.Count() != 0)
                    {
                        for (int j = 0; j < opt.Count(); j++)
                        {
                            OptionsList.Add(opt[j]);
                        }
                    }

                    var resultList = new List<NGF>();

                    for (int f = 0; f < OptionsList.Count(); f++)
                    {
                        var OptionsName = db.NGF.Where(x => x.OptionsId == OptionsList[f].Id).ToList();
                        
                            var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                            for (int s = 0; s < result.Count(); s++)
                            {
                                resultList.Add(result[s]);

                            }
                        
                       
                    }


                    for (int s = 0; s < resultList.Count(); s++)
                    {
                        var ItemList = db.Profil_Doppelzylinder_Options.Where(x => x.Id == resultList[s].OptionsId).ToList();
                        OptionsList = ItemList;
                    }


                    for (int l = 0; l < OptionsList.Count(); l++)
                    {
                        var dopel = db.Profil_Doppelzylinder.Where(x => x.Id == OptionsList[l].DoppelzylinderId).ToList();

                        cheked = dopel;
                    }
               
                    for (int xf = 0; xf < cheked.Count(); xf++)
                    {
                        var ms = db.Profil_Doppelzylinder.Where(x => x.Id == cheked[xf].Id).Select(x => x).ToList();
                        foreach(var list in ms)
                        dopelProduct.Add(list);
                    }
                        ViewBag.a = dopelProduct.ToList();
                }
                    
            }
                var query = from t1 in cheked
                            join t2 in allUserListOrder
                            on t1.schliessanlagenId equals t2.ZylinderId
                            select new
                            {
                                Id = t1.Id,
                                userKey = keyUser,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t1.description,
                                NameSystem = t1.NameSystem,
                                Cost = t1.Cost,
                                ImageName = t1.ImageName
                                // Здесь можно указать другие поля, которые вы хотите получить
                            };
                var rl = query.ToList();

                ViewBag.a = rl;
                //    if (allUserListOrder[d].ZylinderId == profilK[d].schliessanlagenId)
                //    {
                //        var dopelId = profilK[d].Id;

                //        var dopelProduct = new List<Profil_Knaufzylinder>();

                //        var products = await db.Aussen_Innen_Knauf.ToListAsync();

                //        var item = products.Where(x => x.aussen >= allUserListOrder[d].aussen & x.Intern <= allUserListOrder[d].innen).ToList();

                //        for (int i = 0; i < item.Count(); i++)
                //        {
                //            var f = db.Profil_Knaufzylinder.Where(x => x.Id == item[i].Profil_KnaufzylinderId).Select(x => x).First();
                //            dopelProduct.Add(f);
                //        }

                //        ViewBag.b = dopelProduct.ToList();
                //    }

                //    if (allUserListOrder[d].ZylinderId == profilH[d].schliessanlagenId)
                //    {
                //        var dopelId = profilH[d].Id;

                //        var dopelProduct = new List<Profil_Halbzylinder>();

                //        var products = await db.Aussen_Innen_Halbzylinder.ToListAsync();

                //        var itemD = products.Where(x => x.aussen >= allUserListOrder[d].aussen).GroupBy(x => x.Profil_HalbzylinderId).ToList();

                //        var itemCount = products.Where(x => x.aussen >= allUserListOrder[d].aussen).Select(x => x.Profil_HalbzylinderId).Max();

                //        for (int i = 0; i < itemCount; i++)
                //        {
                //            var f = db.Profil_Halbzylinder.Where(x => x.Id == itemD[i].Key).Select(x => x).First();

                //            dopelProduct.Add(f);
                //        }

                //        ViewBag.c = dopelProduct.ToList();
                //    }

                //    if (allUserListOrder[d].ZylinderId == Vorhangschloss[d].schliessanlagenId)
                //    {
                //        var dopelId = Vorhangschloss[d].Id;

                //        var dopelProduct = new List<Vorhangschloss>();

                //        var products = await db.Size.ToListAsync();

                //        var item = products.Where(x => x.sizeVorhangschloss >= allUserListOrder[d].aussen).ToList();


                //        for (int i = 0; i < item.Count(); i++)
                //        {
                //            var f = db.Vorhangschloss.Where(x => x.Id == item[i].VorhangschlossId).Select(x => x).First();
                //            dopelProduct.Add(f);
                //        }

                //        ViewBag.d = dopelProduct.ToList();
                //    }

                //    if (allUserListOrder[d].ZylinderId == hebel[0].schliessanlagenId)
                //    {
                //        var dopelId = hebel[d].Id;

                //        var hebl = hebel.Where(x => x.Id == dopelId).ToList();

                //        var products = await db.Size.ToListAsync();

                //        var item = products.Where(x => x.sizeVorhangschloss >= allUserListOrder[d].aussen).ToList();

                //        for (int i = 0; i < item.Count(); i++)
                //        {
                //            var f = db.Hebelzylinder.Where(x => x.Id == item[i].Id).Select(x => x).First();
                //            dopelProduct.Add(f);
                //        }

                //        ViewBag.f = hebl.ToList();
                //    }
                //    if (allUserListOrder[d].ZylinderId == Aussenzylinder[0].schliessanlagenId)
                //    {
                //        var dopelId = Aussenzylinder[d].Id;

                //        var aussen = Aussenzylinder.Where(x => x.Id == dopelId).ToList();

                //        var options = await db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == Aussenzylinder[d].Id).Select(x => x.Id).ToListAsync();

                //        if (options.Count() > 0)
                //        {


                //            var s = new List<Aussen_Rund_all>();
                //            for (int i = 0; i < options.Count(); i++)
                //            {
                //                var option = await db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == options[i]).ToListAsync();
                //                s.Add(option.First());
                //            }

                //            for (int i = 0; i < s.Count(); i++)
                //            {
                //                var f = db.Aussen_Rund_all.Where(x => x.Name == allUserListOrder[d].Options).Select(x => x).First();

                //                var fs = await db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == f.Id).Select(x => x.Id).ToListAsync();

                //                //var info =  Aussenzylinder.Where(x=>x.Aussen_Rund_options==fs).ToListAsync();

                //                // dopelProduct.Add(info);
                //            }
                //        }


                //        ViewBag.e = aussen.ToList();
                //    }


            }

            return View("System_Auswählen", userKey);
        }

        //[HttpPost]
        //public async Task<IActionResult> System_Auswählen(string key)
        //{
        //    var order = db.Orders.Where(x=>x.userKey ==  key).ToList();

        //    return RedirectToAction("Finisher",order);
        //}
        [HttpGet]
        public async Task<IActionResult> OrdersKey(string param1, string param2)
        {
            var key = db.Orders.Where(x => x.userKey == param2).ToList();
            //var OrderList = db.Profil_Doppelzylinder.Where(x => x.Id == profilD.Id).ToList();

            //var allOrder = db.Orders.Where(x => x.userKey == key.use).Last();

            //var ordersDople = allOrder.Where(x => x.ZylinderId == profilD.schliessanlagenId).Last();

           ViewBag.Order = key;

            return View("Finisher", key.Last() );
        }
        [HttpPost]
        public async Task<IActionResult> Create_Exel(List<string> tur, Profil_Doppelzylinder profil_Doppelzylinder, Profil_Halbzylinder profil_Halbzylinder, Profil_Knaufzylinder Profil_Knaufzylinder, Vorhangschloss Vorhang, Hebel hebelzylinder, Aussenzylinder_Rundzylinder aussenzylinder_Rundzylinder)
        {
            var Zylinder_Typ = await db.Schliessanlagen.ToListAsync();
            var profilD = db.Profil_Doppelzylinder.ToList();
            var profilH = db.Profil_Halbzylinder.ToList();
            var profilK = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var Vorhangschloss = db.Vorhangschloss.ToList();
            var Aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();
            var Orders = await db.Orders.ToListAsync();

            //var listType = profilD.Where(x => x.Id == profilD[i]).Select(d => d.Extern);

            Workbook workbook = new Workbook();

            workbook.LoadFromFile("CES_schliessplan_DE_schliessanlagen.xltx");


            Worksheet worksheet = workbook.Worksheets[0];

            
            int count = 0;
            int Row = 18;
            for (int i = 0; i < profilD.Count(); i++)
            {
                //var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilD[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                //var Extern = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.aussen).ToList().First();
                //var NameZ = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.Name).ToList().First();
                //var Intern = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.Intern).ToList().First();
                //var TurName = Orders.Where(x => x.ZylinderId == profilD[count].schliessanlagenId).Select(x=>x.Tur).ToList();
               
                //worksheet.Range[Row, 1].Value = $"{Row - 1}";
                //worksheet.Range[Row, 2].Value = $"{TurName}";
                //worksheet.Range[Row, 3].Value = $"{NameZ}";
                //worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                //worksheet.Range[Row, 5].Value = $"{Extern}\t|\t{Intern}";
                //worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < profilH.Count(); i++)
            {

                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilH[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
    
                var NameZ = profilH.Where(x => x.Id == profilH[count].Id).Select(d => d.Name).ToList().First();
             

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{Row - 1}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                //worksheet.Range[Row, 5].Value = $"{Extern}";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < profilK.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilK[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
   
                var NameZ = profilK.Where(x => x.Id == profilK[count].Id).Select(d => d.Name).ToList().Last();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                //worksheet.Range[Row, 5].Value = $"{Extern}\t|\t{Intern}";
                worksheet.Range[Row, 6].Value = $"";
                Row++;
            }
            count = 0;
            for (int i = 0; i < hebel.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == hebel[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var NameZ = hebel.Where(x => x.Id == hebel[count].Id).Select(d => d.Name).ToList().First();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = $"";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < Vorhangschloss.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == Vorhangschloss[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var NameZ = Vorhangschloss.Where(x => x.Id == Vorhangschloss[count].Id).Select(d => d.Name).ToList().First();
                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = $"";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < Aussenzylinder.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == Aussenzylinder[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var NameZ = Aussenzylinder.Where(x => x.Id == Aussenzylinder[count].Id).Select(d => d.Name).ToList().First();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = "";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            //Автоматическое подгонка ширины столбцов
            //worksheet.AllocatedRange.AutoFitColumns();

            //Применение стиля к первой строке
            //CellStyle style = workbook.Styles.Add("newStyle");
            //style.Font.IsBold = true;
            //worksheet.Range[1, 1, 1, 4].Style = style;

            //Сохранение в файл Excel
            workbook.SaveToFile("CES_schliessplan_DE_schliessanlagen.xltx", ExcelVersion.Version2016);



            return View("Finisher");
        }
        [HttpPost]
        public ActionResult SaveOrder(Orders Key, List<string> Turname, List<string> ZylinderId, List<float> aussen, List<float> innen, List<int> Count, List<string> NameKey, List<int> CountKey, List<string> IsOppen, List<string> Options, List<int> ItemCount)
        {
            int CountOrders;
           
            string zylinderTyp;

            if (Turname.Count >= NameKey.Count())
            {
                CountOrders = Turname.Count();
            }
            else
            {
                CountOrders = NameKey.Count();
            }

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
                string NameKeyValue;
                int CountkeyOrders;
                string optionValue;

                if (i >= CountKey.Count())
                {
                    CountkeyOrders = CountKey.Last();
                }
                else
                {
                    CountkeyOrders = CountKey[i];
                }

                if (i >= Options.Count())
                {
                    optionValue = Options.Last();
                }
                else
                {
                    optionValue = Options[i];
                }

                if (i >= Turname.Count())
                {
                    TurnameValue= Turname.Last();
                }
                else
                {
                    TurnameValue = Turname[i];
                }

                if (i >= NameKey.Count())
                {
                    NameKeyValue = NameKey.Last();
                }
                else
                {
                    NameKeyValue = NameKey[i];
                }

                var orders = new Orders 
                {
                    userKey = Key.userKey,
                    DorName = TurnameValue,
                    ZylinderId = idZylinder,
                    
                    NameKey = NameKeyValue,
                    CountKey = CountkeyOrders,
                    Options = optionValue
                };
                if (innen.Count() > 0)
                {
                    if (i >= innen.Count())
                    {
                        orders.innen = innen.Last();
                    }
                    else
                    {
                        orders.innen = innen[i];
                    }

                    
                }
                if (aussen.Count() > 0)
                {
                    if (i >= innen.Count())
                    {
                        orders.aussen = aussen.Last();
                    }
                    else
                    {
                        orders.aussen = aussen[i];
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

                var order_open = db.isOpen_Order.Select(x => x.Id).ToList();

                for (var s = 0; s < ItemCount.Count(); s++)
                {
                    for (var f = 0; f < ItemCount[s]; f++)
                    {
                        bool valueOppen = false;

                        if (IsOppen[f] == "true")
                        {
                            valueOppen = true;
                        }
                        if (IsOppen[f] == "true")
                        {
                            valueOppen = false;
                        }
                        var Open_value = new isOpen_value
                        {
                            isOpen_OrderId = order_open.Last(),
                            isOpen = valueOppen,
                        };
                        db.isOpen_value.Add(Open_value);
                    }

                }
                db.SaveChanges();
            }
           
            return RedirectToAction("System_Auswählen", "Konfigurator", new { Key } );
        }
        
    }
}
