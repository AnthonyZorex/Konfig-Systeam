using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models;
using System.Diagnostics;
using schliessanlagen_konfigurator.Data;
using Microsoft.EntityFrameworkCore;

using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Halbzylinder;

using schliessanlagen_konfigurator.Models.Halbzylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Vorhan;
using schliessanlagen_konfigurator.Models.Hebelzylinder;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.Options;




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
        List<float> innen,List<string> valueNGF, List<float> costNGF,List<int> input_counter)
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


            int counter = 0;
            for (var i=0; i< Options.Count(); i++)
            {

                var dopOptions = new Profil_Doppelzylinder_Options
                {
                    DoppelzylinderId = s.Last(),

                };
                db.Profil_Doppelzylinder_Options.Add(dopOptions);
                db.SaveChanges();

                var x = db.Profil_Doppelzylinder_Options.Select(x => x.Id).ToList();

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
              
                 for (var j=0; j< input_counter[i]; j++)
                 {
                        var ngfValue = new NGF_Value
                        {
                            NGFId = ng.Last(),
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.NGF_Value.Add(ngfValue);
                  
                    counter++;

                 }
                       
                        db.SaveChanges();
                   
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Create_Profil_Knaufzylinder(Profil_Knaufzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen,
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
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
            if (Options != null)
            {
                var dopOptions = new Profil_Knaufzylinder_Options
                {
                    Profil_KnaufzylinderId = s.Last(),
                };
                db.Profil_Knaufzylinder_Options.Add(dopOptions);
                db.SaveChanges();

                var x = db.Profil_Knaufzylinder_Options.Select(x => x.Id).ToList();

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
                    int counter = 0;
                    for (var j = 0; j < input_counter[i]; j++)
                    {
                        var ngfValue = new Knayf_Options_value
                        {
                            Knayf_OptionsId = ng.Last(),
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.Knayf_Options_value.Add(ngfValue);

                        counter++;

                    }

                    db.SaveChanges();
                }
           

            }
            return RedirectToAction("Profil_KnaufzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Halbzylinder(Profil_Halbzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen,
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
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

            db.Profil_Halbzylinder.Add(Profil_Doppelzylinder);

            db.SaveChanges();

            var s = db.Profil_Halbzylinder.Select(x => x.Id).ToList();

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen_Halbzylinder
                {
                    Profil_HalbzylinderId = s.Last(),
                    aussen = aussen[i]
                };
                db.Aussen_Innen_Halbzylinder.Add(ausse_innen);
                db.SaveChanges();
            }
            if (Options != null)
            {
                var dopOptions = new Profil_Halbzylinder_Options
                {
                    Profil_HalbzylinderId = s.Last(),

                };
                db.Profil_Halbzylinder_Options.Add(dopOptions);
                db.SaveChanges();

                var x = db.Profil_Halbzylinder_Options.Select(x => x.Id).ToList();

                int counter = 0;

                for (var i = 0; i < Options.Count(); i++)
                {
                    var ngf = new Halbzylinder_Options
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

                    db.Halbzylinder_Options.Add(ngf);
                    db.SaveChanges();

                    var ng = db.Halbzylinder_Options.Select(x => x.Id).ToList();

                    for (var j = 0; j < input_counter[i]; j++)
                    {
                        var ngfValue = new Halbzylinder_Options_value
                        {
                            Halbzylinder_OptionsId = ng.Last(),
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.Halbzylinder_Options_value.Add(ngfValue);

                        counter++;

                    }

                    db.SaveChanges();

                }
            }
               
            return RedirectToAction("Profil_HalbzylinderRout");
        }

        public async Task<IActionResult> Create_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile,List<string> valueNGF, List<float> costNGF, List<int> input_counter)
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

            db.Aussenzylinder_Rundzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            var s = db.Aussenzylinder_Rundzylinder.Select(x => x.Id).ToList();

            int counter = 0;
            for (var i = 0; i < Options.Count(); i++)
            {

                var dopOptions = new Aussen_Rund_options
                {
                    Aussenzylinder_RundzylinderId = s.Last(),

                };
                db.Aussen_Rund_options.Add(dopOptions);
                db.SaveChanges();

                var x = db.Aussen_Rund_options.Select(x => x.Id).ToList();

                var ngf = new Aussen_Rund_all
                {
                    Aussen_Rund_optionsId = x.Last(),
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

                db.Aussen_Rund_all.Add(ngf);
                db.SaveChanges();

                var ng = db.Aussen_Rund_all.Select(x => x.Id).ToList();

                for (var j = 0; j < input_counter[i]; j++)
                {
                    var ngfValue = new Aussen_Rouns_all_value
                    {
                        Aussen_Rund_allId = ng.Last(),
                        Value = valueNGF[counter],
                        Cost = costNGF[counter]
                    };
                    db.Aussen_Rouns_all_value.Add(ngfValue);

                    counter++;
                }

                db.SaveChanges();
               
            }
            return RedirectToAction("Aussenzylinder_RundzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Vorhangschloss(Vorhangschloss Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<string> valueNGF, List<float> costNGF, List<float> aussen, List<int> input_counter)
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

            db.Vorhangschloss.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            var s = db.Vorhangschloss.Select(x => x.Id).ToList();

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Models.Vorhan.Size
                {
                    VorhangschlossId = s.Last(),
                    sizeVorhangschloss = aussen[i],
                };
                db.Size.Add(ausse_innen);
                db.SaveChanges();
            }

            

            int counter = 0;
            for (var i = 0; i < Options.Count(); i++)
            {

                var dopOptions = new Vorhan_Options
                {
                    VorhangschlossId = s.Last(),

                };
                db.Vorhan_Options.Add(dopOptions);
                db.SaveChanges();

                var x = db.Vorhan_Options.Select(x => x.Id).ToList();

                var ngf = new OptionsVorhan
                {
                    OptioId = x.Last(),
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

                db.OptionsVorhan.Add(ngf);
                db.SaveChanges();

                var ng = db.OptionsVorhan.Select(x => x.Id).ToList();

                for (var j = 0; j < input_counter[i]; j++)
                {
                    var ngfValue = new OptionsVorhan_value
                    {
                        OptionsId = ng.Last(),
                        Value = valueNGF[counter],
                        Cost = costNGF[counter]
                    };
                    db.OptionsVorhan_value.Add(ngfValue);

                    counter++;
                }

                db.SaveChanges();

            }
            return RedirectToAction("VorhangschlossRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Hebelzylinder(Hebel Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
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

            db.Hebelzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            var s = db.Hebelzylinder.Select(x => x.Id).ToList();

            int counter = 0;

            for (var i = 0; i < Options.Count(); i++)
            {

                var dopOptions = new Hebelzylinder_Options
                {
                    HebelzylinderId = s.Last(),
                };
                db.Hebelzylinder_Options.Add(dopOptions);

                db.SaveChanges();

                var x = db.Hebelzylinder_Options.Select(x => x.Id).ToList();

                var ngf = new Options 
                {
                    OptionId = x.Last(),
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

                db.Options.Add(ngf);
                db.SaveChanges();

                var ng = db.Options.Select(x => x.Id).ToList();

                for (var j = 0; j < input_counter[i]; j++)
                {
                    var ngfValue = new Options_value
                    {
                        OptionsId = ng.Last(),
                        Value = valueNGF[counter],
                        Cost = costNGF[counter]
                    };
                    db.Options_value.Add(ngfValue);

                    counter++;
                }

                db.SaveChanges();

            }
            return RedirectToAction("HebelzylinderRout");
        }
        #endregion
        #region DelitZylinderItem
        [HttpGet]
        [Route("Home/Delete_Doppelzylinder")]

        public async Task<IActionResult> Delete_KnayfZylinder(int id)
        {
            var Knaufzylinder = db.Profil_Knaufzylinder.Find(id);

            var a = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == id).First();
                var option = db.Knayf_Options.Where(x => x.OptionsId == a.Id).First();
                var Size = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == id).First();
                var optionV = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == option.Id).ToList();

                db.Aussen_Innen_Knauf.Remove(Size);
                db.SaveChanges();
            db.Profil_Knaufzylinder.Remove(Knaufzylinder);
            db.SaveChanges();
            for (int i = 0; i < optionV.Count(); i++)
                {
                    db.Knayf_Options_value.Remove(optionV[i]);
                    db.SaveChanges();
                }

                if (option != null)
                {
                    db.Knayf_Options.Remove(option);
                    db.SaveChanges();
                    db.Profil_Knaufzylinder_Options.Remove(a);
                    db.SaveChanges();
                }
            

            return RedirectToAction("Profil_KnaufzylinderRout");
            
        }
        public async Task<IActionResult> Delete_Hebel(int id)
        {
            var hebel = db.Hebelzylinder.Find(id);
            var a = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel.Id).First();

            var option = db.Options.Where(x => x.OptionId == a.Id).First();

            var optionV = db.Options_value.Where(x => x.OptionsId == option.Id).ToList();
            db.Hebelzylinder.Remove(hebel);

            db.SaveChanges();
            for (int i = 0; i < optionV.Count(); i++)
            {
                db.Options_value.Remove(optionV[i]);
                db.SaveChanges();
            }
            if (option != null)
            {
                db.Options.Remove(option);
                db.SaveChanges();
                db.Hebelzylinder_Options.Remove(a);
                db.SaveChanges();
            }
          

            return RedirectToAction("HebelzylinderRout");

        }
        public async Task<IActionResult> Delete_Vorhan(int id)
        {
            var Vorhang = db.Vorhangschloss.Find(id);

            var a = db.Vorhan_Options.Where(x => x.VorhangschlossId == Vorhang.Id).First();

            var option = db.OptionsVorhan.Where(x => x.OptioId == a.Id).First();

            var optionV = db.OptionsVorhan_value.Where(x => x.OptionsId == option.Id).ToList();


            db.Vorhangschloss.Remove(Vorhang);
            db.SaveChanges();
            for (int i = 0; i < optionV.Count(); i++)
            {
                db.OptionsVorhan_value.Remove(optionV[i]);
                db.SaveChanges();
            }
            if (option != null)
            {
                db.OptionsVorhan.Remove(option);
                db.SaveChanges();
                db.Vorhan_Options.Remove(a);
                db.SaveChanges();
            }


            return RedirectToAction("VorhangschlossRout");

        }
        public async Task<IActionResult> Delete_Halbzylinder(int id)
        {
            var Halbzylinder = db.Profil_Halbzylinder.Find(id);

            var a = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Halbzylinder.Id).First();

            var option = db.Halbzylinder_Options.Where(x => x.OptionsId == a.Id).First();
            var Size = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halbzylinder.Id).First();
            var optionV = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == option.Id).ToList();
            db.Profil_Halbzylinder.Remove(Halbzylinder);
            db.SaveChanges();
            for (int i = 0; i < optionV.Count(); i++)
            {
                db.Halbzylinder_Options_value.Remove(optionV[i]);
                db.SaveChanges();
            }
            if (option != null)
            {
                db.Halbzylinder_Options.Remove(option);
                db.SaveChanges();
                db.Profil_Halbzylinder_Options.Remove(a);
                db.SaveChanges();
            }
          

            return RedirectToAction("Profil_HalbzylinderRout");
        }
        public async Task<IActionResult> Delete_Aussen(int id)
        {
            var aussenzylinder = db.Aussenzylinder_Rundzylinder.Find(id);

           
                var a = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == aussenzylinder.Id).First();

                var option = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == a.Id).First();

                var optionV = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == option.Id).ToList();

                db.Aussen_Rund_options.Remove(a);
                db.SaveChanges();
                for (int i = 0; i < optionV.Count(); i++)
                {
                    db.Aussen_Rouns_all_value.Remove(optionV[i]);
                    db.SaveChanges();
                }
                if (option != null)
                {
                    db.Aussen_Rund_all.Remove(option);
                    db.SaveChanges();
                    db.Aussen_Rund_options.Remove(a);
                    db.SaveChanges();
                }
              

            db.Aussenzylinder_Rundzylinder.Remove(aussenzylinder);
            db.SaveChanges();

            return RedirectToAction("Aussenzylinder_RundzylinderRout");
          
        }
        public async Task<IActionResult> Delete_Doppelzylinder(int id)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(id);

                var a = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == doppelzylinder.Id).ToList();

                var option = new List<NGF>()

;               for (int i = 0; i < a.Count(); i++)
                {
                    var of = db.NGF.Where(x => x.OptionsId == a[i].Id).ToList();
                    
                    for (int j = 0; j < of.Count(); j++)
                    {
                        option.Add(of[j]);
                    }
                    
                }
           

            var Size = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == doppelzylinder.Id).ToList();

            for (int i = 0; i < a.Count(); i++)
            {
                db.Aussen_Innen.Remove(Size[i]);
                db.SaveChanges();
            }

                for (int i = 0; i < option.Count(); i++)
                {
                    var optionV = db.NGF_Value.Where(x => x.NGFId == option[i].Id).ToList();
                    for (int j = 0; j < optionV.Count(); j++)
                    {
                        db.NGF_Value.Remove(optionV[j]);
                        db.SaveChanges();
                    }
                    db.NGF.Remove(option[i]);
                    db.SaveChanges();
                }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Profil_Doppelzylinder_Options.Remove(a[i]);
                db.SaveChanges();
            }
                

            db.Profil_Doppelzylinder.Remove(doppelzylinder);
            db.SaveChanges();

            return RedirectToAction("Index");
         
        }
        #endregion
        #region EditForm
        [HttpGet]
        public async Task<IActionResult> Edit_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);

            ViewBag.AussenProduct = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).Select(x => x.aussen).ToList();
            ViewBag.InternProduct = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).Select(x => x.Intern).ToList();


            var dopelOptions = db.Profil_Doppelzylinder_Options.Where(x=>x.DoppelzylinderId == profil_Doppelzylinder.Id).Select(x => x.Id).ToList();

            var options = new List<NGF>();
            var value = new List<NGF_Value>();
          

            for (int i = 0; i < dopelOptions.Count(); i++)
            {
                var s = db.NGF.Where(x => x.OptionsId == dopelOptions[i]).ToList();
                for (int j = 0; j < s.Count(); j++)
                {
                    options.Add(s[j]);

                    var valueItem = db.NGF_Value.Where(x => x.NGFId == s[j].Id).ToList();
                   
                    ViewBag.countValue = valueItem.Count();

                    for (int d = 0; d < valueItem.Count(); d++)
                    {
                        value.Add(valueItem[d]);
                    }
                }
  
            }

            ViewBag.countOptions = options.Count();

            ViewBag.DopelOptionsName = options.Select(x=>x.Name).ToList();

            ViewBag.DopelOptionsValue = value.Select(x=>x.Value).ToList();

            ViewBag.DopelOptionsCost = value.Select(x => x.Cost).ToList();

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
        //Update
        public async Task<IActionResult> Save (Profil_Doppelzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen,
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
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

            db.Profil_Doppelzylinder.Update(Profil_Doppelzylinder);
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
                db.Aussen_Innen.Update(ausse_innen);
                db.SaveChanges();
            }


            int counter = 0;
            for (var i = 0; i < Options.Count(); i++)
            {

                var dopOptions = new Profil_Doppelzylinder_Options
                {
                    DoppelzylinderId = s.Last(),

                };
                db.Profil_Doppelzylinder_Options.Update(dopOptions);
                db.SaveChanges();

                var x = db.Profil_Doppelzylinder_Options.Select(x => x.Id).ToList();

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

                db.NGF.Update(ngf);
                db.SaveChanges();

                var ng = db.NGF.Select(x => x.Id).ToList();

                for (var j = 0; j < input_counter[i]; j++)
                {
                    var ngfValue = new NGF_Value
                    {
                        NGFId = ng.Last(),
                        Value = valueNGF[counter],
                        Cost = costNGF[counter]
                    };
                    db.NGF_Value.Update(ngfValue);

                    counter++;

                }

                db.SaveChanges();

            }
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

                    var queryableOptions = db.Profil_Doppelzylinder_Options.Where(x=>x.DoppelzylinderId== profil.Id).Select(x=>x.Id).ToList();

                    ViewBag.aussen = aussen;

                    ViewBag.innen = innen;
                    ViewBag.countOptions = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<NGF> ngf = new List<NGF>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.NGF.Where(x => x.OptionsId == queryableOptions[z]).ToList();
                    ngf.Add(allOptions.First());
                }



                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<NGF_Value> ngfList = new List<NGF_Value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.NGF_Value.Where(x => x.NGFId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);
                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }
            

            return View("ProductProfil_Doppelzylinder",profilInfo);
        }

        #endregion
 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
