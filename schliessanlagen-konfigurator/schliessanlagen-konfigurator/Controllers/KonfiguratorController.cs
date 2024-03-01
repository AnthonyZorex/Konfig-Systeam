using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using Spire.Xls;

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
            var session = _contextAccessor.HttpContext.Session;
            var UserKey = session.Id;
            Orders user = new Orders();
            user.userKey = UserKey;
            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult>  System_Auswählen(Orders userKey)
        {

            var orders = await db.Orders.ToListAsync();
            var keyUser = orders.Last().userKey;
            var allUserListOrder = await db.Orders.Where(x => x.userKey == keyUser).ToListAsync();

            ViewBag.Zylinder_Typ = await db.Schliessanlagen.ToListAsync();
            var profilD = await db.Profil_Doppelzylinder.ToListAsync();
            var profilH = await db.Profil_Halbzylinder.ToListAsync();
            var profilK = await db.Profil_Knaufzylinder.ToListAsync();
            var hebel = await db.Hebelzylinder.ToListAsync();
            var Vorhangschloss = await db.Vorhangschloss.ToListAsync();
            var Aussenzylinder = await db.Aussenzylinder_Rundzylinder.ToListAsync();
            var Zylinder_Typ = await db.Schliessanlagen.ToListAsync();


            for (var d = 0; d < allUserListOrder.Count(); d++)
            {
                //if (allUserListOrder[d].ZylinderId == profilD[0].schliessanlagenId)
                //{
                

                //    if (profilD.Count() > d)
                //    {
                //        var pramaetrInnen = profilD[d].Intern;
                //        var pramaetrAussen = profilD[d].aussen;
                //        var sum = profilD[d].Cost;
                //        for (; pramaetrInnen <= allUserListOrder[d].innen;)
                //        {
                //            if (pramaetrInnen < allUserListOrder[d].innen)
                //            {
                //                pramaetrInnen = pramaetrInnen + 5.0f;
                //                sum = sum + 4;
                //            }

                //            if (pramaetrAussen != allUserListOrder[d].aussen)
                //            {
                //                sum = sum + 4;
                //                pramaetrAussen = pramaetrAussen + 5.0f;

                //            }
                //            if (allUserListOrder[d].innen == pramaetrInnen && allUserListOrder[d].aussen == pramaetrAussen)
                //            {
                //                var id = profilD[d].Id;
                //                var listDopel = new Profil_Doppelzylinder
                //                {
                //                    schliessanlagenId = profilD[d].schliessanlagenId,
                //                    Name = profilD[d].Name,
                //                    ImageName = profilD[d].ImageName,
                //                    ImageFile = profilD[d].ImageFile,
                //                    aussen = pramaetrAussen,
                //                    Artikelnummer = profilD[d].Artikelnummer,
                //                    Cost = sum,
                //                    Intern = pramaetrAussen

                //                };

                //                var allList = new List<Profil_Doppelzylinder>();
                //                allList.Add(listDopel);
                //                ViewBag.dopel = allList;
                //                break;
                //            }
                //        }
                //    }
                    
                   
                   
                       
                    
                //}
                if (allUserListOrder[d].ZylinderId == profilH[0].schliessanlagenId)
                {
                 

                    var HalbzylinderId = profilH[d].Id;
                    ViewBag.a = await db.Profil_Halbzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToListAsync();
                }
                if (allUserListOrder[d].ZylinderId == Zylinder_Typ[2].Id)
                {

                }
                if (allUserListOrder[d].ZylinderId == Zylinder_Typ[3].Id)
                {

                }
                if (allUserListOrder[d].ZylinderId == Zylinder_Typ[4].Id)
                {

                }
                if (allUserListOrder[d].ZylinderId == Zylinder_Typ[5].Id)
                {

                }
            }

            //for (int j = 0; j < profilD.Count(); j++)
            //{
            //    for (int i = 0; i < profilD.Count(); i++)
            //    {
            //        var id = profilD[j].Id;

            //        ViewBag.dopel = db.Profil_Doppelzylinder.Where(x => x.Id == id || x.Id < id).ToList();

            //        if (profilH.Count() > i)
            //        {
            //            var HalbzylinderId = profilH[i].Id;
            //            ViewBag.a = db.Profil_Halbzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
            //        }
            //        if (profilK.Count() > i)
            //        {
            //            var HalbzylinderId = profilK[i].Id;
            //            ViewBag.b = db.Profil_Knaufzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
            //        }
            //        if (hebel.Count() > i)
            //        {
            //            var HalbzylinderId = hebel[i].Id;
            //            ViewBag.c = db.Hebelzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
            //        }
            //        if (Vorhangschloss.Count() > i)
            //        {
            //            var HalbzylinderId = Vorhangschloss[i].Id;
            //            ViewBag.d = db.Vorhangschloss.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId);
            //        }
            //        if (Aussenzylinder.Count() > i)
            //        {
            //            var HalbzylinderId = Aussenzylinder[i].Id;
            //            ViewBag.f = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
            //        }

            //    }
            //}
            //userKey = allUserListOrder.GroupBy(x=>x.userKey).;
            return View("System_Auswählen", userKey);
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
        public ActionResult SaveOrder(Orders Key, List<string> Turname, List<int> ZylinderId, List<float> aussen, List<float> innen, List<int> Count, List<string> NameKey, List<int> CountKey, List<bool> IsOppen, List<bool> IsOppen2, List<bool> IsOppen3)
        {
            for (int i = 0; i < Turname.Count(); i++)
            {
                var orders = new Orders{
                    Tur = Turname[i],
                    userKey = Key.userKey,
                    ZylinderId = ZylinderId[i],
                    aussen = aussen[i],
                    innen = innen[i],
                    Count = Count[i],
                    NameKey = NameKey[i],
                    CountKey = CountKey[i],
                   
                };
                db.Orders.Add(orders); 
            }
            db.SaveChanges();



            return RedirectToAction("System_Auswählen");
        }
        
    }
}
