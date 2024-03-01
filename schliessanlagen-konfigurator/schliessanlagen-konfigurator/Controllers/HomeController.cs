using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models;
using System.Diagnostics;
using schliessanlagen_konfigurator.Data;
using Microsoft.EntityFrameworkCore;

using schliessanlagen_konfigurator.IEnumerable.DoppelZylinder;
using Microsoft.AspNetCore.Mvc.Rendering;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using Microsoft.Extensions.Options;
using NuGet.ContentModel;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Halbzylinder;

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
       
       
        #region ViewZylinder
        public async Task<IActionResult> Index()
        {
            ViewBag.item = await db.Profil_Doppelzylinder.ToListAsync();
           
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
        #endregion
        #region CreateNewItemZylinder

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Doppelzylinder(Profil_Doppelzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile  postedFile, List<float> aussen,
        List<float> innen,List<string> valueNGF, List<float> costNGF)
        {

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Profil_Doppelzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            var s = db.Profil_Doppelzylinder.Select(x => x.Id).ToList();

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen
                {
                    Profil_DoppelzylinderId = s.Last(),
                    aussen = aussen[i],
                    Intern = innen[i]
                };
                db.Aussen_Innen.Add(ausse_innen);
                db.SaveChanges();
            }

            var dopOptions = new Profil_Doppelzylinder_Options
            {
                DoppelzylinderId = s.Last(),

            };
            db.Profil_Doppelzylinder_Options.Add(dopOptions);
            db.SaveChanges();

            var x = db.Profil_Doppelzylinder_Options.Select(x => x.Id).ToList();

            for (var i=0; i< Options.Count(); i++)
            {
                    var ngf = new NGF
                    {
                        OptionsId = x.Last(),
                        Name = Options[i],
                        Description = NGFDescriptions[i],
                        ImageFile = postedFile
                    };


                    if (ngf.ImageFile != null)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                        string extension = Path.GetExtension(ngf.ImageFile.FileName);

                        ngf.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await ngf.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    db.NGF.Add(ngf);
                    db.SaveChanges();

                    var ng = db.NGF.Select(x => x.Id).ToList();
                 for(var j=0; j<valueNGF.Count(); j++)
                 {
                    var ngfValue = new NGF_Value
                    {
                        NGFId = ng.Last(),
                        Value = valueNGF[j],
                        Cost = costNGF[j]
                    };
                    db.NGF_Value.Add(ngfValue);
                  }
                       
                        db.SaveChanges();
                   
            }
            return RedirectToAction("Index");
        }
        //Create_Profil_Knaufzylinder
        [HttpPost]
        public async Task<IActionResult> Create_Profil_Knaufzylinder(Profil_Knaufzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen,
        List<float> innen, List<string> valueNGF, List<float> costNGF)
        {

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Profil_Knaufzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            var s = db.Profil_Knaufzylinder.Select(x => x.Id).ToList();

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen_Knauf
                {
                    Profil_KnaufzylinderId = s.Last(),
                    aussen = aussen[i],
                    Intern = innen[i]
                };
                db.Aussen_Innen_Knauf.Add(ausse_innen);
                db.SaveChanges();
            }

            var dopOptions = new Profil_Knaufzylinder_Options
            {
                Profil_KnaufzylinderId = s.Last(),

            };
            db.Profil_Knaufzylinder_Options.Add(dopOptions);
            db.SaveChanges();

            var x = db.Profil_Doppelzylinder_Options.Select(x => x.Id).ToList();

            for (var i = 0; i < Options.Count(); i++)
            {
                var ngf = new Knayf_Options
                {
                    OptionsId = x.Last(),
                    Name = Options[i],
                    Description = NGFDescriptions[i],
                    ImageFile = postedFile
                };


                if (ngf.ImageFile != null)
                {
                    string wwwRootPath = Environment.WebRootPath;

                    string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                    string extension = Path.GetExtension(ngf.ImageFile.FileName);

                    ngf.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await ngf.ImageFile.CopyToAsync(fileStream);
                    }
                }

                db.Knayf_Options.Add(ngf);
                db.SaveChanges();

                var ng = db.Knayf_Options.Select(x => x.Id).ToList();
                for (var j = 0; j < valueNGF.Count(); j++)
                {
                    var ngfValue = new Knayf_Options_value
                    {
                        Knayf_OptionsId = ng.Last(),
                        Value = valueNGF[j],
                        Cost = costNGF[j]
                    };
                    db.Knayf_Options_value.Add(ngfValue);
                }

                db.SaveChanges();

            }
            return RedirectToAction("Profil_KnaufzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Halbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            try
            {
                if (profil_Halbzylinder.ImageFile != null)
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
                }
                db.Profil_Halbzylinder.Add(profil_Halbzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("Profil_HalbzylinderRout");

            }
            catch (Exception ex)
            {
                return new JsonResult(
                       new ErrorDto
                       {
                           IsError = true,
                           Message = ex.Message
                       });
            }
        }

        public async Task<IActionResult> Create_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Doppelzylinder)
        {
            try
            {
                if (profil_Doppelzylinder.ImageFile != null)
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
                }
                db.Aussenzylinder_Rundzylinder.Add(profil_Doppelzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("Aussenzylinder_RundzylinderRout");
            }
            catch (Exception ex)
            {
                return new JsonResult(
                      new ErrorDto
                      {
                          IsError = true,
                          Message = ex.Message
                      });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create_Vorhangschloss(Vorhangschloss profil_Doppelzylinder)
        {
            try
            {
                if (profil_Doppelzylinder.ImageFile != null)
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
                }
                db.Vorhangschloss.Add(profil_Doppelzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("VorhangschlossRout");
            }
            catch (Exception ex)
            {
                return new JsonResult(
                      new ErrorDto
                      {
                          IsError = true,
                          Message = ex.Message
                      });
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create_Hebelzylinder(Hebel profil_Doppelzylinder)
        {
            try
            {
                if (profil_Doppelzylinder.ImageFile != null)
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
                }
                db.Hebelzylinder.Add(profil_Doppelzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("HebelzylinderRout");
            }
            catch (Exception ex)
            {
                return new JsonResult(
                     new ErrorDto
                     {
                         IsError = true,
                         Message = ex.Message
                     });
            }


        }
        #endregion
        #region DelitZylinderItem
        [HttpGet]
        [Route("Home/Delete_Doppelzylinder")]

        public async Task<IActionResult> Delete_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder, Profil_Halbzylinder profil_Halbzylinder, Profil_Knaufzylinder Profil_Knaufzylinder, Vorhangschloss Vorhangschloss, Hebel hebelzylinder, Aussenzylinder_Rundzylinder aussenzylinder_Rundzylinder)
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

            else if (Vorhang != null)
            {
                db.Vorhangschloss.Remove(Vorhang);
                db.SaveChanges();
                return RedirectToAction("VorhangschlossRout");
            }
            else if (Halbzylinder != null)
            {
                db.Profil_Halbzylinder.Remove(Halbzylinder);
                db.SaveChanges();
                return RedirectToAction("Profil_HalbzylinderRout");
            }

            else if (doppelzylinder != null)
            {
                var a = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == doppelzylinder.Id).First();
                var option = db.NGF.Where(x => x.OptionsId == a.Id).First();
                var Size = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == doppelzylinder.Id).First();
                var optionV = db.NGF_Value.Where(x => x.NGFId == option.Id).First();

                db.Aussen_Innen.Remove(Size);
                db.SaveChanges();
                db.Profil_Doppelzylinder.Remove(doppelzylinder);
                db.SaveChanges();
                db.NGF_Value.Remove(optionV);
                db.SaveChanges();
                db.NGF.Remove(option);
                db.SaveChanges();
                db.Profil_Doppelzylinder_Options.Remove(a);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else if (aussenzylinder != null)
            {
                db.Aussenzylinder_Rundzylinder.Remove(aussenzylinder);
                db.SaveChanges();
                return RedirectToAction("Aussenzylinder_RundzylinderRout");
            }

            return RedirectToAction("Index");
        }
        #endregion
        #region EditForm
        [HttpGet]
        public async Task<IActionResult> Edit_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);

            return View("../Edit/Edit_Doppelzylinder", doppelzylinder);
        }

        [HttpGet]
        public async Task<IActionResult> Edit_Halbzylinder(Hebel profil_Halbzylinder)
        {
            var Halbzylinder = db.Hebelzylinder.Find(profil_Halbzylinder.Id);
            return View("../Edit/Edit_Halbzylinder", Halbzylinder);
        }

        [HttpGet]
        public async Task<IActionResult> Edit_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Aussenzylinder_Rundzylinder.Find(profil_Halbzylinder.Id);

            return View("../Edit/Edit_Aussenzylinder_Rundzylinder", Halbzylinder);
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Vorhangschloss(Vorhangschloss profil_Halbzylinder)
        {
            var Halbzylinder = db.Vorhangschloss.Find(profil_Halbzylinder.Id);

            return View("../Edit/Edit_Vorhangschloss", Halbzylinder);
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Profil_Halbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Profil_Halbzylinder.Find(profil_Halbzylinder.Id);

            return View("../Edit/Edit_Profil_Halbzylinder", Halbzylinder);
        }
        public async Task<IActionResult> Edit_Profil_Knaufzylinder(Profil_Knaufzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Profil_Knaufzylinder.Find(profil_Halbzylinder.Id);

            return View("../Edit/Edit_Profil_Knaufzylinder", Halbzylinder);
        }
        #endregion
        #region SaveEditForm
        [HttpPost]
        public ActionResult SaveHalbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            db.Profil_Halbzylinder.Update(profil_Halbzylinder);
            db.SaveChanges();
            return RedirectToAction("Profil_HalbzylinderRout");
        }
        [HttpPost]
        public ActionResult SaveHebelzylinder(Hebel profil_Halbzylinder)
        {
            db.Hebelzylinder.Update(profil_Halbzylinder);
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
        [HttpPost]
        public ActionResult Save(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            db.Profil_Doppelzylinder.Update(profil_Doppelzylinder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SaveProfil_Knaufzylinder(Profil_Knaufzylinder profil_Doppelzylinder)
        {
            db.Profil_Knaufzylinder.Update(profil_Doppelzylinder);
            db.SaveChanges();
            return RedirectToAction("Profil_KnaufzylinderRout");
        }
        #endregion
        #region Product
      

        [HttpGet]
        [Route("Home/ProductProfil_Doppelzylinder")]
        public ActionResult ProductProfil_Doppelzylinder(Profil_Doppelzylinder profil)
        {
            var profilInfo = db.Profil_Doppelzylinder.FirstOrDefault(x => x.Id == profil.Id);

                    var aussen = db.Aussen_Innen.Where(x=>x.Profil_DoppelzylinderId==profil.Id).Select(x => x.aussen).ToList();
                    var innen = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil.Id).Select(x => x.Intern).ToList();

                    
                    var dopelOptions = db.Profil_Doppelzylinder_Options.Where(x=>x.DoppelzylinderId == profil.Id).Select(x=>x.Id).First();

                    var allOptions = db.NGF.Where(x => x.OptionsId == dopelOptions).ToList();        

                    ViewBag.optionsName = allOptions.Select(x=>x.Name);

                    var opValue = db.NGF_Value.Where(x => x.NGFId == allOptions.First().Id).ToList();
                    
                    ViewBag.optionsValue = opValue.Select(x=>x.Value).ToList();

                     ViewBag.optionsPrise = opValue.Select(x => x.Cost).ToList();
                    
                    ViewBag.aussen = aussen;
                    ViewBag.innen = innen;
            

            return View("ProductProfil_Doppelzylinder",profilInfo);
        }

        #endregion
        #region Options
        [HttpGet]
        [Route("Home/Add_All_Options")]
        public ActionResult Add_All_Options(string sortOrder, string SearchDopppelzylinder, string SearchHalbzylinder, string SearchKnaufzylinder, string SearchHebelzylinder, string SearchVorhangschloss, string SearchAussenzylinder_Rundzylinder)
        {
            var dopelzylinder = db.Profil_Doppelzylinder.ToList();
            var halb = db.Profil_Halbzylinder.ToList();
            var knayf = db.Profil_Knaufzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var vorhangschloos = db.Vorhangschloss.ToList();
            var aussenzylinder = db.Aussenzylinder_Rundzylinder.ToList();
            var options = db.Profil_Doppelzylinder_Options.ToList();

            ViewBag.NameSortParm = System.String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (!System.String.IsNullOrEmpty(SearchDopppelzylinder))
            {
                dopelzylinder = dopelzylinder.Where(s => s.Name == SearchDopppelzylinder).ToList();
            }

            if (!System.String.IsNullOrEmpty(SearchHalbzylinder))
            {
                halb = halb.Where(s => s.Name == SearchHalbzylinder).ToList();
            }

            if (!System.String.IsNullOrEmpty(SearchKnaufzylinder))
            {
                knayf = knayf.Where(s => s.Name == SearchKnaufzylinder).ToList();
            }

            if (!System.String.IsNullOrEmpty(SearchHebelzylinder))
            {
                hebel = hebel.Where(s => s.Name == SearchHebelzylinder).ToList();
            }

            if (!System.String.IsNullOrEmpty(SearchVorhangschloss))
            {
                vorhangschloos = vorhangschloos.Where(s => s.Name == SearchVorhangschloss).ToList();
            }

            if (!System.String.IsNullOrEmpty(SearchAussenzylinder_Rundzylinder))
            {
                aussenzylinder = aussenzylinder.Where(s => s.Name == SearchAussenzylinder_Rundzylinder).ToList();
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
        public async Task<IActionResult> Create_Options(Profil_Doppelzylinder_Options options)
        {
            
            if (options != null)
            {
                var optionsItem = new Profil_Doppelzylinder_Options
                {

                };
                db.Profil_Doppelzylinder_Options.Add(optionsItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Add_All_Options");
            }
            return RedirectToAction("Add_All_Options");
        }
        #endregion 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
