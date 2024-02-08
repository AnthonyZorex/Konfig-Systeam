using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models;
using System.Diagnostics;
using schliessanlagen_konfigurator.Data;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace schliessanlagen_konfigurator.Controllers
{
    public class HomeController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        public HomeController(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.item = db.Profil_Doppelzylinder;
            return View();
        }

        public async Task<IActionResult> Profil_KnaufzylinderRout()
        {
            ViewBag.item = db.Profil_Knaufzylinder;
            return View();
        }
        [HttpGet]
        public IActionResult HebelzylinderRout()
        {
            ViewBag.item = db.Hebelzylinder;
            return View();
        }
        [HttpGet]
        public IActionResult VorhangschlossRout()
        {
            ViewBag.item = db.Vorhangschloss;
            return View();
        }
        [HttpGet] 
        public IActionResult Aussenzylinder_RundzylinderRout()
        {
            ViewBag.item = db.Aussenzylinder_Rundzylinder;
            return View();
        }
        [HttpGet]
        public IActionResult Profil_HalbzylinderRout()
        {
            ViewBag.item = db.Profil_Halbzylinder;
            return View();
        }
        [HttpPost]
        //Schliessanlagen Create    
        public async Task<IActionResult> Create(Schliessanlagen schliessanlagen)
        {
            db.Schliessanlagen.Add(schliessanlagen);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create_Profil_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            string wwwRootPath = Environment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(profil_Doppelzylinder.ImageFile.FileName);

            string extension = Path.GetExtension(profil_Doppelzylinder.ImageFile.FileName);

            profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
            }
            db.Profil_Doppelzylinder.Add(profil_Doppelzylinder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Knaufzylinder(Profil_Knaufzylinder Profil_Knaufzylinder)
        {
                string wwwRootPath = Environment.WebRootPath;       
                
                 string fileName = Path.GetFileNameWithoutExtension(Profil_Knaufzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Knaufzylinder.ImageFile.FileName);

                Profil_Knaufzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                 string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                 using (var fileStream = new FileStream(path, FileMode.Create))
                 {
                    await Profil_Knaufzylinder.ImageFile.CopyToAsync(fileStream);
                 }
                 db.Profil_Knaufzylinder.Add(Profil_Knaufzylinder);
                 await db.SaveChangesAsync();
                 return RedirectToAction("Profil_KnaufzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Halbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            string wwwRootPath = Environment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(profil_Halbzylinder.ImageFile.FileName);

            string extension = Path.GetExtension(profil_Halbzylinder.ImageFile.FileName);

            profil_Halbzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profil_Halbzylinder.ImageFile.CopyToAsync(fileStream);
            }
            db.Profil_Halbzylinder.Add(profil_Halbzylinder);
            await db.SaveChangesAsync();
            return RedirectToAction("Profil_HalbzylinderRout");
        }

        public async Task<IActionResult> Create_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Doppelzylinder)
        {
            string wwwRootPath = Environment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(profil_Doppelzylinder.ImageFile.FileName);

            string extension = Path.GetExtension(profil_Doppelzylinder.ImageFile.FileName);

            profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
            }
            db.Aussenzylinder_Rundzylinder.Add(profil_Doppelzylinder);
            await db.SaveChangesAsync();
            return RedirectToAction("Aussenzylinder_RundzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Vorhangschloss(Vorhangschloss profil_Doppelzylinder)
        {
            string wwwRootPath = Environment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(profil_Doppelzylinder.ImageFile.FileName);

            string extension = Path.GetExtension(profil_Doppelzylinder.ImageFile.FileName);

            profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
            }
            db.Vorhangschloss.Add(profil_Doppelzylinder);
            await db.SaveChangesAsync();
            return RedirectToAction("VorhangschlossRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Hebelzylinder(Hebelzylinder profil_Doppelzylinder)
        {
            string wwwRootPath = Environment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(profil_Doppelzylinder.ImageFile.FileName);

            string extension = Path.GetExtension(profil_Doppelzylinder.ImageFile.FileName);

            profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
            }
            db.Hebelzylinder.Add(profil_Doppelzylinder);
            await db.SaveChangesAsync();
            return RedirectToAction("HebelzylinderRout");
        }
        [HttpGet]
        [Route("Home/Delete_Doppelzylinder")]
        
        public async Task<IActionResult> Delete_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder, Profil_Halbzylinder profil_Halbzylinder, Profil_Knaufzylinder Profil_Knaufzylinder, Vorhangschloss Vorhangschloss,Hebelzylinder hebelzylinder, Aussenzylinder_Rundzylinder aussenzylinder_Rundzylinder)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);

            var Halbzylinder = db.Profil_Halbzylinder.Find(profil_Halbzylinder.Id);

            var Knaufzylinder = db.Profil_Knaufzylinder.Find(Profil_Knaufzylinder.Id);

            var Vorhang = db.Vorhangschloss.Find(Vorhangschloss.Id);

            var hebel = db.Hebelzylinder.Find(hebelzylinder.Id);

            var aussenzylinder = db.Aussenzylinder_Rundzylinder.Find(aussenzylinder_Rundzylinder.Id);

            if (Knaufzylinder != null)
            {
                db.Profil_Knaufzylinder.Remove(Knaufzylinder);
                db.SaveChanges();
                return RedirectToAction("Profil_KnaufzylinderRout");
            }
            else if (hebel != null)
            {
                db.Hebelzylinder.Remove(hebel);
                db.SaveChanges();
                return RedirectToAction("HebelzylinderRout");
            }

           else  if (Vorhang != null)
            {
                db.Vorhangschloss.Remove(Vorhang);
                db.SaveChanges();
                return RedirectToAction("VorhangschlossRout");
            }
            else  if (Halbzylinder != null)
            {
                db.Profil_Halbzylinder.Remove(Halbzylinder);
                db.SaveChanges();
                return RedirectToAction("Profil_HalbzylinderRout");
            }

            else  if (doppelzylinder != null)
            {
                db.Profil_Doppelzylinder.Remove(doppelzylinder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else  if (aussenzylinder != null)
            {
                db.Aussenzylinder_Rundzylinder.Remove(aussenzylinder);
                db.SaveChanges();
                return RedirectToAction("Aussenzylinder_RundzylinderRout");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Home/Edit_Doppelzylinder")]
        public async Task<IActionResult> Edit_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);

            return View(doppelzylinder);
        }
        [HttpPost]
        public ActionResult Save(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            db.Profil_Doppelzylinder.Update(profil_Doppelzylinder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("Home/Edit_Halbzylinder")]
        public async Task<IActionResult> Edit_Halbzylinder(Hebelzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Hebelzylinder.Find(profil_Halbzylinder.Id);
            
            return View(Halbzylinder);
        }
        [HttpGet]
        [Route("Home/Edit_Aussenzylinder_Rundzylinder")]
        public async Task<IActionResult> Edit_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Aussenzylinder_Rundzylinder.Find(profil_Halbzylinder.Id);

            return View(Halbzylinder);
        }
        [HttpGet]
        [Route("Home/Edit_Edit_Vorhangschloss")]
        public async Task<IActionResult> Edit_Vorhangschloss(Vorhangschloss profil_Halbzylinder)
        {
            var Halbzylinder = db.Vorhangschloss.Find(profil_Halbzylinder.Id);

            return View(Halbzylinder);
        }

        [HttpPost]
        public ActionResult SaveHalbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            db.Profil_Halbzylinder.Update(profil_Halbzylinder);
            db.SaveChanges();
            return RedirectToAction("Profil_HalbzylinderRout");
        }
        [HttpPost]
        public ActionResult SaveAussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Halbzylinder)
        {
            db.Aussenzylinder_Rundzylinder.Update(profil_Halbzylinder);
            db.SaveChanges();
            return RedirectToAction("Aussenzylinder_RundzylinderRout");
        }
        [HttpPost]
        public ActionResult SaveVorhangschloss(Vorhangschloss profil_Halbzylinder)
        {
            db.Vorhangschloss.Update(profil_Halbzylinder);
            db.SaveChanges();
            return RedirectToAction("VorhangschlossRout");
        }
        public ActionResult DetailsProfil_Doppelzylinder(int id)
        {
           //var c = db.Options.FirstOrDefault(com => com.Id == id);
             
           ViewBag.c = db.Options.Where(x=>x.IdProfil_Doppelzylinder==id).ToList();
           return View();
            //if (c != null)
            //    return PartialView(c);
            ////return HttpNotFound();
            //return PartialView(c);
        }
        public ActionResult DetailsAussenzylinder_Rundzylinder(int id)
        {
            ViewBag.c = db.Options.Where(x => x.IdAussenzylinder_Rundzylinder == id).ToList();
            return View();
        }
        public ActionResult DetailsVorhangschloss(int id)
        {
            ViewBag.c = db.Options.Where(x => x.IdVorhangschloss == id).ToList();
            return View();  
        }
        public ActionResult DetailsHebelZylinder(int id)
        {
            ViewBag.c = db.Options.Where(x => x.IdHebelzylinder == id).ToList();
            return View();
        }
        [HttpGet]
        [Route("Home/Add_All_Options")]
        public ActionResult Add_All_Options(string sortOrder, string searchString)
        { 
            var dopelzylinder = db.Profil_Doppelzylinder.ToList();
            var halb = db.Profil_Halbzylinder.ToList();
            var knayf = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var vorhangschloos = db.Vorhangschloss.ToList();
            var aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();
            var options = db.Options.ToList();

            ViewBag.NameSortParm = System.String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            if (!System.String.IsNullOrEmpty(searchString))
            {
                dopelzylinder = dopelzylinder.Where(s => s.Name == searchString).ToList();
                if (dopelzylinder == null)
                {

                }
                if (halb == null)
                {

                }
            }

            ViewBag.dop = dopelzylinder;
            ViewBag.halb = halb;
            ViewBag.knayf = knayf;
            ViewBag.hebel = hebel;
            ViewBag.vorhangschloos = vorhangschloos;
            ViewBag.aussenzylinder = aussenzylinder;
            ViewBag.options = options;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create_Options(Options options)
        {
            if (options != null) {
                db.Options.Add(options);
                await db.SaveChangesAsync();
                return RedirectToAction("Add_All_Options");
            }
            return RedirectToAction("Add_All_Options");
        }

        [HttpPost]
        public async Task<IActionResult> Create_DopOptions(int id)
        {
            var dopelOptions =db.Profil_Doppelzylinder.Where(x=>x.Id == id).ToList();
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
