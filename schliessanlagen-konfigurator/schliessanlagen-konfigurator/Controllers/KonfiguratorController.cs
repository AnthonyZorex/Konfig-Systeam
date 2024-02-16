using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using System.Threading.Tasks;
using Spire.Xls;
using System.Web;
using Microsoft.AspNetCore.Http;
using schliessanlagenkonfigurator.Migrations;
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
      
        public IActionResult IndexKonfigurator()
        {
            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();
            var session = _contextAccessor.HttpContext.Session;
            var UserKey = session.Id;
            Orders user = new Orders();
            user.userKey = UserKey;
            return View(user);
        }

        public IActionResult System_Auswählen()
        {
            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();
            var profilD = db.Profil_Doppelzylinder.ToList();
            var profilH = db.Profil_Halbzylinder.ToList();
            var profilK = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var Vorhangschloss = db.Vorhangschloss.ToList();
            var Aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();

            for (int j = 0; j < profilD.Count(); j++)
            {
                for (int i = 0; i < profilD.Count(); i++)
                {
                    var id = profilD[j].Id;

                    ViewBag.dopel = db.Profil_Doppelzylinder.Where(x => x.Id == id || x.Id < id).ToList();

                    if (profilH.Count() > i)
                    {
                        var HalbzylinderId = profilH[i].Id;
                        ViewBag.a = db.Profil_Halbzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
                    }
                    if (profilK.Count() > i)
                    {
                        var HalbzylinderId = profilK[i].Id;
                        ViewBag.b = db.Profil_Knaufzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
                    }
                    if (hebel.Count() > i)
                    {
                        var HalbzylinderId = hebel[i].Id;
                        ViewBag.c = db.Hebelzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
                    }
                    if (Vorhangschloss.Count() > i)
                    {
                        var HalbzylinderId = Vorhangschloss[i].Id;
                        ViewBag.d = db.Vorhangschloss.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId);
                    }
                    if (Aussenzylinder.Count() > i)
                    {
                        var HalbzylinderId = Aussenzylinder[i].Id;
                        ViewBag.f = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == HalbzylinderId || x.Id < HalbzylinderId).ToList();
                    }

                }
            }

            return View();/* System_Auswählen*/
        }
       
        [HttpPost]
        public async Task<IActionResult> Create_Exel(List<string> tur, Profil_Doppelzylinder profil_Doppelzylinder, Profil_Halbzylinder profil_Halbzylinder, Profil_Knaufzylinder Profil_Knaufzylinder, Vorhangschloss Vorhang, Hebelzylinder hebelzylinder, Aussenzylinder_Rundzylinder aussenzylinder_Rundzylinder)
        {
            var Zylinder_Typ = db.Schliessanlagen.ToList();
            var profilD = db.Profil_Doppelzylinder.ToList();
            var profilH = db.Profil_Halbzylinder.ToList();
            var profilK = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var Vorhangschloss = db.Vorhangschloss.ToList();
            var Aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();

            //var listType = profilD.Where(x => x.Id == profilD[i]).Select(d => d.Extern);

            Workbook workbook = new Workbook();

            workbook.LoadFromFile("CES_schliessplan_DE_schliessanlagen.xltx");


            Worksheet worksheet = workbook.Worksheets[0];

            
            int count = 0;
            int Row = 18;
            for (int i = 0; i < profilD.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilD[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var Extern = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.Extern).ToList().First();
                var NameZ = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.Name).ToList().First();
                var Intern = profilD.Where(x => x.Id == profilD[count].Id).Select(d => d.Intern).ToList().First();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = $"{Extern}\t|\t{Intern}";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < profilH.Count(); i++)
            {

                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilH[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var Extern = profilH.Where(x => x.Id == profilH[count].Id).Select(d => d.Außen).ToList().First();
                var NameZ = profilH.Where(x => x.Id == profilH[count].Id).Select(d => d.Name).ToList().First();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = $"{Extern}";
                worksheet.Range[Row, 6].Value = $"";
                count++;
                Row++;
            }
            count = 0;
            for (int i = 0; i < profilK.Count(); i++)
            {
                var TypeSylinder = Zylinder_Typ.Where(x => x.Id == profilK[count].schliessanlagenId).Select(x => x.nameType).ToList().First();
                var Extern = profilK.Where(x => x.Id == profilK[count].Id).Select(d => d.Extern).ToList().First();
                var Intern = profilK.Where(x => x.Id == profilK[count].Id).Select(d => d.Intern).ToList().First();
                var NameZ = profilK.Where(x => x.Id == profilK[count].Id).Select(d => d.Name).ToList().First();

                worksheet.Range[Row, 1].Value = $"{Row - 1}";
                worksheet.Range[Row, 2].Value = $"{Row - 1}";
                worksheet.Range[Row, 3].Value = $"{NameZ}";
                worksheet.Range[Row, 4].Value = $"{TypeSylinder}";
                worksheet.Range[Row, 5].Value = $"{Extern}\t|\t{Intern}";
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
        public ActionResult SaveOrder( List<string> Turname, List<int> ZylinderId, List<float> aussen, List<float> innen, List<int> Count, List<string> NameKey, List<int> CountKey, List<bool> IsOppen, List<bool> IsOppen2, List<bool> IsOppen3)
        {
            for (int i = 0; i < Turname.Count(); i++)
            {

                var orders = new Orders{
                    Tur = Turname[i],
                    ZylinderId = ZylinderId[i],
                    IsOppen3 = IsOppen3[i],
                    aussen = aussen[i],
                    innen = innen[i],
                    Count = Count[i],
                    NameKey = NameKey[i],
                    CountKey = CountKey[i],
                    IsOppen = IsOppen[i],
                    IsOppen2 = IsOppen2[i]
                };
                
                db.Orders.Add(orders);
                db.SaveChanges();
            }
           

            
            return RedirectToAction("System_Auswählen");
        }
        
    }
}
