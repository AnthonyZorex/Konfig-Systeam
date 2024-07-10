using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using OfficeOpenXml;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Cors;
using System.Security.Policy;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using schliessanlagen_konfigurator.Models.Users;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
namespace schliessanlagen_konfigurator.Controllers
{
    [EnableCors("*")]
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
        [HttpGet]
        public ActionResult ChangedKonfigPlan(string userKey)
        {
            var Orders = db.Orders.Where(x => x.userKey == userKey).ToList();
            
            var isOpen = new List<isOpen_Order>();
            
            foreach (var list in Orders)
            {
                var open = db.isOpen_Order.Where(x => x.OrdersId == list.Id).ToList();
                
                foreach(var item in open)
                {
                    isOpen.Add(item);
                }
            }

            ViewBag.Orders = Orders.ToList();

            var isOpen_value = new List<isOpen_value>();

            foreach (var item in isOpen)
            {
                var key = db.isOpen_value.Where(x => x.isOpen_OrderId == item.Id).ToList();
                
                foreach(var keyItem in key)
                {
                    isOpen_value.Add(keyItem);
                }
            }
            var Key = isOpen_value.GroupBy(item => item.NameKey)
                      .Select(group => group.First()).ToList();

            ViewBag.key = Key;

            var keyValue = new List<KeyValue>();
            
            foreach (var item in isOpen_value)
            {
                var itemKey = db.KeyValue.Where(x => x.OpenKeyId == item.Id).ToList();
               
                foreach(var list in itemKey)
                {
                    keyValue.Add(list);
                }
            }

            ViewBag.KeyValueFT = keyValue;

            var chek = keyValue.Select(x => x.isOpen).ToList();

            ViewBag.KeyValue = keyValue.ToList();

            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();

            var a = db.Aussen_Innen.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();
            var d = db.Aussen_Innen.Select(x => x.Intern).Distinct().OrderBy(x => x).ToList();

            var listAllInnen = new List<float>();

            var ListAussenDopple = new List<float>();

            for (int i = 0; i < a.Count(); i++)
                ListAussenDopple.Add(a[i]);

            ViewBag.DoppelAussen = ListAussenDopple.Distinct();

            var ListInternDopple = new List<float>();
            for (int i = 0; i < d.Count(); i++)
                ListInternDopple.Add(d[i]);

            ViewBag.DoppelIntern = ListInternDopple.Distinct();


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

            var c = db.Aussen_Innen_Halbzylinder.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();

            var HalbzylinderAussen = new List<float>();

            for (int i = 0; i < c.Count(); i++)
                HalbzylinderAussen.Add(c[i]);

            var size = db.Size.Select(x => x.sizeVorhangschloss).ToList();
            var VorhanSchlossSize = new List<float>();

            for (int i = 0; i < size.Count(); i++)
                VorhanSchlossSize.Add(size[i]);

            var UserKey = userKey;
            Orders user = new Orders();

            user.userKey = UserKey;
            ViewBag.isOpen = db.KeyValue.Select(x => x.isOpen);

            ViewBag.KnayfAussen = listKnayfAussen.Distinct();
            ViewBag.KnayfInter = listKnayfIntern.Distinct();

            ViewBag.SizeDoppelAussen = JsonConvert.SerializeObject(ListAussenDopple.Distinct());
            ViewBag.SizeDoppelIntern = JsonConvert.SerializeObject(ListInternDopple.Distinct());

            ViewBag.SizeKnayfAussen = JsonConvert.SerializeObject(listKnayfAussen.Distinct());
            ViewBag.SizeKnayfIntern = JsonConvert.SerializeObject(listKnayfIntern.Distinct());

            ViewBag.SizeHalb = JsonConvert.SerializeObject(HalbzylinderAussen.Distinct());

            return View(user);
        }

        [HttpPost]
        public ActionResult ChangedKonfigPlanPost(List<string> FurNameKey, string userName, Orders Key, List<string> Turname, 
        List<string> ZylinderId, List<float> aussen, List<float> innen, List<string> NameKey, List<int> CountKey,
        List<string> IsOppen, List<int> CountTur)
        {

            if (IsOppen.Count > 1)
            {
                IsOppen.RemoveRange(0, IsOppen.Count - 1);
            }

            var ordersItem = db.Orders.Where(x => x.userKey == Key.userKey).ToList();
            
            var isOpen = new List<isOpen_Order>();

            foreach (var list in ordersItem)
            {
                var open = db.isOpen_Order.Where(x => x.OrdersId == list.Id).ToList();

                foreach (var item in open)
                {
                    isOpen.Add(item);
                }
            }

            var isOpen_value = new List<isOpen_value>();

            foreach (var item in isOpen)
            {
                var key = db.isOpen_value.Where(x => x.isOpen_OrderId == item.Id).ToList();

                foreach (var keyItem in key)
                {
                    isOpen_value.Add(keyItem);
                }
            }
            

            foreach (var item in isOpen_value)
            {
                var itemKey = db.KeyValue.Where(x => x.OpenKeyId == item.Id).ToList();

                foreach (var list in itemKey)
                {
                    db.KeyValue.Remove(list);

                    db.SaveChanges();
                }
            }
            foreach(var list in isOpen_value)
            {
                db.isOpen_value.Remove(list);
                db.SaveChanges();
            }

            foreach (var list in isOpen)
            {
                db.isOpen_Order.Remove(list);
                db.SaveChanges();
            }
            foreach(var list in ordersItem)
            {
                db.Orders.Remove(list);
                db.SaveChanges();
            }

            int CountOrders = Turname.Count();

            List<bool> isOpenList = new List<bool>();

            string pattern = @",\s*";

            string[] elements = Regex.Split(IsOppen.Last(), pattern);
            
            int aussenCounter = 0;
            int InterCounter = 0;

            foreach (string element in elements)
            {
                if (element == "true")
                {
                    isOpenList.Add(true);
                }
                else
                {
                    isOpenList.Add(false);
                }

            }
            string zylinderTyp;

            for (int i = 0; i < CountOrders; i++)
            {

                int idZylinder = 0;

                if (ZylinderId.Count() <= i)
                {
                    zylinderTyp = ZylinderId.Last();

                    if (zylinderTyp == "Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Knaufzylinder")
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
                    if (zylinderTyp == "Aussenzylinder")
                    {
                        idZylinder = 6;
                    }
                }
                else
                {
                    zylinderTyp = ZylinderId[i];

                    if (zylinderTyp == "Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Knaufzylinder")
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
                    if (zylinderTyp == "Aussenzylinder")
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

                var orders = new Orders
                {
                    userKey = Key.userKey,
                    DorName = TurnameValue,
                    ZylinderId = idZylinder,
                    Created = DateTime.Now,
                    Count = CountTur[i]
                };


                if (innen.Count() > 0)
                {
                    if (InterCounter > innen.Count() || idZylinder == 2 || idZylinder == 4 || idZylinder == 5 || idZylinder == 6)
                    {

                    }
                    else
                    {
                        if (innen[InterCounter] == 0)
                        {
                            orders.innen = null;
                        }
                        else
                        {
                            orders.innen = innen[InterCounter];
                            InterCounter++;
                        }

                    }


                }
                if (aussen.Count() > 0)
                {
                    if (aussenCounter > aussen.Count() || idZylinder == 4 || idZylinder == 5 || idZylinder == 6)
                    {

                    }
                    else
                    {
                        if (aussen[aussenCounter] == 0)
                        {
                            orders.aussen = null;
                        }
                        else
                        {
                            orders.aussen = aussen[aussenCounter];
                            aussenCounter++;
                        }

                    }

                }
                db.Orders.Add(orders);
                db.SaveChanges();

                var Open = new isOpen_Order
                {
                    OrdersId = orders.Id
                };
                db.isOpen_Order.Add(Open);
                db.SaveChanges();

            }
            var order_open = db.isOpen_Order.Select(x => x.Id).ToList();

            var d = 0;

            if (CountOrders > NameKey.Count())
            {
                var itemsCount = isOpenList.Count() / CountOrders;

                for (var s = 0; s < CountOrders; s++)
                {
                    string NameKeyValue;

                    int CountkeyOrders;
                    string FurNameKeyValue;

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
                    if (s >= FurNameKey.Count())
                    {
                        FurNameKeyValue = FurNameKey.Last();
                    }
                    else
                    {
                        FurNameKeyValue = FurNameKey[s];
                    }
                    var Open_value = new isOpen_value
                    {
                        isOpen_OrderId = order_open.Last(),
                        NameKey = NameKeyValue,
                        CountKey = CountkeyOrders,
                        ForNameKey = FurNameKeyValue
                    };

                    db.isOpen_value.Add(Open_value);

                    db.SaveChanges();

                    for (var f = 0; f < itemsCount; f++)
                    {
                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = isOpenList[d]
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            else
            {
                for (var s = 0; s < NameKey.Count(); s++)
                {
                    string NameKeyValue;
                    int CountkeyOrders;

                    var itemsCount = CountOrders;

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

                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = isOpenList[d]
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            db.SaveChanges();
            return RedirectToAction("System_Auswählen", "Konfigurator", new { userName });
        }
       public ActionResult endOrder(int data)
       {
            var PaymentOrder = db.UserOrdersShop.Find(data);

            PaymentOrder.OrderStatus = "Bezahlt";

            db.SaveChanges();

            return Redirect("/Identity/Account/Manage/PagePersonalOrders");
       }

        [HttpPost]
        public async Task<IActionResult> SetInfo(string data)
        {
            var setI = db.SysteamPriceKey.Where(x => x.NameSysteam == data).Select(x => x).ToList();

            ViewBag.System = setI.ToList();

            return View();
        }

        public ActionResult IndexKonfigurator()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            if (ident.IsAuthenticated == true)
            {
                string loginInform = ident.Claims.Select(x => x.Value).First();
                var users = db.Users.FirstOrDefault(x => x.Id == loginInform);

                var Ord = db.Orders.ToList();

                var userOrders = db.UserOrdersShop.Where(x => x.UserId == users.Id).ToList();

                var ordList = new List<Orders>();

                    foreach (var list in userOrders)
                    {
                        var lastOrder = Ord.Where(x => x.userKey == list.UserOrderKey).ToList();

                        foreach (var item in lastOrder)
                        {
                            ordList.Add(item);
                        }
                    }
                if (ordList.Count() > 0)
                {
                    var lastlist = ordList.OrderBy(x => x.Created).Last();

                    ViewBag.UserNameItem = lastlist.userKey;
                }
            }
            else
            {
                ViewBag.UserNameItem = "";
            }
            
            var UserOList = new List<UserOrdersShop>();

            var oldData = db.UserOrdersShop.Where(e => e.OrderStatus == "Nicht bezahlt").ToList();

            foreach(var order in oldData)
            {
                var item = DateTime.Now.Month - order.createData.Value.Month;
                
                var ChekedWik = DateTime.Now.Day - order.createData.Value.Day;

                if (item > 0)
                {
                    var users = db.Users.FirstOrDefault(x=>x.Id==order.UserId);
                    UserOList.Add(order);

                    string destinationFilePath = @$"wwwroot/Orders/{users.FirstName + users.LastName + order.createData.Value.Minute + order.createData.Value.Hour  + order.createData.Value.Day + order.createData.Value.Month + order.createData.Value.Year} OrderFile.xlsx";

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Schlüssel Discount Store", "info@schluessel.discount"));
                    message.To.Add(new MailboxAddress(users.FirstName + users.LastName, users.UserName));
                    message.Subject = "Schlüssel Discount Store";
                    
                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Ваша система была удалена так как истёк срок ожидания!";

                  
                    using (var wc = new WebClient())
                    {
                        byte[] fileBytes = wc.DownloadData(destinationFilePath);
                        bodyBuilder.Attachments.Add("Email.xlsx", fileBytes);
                    }

                    message.Body = bodyBuilder.ToMessageBody();
                    using (var client = new SmtpClient())
                    {
                        client.Connect("sslout.de", 25, false);
                        client.Authenticate("info@schluessel.discount", "qvVH$ipz2BNdWy");
                        client.Send(message);

                        client.Disconnect(true);
                    }

                    var data = db.ProductSysteam.Where(x => x.UserOrdersShopId == order.Id).ToList();

                    foreach (var x in data)
                    {
                        db.ProductSysteam.Remove(x);
                        db.SaveChanges();
                    }

                    // Удаление данных
                    db.UserOrdersShop.RemoveRange(order);
                    db.SaveChanges();


                    if (System.IO.File.Exists(destinationFilePath))
                    {
                        System.IO.File.Delete(destinationFilePath);
                    }

                }
                else if (ChekedWik >= 7)
                {
                    var users = db.Users.FirstOrDefault(x => x.Id == order.UserId);

                    string destinationFilePath = @$"wwwroot/Orders/{users.FirstName + users.LastName + order.createData.Value.Minute + order.createData.Value.Hour + order.createData.Value.Day + order.createData.Value.Month + order.createData.Value.Year} OrderFile.xlsx";

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Schlüssel Discount Store", "bonettaanthony466@gmail.com"));
                    message.To.Add(new MailboxAddress(users.FirstName + users.LastName, users.UserName));
                    message.Subject = "Schlüssel Discount Store";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = $"Прошло {ChekedWik} дней с даты создания системы { order.ProductName}!";

                    using (var wc = new WebClient())
                    {
                        byte[] fileBytes = wc.DownloadData(destinationFilePath);
                        bodyBuilder.Attachments.Add("Email.xlsx", fileBytes);
                    }

                    message.Body = bodyBuilder.ToMessageBody();
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("bonettaanthony466@gmail.com", "huqf ddvv mnba lcug ");
                        client.Send(message);

                        client.Disconnect(true);
                    }  
                }
            }

            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();

            var a = db.Aussen_Innen.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();
            var d = db.Aussen_Innen.Select(x => x.Intern).Distinct().OrderBy(x => x).ToList();

            var listAllInnen = new List<float>();

            var ListAussenDopple = new List<float>();

            for (int i = 0; i < a.Count(); i++)
                ListAussenDopple.Add(a[i]);

            ViewBag.DoppelAussen = ListAussenDopple.Distinct();

            var ListInternDopple = new List<float>();
            for (int i = 0; i < d.Count(); i++)
                ListInternDopple.Add(d[i]);

            ViewBag.DoppelIntern = ListInternDopple.Distinct();


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

            var c = db.Aussen_Innen_Halbzylinder.Select(x => x.aussen).Distinct().OrderBy(x => x).ToList();

            var HalbzylinderAussen = new List<float>();

            for (int i = 0; i < c.Count(); i++)
                HalbzylinderAussen.Add(c[i]);
         
            var size = db.Size.Select(x => x.sizeVorhangschloss).ToList();
            var VorhanSchlossSize = new List<float>();

            for (int i = 0; i < size.Count(); i++)
                VorhanSchlossSize.Add(size[i]);

            var session = _contextAccessor.HttpContext.Session;

            var UserKey = session.Id;
            Orders user = new Orders();

            user.userKey = UserKey;
            ViewBag.isOpen = db.KeyValue.Select(x => x.isOpen);

            ViewBag.KnayfAussen = listKnayfAussen.Distinct();
            ViewBag.KnayfInter = listKnayfIntern.Distinct();

            ViewBag.SizeDoppelAussen = JsonConvert.SerializeObject(ListAussenDopple.Distinct());
            ViewBag.SizeDoppelIntern = JsonConvert.SerializeObject(ListInternDopple.Distinct());

            ViewBag.SizeKnayfAussen = JsonConvert.SerializeObject(listKnayfAussen.Distinct());
            ViewBag.SizeKnayfIntern = JsonConvert.SerializeObject(listKnayfIntern.Distinct());

            ViewBag.SizeHalb = JsonConvert.SerializeObject(HalbzylinderAussen.Distinct());

            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult> System_Auswählen(string Key, string userName, bool isNewKonfig,bool Biarbeiten)
        {
            var xtr = Key;

            var orders = await db.Orders.ToListAsync();

            if (userName!=null && isNewKonfig == true)
            {
               orders = orders.Where(x => x.userKey == userName).ToList();
                
                if (Biarbeiten == true)
                {
                    ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
                    string loginInform = ident.Claims.Select(x => x.Value).First();
                    var users = db.Users.FirstOrDefault(x => x.Id == loginInform);

                    var RemoveOrder = db.UserOrdersShop.Where(x => x.UserOrderKey == userName).ToList();

                    var currentTime = RemoveOrder.First().createData.Value;

                    var OrderProduct = db.ProductSysteam.Where(x => x.UserOrdersShopId == RemoveOrder.First().Id).ToList();

                    foreach (var listProduct in OrderProduct)
                    {
                        db.ProductSysteam.Remove(listProduct);

                        foreach (var listOrder in RemoveOrder)
                        {
                            db.UserOrdersShop.Remove(listOrder);

                        }
                    }
                    string destinationFilePath = @$"wwwroot/Orders/{users.FirstName + users.LastName + currentTime.Minute + currentTime.Hour + currentTime.Day + currentTime.Month + currentTime.Year} OrderFile.xlsx";

                    if (System.IO.File.Exists(destinationFilePath))
                    {
                        System.IO.File.Delete(destinationFilePath);
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                if (Key != null)
                {
                    orders = orders.Where(x => x.userKey == Key).ToList();
                }
                else
                {
                    orders = orders.Where(x => x.userKey == userName).ToList();
                }
               
            }

            var keyUser = orders.Last();

            var allUserListOrder = await db.Orders.Where(x => x.userKey == keyUser.userKey).ToListAsync();

            var isOpenList = new List<isOpen_Order>();

            foreach(var item in allUserListOrder)
            {
                var orderItem = await db.isOpen_Order.Where(x => x.OrdersId == item.Id).ToListAsync();
                foreach (var list in orderItem)
                {
                    isOpenList.Add(list);
                }
            }

            var isOpen = new List<isOpen_value>();

            foreach (var item in isOpenList)
            {
                var orderItem = await db.isOpen_value.Where(x => x.isOpen_OrderId == item.Id).GroupBy(item => item.NameKey)
                .Select(group => group.First()).ToListAsync();
                foreach (var list in orderItem)
                {
                    isOpen.Add(list);
                }
            }

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

            int VorhCount = 0;
            var allOderDopelSyze = allUserListOrder.Where(x => x.ZylinderId == dopelType).ToList();

            if (allOderDopelSyze.Count() > 0)
            {
                var maxInnenParameter = allOderDopelSyze.Max(x => x.innen);
                var maxAussenParameter = allOderDopelSyze.Max(x => x.aussen);
                var CountProduct = allOderDopelSyze.Select(x => x.Count).ToList();

                var dopelProduct = new List<Profil_Doppelzylinder>();

                var products = await db.Aussen_Innen.ToListAsync();

                var items = products.Where(x => x.aussen >= maxInnenParameter & x.Intern >= maxAussenParameter).Select(x => x.Profil_DoppelzylinderId).Distinct().ToList();

                var safeDoppelItem = new List<Profil_Doppelzylinder>();

                for (int i = 0; i < items.Count(); i++)
                {
                    var chekedItem = db.Profil_Doppelzylinder.Where(x => x.Id == items[i]).ToList();

                    for (int g = 0; g < chekedItem.Count(); g++)
                    {
                        safeDoppelItem.Add(chekedItem[g]);
                    }
                      
                }
                for (int j = 0; j < safeDoppelItem.Count(); j++)
                {
                    dopelProduct.Add(safeDoppelItem[j]);
                }
                    foreach (var g in dopelProduct.Distinct())
                    {
                        cheked.Add(g);
                    }
            }

            var allOderKnayf = allUserListOrder.Where(x => x.ZylinderId == KnayfType).ToList();

            if (allOderKnayf.Count() > 0)
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

                    foreach (var g in KnayfProduct)
                    {
                        cheked2.Add(g);
                    }

            }

            var allOderHalb = allUserListOrder.Where(x => x.ZylinderId == HalbType).ToList();


            if (allOderHalb.Count() > 0)
            {
                var maxAussenParameter = allOderHalb.Max(x => x.aussen);

                var HalbProduct = new List<Profil_Halbzylinder>();

                var products = await db.Aussen_Innen_Halbzylinder.ToListAsync();

                var items = products.Where(x => x.aussen == maxAussenParameter).Select(x => x.Profil_HalbzylinderId).Distinct().ToList();

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

                
                    foreach (var g in HalbProduct)
                    {
                        cheked3.Add(g);
                    }


            }
            var allOderHebel = allUserListOrder.Where(x => x.ZylinderId == HebelType).ToList();

            if (allOderHebel.Count() > 0)
            {
                    foreach (var g in hebel)
                    {
                        cheked4.Add(g);
                    }
            }

            var allOderVorhan = allUserListOrder.Where(x => x.ZylinderId == VorhanType).Select(x=>x.aussen).ToList();

           

            if (allOderVorhan.Count() > 0)
            {

                var VorhangProduct = db.Vorhangschloss.ToList();

                     foreach (var g in VorhangProduct.Distinct())
                     {
                        cheked5.Add(g);
                     }


                VorhCount++;
            }

            var allOderAussen = allUserListOrder.Where(x => x.ZylinderId == AussenType).ToList();

            if (allOderAussen.Count() > 0)
            {
                    foreach (var g in Aussenzylinder)
                    {
                        cheked6.Add(g);
                    }
            }

            #region presset
            if (cheked.Count() > 0 && cheked2.Count==0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {

                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked3 = 0,
                                     cheked2 = 0,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     cheked = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x=>x.Count.Value).Sum() +
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                                var ListOrder = queryOrder.Distinct().ToList();
                                ViewBag.Doppel = ListOrder.Distinct().OrderBy(x => x.Cost).ToList();
            }

           if (cheked2.Count() > 0 && cheked.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
           {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked2 = t1.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum()+
                                       (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Knaufzylinder = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Knaufzylinder.Distinct().OrderBy(x => x.Cost).ToList();
           }
            if (cheked3.Count() > 0 && cheked2.Count == 0 && cheked.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var query = from t1 in cheked3
                            join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                            join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                            select new
                            {
                                cheked = 0,
                                cheked3 = t1.Id,
                                cheked2 = 0,
                                cheked4 = 0,
                                cheked5 = 0,
                                cheked6 = 0,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t3.DesctiptionsSysteam,
                                NameSystem = t1.NameSystem,
                                Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum()+
                                (t3.Price * isOpen.Count()), precision),
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.Halb = rl.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked4.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var query = from t1 in cheked4
                            join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                            join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                            select new
                            {
                                cheked = 0,
                                cheked3 = 0,
                                cheked2 = 0,
                                cheked4 = t1.Id,
                                cheked5 = 0,
                                cheked6 = 0,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t3.DesctiptionsSysteam,
                                NameSystem = t1.NameSystem,
                                Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum()+
                                (t3.Price * isOpen.Count()), precision),
                                ImageName = t1.ImageName

                            };
                var rl = query.Distinct().ToList();

                ViewBag.Hebel = rl.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked5.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var query = from t1 in cheked5
                            join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                            join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                            select new
                            {
                                cheked = 0,
                                cheked3 = 0,
                                cheked2 = 0,
                                cheked4 = 0,
                                cheked5 = t1.Id,
                                cheked6 = 0,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t3.DesctiptionsSysteam,
                                NameSystem = t1.NameSystem,
                                Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+
                                (t3.Price * isOpen.Count()), precision),
                                ImageName = t1.ImageName
                            };
                var rl = query.Distinct().ToList();

                ViewBag.VorhanSchloss = rl.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked6.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked.Count == 0)
            {
                int precision = 2;
                
                var keySum = db.SysteamPriceKey.ToList();

                var query = from t1 in cheked6
                            join t2 in allUserListOrder on t1.schliessanlagenId equals t2.ZylinderId
                            join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                            select new
                            {
                                cheked = 0,
                                cheked3 = 0,
                                cheked2 = 0,
                                cheked4 = 0,
                                cheked5 = 0,
                                cheked6 = t1.Id,
                                userKey = keyUser.userKey,
                                Name = t1.Name,
                                companyName = t1.companyName,
                                description = t3.DesctiptionsSysteam,
                                NameSystem = t1.NameSystem,
                                Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                  (t3.Price * isOpen.Count()), precision),
                                ImageName = t1.ImageName
                            };
                var rl = query.Distinct().ToList();

                ViewBag.Aussen = rl.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t2.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     cheked2 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum()+
                                        (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked3.Count() > 0 && cheked2.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t2.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     userKey = keyUser.userKey,
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     cheked2 = 0,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum()+
                                      (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked4.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;
                
                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t2.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     cheked2 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum()+
                                      (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked5.Count() > 0 && cheked2.Count == 0 && cheked4.Count == 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t2.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked6 = 0,
                                     cheked2 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+
                                   (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };
                var Join = queryOrder.Distinct().ToList();
                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked.Count() > 0 && cheked6.Count() > 0 && cheked2.Count == 0 && cheked4.Count == 0 && cheked3.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked6 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = t2.Id,
                                     cheked2 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                      (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList(); 
            }

            if (cheked2.Count() > 0 && cheked3.Count() > 0 && cheked.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;
                
                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     cheked2 = t1.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum()+
                                    (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked4.Count() > 0 && cheked.Count == 0 && cheked3.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked6 = 0,
                                     cheked2 = t1.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum()+
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked6 = 0,
                                     cheked2 = t1.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked6 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = t1.Id,
                                     cheked6 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                       (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked4.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;
                
                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum()+
                                       (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Halb = Join.Distinct().OrderBy(x => x.Cost).ToList();
               
            }
            if (cheked3.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked6.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Halb = Join.Distinct().OrderBy(x => x.Cost).ToList(); 

            }
            if (cheked3.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked4.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked6 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Halb = Join.Distinct().OrderBy(x => x.Cost).ToList(); 
         
            }
            if (cheked4.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked4
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t1.Id,
                                     cheked5 = t2.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+
                                     (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Hebel = Join.Distinct().OrderBy(x => x.Cost).ToList(); 
                
            }
            if (cheked4.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked4
                                 join t2 in cheked6 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t1.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Id = t1.Id,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                      (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Hebel = Join.Distinct().OrderBy(x => x.Cost).ToList(); 
                
            }
            if (cheked5.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked5
                                 join t2 in cheked6 on t1.NameSystem equals t2.NameSystem
                                 join t3 in keySum on t1.NameSystem equals t3.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t1.Id,
                                     cheked2 = 0,
                                     cheked6 = t2.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t3.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+
                                       (t3.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.VorhanSchloss = Join.Distinct().OrderBy(x => x.Cost).ToList(); 
             
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + 
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked4.Count() > 0 && cheked3.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();
                
                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked5.Count() > 0 && cheked4.Count == 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked6.Count() > 0 && cheked4.Count == 0 && cheked5.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked.Count() > 0 && cheked4.Count() > 0 && cheked6.Count == 0 && cheked5.Count == 0 && cheked2.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum()+ (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked.Count() > 0 && cheked5.Count() > 0 && cheked6.Count == 0 && cheked4.Count == 0 && cheked2.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+ (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked.Count() > 0 && cheked6.Count() > 0 && cheked5.Count == 0 && cheked4.Count == 0 && cheked2.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked4.Count() > 0 && cheked.Count() > 0 && cheked5.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum()+ (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked4.Count() > 0 && cheked.Count() > 0 && cheked6.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum()+(t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked5.Count() > 0 && cheked.Count() > 0 && cheked6.Count() > 0 && cheked2.Count == 0 && cheked3.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked3.Count() > 0 && cheked2.Count() > 0 && cheked4.Count() > 0 && cheked.Count == 0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t2.Id,
                                     cheked4 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = t1.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked2.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked4.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked2.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked5.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = 0,
                                     cheked2 = t1.Id,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked4.Count() > 0 && cheked2.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked4.Count() > 0 && cheked2.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked5.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked2 = t1.Id,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked5.Count() > 0 && cheked2.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked4.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();
                
                var queryOrder = from t1 in cheked2
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked4.Count() > 0 && cheked3.Count() > 0 && cheked5.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked4.Count() > 0 && cheked3.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;
                
                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = t2.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked5.Count() > 0 && cheked3.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = t1.Id,
                                     cheked4 = 0,
                                     cheked5 = t2.Id,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked5.Count() > 0 && cheked4.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0 && cheked2.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked4
                                 join t2 in cheked5 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked6 on t2.NameSystem equals t3.NameSystem
                                 join t4 in keySum on t3.NameSystem equals t4.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked3 = 0,
                                     cheked4 = t1.Id,
                                     cheked5 = t2.Id,
                                     cheked2 = 0,
                                     cheked6 = t3.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t4.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t4.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked3.Count() > 0 && cheked4.Count>0 && cheked5.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = t4.Id,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked3.Count() > 0 && cheked5.Count > 0 && cheked4.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked5 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = 0,
                                     cheked5 = t4.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() + 
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked3.Count() > 0 && cheked6.Count > 0 && cheked4.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t3.Id,
                                     cheked4 = 0,
                                     cheked5 =0,
                                     cheked2 = t2.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked4.Count() > 0 && cheked5.Count > 0 && cheked3.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked5 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 =0,
                                     cheked4 = t3.Id,
                                     cheked5 = t4.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked4.Count() > 0 && cheked6.Count > 0 && cheked3.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked2.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked3.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = 0,
                                     cheked4 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count > 0 && cheked2.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked5 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = t3.Id,
                                     cheked5 = t4.Id,
                                     cheked2 = 0,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked6.Count > 0 && cheked2.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = 0,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }

            if (cheked.Count() > 0 && cheked3.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked2.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked3 = t2.Id,
                                     cheked4 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked2.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t2.Id,
                                     cheked3 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count > 0 && cheked.Count == 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked5 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = t3.Id,
                                     cheked3 = t2.Id,
                                     cheked5 = t4.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked6.Count > 0 && cheked.Count == 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked4 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = t3.Id,
                                     cheked3 = t2.Id,
                                     cheked5 = 0,
                                     cheked2 = t1.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked3.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked.Count == 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = 0,
                                     cheked3 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked.Count == 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = t2.Id,
                                     cheked3 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count > 0 && cheked.Count == 0 && cheked2.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked3
                                 join t2 in cheked4 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked6 on t3.NameSystem equals t4.NameSystem
                                 join t5 in keySum on t4.NameSystem equals t5.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = t2.Id,
                                     cheked3 = t1.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = t4.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t5.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t5.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Knaufzylinder = Join.Distinct().OrderBy(x => x.Cost).ToList();
            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked5 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t4.Id,
                                     cheked3 = t3.Id,
                                     cheked5 = t5.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = 0,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked6.Count() > 0 && cheked5.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked6 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t4.Id,
                                     cheked3 = t3.Id,
                                     cheked5 = 0,
                                     cheked2 = t2.Id,
                                     cheked6 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0 && cheked4.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked5 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked6 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = 0,
                                     cheked3 = t3.Id,
                                     cheked5 = t4.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked5.Count() > 0 && cheked4.Count() > 0 && cheked6.Count() > 0 && cheked3.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked6 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t4.Id,
                                     cheked3 = 0,
                                     cheked5 = t3.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked3.Count() > 0 && cheked.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0 && cheked2.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked6 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t4.Id,
                                     cheked3 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = 0,
                                     cheked6 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked3.Count() > 0 && cheked2.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0 && cheked.Count == 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked2
                                 join t2 in cheked3 on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked5 on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked6 on t4.NameSystem equals t5.NameSystem
                                 join t6 in keySum on t5.NameSystem equals t6.NameSysteam
                                 select new
                                 {
                                     cheked = 0,
                                     cheked4 = t4.Id,
                                     cheked3 = t2.Id,
                                     cheked5 = t3.Id,
                                     cheked2 = t1.Id,
                                     cheked6 = t5.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t6.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t6.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();

                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();

            }
            if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0)
            {
                int precision = 2;

                var keySum = db.SysteamPriceKey.ToList();

                var queryOrder = from t1 in cheked.Distinct()
                                 join t2 in cheked2.Distinct() on t1.NameSystem equals t2.NameSystem
                                 join t3 in cheked3.Distinct() on t2.NameSystem equals t3.NameSystem
                                 join t4 in cheked4.Distinct() on t3.NameSystem equals t4.NameSystem
                                 join t5 in cheked5.Distinct() on t4.NameSystem equals t5.NameSystem
                                 join t6 in cheked6.Distinct() on t5.NameSystem equals t6.NameSystem
                                 join t7 in keySum on t6.NameSystem equals t7.NameSysteam
                                 select new
                                 {
                                     cheked = t1.Id,
                                     cheked4 = t4.Id,
                                     cheked3 = t3.Id,
                                     cheked5 = t5.Id,
                                     cheked2 = t2.Id,
                                     cheked6 = t6.Id,
                                     userKey = keyUser.userKey,
                                     Name = t1.Name,
                                     companyName = t1.companyName,
                                     description = t7.DesctiptionsSysteam,
                                     NameSystem = t1.NameSystem,
                                     Cost = Math.Round(t1.Price * allUserListOrder.Where(x => x.ZylinderId == 1).Select(x => x.Count.Value).Sum() +
                                     t2.Price * allUserListOrder.Where(x => x.ZylinderId == 2).Select(x => x.Count.Value).Sum() +
                                     t3.Price * allUserListOrder.Where(x => x.ZylinderId == 3).Select(x => x.Count.Value).Sum() +
                                     t4.Price * allUserListOrder.Where(x => x.ZylinderId == 4).Select(x => x.Count.Value).Sum() +
                                     t5.Price * allUserListOrder.Where(x => x.ZylinderId == 5).Select(x => x.Count.Value).Sum() +
                                     t6.Price * allUserListOrder.Where(x => x.ZylinderId == 6).Select(x => x.Count.Value).Sum() + (t7.Price * isOpen.Count()), precision),
                                     ImageName = t1.ImageName,
                                 };

                var Join = queryOrder.Distinct().ToList();
                ViewBag.Doppel = Join.Distinct().OrderBy(x => x.Cost).ToList();
             
            }
            #endregion

            return View("System_Auswählen", keyUser);
        }


        [HttpGet]
        public IActionResult OrdersKey(string Systeam, int DopelId, List<string> dopelOption, string param2, int KnayfID, int Halb, int Hebel, int Aussen, int Vorhan)
        {
            var key = db.Orders.Where(x => x.userKey == param2).Distinct().ToList();

            var DopelOrderlist = new List<Profil_Doppelzylinder>();

            var OrderList = db.Profil_Doppelzylinder.Where(x => x.Id == Convert.ToInt32(DopelId)).ToList();

            var AussenInen = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == Convert.ToInt32(DopelId)).Select(x => new { x.aussen, x.Intern, x.costSizeIntern,x.costSizeAussen }).ToList();

            var Halbzylinder = new List<Profil_Halbzylinder>();

            var SelectHalbzylinder = db.Profil_Halbzylinder.Where(x => x.Id == Halb).ToList();

            var halbAussen_Inter = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halb).ToList();

            var KnaufZelinder = db.Profil_Knaufzylinder.Where(x => x.Id == KnayfID).ToList();

            var Kanyf_AussenInen = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == Convert.ToInt32(KnayfID)).ToList();

            var IsOpenValue = new List<isOpen_value>();

            var Vorhanschlos = new List<Vorhangschloss>();

            var SelectVorhanschlos = db.Vorhangschloss.Where(x => x.Id == Vorhan).ToList();

            var SizeVorhanschloss = db.Size.Where(x => x.VorhangschlossId == Vorhan).Select(x => x.sizeVorhangschloss).ToList();

            var listVorHanOptions = new List<int>();

            foreach (var list in SelectVorhanschlos)
            {
                var VorhanOptions = db.Vorhan_Options.Where(x => x.VorhangschlossId == list.Id).Select(x => x.Id).ToList();

                foreach (var s in VorhanOptions)
                {
                    listVorHanOptions.Add(s);
                }
            }

            ViewBag.VorhanschlossCount = listVorHanOptions.Count();


            var listVorHanOptionsValueName = new List<OptionsVorhan>();

            foreach (var ls in listVorHanOptions)
            {
                var listOptionVorhanValue = db.OptionsVorhan.Where(x => x.OptionId == ls).ToList();

                foreach (var lst in listOptionVorhanValue)
                {
                    listVorHanOptionsValueName.Add(lst);
                }
            }

            ViewBag.VorhanschlossOptionName = listVorHanOptionsValueName.Select(x => x.Name).ToList();

            ViewBag.VorhanschlossOptionNameJson = JsonConvert.SerializeObject(listVorHanOptionsValueName.Select(x => x.Name).ToList());

            var VorhanOptionValue = new List<OptionsVorhan_value>();

            foreach (var ls in listVorHanOptionsValueName)
            {
                var listOptionVorhanInfoValue = db.OptionsVorhan_value.Where(x => x.OptionsId == ls.Id).ToList();

                foreach (var lst in listOptionVorhanInfoValue)
                {
                    VorhanOptionValue.Add(lst);
                }
            }
            ViewBag.VorhanOptionCount = VorhanOptionValue.Count();

            ViewBag.VorhanValue = VorhanOptionValue.Select(x => x.Value).ToList();

            ViewBag.VorhanValueJson = JsonConvert.SerializeObject(VorhanOptionValue.Select(x => x.Value).ToList());
            ViewBag.VorhanValueCostJson = JsonConvert.SerializeObject(VorhanOptionValue.Select(x => x.Cost).ToList());

            ViewBag.VorhanSize = SizeVorhanschloss;

            float KhaufAussenCost = 0f;
            float DoppelAussenCost = 0f;
            float halbAussenCost = 0f;

            var Aussenzylinder = new List<Aussenzylinder_Rundzylinder>();

            var SelectAussenzylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == Aussen).ToList();

            var AussenOption = new List<int>();

            foreach (var x in SelectAussenzylinder)
            {
                var listOptionsAussenZylinder = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == x.Id).Select(x => x.Id).ToList();

                foreach (var f in listOptionsAussenZylinder)
                {
                    AussenOption.Add(f);
                }
                ViewBag.AussenCountOption = listOptionsAussenZylinder.Count();

            }

            var AussenListRundAll = new List<Aussen_Rund_all>();

            foreach (var ls in AussenOption)
            {
                var list = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == ls).ToList();
                foreach (var l in list)
                {
                    AussenListRundAll.Add(l);
                }
            }

            ViewBag.AussenName = AussenListRundAll.Select(x => x.Name).ToList();

            ViewBag.AussenNameJson = JsonConvert.SerializeObject(AussenListRundAll.Select(x => x.Name).ToList());

            ViewBag.AussenOptionSelected = key.Where(x => x.ZylinderId == 6).Select(x => x.Options).ToList();

            ViewBag.AussenOptionSelectedJson = JsonConvert.SerializeObject(key.Where(x => x.ZylinderId == 6).Select(x => x.Options).ToList());

            var AussenListvalue = new List<Aussen_Rouns_all_value>();

            foreach (var listValueAussen in AussenListRundAll)
            {
                var valueList = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == listValueAussen.Id).ToList();

                foreach (var f in valueList)
                {
                    AussenListvalue.Add(f);
                }
            }

            ViewBag.AussenValue = AussenListvalue.Select(x => x.Value).ToList();
            ViewBag.AussenValueJson = JsonConvert.SerializeObject(AussenListvalue.Select(x => x.Value).ToList());
            ViewBag.AussenValueCostJson = JsonConvert.SerializeObject(AussenListvalue.Select(x => x.Cost).ToList());

            var HelbZ = new List<Hebel>();
            var HebelZylinder = db.Hebelzylinder.Where(x => x.Id == Hebel).ToList();
            var HebelOption = new List<Hebelzylinder_Options>();

            foreach (var list in HebelZylinder)
            {
                var HebelOptions = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == list.Id).ToList();

                foreach (var Optionslist in HebelOptions)
                {
                    HebelOption.Add(Optionslist);
                }

            }

            ViewBag.CountOptionsHebel = HebelOption.Count();

            var HebelOptionListAll = new List<Options>();

            foreach (var list in HebelOption)
            {
                var HebelOptionsList = db.Options.Where(x => x.OptionId == list.Id).ToList();

                foreach (var Optionslist in HebelOptionsList)
                {
                    HebelOptionListAll.Add(Optionslist);
                }

            }

            ViewBag.OptionHebelName = HebelOptionListAll.Select(x => x.Name).Distinct().ToList();

            ViewBag.OptionsHebelNameJson = JsonConvert.SerializeObject(HebelOptionListAll.Select(x => x.Name).Distinct().ToList());

            var HebelOptionValueList = new List<Options_value>();

            foreach (var listValue in HebelOptionListAll)
            {
                var list = db.Options_value.Where(x => x.OptionsId == listValue.Id).ToList();
                foreach (var l in list)
                {
                    HebelOptionValueList.Add(l);
                }
            }

            var listAllValueHebel = new List<int>();

            listAllValueHebel.Add(HebelOptionValueList.Count());

            ViewBag.CountValueHebel = listAllValueHebel;

            ViewBag.ValueHebel = HebelOptionValueList.Select(x => x.Value).Distinct().ToList();

            ViewBag.HebelValueJson = JsonConvert.SerializeObject(HebelOptionValueList.Select(x => x.Value).ToList());

            ViewBag.HebelValueCostJson = JsonConvert.SerializeObject(HebelOptionValueList.Select(x => x.Cost).ToList());

            var queryableOptionsHalb = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Convert.ToInt32(Halb)).Select(x => x.Id).ToList();

            var OptionsHalb = new List<Halbzylinder_Options>();

            for (int f = 0; f < queryableOptionsHalb.Count(); f++)
            {
                var optionsHabel = db.Halbzylinder_Options.Where(x => x.OptionsId == queryableOptionsHalb[f]).ToList();

                foreach (var listHalb in optionsHabel)
                {
                    OptionsHalb.Add(listHalb);
                }
            }
            var OptionsValueHalb = new List<Halbzylinder_Options_value>();

            for (int t = 0; t < OptionsHalb.Count(); t++)
            {
                var listValueOptionsHalb = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == OptionsHalb[t].Id).ToList();
                foreach (var listvalue in listValueOptionsHalb)
                    OptionsValueHalb.Add(listvalue);
            }

            var listCountHalb = new List<int>();
            foreach (var f in OptionsHalb)
                listCountHalb.Add(f.Halbzylinder_Options_value.Count());

            ViewBag.countOptionsQueryHalb = queryableOptionsHalb.Count();

            ViewBag.HalbOptionsName = OptionsHalb.Select(x => x).ToList();

            ViewBag.HalbOptionsNameJson = JsonConvert.SerializeObject(OptionsHalb.Select(x => x.Name).ToList());

            ViewBag.HalbOptionsValue = OptionsValueHalb.Select(x => x.Value).ToList();

            ViewBag.HalbOptionsValueJson = JsonConvert.SerializeObject(OptionsValueHalb.Select(x => x.Value).ToList());

            ViewBag.HalbOptionsValueCount = listCountHalb.ToList();

            ViewBag.HalbOptionsValueCostJson = JsonConvert.SerializeObject(OptionsValueHalb.Select(x => x.Cost).ToList());

            var KnayfOrderlist = new List<Profil_Knaufzylinder>();

            int doppel = 0;
            int knayfC = 0;
            int hebelC = 0;
            int halbC = 0;
            int vorhanC = 0;
            int aussenC = 0;

            float SumcostedDopSylinder = 0;

            var  countDoppel = new List<int>();
            var countKnayf = new List<int>();
            var countHebel = new List<int>();
            var countHalb = new List<int>();
            var countVorhan = new List<int>();
            var countAussen = new List<int>();

            for (var i = 0; i < key.Count(); i++)
            {
                if (OrderList.Count() > 0)
                {

                    var counter = key.Where(x => x.ZylinderId == OrderList.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();
                    
                    foreach (int list in counter)
                    {
                        countDoppel.Add(list);
                    }

                    ViewBag.CounterTur = counter;

                    if (key[i].ZylinderId == OrderList.Select(x => x.schliessanlagenId).First())
                    {

                        if (counter[doppel].Value > 1)
                        {

                            var dopel = new Profil_Doppelzylinder
                            {
                                Id = OrderList.Last().Id,
                                Name = OrderList.Last().Name,
                                description = OrderList.Last().description,
                                companyName = OrderList.Last().companyName,
                                NameSystem = OrderList.Last().NameSystem,
                                Price = OrderList.Last().Price,
                                ImageFile = OrderList.Last().ImageFile,
                                ImageName = OrderList.Last().ImageName,
                                schliessanlagenId = OrderList.Last().schliessanlagenId
                            };
                            SumcostedDopSylinder += (OrderList.Last().Price * counter[doppel].Value) - OrderList.Last().Price;
                            DopelOrderlist.Add(dopel);
                        }
                        else
                        {
                            DopelOrderlist.Add(OrderList.Last());
                        }
                        doppel++;
                    }

                }

                if (KnaufZelinder.Count() > 0)
                {
                    if (key[i].ZylinderId == KnaufZelinder.Select(x => x.schliessanlagenId).First())
                    {
                        var counter = key.Where(x => x.ZylinderId == KnaufZelinder.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();

                        foreach (int list in counter)
                        {
                            countKnayf.Add(list);
                        }

                        ViewBag.CounterTurKnayf = counter;

                        if (counter[knayfC].Value > 1)
                        {
                            var knayf = new Profil_Knaufzylinder
                            {
                                Id = KnaufZelinder.Last().Id,
                                Name = KnaufZelinder.Last().Name,
                                description = KnaufZelinder.Last().description,
                                companyName = KnaufZelinder.Last().companyName,
                                NameSystem = KnaufZelinder.Last().NameSystem,
                                Price = KnaufZelinder.Last().Price,
                                ImageFile = KnaufZelinder.Last().ImageFile,
                                ImageName = KnaufZelinder.Last().ImageName,
                                schliessanlagenId = KnaufZelinder.Last().schliessanlagenId
                            };
                            SumcostedDopSylinder += (KnaufZelinder.Last().Price * counter[knayfC].Value) - KnaufZelinder.Last().Price;
                            KnayfOrderlist.Add(knayf);
                        }
                        else
                        {
                            KnayfOrderlist.Add(KnaufZelinder.Last());
                        }
                        knayfC++;
                    }

                }
                if (SelectHalbzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectHalbzylinder.Select(x => x.schliessanlagenId).First())
                    {
                        var counter = key.Where(x => x.ZylinderId == SelectHalbzylinder.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();

                        foreach (int list in counter)
                        {
                            countHalb.Add(list);
                        }

                        ViewBag.CounterTurHalb = counter;

                        if (counter[halbC].Value > 1)
                        {
                            var HalbNew = new Profil_Halbzylinder
                            {
                                Id = SelectHalbzylinder.Last().Id,
                                Name = SelectHalbzylinder.Last().Name,
                                description = SelectHalbzylinder.Last().description,
                                companyName = SelectHalbzylinder.Last().companyName,
                                NameSystem = SelectHalbzylinder.Last().NameSystem,
                                Price = SelectHalbzylinder.Last().Price,
                                ImageFile = SelectHalbzylinder.Last().ImageFile,
                                ImageName = SelectHalbzylinder.Last().ImageName,
                                schliessanlagenId = SelectHalbzylinder.Last().schliessanlagenId
                            };
                            SumcostedDopSylinder += (SelectHalbzylinder.Last().Price * counter[halbC].Value) - SelectHalbzylinder.Last().Price;
                            Halbzylinder.Add(HalbNew);
                        }
                        else
                        {
                            Halbzylinder.Add(SelectHalbzylinder.Last());
                        }
                        halbC++;
                    }

                }

                if (HebelZylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == HebelZylinder.Select(x => x.schliessanlagenId).First())
                    {
                        var counter = key.Where(x => x.ZylinderId == HebelZylinder.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();

                        foreach (int list in counter)
                        {
                            countHebel.Add(list);
                        }

                        ViewBag.CounterTurHebel = counter;

                        if (counter[hebelC].Value > 1)
                        {
                            var HebelNew = new Hebel
                            {
                                Id = HebelZylinder.Last().Id,
                                Name = HebelZylinder.Last().Name,
                                description = HebelZylinder.Last().description,
                                companyName = HebelZylinder.Last().companyName,
                                NameSystem = HebelZylinder.Last().NameSystem,
                                Price = HebelZylinder.Last().Price,
                                ImageFile = HebelZylinder.Last().ImageFile,
                                ImageName = HebelZylinder.Last().ImageName,
                                schliessanlagenId = HebelZylinder.Last().schliessanlagenId
                            };
                            SumcostedDopSylinder += (HebelZylinder.Last().Price * counter[hebelC].Value) - HebelZylinder.Last().Price;
                            HelbZ.Add(HebelNew);
                        }
                        else
                        {
                            HelbZ.Add(HebelZylinder.Last());
                        }
                        hebelC++;
                    }

                }
                if (SelectVorhanschlos.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectVorhanschlos.Select(x => x.schliessanlagenId).First())
                    {
                        var counter = key.Where(x => x.ZylinderId == SelectVorhanschlos.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();
                        
                        foreach (int list in counter)
                        {
                            countVorhan.Add(list);
                        }

                        ViewBag.CounterTurVorhan = counter;

                        if (counter[vorhanC].Value > 1)
                        {
                            var vorhan = new Vorhangschloss
                            {
                                Id = SelectVorhanschlos.Last().Id,
                                Name = SelectVorhanschlos.Last().Name,
                                description = SelectVorhanschlos.Last().description,
                                companyName = SelectVorhanschlos.Last().companyName,
                                NameSystem = SelectVorhanschlos.Last().NameSystem,
                                Price = SelectVorhanschlos.Last().Price,
                                ImageFile = SelectVorhanschlos.Last().ImageFile,
                                ImageName = SelectVorhanschlos.Last().ImageName,
                                schliessanlagenId = SelectVorhanschlos.Last().schliessanlagenId
                            };
                            float sum = SelectVorhanschlos.Last().Price * counter[vorhanC].Value;
                            SumcostedDopSylinder += sum - SelectVorhanschlos.Last().Price;
                            Vorhanschlos.Add(vorhan);
                        }
                        else
                        {
                            Vorhanschlos.Add(SelectVorhanschlos.Last());
                        }
                        vorhanC++;
                    }

                }

                if (SelectAussenzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectAussenzylinder.Select(x => x.schliessanlagenId).First())
                    {
                        var counter = key.Where(x => x.ZylinderId == SelectAussenzylinder.Select(x => x.schliessanlagenId).First()).Select(x => x.Count).ToList();

                        foreach (int list in counter)
                        {
                            countAussen.Add(list);
                        }

                        ViewBag.CounterTurAussen = counter;

                        if (counter[aussenC].Value > 1)
                        {
                            var aussen = new Aussenzylinder_Rundzylinder
                            {
                                Id = SelectAussenzylinder.Last().Id,
                                Name = SelectAussenzylinder.Last().Name,
                                description = SelectAussenzylinder.Last().description,
                                companyName = SelectAussenzylinder.Last().companyName,
                                NameSystem = SelectAussenzylinder.Last().NameSystem,
                                Price = SelectAussenzylinder.Last().Price,
                                ImageFile = SelectAussenzylinder.Last().ImageFile,
                                ImageName = SelectAussenzylinder.Last().ImageName,
                                schliessanlagenId = SelectAussenzylinder.Last().schliessanlagenId
                            };
                            SumcostedDopSylinder += (SelectAussenzylinder.Last().Price * counter[aussenC].Value) - SelectAussenzylinder.Last().Price;
                            Aussenzylinder.Add(aussen);
                        }
                        else
                        {
                            Aussenzylinder.Add(SelectAussenzylinder.Last());
                        }
                        aussenC++;
                    }

                }


            }

            var queryableOptions = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Convert.ToInt32(DopelId)).Select(x => x.Id).ToList();
            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {
                List<NGF> ngf = new List<NGF>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.NGF.Where(x => x.OptionsId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x).ToList();

                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject(ngf.Select(x => x.Name).ToList());

                List<NGF_Value> ngfList = new List<NGF_Value>();

                var oPvalueCount = new List<int>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.NGF_Value.Where(x => x.NGFId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    oPvalueCount.Add(opValue.Count());
                }
                ViewBag.optionValueCount = oPvalueCount;

                foreach (var order in key)
                {
                    var optionDopel = ngf.Where(x => x.Name == order.Options).ToList();
                    foreach (var option in optionDopel)
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

                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject(ngfList.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

                ViewBag.DoppelOptionSelected = key.Where(x => x.ZylinderId == 1).Select(x => x.Options).ToList();
            }

            if (queryableOptions.Count() == 0)
            {
                ViewBag.optionsPrise = JsonConvert.SerializeObject("");
                ViewBag.DoppelOptionsValue = JsonConvert.SerializeObject("");
                ViewBag.DoppelOptionsNameJson = JsonConvert.SerializeObject("");
            }

            var queryableOptionsKnayf = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == Convert.ToInt32(KnayfID)).Select(x => x.Id).ToList();
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

                ViewBag.optionsNameKnayf = ngf.Select(x => x).ToList();

                ViewBag.OptionsNameKnayfJson = JsonConvert.SerializeObject(ngf.Select(x => x.Name).ToList());

                List<Knayf_Options_value> ngfList = new List<Knayf_Options_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == ngf[s].Id).ToList();

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

                ViewBag.optionsValueKnayfJson = JsonConvert.SerializeObject(ngfList.Select(x => x.Value).ToList());

                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }

            if (queryableOptionsKnayf.Count() == 0)
            {
                ViewBag.optionsValueKnayfJson = JsonConvert.SerializeObject("");

                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject("");

                ViewBag.OptionsNameKnayfJson = JsonConvert.SerializeObject("");
            }
            var keyOpenOrder = new List<isOpen_Order>();

            foreach (var order in key)
            {
                var isOpen = db.isOpen_Order.Where(x => x.OrdersId == order.Id).ToList();

                foreach (var list in isOpen)
                    keyOpenOrder.Add(list);
            }


            foreach (var order in keyOpenOrder)
            {
                var opens = db.isOpen_value.Where(x => x.isOpen_OrderId == order.Id).ToList();
                foreach (var cheked in opens)
                    IsOpenValue.Add(cheked);
            }
            var ValueKeyOpen = new List<KeyValue>();

            var keykosted = IsOpenValue.GroupBy(item => item.NameKey)
           .Select(group => group.First()).ToList();



            foreach (var tl in IsOpenValue)
            {
                var listValueOpen = db.KeyValue.Where(x => x.OpenKeyId == tl.Id).ToList();
                var id = listValueOpen.Select(x => x.OpenKeyId).ToList();
                foreach (var tlr in listValueOpen)
                    ValueKeyOpen.Add(tlr);
            }

            ViewBag.Order = IsOpenValue.GroupBy(item => item.NameKey)
           .Select(group => group.First()).ToList();

            var keycount = IsOpenValue.GroupBy(item => item.NameKey)
           .Select(group => group.First()).ToList();

            var CountAussenDoppel = 0;
            var countZylinder = countDoppel.Distinct().ToList();
           
            int dpCount = 0;
            int knayfCount = 0;
            int HalbCount = 0;

            foreach (var order in key)
            {
                if (order.ZylinderId == 1)
                {
                    var DoppelSize = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == DopelOrderlist[dpCount].Id).ToList();

                    var nÍtem = new Profil_Doppelzylinder {

                        Id = DopelOrderlist[dpCount].Id,
                        Name = DopelOrderlist[dpCount].Name,
                        description = DopelOrderlist[dpCount].description,
                        companyName = DopelOrderlist[dpCount].companyName,
                        NameSystem = DopelOrderlist[dpCount].NameSystem,
                        Price = DopelOrderlist[dpCount].Price,
                        ImageFile = DopelOrderlist[dpCount].ImageFile,
                        ImageName = DopelOrderlist[dpCount].ImageName,
                        schliessanlagenId = DopelOrderlist[dpCount].schliessanlagenId
                    };

                    var aussen = DoppelSize.Min(x => x.aussen);
                    var aussenSize = DoppelSize.Where(x => x.aussen > aussen).Select(x => x.aussen).ToList();

                    var innen = DoppelSize.Min(x => x.Intern);
                    var innenSize = DoppelSize.Where(x => x.Intern > innen).Select(x => x.Intern).ToList();

                    foreach (var aus in aussenSize)
                    {
                        if (aus < order.aussen)
                        {
                            if (countZylinder.Count() > CountAussenDoppel)
                            {
                                for (int c = 0; c < countZylinder[CountAussenDoppel]; c++)
                                {
                                    var costAussen = DoppelSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                                    nÍtem.Price = nÍtem.Price + costAussen;
                                }
                            }
                            else
                            {
                                var costAussen = DoppelSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                                nÍtem.Price = nÍtem.Price + costAussen;
                            }
                          
                        }
                        else
                        {
                            if(countZylinder.Count() > CountAussenDoppel)
                            {
                                for (int c = 0; c < countZylinder[CountAussenDoppel]; c++)
                                {
                                    var costAussen = DoppelSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                                    nÍtem.Price = nÍtem.Price + costAussen;
                                    aussen = aus;
                                }
                            }
                            else
                            {
                                var costAussen = DoppelSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                                nÍtem.Price = nÍtem.Price + costAussen;
                                aussen = aus;
                            }
                            break;
                        }
                    }
                    foreach (var ine in innenSize)
                    {
                            if (ine < order.innen)
                            {
                                if (countZylinder.Count() > CountAussenDoppel)
                                {
                                     for (int c = 0; c < countZylinder[CountAussenDoppel]; c++)
                                     {
                                        var costIntern = DoppelSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                                        nÍtem.Price = nÍtem.Price + costIntern;
                                     }
                                }
                                else
                                {
                                     var costIntern = DoppelSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                                     nÍtem.Price = nÍtem.Price + costIntern;
                                }
                            }
                            else
                            {
                                if (countZylinder.Count() > CountAussenDoppel)
                                {
                                    for (int c = 0; c < countZylinder[CountAussenDoppel]; c++)
                                    {
                                        var costIntern = DoppelSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                                        nÍtem.Price = nÍtem.Price + costIntern;
                                        innen = ine;
                                    }
                                }
                                else
                                {
                                    var costIntern = DoppelSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                                    nÍtem.Price = nÍtem.Price + costIntern;
                                    innen = ine;
                                }

                              break;
                            }
 
                    }
                    DopelOrderlist[dpCount] = nÍtem;
                    CountAussenDoppel++;
                    dpCount++;
                }

                if (order.ZylinderId == 2)
                {
                    ViewBag.HalbAussen = order.aussen;

                    var SizeHalbzylinder = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halbzylinder[HalbCount].Id).ToList();

                    var nÍtem = new Profil_Halbzylinder
                    {
                        Id = Halbzylinder[HalbCount].Id,
                        Name = Halbzylinder[HalbCount].Name,
                        description = Halbzylinder[HalbCount].description,
                        companyName = Halbzylinder[HalbCount].companyName,
                        NameSystem = Halbzylinder[HalbCount].NameSystem,
                        Price = Halbzylinder[HalbCount].Price,
                        ImageFile = Halbzylinder[HalbCount].ImageFile,
                        ImageName = Halbzylinder[HalbCount].ImageName,
                        schliessanlagenId = Halbzylinder[HalbCount].schliessanlagenId
                    };

                    var aussen = SizeHalbzylinder.Min(x => x.aussen);

                    var aussenSize = SizeHalbzylinder.Where(x => x.aussen > aussen).Select(x => x.aussen).ToList();

                    foreach (var aus in aussenSize)
                    {
                        if (aus < order.aussen)
                        {
                            var costAussen = SizeHalbzylinder.FirstOrDefault(x => x.aussen == aus).costAussen;
                            nÍtem.Price = nÍtem.Price + costAussen;
                        }
                        else
                        {
                            var costAussen = SizeHalbzylinder.FirstOrDefault(x => x.aussen == aus).costAussen;
                            nÍtem.Price = nÍtem.Price + costAussen;
                            aussen = aus;
                            break;
                        }
                    }
                    Halbzylinder[HalbCount]= nÍtem;
                    HalbCount++;
                }
                if (order.ZylinderId == 3)
                {
                    var KnayfSize = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == KnayfOrderlist[knayfCount].Id).ToList();

                    var nÍtem = new Profil_Knaufzylinder
                    {
                        Id = KnayfOrderlist[knayfCount].Id,
                        Name = KnayfOrderlist[knayfCount].Name,
                        description = KnayfOrderlist[knayfCount].description,
                        companyName = KnayfOrderlist[knayfCount].companyName,
                        NameSystem = KnayfOrderlist[knayfCount].NameSystem,
                        Price = KnayfOrderlist[knayfCount].Price,
                        ImageFile = KnayfOrderlist[knayfCount].ImageFile,
                        ImageName = KnayfOrderlist[knayfCount].ImageName,
                        schliessanlagenId = KnayfOrderlist[knayfCount].schliessanlagenId
                    };

                    var aussen = KnayfSize.Min(x => x.aussen);
                    var aussenSize = KnayfSize.Where(x => x.aussen > aussen).Select(x => x.aussen).ToList();

                    var innen = KnayfSize.Min(x => x.Intern);
                    var innenSize = KnayfSize.Where(x => x.Intern> innen).Select(x => x.Intern).ToList();
                   
                    foreach (var aus in aussenSize)
                    {
                        if(aus < order.aussen)
                        {
                            var costAussen = KnayfSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                            nÍtem.Price = nÍtem.Price + costAussen;
                        }
                        else
                        {
                            var costAussen = KnayfSize.FirstOrDefault(x => x.aussen == aus).costSizeAussen;
                            nÍtem.Price = nÍtem.Price + costAussen;
                            aussen = aus;
                            break;
                        }
                    }
                    foreach (var ine in innenSize)
                    {
                        if (ine < order.innen)
                        {
                            var costIntern = KnayfSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                            nÍtem.Price = nÍtem.Price + costIntern;
                        }
                        else
                        {
                            var costIntern = KnayfSize.FirstOrDefault(x => x.Intern == ine).costSizeIntern;
                            nÍtem.Price = nÍtem.Price + costIntern;
                            innen = ine;
                            break;
                        }
                    }

                    KnayfOrderlist[knayfCount] = nÍtem;
                    knayfCount++;
                }
                if (order.ZylinderId == 5)
                {
                    ViewBag.Vorhangschloss = Vorhanschlos;

                    var Size = db.Size.Where(x => x.VorhangschlossId == Vorhanschlos.First().Id).ToList();

                    ViewBag.VorhanschlosSizeJson = JsonConvert.SerializeObject(Size.Select(x => x.sizeVorhangschloss).ToList());

                    ViewBag.VorhanschlosSizeCostedJson = JsonConvert.SerializeObject(Size.Select(x => x.Cost).ToList());

                }
            }

            var AussenD = key.Where(x => x.ZylinderId == 1).Select(x => x).ToList();
            var DaussenActual = new List<float>();
            var InenActual = new List<float>();

            var KnayfSizeItem = key.Where(x => x.ZylinderId == 3).Select(x => x).ToList();
            var KnayfaussenActual = new List<float>();
            var KnayfInenActual = new List<float>();

            var SizeHalb = key.Where(x => x.ZylinderId == 2).Select(x => x).ToList();
            var HalbaussenActual = new List<float>();

            foreach (var list in SizeHalb)
            {
                var SizeHalbzylinder = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halbzylinder.First().Id).ToList();
                var Aussenitem = SizeHalbzylinder.Where(x => x.aussen > list.aussen || x.aussen == list.aussen).Min(x => x.aussen);

                HalbaussenActual.Add(Aussenitem);
            }

            foreach (var list in KnayfSizeItem)
            {
                var KnayfSize = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == KnayfOrderlist.First().Id).ToList();
                var Aussenitem = KnayfSize.Where(x => x.aussen > list.aussen || x.aussen == list.aussen).Min(x => x.aussen);
                var Innenitem = KnayfSize.Where(x => x.aussen > list.innen || x.aussen == list.innen).Min(x => x.aussen);

                KnayfaussenActual.Add(Aussenitem);

                KnayfInenActual.Add(Innenitem);
            }
            foreach (var list in AussenD)
            {
                var DoppelSize = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == DopelOrderlist.First().Id).ToList();
                var Aussenitem = DoppelSize.Where(x => x.aussen > list.aussen || x.aussen == list.aussen).Min(x => x.aussen);
                var Innenitem = DoppelSize.Where(x => x.aussen > list.innen || x.aussen == list.innen).Min(x => x.aussen);

                DaussenActual.Add(Aussenitem);

                InenActual.Add(Innenitem);
            }

            if (Vorhanschlos.Count() == 0)
            {
                int SizeN = 0;
                ViewBag.VorhanschlosSizeJson = JsonConvert.SerializeObject(SizeN);
                ViewBag.VorhanschlosSizeCostedJson = JsonConvert.SerializeObject(SizeN);
            }

            var systeamkeyPrice = db.SysteamPriceKey.Where(x => x.NameSysteam == Systeam).Select(x => x.Price).ToList();

            ViewBag.KeyCost = JsonConvert.SerializeObject(systeamkeyPrice.First());

            var costKey = systeamkeyPrice.First() * keycount.Select(x=>x.CountKey).Sum();

            ViewBag.keySumCost = JsonConvert.SerializeObject(costKey);

            var HablAussen = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Convert.ToInt32(Halb)).ToList();

            ViewBag.SelectHalb = HalbaussenActual.ToList();

            ViewBag.Halb = Halbzylinder.ToList();

            if (Halbzylinder.Count() > 0)
            {
                ViewBag.HalbJson = JsonConvert.SerializeObject(Halbzylinder.ToList());
                ViewBag.CostHalbSize = JsonConvert.SerializeObject(HablAussen.Select(x => x.costAussen).ToList());
                ViewBag.AussenHalb = halbAussen_Inter.Select(x => x.aussen).ToList();

                var option = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Halbzylinder[0].Id).ToList();
                ViewBag.countOptionsQueryHalbI = option.Count();

                var halboptions = new List<Halbzylinder_Options>();

                foreach (var list in option)
                {
                    var item = db.Halbzylinder_Options.Where(x => x.OptionsId == list.Id).ToList();

                    foreach (var listI in item)
                    {
                        halboptions.Add(listI);
                    }
                }

                ViewBag.HalbOptionsNameJson = JsonConvert.SerializeObject(halboptions.Select(x => x.Name).ToList());


                var halbOptionsValue = new List<Halbzylinder_Options_value>();

                foreach (var value in halboptions)
                {
                    var item = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == value.Id).ToList();

                    foreach (var l in item)
                    {
                        halbOptionsValue.Add(l);
                    }
                }
                var L = new List<int>();

                foreach (var fs in halboptions)
                {
                    L.Add(fs.Halbzylinder_Options_value.Count());
                }

                ViewBag.HalbOptionsValueICount = L;

                ViewBag.HalbOptionsNameI = halboptions.Select(x => x).ToList();
                ViewBag.HalbOptionsValueI = halbOptionsValue.Select(x => x.Value).ToList();

                ViewBag.HalbOptionsValueJson = JsonConvert.SerializeObject(halbOptionsValue.Select(x => x.Value).ToList());
                ViewBag.HalbOptionsValueCostJson = JsonConvert.SerializeObject(halbOptionsValue.Select(x => x.Cost).ToList());
            }
            else
            {
                var HalbInfo = db.Profil_Halbzylinder.Where(x => x.NameSystem == Systeam).ToList();
                ViewBag.HalbJson = JsonConvert.SerializeObject(HalbInfo.ToList());

                var HalbInfoSize = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == HalbInfo[0].Id).ToList();

                ViewBag.CostHalbSize = JsonConvert.SerializeObject(HalbInfoSize.Select(x => x.costAussen).ToList());
                ViewBag.AussenHalb = HalbInfoSize.Select(x => x.aussen).ToList();

                var option = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == HalbInfo[0].Id).ToList();
                ViewBag.countOptionsQueryHalbI = option.Count();

                var halboptions = new List<Halbzylinder_Options>();

                foreach(var list in option)
                {
                    var item = db.Halbzylinder_Options.Where(x => x.OptionsId == list.Id).ToList();

                    foreach(var listI in item)
                    {
                        halboptions.Add(listI);
                    }
                }

                ViewBag.HalbOptionsNameJson = JsonConvert.SerializeObject(halboptions.Select(x => x.Name).ToList());
            

                var halbOptionsValue = new List<Halbzylinder_Options_value>();

                foreach(var value in halboptions)
                {
                    var item = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == value.Id).ToList();
                    
                    foreach(var l in item)
                    {
                        halbOptionsValue.Add(l);
                    }
                }
                var L = new List<int>();

                foreach (var fs in halboptions)
                {
                    L.Add(fs.Halbzylinder_Options_value.Count());
                }

                ViewBag.HalbOptionsValueICount = L;

                ViewBag.HalbOptionsNameI = halboptions.Select(x => x).ToList();
                ViewBag.HalbOptionsValueI = halbOptionsValue.Select(x => x.Value).ToList();

                ViewBag.HalbOptionsValueJson = JsonConvert.SerializeObject(halbOptionsValue.Select(x=>x.Value).ToList());
                ViewBag.HalbOptionsValueCostJson = JsonConvert.SerializeObject(halbOptionsValue.Select(x => x.Cost).ToList());
            }

            ViewBag.HalbItem = Halbzylinder.Select(x => x.Id).ToList();
            ViewBag.HalbItemJson = JsonConvert.SerializeObject(Halbzylinder.Select(x => x.Id).ToList());

            ViewBag.HalbAussenList = HablAussen.Select(x => x.aussen).Distinct().ToList();
            ViewBag.HalbOrderAussen = key.Where(x => x.ZylinderId == 2).Select(x => x.aussen).ToList();

            ViewBag.KnayfZelinder = KnayfOrderlist.ToList();

            ViewBag.KnayfItemId = KnayfOrderlist.Select(x => x.Id).ToList();

            ViewBag.KnayfItemIdJson = JsonConvert.SerializeObject(KnayfOrderlist.Select(x => x.Id).ToList());

            ViewBag.CountKey = IsOpenValue.Select(x => x.NameKey).Distinct().Count();


            if (KnayfOrderlist.Count() > 0)
            {
                ViewBag.KnayfZelinderJson = JsonConvert.SerializeObject(KnayfOrderlist.ToList());

                ViewBag.KnayfZelinderAussen = Kanyf_AussenInen.Select(x => x.aussen).Distinct().ToList();
                ViewBag.KnayfZelinderIntern = Kanyf_AussenInen.Select(x => x.Intern).Distinct().ToList();

                ViewBag.KnayfAussenCost = JsonConvert.SerializeObject(Kanyf_AussenInen.Select(x => x.costSizeAussen).ToList());
                ViewBag.KnayfInternCost = JsonConvert.SerializeObject(Kanyf_AussenInen.Select(x => x.costSizeIntern).ToList());

                ViewBag.KAussen = KnayfaussenActual.ToList();
                ViewBag.KInter = KnayfInenActual.ToList();


                var K_option = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == KnayfOrderlist[0].Id).ToList();

                var KnayfOptionList = new List<Knayf_Options>();

                foreach (var list in K_option)
                {
                    var OptionKnayf = db.Knayf_Options.Where(x => x.OptionsId == list.Id).ToList();

                    foreach (var item in OptionKnayf)
                    {
                        KnayfOptionList.Add(item);
                    }
                }

                ViewBag.OptionsNameKnayfJson = JsonConvert.SerializeObject(KnayfOptionList.Select(x => x.Name).ToList());
                ViewBag.optionsNameKnayfI = KnayfOptionList.Select(x => x).ToList();

                var KnayfOptionsValue = new List<Knayf_Options_value>();

                foreach (var list in KnayfOptionList)
                {
                    var OptionKnayf = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == list.Id).ToList();

                    foreach (var item in OptionKnayf)
                    {
                        KnayfOptionsValue.Add(item);
                    }
                }
                ViewBag.countOptionsQueryKnayfI = K_option.Count();

                var L = new List<int>();

                foreach (var fs in KnayfOptionList)
                {
                    L.Add(fs.Knayf_Options_value.Count());
                }


                ViewBag.countOptionsListKnayfI = L;

                ViewBag.optionsValueKnayfI = KnayfOptionsValue.Select(x => x.Value).ToList();
                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject(KnayfOptionsValue.Select(x => x.Cost).ToList());
                ViewBag.optionsValueKnayfJson = JsonConvert.SerializeObject(KnayfOptionsValue.Select(x => x.Value).ToList());
            }
            else
            {
                var KnayfInfo = db.Profil_Knaufzylinder.Where(x => x.NameSystem == Systeam).ToList();
                ViewBag.KnayfZelinderJson = JsonConvert.SerializeObject(KnayfInfo.ToList());

                var KnayfSize = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == KnayfInfo[0].Id).ToList();

                ViewBag.KnayfZelinderAussen = KnayfSize.Select(x => x.aussen).ToList();
                ViewBag.KnayfZelinderIntern = KnayfSize.Select(x => x.Intern).ToList();

                ViewBag.KnayfAussenCost = JsonConvert.SerializeObject(KnayfSize.Select(x => x.costSizeAussen).ToList());
                ViewBag.KnayfInternCost = JsonConvert.SerializeObject(KnayfSize.Select(x => x.costSizeIntern).ToList());

                var K_option = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == KnayfInfo[0].Id).ToList();

                var KnayfOptionList = new List<Knayf_Options>();

                foreach(var list in K_option)
                {
                    var OptionKnayf = db.Knayf_Options.Where(x => x.OptionsId == list.Id).ToList();

                    foreach(var item in OptionKnayf)
                    {
                        KnayfOptionList.Add(item);
                    }
                }

                ViewBag.OptionsNameKnayfJson = JsonConvert.SerializeObject(KnayfOptionList.Select(x => x.Name).ToList());
                ViewBag.optionsNameKnayfI = KnayfOptionList.Select(x => x).ToList();

                var KnayfOptionsValue = new List<Knayf_Options_value>();

                foreach (var list in KnayfOptionList)
                {
                    var OptionKnayf = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == list.Id).ToList();

                    foreach (var item in OptionKnayf)
                    {
                        KnayfOptionsValue.Add(item);
                    }
                }
                ViewBag.countOptionsQueryKnayfI = K_option.Count();

                var L = new List<int>();

                foreach (var fs in KnayfOptionList)
                {
                    L.Add(fs.Knayf_Options_value.Count());
                }


                ViewBag.countOptionsListKnayfI = L;

                ViewBag.optionsValueKnayfI = KnayfOptionsValue.Select(x => x.Value).ToList();
                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject(KnayfOptionsValue.Select(x => x.Cost).ToList());
                ViewBag.optionsValueKnayfJson = JsonConvert.SerializeObject(KnayfOptionsValue.Select(x => x.Value).ToList());

            }

            ViewBag.DopelzylinderIdList = DopelOrderlist.Select(x => x.Id).ToList();

            ViewBag.DopelzylinderIdJson = JsonConvert.SerializeObject(DopelOrderlist.Select(x => x.Id).ToList());

            ViewBag.Dopelzylinder = DopelOrderlist.ToList();

            if (DopelOrderlist.Count() > 0)
            {
                ViewBag.DopelzylinderJson = JsonConvert.SerializeObject(DopelOrderlist.ToList());

                ViewBag.DoppelzylinderDescriptions = JsonConvert.SerializeObject(DopelOrderlist.Select(x=>x.description).ToList());

                ViewBag.Dopelzylinderaussen = AussenInen.Select(x => x.aussen).ToList();

                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(AussenInen.Select(x => x.costSizeAussen).ToList());

                ViewBag.DopelzylinderIntern = AussenInen.Select(x => x.Intern).ToList();

                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(AussenInen.Select(x => x.costSizeIntern).ToList());

                var option = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == DopelOrderlist[0].Id).ToList();

                var ngf = new List<NGF>();
                var ngfValue = new List<NGF_Value>();

                foreach (var list in option)
                {
                    var items = db.NGF.Where(x => x.OptionsId == list.Id).ToList();

                    foreach (var i in items)
                    {
                        ngf.Add(i);
                    }
                }

                foreach (var list in ngf)
                {
                    var items = db.NGF_Value.Where(x => x.NGFId == list.Id).ToList();

                    foreach (var i in items)
                    {
                        ngfValue.Add(i);
                    }
                }

                ViewBag.countOptionsQueryI = option.Count();

                var L = new List<int>();

                foreach (var fs in ngf)
                {
                    L.Add(fs.NGF_Value.Count());
                }


                ViewBag.countOptionsListI = L;

                ViewBag.DoppelOptionsNameJsonI = JsonConvert.SerializeObject(ngf.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValueI = JsonConvert.SerializeObject(ngfValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfValue.Select(x => x.Cost).ToList());

                ViewBag.optionsNameI = ngf.Select(x => x).ToList();
                ViewBag.optionsValueI = ngfValue.Select(x => x.Value).ToList();
            }
            else
            {
                var DoppelInfo = db.Profil_Doppelzylinder.Where(x => x.NameSystem == Systeam).ToList();
                ViewBag.DopelzylinderJson = JsonConvert.SerializeObject(DoppelInfo);
                ViewBag.DoppelzylinderDescriptions = JsonConvert.SerializeObject(DoppelInfo.Select(x => x.description).ToList());
                
                var DSize = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == DoppelInfo[0].Id).ToList();
                ViewBag.Dopelzylinderaussen = DSize.Select(x=>x.aussen).ToList();
                ViewBag.DopelzylinderIntern = DSize.Select(x => x.Intern).ToList();

                ViewBag.CostDoppelAussen = JsonConvert.SerializeObject(DSize.Select(x => x.costSizeAussen).ToList());
                ViewBag.CostDoppelIntern = JsonConvert.SerializeObject(DSize.Select(x => x.costSizeIntern).ToList());

                var option = db.Profil_Doppelzylinder_Options.Where(x=>x.DoppelzylinderId == DoppelInfo[0].Id).ToList();
                
                var ngf = new List<NGF>();
                var ngfValue = new List<NGF_Value>();

                foreach (var list in option)
                {
                    var items = db.NGF.Where(x => x.OptionsId == list.Id).ToList();
                    
                    foreach(var i in items)
                    {
                        ngf.Add(i);
                    }
                }

                foreach(var list in ngf)
                {
                    var items = db.NGF_Value.Where(x => x.NGFId == list.Id).ToList();

                    foreach (var i in items)
                    {
                        ngfValue.Add(i);
                    }
                }

                ViewBag.countOptionsQueryI = option.Count();

                var L = new List<int>();

                foreach (var fs in ngf)
                {
                    L.Add(fs.NGF_Value.Count());
                }


                ViewBag.countOptionsListI = L;
                
                ViewBag.DoppelOptionsNameJsonI = JsonConvert.SerializeObject(ngf.Select(x => x.Name).ToList());
                ViewBag.DoppelOptionsValueI = JsonConvert.SerializeObject(ngfValue.Select(x => x.Value).ToList());
                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfValue.Select(x => x.Cost).ToList());

                ViewBag.optionsNameI = ngf.Select(x => x).ToList();
                ViewBag.optionsValueI = ngfValue.Select(x => x.Value).ToList();
            }


            ViewBag.DAussen = DaussenActual.ToList();

            ViewBag.DInter = InenActual.ToList();

            ViewBag.HelbZ = HelbZ.ToList();

            if (HelbZ.Count() > 0)
            {
                ViewBag.HelbZJson = JsonConvert.SerializeObject(HelbZ.ToList());
            }
            else
            {
                var HelbInfo = db.Hebelzylinder.Where(x => x.NameSystem == Systeam).ToList();
                ViewBag.HelbZJson = JsonConvert.SerializeObject(HelbInfo.ToList());
            }

            ViewBag.HelbItem = HelbZ.Select(x => x.Id).ToList();
            ViewBag.HelbItemJson = JsonConvert.SerializeObject(HelbZ.Select(x => x.Id).ToList());

            if (Vorhanschlos.Count() > 0)
            {
                ViewBag.Vorhanschlos = Vorhanschlos.ToList();
                ViewBag.VorhanschlosJson = JsonConvert.SerializeObject(Vorhanschlos.ToList());

                var Vsize = db.Size.Where(x => x.VorhangschlossId == Vorhanschlos[0].Id).ToList();

                ViewBag.VorhanSize = Vsize.Select(x => x.sizeVorhangschloss).ToList();
                ViewBag.VorhanschlosSizeJson = JsonConvert.SerializeObject(Vsize.Select(x => x.sizeVorhangschloss).ToList());
                ViewBag.VorhanschlosSizeCostedJson = JsonConvert.SerializeObject(Vsize.Select(x => x.Cost).ToList());
            }
            else
            {
                var VorhanInfo = db.Vorhangschloss.Where(x => x.NameSystem == Systeam).ToList();
                ViewBag.VorhanschlosJson = JsonConvert.SerializeObject(VorhanInfo.ToList());

                if (VorhanInfo.Count() > 0)
                {
                    var Vsize = db.Size.Where(x => x.VorhangschlossId == VorhanInfo[0].Id).ToList();

                    ViewBag.VorhanSize = Vsize.Select(x => x.sizeVorhangschloss).ToList();
                    ViewBag.VorhanschlosSizeJson = JsonConvert.SerializeObject(Vsize.Select(x => x.sizeVorhangschloss).ToList());
                    ViewBag.VorhanschlosSizeCostedJson = JsonConvert.SerializeObject(Vsize.Select(x => x.Cost).ToList());
                }
            }

            ViewBag.VorhanschlosItem = Vorhanschlos.Select(x => x.Id).ToList();
            ViewBag.VorhanschlosItemJson = JsonConvert.SerializeObject(Vorhanschlos.Select(x => x.Id).ToList());

            ViewBag.VorhanOrderAussen = key.Where(x => x.ZylinderId == 5).Select(x => x.aussen).ToList();

            ViewBag.Aussenzylinder = Aussenzylinder.ToList();


            if (Aussenzylinder.Count() > 0)
            {
                ViewBag.AussenzylinderJson = JsonConvert.SerializeObject(Aussenzylinder.ToList());
            }
            else
            {
                var AussenzylinderInfo = db.Aussenzylinder_Rundzylinder.Where(x => x.NameSystem == Systeam).ToList();

                ViewBag.AussenzylinderJson = JsonConvert.SerializeObject(AussenzylinderInfo.ToList());
            }

            

            ViewBag.AussenzylinderItem = Aussenzylinder.Select(x => x.Id).ToList();
            ViewBag.AussenzylinderItemJson = JsonConvert.SerializeObject(Aussenzylinder.Select(x => x.Id).ToList());

            ViewBag.KeyCount = IsOpenValue.Count;
            ViewBag.KeyValue = ValueKeyOpen.Select(x => x.isOpen).ToList();
            ViewBag.DorName = key.Select(x => x.DorName).Distinct().ToList();

            ViewBag.DornameJson = JsonConvert.SerializeObject(key.Select(x => x.DorName).Distinct().ToList());

            ViewBag.User = key.Select(x => x.userKey).Distinct().ToList();

            ViewBag.UserJson = JsonConvert.SerializeObject(key.Select(x => x.userKey).Distinct().ToList());

            ViewBag.AllType = db.Schliessanlagen.ToList();

            var SumCost = DopelOrderlist.Select(x => x.Price).Sum() + KnaufZelinder.Select(x => x.Price).Sum() + Halbzylinder.Select(x => x.Price).Sum() +
                HelbZ.Select(x => x.Price).Sum() + Vorhanschlos.Select(x => x.Price).Sum() + Aussenzylinder.Select(x => x.Price).Sum() + DoppelAussenCost
                + KhaufAussenCost + halbAussenCost + SumcostedDopSylinder + costKey;

            var SumCostProduct = DopelOrderlist.Select(x => x.Price).Sum() + KnaufZelinder.Select(x => x.Price).Sum() + Halbzylinder.Select(x => x.Price).Sum() +
                HelbZ.Select(x => x.Price).Sum() + Vorhanschlos.Select(x => x.Price).Sum() + Aussenzylinder.Select(x => x.Price).Sum() + costKey;

            int precision = 2;

            double Costed = Math.Round(SumCost, precision);

            double CostedProduct = Math.Round(SumCostProduct, precision);

            ViewBag.CostProducted = JsonConvert.SerializeObject(CostedProduct);

            ViewBag.Cost = Costed;

            return View("Finisher", key.Last());
        }

        [HttpPost]
        public ActionResult RemoveOrder(int data)
        { ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            string loginInform = ident.Claims.Select(x => x.Value).First();
            var users = db.Users.FirstOrDefault(x => x.Id == loginInform);

            var RemoveOrder = db.UserOrdersShop.Where(x => x.Id == data).ToList();

            var currentTime = RemoveOrder.First().createData.Value;

            var OrderProduct = db.ProductSysteam.Where(x => x.UserOrdersShopId == data).ToList();

            foreach (var listProduct in OrderProduct)
            {
                db.ProductSysteam.Remove(listProduct);

                foreach (var listOrder in RemoveOrder)
                {
                    db.UserOrdersShop.Remove(listOrder);

                }
            }
            string destinationFilePath = @$"wwwroot/Orders/{users.FirstName + users.LastName + currentTime.Minute + currentTime.Hour + currentTime.Day + currentTime.Month + currentTime.Year} OrderFile.xlsx";

            if (System.IO.File.Exists(destinationFilePath))
            {
                System.IO.File.Delete(destinationFilePath);
            }

            db.SaveChanges();
           
            return Redirect("/Identity/Account/Manage/PagePersonalOrders");
        }
        public async Task<IActionResult> SaveUserOrders(string user, List<string> TurName, List<string> DopelName, List<float> DoppelAussen, List<float> DoppelIntern
        ,List<string> DoppelOption, List<string> KnayfOption, List<string> HalbOption, List<string> HebelOption, List<string> VorhnaOption, List<string> AussenOption,
        List<string> KnayfName, List<float> KnayfAussen, List<float> KnayfIntern, List<string> HalbName, List<float> HalbAussen, List<string> HelbName,
        List<string> VorhanName, List<float> VorhanAussen, List<string> AussenName, string cost, List<string> key, List<bool> keyIsOpen, List<int> countKey,
        List<int> TurCounter,List<string> FurKey, string NameSystem)
        {
            var countItemOrder = DopelName.Count() + AussenName.Count() + VorhanName.Count() + KnayfName.Count() + HelbName.Count() + HalbName.Count();

            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            string loginInform = ident.Claims.Select(x => x.Value).First();
            var users = db.Users.FirstOrDefault(x => x.Id == loginInform);

            string result = cost.Replace("€", "");

            var Zylinder_Typ = db.Schliessanlagen.ToList();
            var profilD = db.Profil_Doppelzylinder.ToList();
            var profilH = db.Profil_Halbzylinder.ToList();
            var profilK = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var Vorhangschloss = db.Vorhangschloss.ToList();
            var Aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();
            var Orders = db.Orders.ToList();

            DateTime currentTime = DateTime.Now;

            string destinationFilePath = @$"wwwroot/Orders/{users.FirstName + users.LastName + currentTime.Minute+currentTime.Hour+currentTime.Day+currentTime.Month + currentTime.Year} OrderFile.xlsx";

            using (FileStream fstream = new FileStream(@$"wwwroot/Orders/{users.FirstName + users.LastName + currentTime.Minute + currentTime.Hour + currentTime.Day + currentTime.Month + currentTime.Year} OrderFile.xlsx", FileMode.OpenOrCreate))
            {
                fstream.Close();
            }

            string sourceFilePath = @"wwwroot/Orders/CES_schliessplan_DE_schliessanlagen.xlsx";


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

            FileInfo fileInfo = new FileInfo(destinationFilePath);

            var UserOrder = new Models.Users.UserOrdersShop
            {
                UserId = users.Id,
                ProductName = NameSystem,
                OrderSum = float.Parse(result.Trim()),
                OrderStatus = "Nicht bezahlt",
                count = 1,
                createData = DateTime.Now,
                UserOrderKey = user,
            };
            db.UserOrdersShop.Add(UserOrder);
            db.SaveChanges();

          

            int Rowcheked = 14;
            int row = 17;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Schließplan"];
                int value = 0;

                for (int i = 0; i < countKey.Count(); i++)
                {
                    for (int z = 0; z < TurCounter.Count(); z++)
                    {
                        if (keyIsOpen[value] == true)
                        {
                            worksheet.Cells[Rowcheked + z, row + i].Value = "X";

                        }
                        else
                        {
                            worksheet.Cells[Rowcheked + z, row + i].Value = "O";
                        }

                        value++;
                    }

                    for (int f = 9; f <= 13; f++)
                    {
                        worksheet.Cells[f, row + i].Value = key[i];
                    }
                    for (int f = 1; f <= 8; f++)
                    {
                        worksheet.Cells[f, row + i].Value = FurKey[i];
                    }
                    worksheet.Cells[$"O{Rowcheked + i}"].Value = countKey[i];
                }
 
                int DoppelCounter = 0;
                int KnayfCounter = 0;
                int HablCounter = 0;
                int HebelCounter = 0;
                int VorhanCounter = 0;
                int AussenCounter = 0;

                worksheet.Cells[$"C{5}"].Value = NameSystem;
                worksheet.Cells[$"C{8}"].Value = Orders.Select(x=>x.Id).Last();
                worksheet.Cells[$"C{9}"].Value = users.Address +"\n"+ users.FirstName + users.LastName;

                for (int i = 0; i < countItemOrder; i++)
                {
                    string Dor = "";
                    int countT = 0;
                    if(i > (TurName.Count()))
                    {
                        countT = TurCounter.Last();
                    }
                    else
                    {
                        Dor = TurName[i];
                    }
                    if (i > (TurCounter.Count()))
                    {
                       
                        countT = TurCounter.Last();
                    }
                    else
                    {
                       
                        countT = TurCounter[i];
                    }
                    worksheet.Cells[$"C{Rowcheked + i}"].Value = Dor;
                    worksheet.Cells[$"A{Rowcheked + i}"].Value = i+1;
                    worksheet.Cells[$"B{Rowcheked + i}"].Value = i + 1;
                    worksheet.Cells[$"H{Rowcheked + i}"].Value = countT;

                    if (DoppelCounter < DopelName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Profil-Doppelzylinder";

                        string Option = "";

                        if (DoppelCounter < DoppelOption.Count())
                        {
                            if (DoppelOption[DoppelCounter] == "Nein")
                            {
                                Option ="";
                            }
                            else
                            {
                                Option = DoppelOption[DoppelCounter];
                            }
                        }
                        else
                        {
                            Option = "";
                        }

                        worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;
                        worksheet.Cells[$"K{Rowcheked + i}"].Value = DoppelAussen[DoppelCounter];
                        worksheet.Cells[$"L{Rowcheked + i}"].Value = DoppelIntern[DoppelCounter];

                        var UserOrderProduct = new Models.Users.ProductSysteam
                        {
                            UserOrdersShopId = UserOrder.Id,
                            Name = DopelName[DoppelCounter],
                            Aussen = DoppelAussen[DoppelCounter],
                            Intern = DoppelIntern[DoppelCounter],
                            Count = countT
                        };

                        if (Option != "")
                        {
                            UserOrderProduct.Option = Option;
                        }

                        db.ProductSysteam.Add(UserOrderProduct);
                        db.SaveChanges();
                       
                        DoppelCounter++;
                    }

                    else if (KnayfCounter < KnayfName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Profil-Knaufzylinder";

                        string Option = "";

                        if (KnayfCounter < KnayfOption.Count())
                        {
                            Option = KnayfOption[KnayfCounter];
                        }
                        else
                        {
                            Option = "";
                        }

                        var UserOrderProduct = new Models.Users.ProductSysteam
                        {
                            UserOrdersShopId = UserOrder.Id,
                            Name = KnayfName[KnayfCounter],
                            Aussen = KnayfAussen[KnayfCounter],
                            Intern = KnayfIntern[KnayfCounter],
                            Count = countT
                        };
                        if (Option!="")
                        {
                            UserOrderProduct.Option = Option;
                        }

                        db.ProductSysteam.Add(UserOrderProduct);
                        db.SaveChanges();

                        worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;
                        worksheet.Cells[$"K{Rowcheked + i}"].Value = KnayfAussen[KnayfCounter];
                        worksheet.Cells[$"L{Rowcheked + i}"].Value = KnayfIntern[KnayfCounter];
                        KnayfCounter++;
                    }


                    else if (HablCounter < HalbName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Profil-Halbzylinder";

                        string Option = "";

                        if (HablCounter < HalbOption.Count())
                        {
                            Option = HalbOption[HablCounter];
                        }
                        else
                        {
                            Option = "";
                        }
                        var UserOrderProduct = new Models.Users.ProductSysteam
                        {
                            UserOrdersShopId = UserOrder.Id,
                            Name = HalbName[HablCounter],
                            Aussen = HalbAussen[HablCounter],
                            Count = countT
                        };
                        if (Option != "")
                        {
                            UserOrderProduct.Option = Option;
                        }
                        db.ProductSysteam.Add(UserOrderProduct);
                        db.SaveChanges();

                        worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;
                        worksheet.Cells[$"K{Rowcheked + i}"].Value = HalbAussen[HablCounter];

                        HablCounter++;
                    }

                    else if (HebelCounter < HelbName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Hebelzylinder";

                        string Option = "";

                        if (HebelCounter < HebelOption.Count())
                        {
                            Option = HebelOption[HebelCounter];
                        }
                        else
                        {
                            Option = "";
                        }

                        var UserOrderProduct = new Models.Users.ProductSysteam
                        {
                            UserOrdersShopId = UserOrder.Id,
                            Name = HelbName[HebelCounter],
                            Count = countT
                        };

                        if (Option != "")
                        {
                            UserOrderProduct.Option = Option;
                        }

                        db.ProductSysteam.Add(UserOrderProduct);
                        db.SaveChanges();
                        worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;

                        HebelCounter++;
                    }

                    else if (VorhanCounter < VorhanName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Vorhangschloss";
                        string Option = "";

                        if (VorhanCounter < VorhnaOption.Count())
                        {
                            Option = VorhnaOption[VorhanCounter];
                        }
                        else
                        {
                            Option = "";
                        }
                        
                        if (VorhanAussen.Count() > 0)
                        {
                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = VorhanName[VorhanCounter],
                                Aussen = VorhanAussen[VorhanCounter],
                                Count = countT
                            };
                            if (Option != "")
                            {
                                UserOrderProduct.Option = Option;
                            }

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();

                            worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;
                            worksheet.Cells[$"K{Rowcheked + i}"].Value = VorhanAussen[VorhanCounter];
                            VorhanCounter++;
                        }
                        else
                        {
                            var UserOrderProduct = new Models.Users.ProductSysteam
                            {
                                UserOrdersShopId = UserOrder.Id,
                                Name = VorhanName[VorhanCounter],
                                Count = countT
                            };
                            if (Option != "")
                            {
                                UserOrderProduct.Option = Option;
                            }

                            db.ProductSysteam.Add(UserOrderProduct);
                            db.SaveChanges();

                            worksheet.Cells[$"J{Rowcheked + i}"].Value = Option;
                            worksheet.Cells[$"K{Rowcheked + i}"].Value = "";
                            VorhanCounter++;
                           
                        }
                    }

                    else if (AussenCounter < AussenName.Count())
                    {
                        worksheet.Cells[$"G{Rowcheked + i}"].Value = "Aussenzylinder_Rundzylinder";
                        string Option = "";

                        if (AussenCounter < AussenOption.Count())
                        {
                            Option = AussenOption[AussenCounter];
                        }
                        else
                        {
                            Option = "";
                        }

                        var UserOrderProduct = new Models.Users.ProductSysteam
                        {
                            UserOrdersShopId = UserOrder.Id,
                            Name = AussenName[AussenCounter],
                        };
                        if (Option != "")
                        {
                            UserOrderProduct.Option = Option;
                        }

                        worksheet.Cells[$"J{Rowcheked+ i}"].Value = Option;
                        worksheet.Cells.AutoFitColumns();
                        db.ProductSysteam.Add(UserOrderProduct);
                        db.SaveChanges();
                        AussenCounter++;
                    }
                    package.Save();
                   
                   
                }
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Schlüssel Discount Store", "oceanwerbung@googlemail.com"));
                message.To.Add(new MailboxAddress(users.FirstName + users.LastName, users.UserName));
                message.Subject = "Schlüssel Discount Store";
                message.Body = new TextPart("plain")
                {
                    Text = "Text",
                };

                MemoryStream memoryStream = new MemoryStream();

                BodyBuilder bb = new BodyBuilder();

                using (var wc = new WebClient())
                {

                    bb.Attachments.Add("Email.xlsx",

                    wc.DownloadData(destinationFilePath));

                }

                message.Body = bb.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("oceanwerbung@googlemail.com", "bouo yqop xsdl qpar");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return Redirect("/Identity/Account/Manage/PagePersonalOrders");
            }

        }
      
        [HttpPost]
        public ActionResult SaveOrder(List<string>FurNameKey,string userName, Orders Key, List<string> Turname, 
        List<string> ZylinderId, List<float> aussen, List<float> innen, List<string> NameKey, List<int> CountKey, 
        string IsOppen,List<int>CountTur)
        {
            int CountOrders = Turname.Count();

            List<bool> isOpenList = new List<bool>();

            string pattern = @",\s*";

            string[] elements = Regex.Split(IsOppen, pattern);

            foreach (string element in elements)
            {
                if (element == "true")
                {
                    isOpenList.Add(true);
                }
                else
                {
                    isOpenList.Add(false);
                }

            }

            int aussenCounter = 0;
            int InterCounter = 0;

            string zylinderTyp;

            for (int i = 0; i < CountOrders; i++)
            {

                int idZylinder = 0;

                if (ZylinderId.Count() <= i)
                {
                    zylinderTyp = ZylinderId.Last();

                    if (zylinderTyp == "Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Knaufzylinder")
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
                    if (zylinderTyp == "Aussenzylinder")
                    {
                        idZylinder = 6;
                    }
                }
                else
                {
                    zylinderTyp = ZylinderId[i];

                    if (zylinderTyp == "Doppelzylinder")
                    {
                        idZylinder = 1;
                    }
                    if (zylinderTyp == "Halbzylinder")
                    {
                        idZylinder = 2;
                    }
                    if (zylinderTyp == "Knaufzylinder")
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
                    if (zylinderTyp == "Aussenzylinder")
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

                var orders = new Orders
                {
                    userKey = Key.userKey,
                    DorName = TurnameValue,
                    ZylinderId = idZylinder,
                    Created = DateTime.Now,
                    Count  = CountTur[i]
                };


                if (innen.Count() > 0)
                {
                    if (InterCounter > innen.Count() || idZylinder==2 || idZylinder == 4 || idZylinder == 5 || idZylinder == 6)
                    {

                    }
                    else
                    {
                        if (innen[InterCounter] == 0)
                        {
                            orders.innen = null;
                        }
                        else
                        {
                            orders.innen = innen[InterCounter];
                            InterCounter++;
                        }

                    }


                }
                if (aussen.Count() > 0)
                {
                    if (aussenCounter > aussen.Count()|| idZylinder == 4 || idZylinder == 5 || idZylinder == 6)
                    {

                    }
                    else
                    {
                        if (aussen[aussenCounter] == 0)
                        {
                            orders.aussen = null;
                        }
                        else
                        {
                            orders.aussen = aussen[aussenCounter];
                            aussenCounter++;
                        }

                    }

                }
                db.Orders.Add(orders);
                db.SaveChanges();

                var Open = new isOpen_Order
                {
                    OrdersId = orders.Id
                };
                db.isOpen_Order.Add(Open);
                db.SaveChanges();

            }
            var order_open = db.isOpen_Order.Select(x => x.Id).ToList();

            var d = 0;

            if (CountOrders > NameKey.Count())
            {
                var itemsCount = isOpenList.Count() / CountOrders;

                for (var s = 0; s < CountOrders; s++)
                {
                    string NameKeyValue;

                    int CountkeyOrders;
                    string FurNameKeyValue;

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
                    if (s >= FurNameKey.Count())
                    {
                        FurNameKeyValue = FurNameKey.Last();
                    }
                    else
                    {
                        FurNameKeyValue = FurNameKey[s];
                    }
                    var Open_value = new isOpen_value
                    {
                        isOpen_OrderId = order_open.Last(),
                        NameKey = NameKeyValue,
                        CountKey = CountkeyOrders,
                        ForNameKey = FurNameKeyValue
                    };

                    db.isOpen_value.Add(Open_value);

                    db.SaveChanges();

                    for (var f = 0; f < itemsCount; f++)
                    {
                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = isOpenList[d]
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            else
            {
                for (var s = 0; s < NameKey.Count(); s++)
                {
                    string NameKeyValue;
                    int CountkeyOrders;

                    var itemsCount =  CountOrders;

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

                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = isOpenList[d]
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            db.SaveChanges();
            return RedirectToAction("System_Auswählen", "Konfigurator", new { Key = Key.userKey});
        }

    }
}
