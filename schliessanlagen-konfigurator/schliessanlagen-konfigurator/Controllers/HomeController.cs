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
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder.ValueOptions;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Models.Vorhan;
using System.Diagnostics;
using System.Security.Claims;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Text;
using System.Text.RegularExpressions;
namespace schliessanlagen_konfigurator.Controllers
{
    public class HomeController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> ImageConfig()
        {
            string sourceFilePath = @"wwwroot/Image/";

            IEnumerable<string> imageFiles = Directory.GetFiles(sourceFilePath, "*.png").Select(Path.GetFileName);

            return View("../Edit/ImageConfig", imageFiles);

        }
        [HttpGet]
        public async Task<IActionResult> SendMail()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Impressum()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMailpost(IFormFile file, string Html , string Css)
        {

            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        stream.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string fileContent = await reader.ReadToEndAsync();

                            int numberOfLines = fileContent.Split('\r').Length;

                            string[] lines = fileContent.Split('\n', '\r');
                            
                            for(int i=0; i<lines.Length; i++)
                            {
                                if (lines[i] != "")
                                {
                                    var message = new MimeMessage();
                                    message.From.Add(new MailboxAddress("Schlüssel Discount Store", "bonettaanthony466@gmail.com"));
                                    message.To.Add(new MailboxAddress(lines[i], lines[i]));
                                    message.Subject = "Schlüssel Discount Store";
                                    var builder = new BodyBuilder();
                                    builder.HtmlBody = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Email Template</title>
                   {Css}
                </head>
                <body>
                    {Html}
                </body>
                </html>
            ";


                                    message.Body = builder.ToMessageBody();

                                    using (var client = new SmtpClient())
                                    {
                                        client.Connect("smtp.gmail.com", 587, false);
                                        client.Authenticate("bonettaanthony466@gmail.com", "huqf ddvv mnba lcug ");
                                        client.Send(message);

                                        client.Disconnect(true);
                                    }
                                }
                                
                            } 
                        }
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ошибка при загрузке файла: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Пожалуйста, выберите файл для загрузки.";
            }
          

            return RedirectToAction("SendMail", "Home");
        }
        [HttpGet]
        public ActionResult Delete(string imageName)
        {
            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, imageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            return RedirectToAction("ImageConfig", "Home");
        }

        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Путь для сохранения файла на сервере (например, папка "uploads" в корне вашего веб-приложения)
                string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");

                // Создаем папку, если она не существует
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Сохраняем файл на сервере
                string filePath = Path.Combine(uploadFolderPath, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Возвращаем сообщение об успешной загрузке
                ViewBag.Message = "File uploaded successfully.";
            }
            else
            {
                // Возвращаем сообщение об ошибке, если файл не выбран
                ViewBag.Message = "Please select a file.";
            }

            return RedirectToAction("ImageConfig", "Home");

        }

        #region ViewZylinder
        public async Task<IActionResult> Index()
        {
            var AllDoppel = await db.Profil_Doppelzylinder.OrderBy(x => x.Price).Select(x=>x.NameSystem).ToListAsync();

            ViewBag.item = await db.Profil_Doppelzylinder.OrderBy(x=>x.Price).ToListAsync();

            var listPriceKey = new List<SysteamPriceKey>();
            
            foreach (var System in AllDoppel)
            {
                var itemKey = await db.SysteamPriceKey.Where(x => x.NameSysteam == System).ToListAsync();
               
                foreach(var i in itemKey)
                {
                    listPriceKey.Add(i);
                }
                
            }

            ViewBag.KeyCost =  listPriceKey.Select(x => x.Price).ToList();

            return View();
        }


        public async Task<IActionResult> Profil_KnaufzylinderRout()
        {
            ViewBag.item = db.Profil_Knaufzylinder.OrderBy(x => x.Price).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult HebelzylinderRout()
        {
            ViewBag.item = db.Hebelzylinder.OrderBy(x => x.Price).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult VorhangschlossRout()
        {
            ViewBag.item = db.Vorhangschloss.OrderBy(x => x.Price).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Aussenzylinder_RundzylinderRout()
        {
            ViewBag.item = db.Aussenzylinder_Rundzylinder.OrderBy(x => x.Price).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Profil_HalbzylinderRout()
        {
            ViewBag.item = db.Profil_Halbzylinder.OrderBy(x => x.Price).ToList();
            return View();
        }
        [HttpPost]   
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
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen,
        List<float> innen,List<float> costSizeAussen, List<float> costSizeIntern, List<string> valueNGF, List<float> costNGF, List<int> input_counter,string KeyName, float KeyCost)
        {

            var key = new SysteamPriceKey
            {
                NameSysteam = KeyName,
                Price = KeyCost,
            };
            db.SysteamPriceKey.Add(key);
            db.SaveChanges();

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
                    Intern = innen[i],
                    costSizeAussen = costSizeIntern[i],
                    costSizeIntern = costSizeIntern[i]
                };
                db.Aussen_Innen.Add(ausse_innen);
                db.SaveChanges();
            }


            int counter = 0;
            for (var i = 0; i < Options.Count(); i++)
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

                for (var j = 0; j < input_counter[i]; j++)
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
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter, List<float> costSizeAussen, List<float> costSizeIntern)
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
                    Intern = innen[i],
                    costSizeAussen = costSizeAussen[i],
                    costSizeIntern = costSizeIntern[i]
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
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter,List<float> costSizeAussen)
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
                    aussen = aussen[i],
                    costAussen = costSizeAussen[i]
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
        public async Task<IActionResult> Create_Vorhangschloss(Vorhangschloss Profil_Doppelzylinder,List<float> costSize,
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
                    Cost = costSize[i]
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

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, Knaufzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

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

            var OptionsCount = db.Hebelzylinder_Options.ToList();

            if (OptionsCount.Count() > 0)
            {
                var a = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel.Id).First();

                string sourceFilePath = @"wwwroot/Image/";

                string imagePathToDelete = Path.Combine(sourceFilePath, hebel.ImageName);

                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }
                var option = db.Options.Where(x => x.OptionId == a.Id).First();

                var optionV = db.Options_value.Where(x => x.OptionsId == option.Id).ToList();


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
            }
           

            db.Hebelzylinder.Remove(hebel);
            db.SaveChanges();
            return RedirectToAction("HebelzylinderRout");

        }
        public async Task<IActionResult> Delete_Vorhan(int id)
        {
            var Vorhang = db.Vorhangschloss.Find(id);

            var a = db.Vorhan_Options.Where(x => x.VorhangschlossId == Vorhang.Id).First();

            var option = db.OptionsVorhan.Where(x => x.OptionId == a.Id).First();

            var optionV = db.OptionsVorhan_value.Where(x => x.OptionsId == option.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, Vorhang.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

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

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, Halbzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

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

            var a = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == aussenzylinder.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, aussenzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<Aussen_Rund_all>();

            for (int i = 0; i < a.Count(); i++)
            {
                var of = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == a[i].Id).ToList();

                for (int j = 0; j < of.Count(); j++)
                {
                    option.Add(of[j]);
                }

            }
            for (int i = 0; i < option.Count(); i++)
            {
                var optionV = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == option[i].Id).ToList();
                for (int j = 0; j < optionV.Count(); j++)
                {
                    db.Aussen_Rouns_all_value.Remove(optionV[j]);
                    db.SaveChanges();
                }
                db.Aussen_Rund_all.Remove(option[i]);
                db.SaveChanges();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Aussen_Rund_options.Remove(a[i]);
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

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, doppelzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<NGF>();           
            
            for (int i = 0; i < a.Count(); i++)
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
        public async Task<IActionResult> Edit_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder, string data,int DoppelId)
        {
            if (data == null)
            {

                var Doppel = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);

                var AllDoppel = await db.Profil_Doppelzylinder.Where(x => x.NameSystem == Doppel.NameSystem).Select(x => x.NameSystem).ToListAsync();

                var listPriceKey = new List<SysteamPriceKey>();



                foreach (var System in AllDoppel)
                {
                    var itemKey = await db.SysteamPriceKey.Where(x => x.NameSysteam == System).ToListAsync();

                    foreach (var i in itemKey)
                    {
                        listPriceKey.Add(i);
                    }

                }

                ViewBag.AllDopel = db.Profil_Doppelzylinder.ToList();

                ViewBag.CountAllDoppel = db.Profil_Doppelzylinder.Select(x => x.Name).Count();

                ViewBag.KeyCost = listPriceKey.ToList();

                var SizeDoppel = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).ToList();
                var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Doppel.Id).ToList();

                if (options != null)
                {
                    var OptionsSylinder = new List<NGF>();

                    foreach (var item in options)
                    {
                        var Opt = db.NGF.Where(x => x.OptionsId == item.Id).ToList();

                        foreach (var list in Opt)
                        {
                            OptionsSylinder.Add(list);
                        }
                    }

                    ViewBag.Options = OptionsSylinder.ToList();

                    var ValueOption = new List<NGF_Value>();

                    var countV = new List<int>();

                    foreach (var list in OptionsSylinder)
                    {
                        var listValue = db.NGF_Value.Where(x => x.NGFId == list.Id).ToList();

                        foreach (var value in listValue)
                        {
                            ValueOption.Add(value);
                        }
                        if (listValue.Count() > 0)
                        {
                            countV.Add(listValue.Count());
                        }

                    }

                    ViewBag.CountOptions = countV.ToList();
                    ViewBag.OptionValue = ValueOption;

                    ViewBag.Size = SizeDoppel;

                    ViewBag.optionV = true;
                }
                else
                {
                    ViewBag.optionV = false;
                }
                return View("../Edit/Edit_Doppelzylinder", Doppel);
            }
            else
            {

                var Doppel = db.Profil_Doppelzylinder.Find(DoppelId);

                var AllDoppel = await db.Profil_Doppelzylinder.Where(x => x.NameSystem == Doppel.NameSystem).Select(x => x.NameSystem).ToListAsync();

                var listPriceKey = new List<SysteamPriceKey>();



                foreach (var System in AllDoppel)
                {
                    var itemKey = await db.SysteamPriceKey.Where(x => x.NameSysteam == System).ToListAsync();

                    foreach (var i in itemKey)
                    {
                        listPriceKey.Add(i);
                    }

                }

                ViewBag.AllDopel = db.Profil_Doppelzylinder.ToList();

                ViewBag.CountAllDoppel = db.Profil_Doppelzylinder.Select(x => x.Name).Count();

                ViewBag.KeyCost = listPriceKey.ToList();


                var dopel = db.Profil_Doppelzylinder.FirstOrDefault(x => x.Name == data);
                var SizeDoppel = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == dopel.Id).ToList();
                var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Doppel.Id).ToList();

                if (options != null)
                {
                    var OptionsSylinder = new List<NGF>();

                    foreach (var item in options)
                    {
                        var Opt = db.NGF.Where(x => x.OptionsId == item.Id).ToList();

                        foreach (var list in Opt)
                        {
                            OptionsSylinder.Add(list);
                        }
                    }

                    ViewBag.Options = OptionsSylinder.ToList();

                    var ValueOption = new List<NGF_Value>();

                    var countV = new List<int>();

                    foreach (var list in OptionsSylinder)
                    {
                        var listValue = db.NGF_Value.Where(x => x.NGFId == list.Id).ToList();

                        foreach (var value in listValue)
                        {
                            ValueOption.Add(value);
                        }
                        if (listValue.Count() > 0)
                        {
                            countV.Add(listValue.Count());
                        }

                    }

                    ViewBag.CountOptions = countV.ToList();
                    ViewBag.OptionValue = ValueOption;

                    ViewBag.Size = SizeDoppel;

                    ViewBag.optionV = true;
                }
                else
                {
                    ViewBag.optionV = false;
                }
                return View("../Edit/Edit_Doppelzylinder", Doppel);
            }
           
        
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoppelZylinder(Profil_Doppelzylinder profil_Doppelzylinder, List<int> SizeAus, List<int> SizeInen, List<string> Options,List<string> ImageNameOption,string Lieferzeit,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter,string NSysteam,float keyCost, string descriptionsSysteam,List<float> costSizeAussen, List<float> costSizeIntern)
        {
            var Items = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);
            Items.schliessanlagenId = profil_Doppelzylinder.schliessanlagenId;
            Items.Name = profil_Doppelzylinder.Name;
            Items.companyName = profil_Doppelzylinder.companyName;
            Items.NameSystem = profil_Doppelzylinder.NameSystem;
            Items.description = profil_Doppelzylinder.description;
            Items.Price = profil_Doppelzylinder.Price;
            Items.ImageName = profil_Doppelzylinder.ImageName;

            var option = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == profil_Doppelzylinder.Id).ToList();

            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<NGF>();

                foreach (var item in option)
                {
                    var OptionDescription = db.NGF.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.NGF_Value.Where(x => x.NGFId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.NGF_Value.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.NGF.Remove(list);
                }

                db.SaveChanges();

                foreach (var optionsList in option)
                {
                    db.Profil_Doppelzylinder_Options.Remove(optionsList);
                }
                db.SaveChanges();
            }
            if (Options.Count > 0)
            {
                int counter = 0;

                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Profil_Doppelzylinder_Options
                    {
                        DoppelzylinderId = Items.Id,
                    };

                    db.Profil_Doppelzylinder_Options.Add(createOptions);
                    db.SaveChanges();

                    var createOptionsAussen = new NGF
                    {
                        OptionsId = createOptions.Id,
                        Name = Options[i]
                        
                    };

                    if (Descriptions.Count() > 0)
                    {
                        createOptionsAussen.Description = Descriptions[i];
                    }
                    if (Descriptions.Count() == ImageNameOption.Count())
                    {
                      
                        createOptionsAussen.ImageName = ImageNameOption[i];
                        
                    }

                    db.NGF.Add(createOptionsAussen);
                    db.SaveChanges();

                    for (int f = 0; f < inputCounter[i]; f++)
                    {
                        var costedValue = new NGF_Value
                        {
                            NGFId = createOptionsAussen.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter],
                        };
                        db.NGF_Value.Add(costedValue);
                        counter++;
                    }
                    db.SaveChanges();
                }

            }

            var KnayfItemSize_Cost = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).ToList();
           
            foreach (var list in KnayfItemSize_Cost)
            {
                db.Aussen_Innen.Remove(list);
            }
            db.SaveChanges();

            for (int i = 0; i < SizeAus.Count(); i++)
            {
                var SizeV = new Aussen_Innen
                {
                    Profil_DoppelzylinderId = Items.Id,
                    aussen = SizeAus[i],
                    Intern = SizeInen[i],
                    costSizeAussen = costSizeAussen[i],
                    costSizeIntern = costSizeIntern[i],
                };
                db.Aussen_Innen.Add(SizeV);
            }
            db.SaveChanges();

            var Key = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Items.NameSystem).Id;

            var keyItem = db.SysteamPriceKey.Find(Key);
            keyItem.NameSysteam = NSysteam;
            keyItem.Price = keyCost;
            keyItem.DesctiptionsSysteam = descriptionsSysteam;
            keyItem.Lieferzeit = Lieferzeit;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Hebel(Hebel profil_Halbzylinder)
        {
            var Halbzylinder = db.Hebelzylinder.Find(profil_Halbzylinder.Id);

            var options = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == Halbzylinder.Id).ToList();
            
            var countV = new List<int>();

            if (options != null)
            {
                var OptionsSylinder = new List<Options>();

                foreach (var item in options)
                {
                    var Opt = db.Options.Where(x => x.OptionId == item.Id).ToList();
                    
                    foreach (var list in Opt)
                    {
                        OptionsSylinder.Add(list);
                    }
                }

                ViewBag.Options = OptionsSylinder.ToList();

                var ValueOption = new List<Options_value>();

                foreach (var list in OptionsSylinder)
                {
                    var listValue = db.Options_value.Where(x => x.OptionsId == list.Id).ToList();
                    foreach (var value in listValue)
                    {
                        ValueOption.Add(value);
                    }
                    countV.Add(listValue.Count());
                }
                ViewBag.OptionValue = ValueOption;

                ViewBag.optionV = true;
            }
            else
            {
                ViewBag.optionV = false;
            }

            ViewBag.CountOptions = countV.ToList();
            
            return View("../Edit/Edit_Hebel", Halbzylinder);
        }

        [HttpGet]
        public async Task<IActionResult> Edit_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder aussenzylinder)
        {
            var Auszylinder = db.Aussenzylinder_Rundzylinder.Find(aussenzylinder.Id);

            var options = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == Auszylinder.Id).ToList();
            var countV = new List<int>();

            if (options != null)
            {
                var OptionsSylinder = new List<Aussen_Rund_all>();

                foreach(var item in options)
                {
                    var Opt = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == item.Id).ToList();
                    foreach(var list in Opt)
                    {
                        OptionsSylinder.Add(list);
                    }
                }

                ViewBag.Options = OptionsSylinder.ToList();

                var ValueOption = new List<Aussen_Rouns_all_value>();
              
                foreach(var list in OptionsSylinder)
                {
                    var listValue = db.Aussen_Rouns_all_value.Where(x=>x.Aussen_Rund_allId == list.Id).ToList();
                    foreach(var value in listValue)
                    {
                        ValueOption.Add(value);
                    }
                    countV.Add(listValue.Count());
                }
                ViewBag.OptionValue = ValueOption;

                ViewBag.optionV = true;
            }
            else
            {
                ViewBag.optionV = false;
            }
            ViewBag.CountOptions = countV.ToList();
            return View("../Edit/Edit_Aussenzylinder_Rundzylinder", Auszylinder);
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Vorhangschloss(Vorhangschloss Vorhan)
        {
            var VorhanItem = db.Vorhangschloss.Find(Vorhan.Id);
            
            var countV = new List<int>();

            var VorhanSize = db.Size.Where(x => x.VorhangschlossId == Vorhan.Id).ToList();

            var options = db.Vorhan_Options.Where(x => x.VorhangschlossId == VorhanItem.Id).ToList();

            if (options != null)
            {
                var OptionsSylinder = new List<OptionsVorhan>();

                foreach (var item in options)
                {
                    var Opt = db.OptionsVorhan.Where(x => x.OptionId == item.Id).ToList();
                    foreach (var list in Opt)
                    {
                        OptionsSylinder.Add(list);
                    }
                }

                ViewBag.Options = OptionsSylinder.ToList();

          
                var ValueOption = new List<OptionsVorhan_value>();

                foreach (var list in OptionsSylinder)
                {
                    var listValue = db.OptionsVorhan_value.Where(x => x.OptionsId == list.Id).ToList();
                    foreach (var value in listValue)
                    {
                        ValueOption.Add(value);
                    }
                    countV.Add(listValue.Count());
                }
                ViewBag.OptionValue = ValueOption;

                ViewBag.optionV = true;
            }
            else
            {
                ViewBag.optionV = false;
            }


            ViewBag.Size = VorhanSize;
            ViewBag.CountOptions = countV.ToList();

            return View("../Edit/Edit_Vorhangschloss", VorhanItem);
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Profil_Halbzylinder(Profil_Halbzylinder profil_Halbzylinder)
        {
            var Halbzylinder = db.Profil_Halbzylinder.Find(profil_Halbzylinder.Id);

            var SizeHalbzylinder = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == profil_Halbzylinder.Id).ToList();

            var options = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Halbzylinder.Id).ToList();

            if (options != null)
            {
                var OptionsSylinder = new List<Halbzylinder_Options>();

                foreach (var item in options)
                {
                    var Opt = db.Halbzylinder_Options.Where(x => x.OptionsId == item.Id).ToList();
                    
                    foreach (var list in Opt)
                    {
                        OptionsSylinder.Add(list);
                    }
                }

                ViewBag.Options = OptionsSylinder.ToList();

                var ValueOption = new List<Halbzylinder_Options_value>();

                var countV = new List<int>();

                foreach (var list in OptionsSylinder)
                {
                    var listValue = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == list.Id).ToList();
                    foreach (var value in listValue)
                    {
                        ValueOption.Add(value);
                    }
                    if (listValue.Count() > 0)
                    {
                        countV.Add(listValue.Count());
                    }
                   
                }
                
                ViewBag.CountOptions = countV.ToList();   
                ViewBag.OptionValue = ValueOption;

                ViewBag.Size = SizeHalbzylinder;

                ViewBag.optionV = true;
            }
            else
            {
                ViewBag.optionV = false;
            }

            return View("../Edit/Edit_Profil_Halbzylinder", Halbzylinder);
        }
        public async Task<IActionResult> Edit_Profil_Knaufzylinder(Profil_Knaufzylinder profil_Halbzylinder)
        {
            var Knayf = db.Profil_Knaufzylinder.Find(profil_Halbzylinder.Id);

            var SizeHalbzylinder = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == profil_Halbzylinder.Id).ToList();

            var options = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == Knayf.Id).ToList();

            if (options != null)
            {
                var OptionsSylinder = new List<Knayf_Options>();

                foreach (var item in options)
                {
                    var Opt = db.Knayf_Options.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var list in Opt)
                    {
                        OptionsSylinder.Add(list);
                    }
                }

                ViewBag.Options = OptionsSylinder.ToList();

                var ValueOption = new List<Knayf_Options_value>();

                var countV = new List<int>();

                foreach (var list in OptionsSylinder)
                {
                    var listValue = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == list.Id).ToList();
                    foreach (var value in listValue)
                    {
                        ValueOption.Add(value);
                    }
                    if (listValue.Count() > 0)
                    {
                        countV.Add(listValue.Count());
                    }

                }

                ViewBag.CountOptions = countV.ToList();
                ViewBag.OptionValue = ValueOption;

                ViewBag.Size = SizeHalbzylinder;

                ViewBag.optionV = true;
            }
            else
            {
                ViewBag.optionV = false;
            }

            return View("../Edit/Edit_Profil_Knaufzylinder", Knayf);
        }
        #endregion
        #region SaveEditForm
        [HttpPost]
        public ActionResult SaveHalbzylinder(Profil_Halbzylinder profil_Halbzylinder, List<int> Size, List<float> CostSize, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter,List<float> costSizeAussen)
        {
            var Items = db.Profil_Halbzylinder.Find(profil_Halbzylinder.Id);
            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;

            var option = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == profil_Halbzylinder.Id).ToList();

            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<Halbzylinder_Options>();

                foreach (var item in option)
                {
                    var OptionDescription = db.Halbzylinder_Options.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.Halbzylinder_Options_value.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.Halbzylinder_Options.Remove(list);
                }

                db.SaveChanges();

                foreach (var optionsList in option)
                {
                    db.Profil_Halbzylinder_Options.Remove(optionsList);
                }
                db.SaveChanges();
            }
            if (Options.Count > 0)
            {
                int counter = 0;

                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Profil_Halbzylinder_Options
                    {
                        Profil_HalbzylinderId = Items.Id,
                    };

                    db.Profil_Halbzylinder_Options.Add(createOptions);
                    db.SaveChanges();

                    var createOptionsAussen = new Halbzylinder_Options
                    {
                        OptionsId = createOptions.Id,
                        Name = Options[i],                       
                    };

                    if (Descriptions.Count() > 0)
                    {
                        createOptionsAussen.Description = Descriptions[i];
                    }
                    if (Descriptions.Count() == ImageNameOption.Count())
                    {
                        createOptionsAussen.ImageName = ImageNameOption[i];
                    }

                    db.Halbzylinder_Options.Add(createOptionsAussen);
                    db.SaveChanges();

                    for (int f = 0; f < inputCounter[i]; f++)
                    {
                        var costedValue = new Halbzylinder_Options_value
                        {
                            Halbzylinder_OptionsId = createOptionsAussen.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter],
                        };
                        db.Halbzylinder_Options_value.Add(costedValue);
                        counter++;
                    }
                    db.SaveChanges();
                }

            }

            var HalbSize_Cost = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == profil_Halbzylinder.Id).ToList();
            foreach (var list in HalbSize_Cost)
            {
                db.Aussen_Innen_Halbzylinder.Remove(list);
            }
            db.SaveChanges();

            for (int i = 0; i < Size.Count(); i++)
            {
                var SizeV = new Aussen_Innen_Halbzylinder
                {
                    Profil_HalbzylinderId = Items.Id,
                    aussen = Size[i],
                    costAussen = costSizeAussen[i]
                };
                db.Aussen_Innen_Halbzylinder.Add(SizeV);
            }
            db.SaveChanges();

            return RedirectToAction("Profil_HalbzylinderRout");
        }
        [HttpPost]
        public ActionResult SaveHebelzylinder(Hebel profil_Halbzylinder, List<string> Options,List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter)
        {
            var Items = db.Hebelzylinder.Find(profil_Halbzylinder.Id);
            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;

            var option = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == profil_Halbzylinder.Id).ToList();

            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<Options>();

                foreach (var item in option)
                {
                    var OptionDescription = db.Options.Where(x => x.OptionId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.Options_value.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.Options_value.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.Options.Remove(list);
                }

                db.SaveChanges();

                foreach (var optionsList in option)
                {
                    db.Hebelzylinder_Options.Remove(optionsList);
                }
                db.SaveChanges();
            }
            if (Options.Count>0)
            {
                int counter = 0;
                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Hebelzylinder_Options
                    {
                        HebelzylinderId = Items.Id,
                    };

                    db.Hebelzylinder_Options.Add(createOptions);
                    db.SaveChanges();

                        var createOptionsAussen = new Options
                        {
                            OptionId = createOptions.Id,
                            Name = Options[i],
                           
                        };
                        if (Descriptions.Count() > 0)
                        {
                            createOptionsAussen.Description = Descriptions[i];
                        }
                        if (Descriptions.Count() == ImageNameOption.Count())
                        {
                            createOptionsAussen.ImageName = ImageNameOption[i];
                        }

                        db.Options.Add(createOptionsAussen);
                        db.SaveChanges();

                        for (int f = 0; f < inputCounter[i]; f++)
                        {
                            var costedValue = new Options_value
                            {
                                OptionsId = createOptionsAussen.Id,
                                Value = valueNGF[counter],
                                Cost = costNGF[counter],
                            };
                            db.Options_value.Add(costedValue);
                            counter++;
                        }
                    db.SaveChanges();
                }
                
            }

            return RedirectToAction("HebelzylinderRout");
        }

        [HttpPost]
        public ActionResult SaveAussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Halbzylinder, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions,List<string> Name, List<string> Description, List<string> valueNGF, List<float> costNGF, List<int> inputCounter)
        {
            var Items = db.Aussenzylinder_Rundzylinder.Find(profil_Halbzylinder.Id);
            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;

            var option = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == profil_Halbzylinder.Id).ToList();

            if(Options.Count()==0 || option.Count() > 0)
            {
                   var listAllOption = new List<Aussen_Rund_all>();

                    foreach (var item in option)
                    {
                        var OptionDescription = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == item.Id).ToList();

                        foreach (var item2 in OptionDescription)
                        {
                            listAllOption.Add(item2);
                        }
                    }

                    foreach (var item in listAllOption)
                    {
                        var optionsValue = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == item.Id).ToList();

                        foreach (var item2 in optionsValue)
                        {
                            db.Aussen_Rouns_all_value.Remove(item2);
                        }
                    }
                    db.SaveChanges();

                    foreach(var list in listAllOption)
                    {
                        db.Aussen_Rund_all.Remove(list);
                    }

                    db.SaveChanges();

                    foreach(var optionsList in option)
                    {
                        db.Aussen_Rund_options.Remove(optionsList);
                    }
                    db.SaveChanges();
            }
            if (Options.Count > 0)
            {
                int counter = 0;
                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Aussen_Rund_options
                    {
                        Aussenzylinder_RundzylinderId = Items.Id,
                    };

                    db.Aussen_Rund_options.Add(createOptions);
                    db.SaveChanges();

                    var createOptionsAussen = new Aussen_Rund_all
                    {
                        Aussen_Rund_optionsId = createOptions.Id,
                        Name = Options[i],                       
                    };
                    if (Descriptions.Count() > 0)
                    {
                        createOptionsAussen.Description = Descriptions[i];
                    }
                    if (Descriptions.Count() == ImageNameOption.Count())
                    {
                        createOptionsAussen.ImageName = ImageNameOption[i];
                    }
                    db.Aussen_Rund_all.Add(createOptionsAussen);
                    db.SaveChanges();

                    for (int f = 0; f < inputCounter[i]; f++)
                    {
                        var costedValue = new Aussen_Rouns_all_value
                        {
                            Aussen_Rund_allId = createOptionsAussen.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter],
                        };
                        db.Aussen_Rouns_all_value.Add(costedValue);
                        counter++;
                    }
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Aussenzylinder_RundzylinderRout");
        }
        [HttpPost]
        public ActionResult SaveVorhangschloss(Vorhangschloss profil_Halbzylinder,List<int>Size,List<float> CostSize, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter)
        {
            var Items = db.Vorhangschloss.Find(profil_Halbzylinder.Id);
            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;

            var option = db.Vorhan_Options.Where(x => x.VorhangschlossId == profil_Halbzylinder.Id).ToList();

            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<OptionsVorhan>();

                foreach (var item in option)
                {
                    var OptionDescription = db.OptionsVorhan.Where(x => x.OptionId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.OptionsVorhan_value.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.OptionsVorhan_value.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.OptionsVorhan.Remove(list);
                }

                db.SaveChanges();

                foreach (var optionsList in option)
                {
                    db.Vorhan_Options.Remove(optionsList);
                }
                db.SaveChanges();
            }
            else
            {
                int counter = 0;
                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Vorhan_Options
                    {
                        VorhangschlossId = Items.Id,
                    };

                    db.Vorhan_Options.Add(createOptions);
                    db.SaveChanges();

                    var createOptionsAussen = new OptionsVorhan
                    {
                        OptionId = createOptions.Id,
                        Name = Options[i],                        
                    };

                    if (Descriptions.Count() > 0)
                    {
                        createOptionsAussen.Description = Descriptions[i];
                    }
                    if (Descriptions.Count() == ImageNameOption.Count())
                    {
                        createOptionsAussen.ImageName = ImageNameOption[i];
                    }


                    db.OptionsVorhan.Add(createOptionsAussen);
                    db.SaveChanges();

                    for (int f = 0; f < inputCounter[i]; f++)
                    {
                        var costedValue = new OptionsVorhan_value
                        {
                            OptionsId = createOptionsAussen.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.OptionsVorhan_value.Add(costedValue);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            var VorhanItemSize_Cost = db.Size.Where(x=>x.VorhangschlossId == profil_Halbzylinder.Id).ToList();
            foreach(var list in VorhanItemSize_Cost)
            {
                db.Size.Remove(list);
            }
            db.SaveChanges();

            for (int i =0;i< Size.Count(); i++)
            {
                var SizeV = new Size
                {
                    VorhangschlossId = Items.Id,
                    sizeVorhangschloss = Size[i],
                    Cost = CostSize[i],
                };
               db.Size.Add(SizeV);
            }
            db.SaveChanges();

            return RedirectToAction("VorhangschlossRout");
        }

        [HttpPost]
        public ActionResult SaveProfil_Knaufzylinder(Profil_Knaufzylinder profil_Knayf,List<int> SizeAus, List<int> SizeInen, List<string> Options,List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<float> costSizeAussen, List<float> costSizeIntern)
        {
            var Items = db.Profil_Knaufzylinder.Find(profil_Knayf.Id);
            Items.schliessanlagenId = profil_Knayf.schliessanlagenId;
            Items.Name = profil_Knayf.Name;
            Items.companyName = profil_Knayf.companyName;
            Items.NameSystem = profil_Knayf.NameSystem;
            Items.description = profil_Knayf.description;
            Items.Price = profil_Knayf.Price;
            Items.ImageName = profil_Knayf.ImageName;

            var option = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == profil_Knayf.Id).ToList();
            
            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<Knayf_Options>();

                foreach (var item in option)
                {
                    var OptionDescription = db.Knayf_Options.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.Knayf_Options_value.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.Knayf_Options.Remove(list);
                }

                db.SaveChanges();

                foreach (var optionsList in option)
                {
                    db.Profil_Knaufzylinder_Options.Remove(optionsList);
                }
                db.SaveChanges();
            }
            if (Options.Count > 0)
            {
                int counter = 0;

                for (var i = 0; i < Options.Count(); i++)
                {
                    var createOptions = new Profil_Knaufzylinder_Options
                    {
                        Profil_KnaufzylinderId = Items.Id,
                    };

                    db.Profil_Knaufzylinder_Options.Add(createOptions);
                    db.SaveChanges();

                    var createOptionsAussen = new Knayf_Options
                    {
                        OptionsId = createOptions.Id,
                        Name = Options[i],
                    };

                    if (Descriptions.Count() > 0)
                    {
                        createOptionsAussen.Description = Descriptions[i];
                    }
                    if (Descriptions.Count() == ImageNameOption.Count())
                    {
                        createOptionsAussen.ImageName = ImageNameOption[i];
                    }


                    db.Knayf_Options.Add(createOptionsAussen);
                    db.SaveChanges();

                    for (int f = 0; f < inputCounter[i]; f++)
                    {
                        var costedValue = new Knayf_Options_value
                        {
                            Knayf_OptionsId = createOptionsAussen.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter],
                        };
                        db.Knayf_Options_value.Add(costedValue);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }

            var KnayfItemSize_Cost = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == profil_Knayf.Id).ToList();
            foreach (var list in KnayfItemSize_Cost)
            {
                db.Aussen_Innen_Knauf.Remove(list);
            }
            db.SaveChanges();

            for (int i = 0; i < SizeAus.Count(); i++)
            {
                var SizeV = new Aussen_Innen_Knauf
                {
                    Profil_KnaufzylinderId = Items.Id,
                    aussen = SizeAus[i],
                    Intern = SizeInen[i],
                    costSizeAussen = costSizeAussen[i],
                    costSizeIntern = costSizeIntern[i]
                };
                db.Aussen_Innen_Knauf.Add(SizeV);
            }
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

            var aussen = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil.Id).Select(x => x.aussen).ToList();

            var innen = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil.Id).Select(x => x.Intern).ToList();

            var queryableOptions = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == profil.Id).Select(x => x.Id).ToList();

            ViewBag.aussen = aussen;

            ViewBag.innen = innen;
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

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.NGF_Value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductProfil_Doppelzylinder", profilInfo);
        }
        [HttpGet]
        [Route("Home/ProductProfil_Knayf")]
        public ActionResult ProductProfil_Knayf(Profil_Knaufzylinder profil)
        {
            var profilInfo = db.Profil_Knaufzylinder.FirstOrDefault(x => x.Id == profil.Id);

            var aussen = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == profil.Id).Select(x => x.aussen).ToList();

            var innen = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == profil.Id).Select(x => x.Intern).ToList();

            var queryableOptions = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == profil.Id).Select(x => x.Id).ToList();

            ViewBag.aussen = aussen;

            ViewBag.innen = innen;
            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<Knayf_Options> ngf = new List<Knayf_Options>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.Knayf_Options.Where(x => x.OptionsId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<Knayf_Options_value> ngfList = new List<Knayf_Options_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Knayf_Options_value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductProfil_Knayf", profilInfo);
        }
        [HttpGet]
        [Route("Home/ProductProfil_Halb")]
        public ActionResult ProductProfil_Halb(Profil_Halbzylinder profil)
        {
            var profilInfo = db.Profil_Halbzylinder.FirstOrDefault(x => x.Id == profil.Id);

            var aussen = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == profil.Id).Select(x => x.aussen).ToList();

            var queryableOptions = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == profil.Id).Select(x => x.Id).ToList();

            ViewBag.aussen = aussen;

            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<Halbzylinder_Options> ngf = new List<Halbzylinder_Options>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.Halbzylinder_Options.Where(x => x.OptionsId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<Halbzylinder_Options_value> ngfList = new List<Halbzylinder_Options_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Halbzylinder_Options_value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductProfil_Halb", profilInfo);
        }
        [HttpGet]
        [Route("Home/ProductHebel")]
        public ActionResult ProductHebel(Hebel profil)
        {
            var profilInfo = db.Hebelzylinder.FirstOrDefault(x => x.Id == profil.Id);

            var queryableOptions = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == profil.Id).Select(x => x.Id).ToList();


            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<Options> ngf = new List<Options>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.Options.Where(x => x.OptionId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<Options_value> ngfList = new List<Options_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.Options_value.Where(x => x.OptionsId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Options_value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductHebel", profilInfo);
        }

        [HttpGet]
        [Route("Home/ProductVorhan")]
        public ActionResult ProductVorhan(Vorhangschloss profil)
        {
            var profilInfo = db.Vorhangschloss.FirstOrDefault(x => x.Id == profil.Id);

            var queryableOptions = db.Vorhan_Options.Where(x => x.VorhangschlossId == profil.Id).Select(x => x.Id).ToList();

            var Size = db.Size.Where(x => x.VorhangschlossId == profilInfo.Id).Select(x => x.sizeVorhangschloss).ToList();
            ViewBag.Size = Size.ToList();
            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<OptionsVorhan> ngf = new List<OptionsVorhan>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.OptionsVorhan.Where(x => x.OptionId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<OptionsVorhan_value> ngfList = new List<OptionsVorhan_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.OptionsVorhan_value.Where(x => x.OptionsId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Options_value.Count());
                }

                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductVorhan", profilInfo);
        }
        [HttpGet]
        [Route("Home/ProductAussenzylinder")]
        public ActionResult ProductAussenzylinder(Aussenzylinder_Rundzylinder profil)
        {
            var profilInfo = db.Aussenzylinder_Rundzylinder.FirstOrDefault(x => x.Id == profil.Id);

            var queryableOptions = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == profil.Id).Select(x => x.Id).ToList();


            ViewBag.countOptionsQuery = queryableOptions.Count();

            if (queryableOptions.Count() > 0)
            {

                List<Aussen_Rund_all> ngf = new List<Aussen_Rund_all>();

                for (int z = 0; z < queryableOptions.Count(); z++)
                {
                    var allOptions = db.Aussen_Rund_all.Where(x => x.Aussen_Rund_optionsId == queryableOptions[z]).ToList();
                    foreach (var option in allOptions)
                    {
                        ngf.Add(option);
                    }

                }

                ViewBag.optionsName = ngf.Select(x => x.Name).ToList();

                List<Aussen_Rouns_all_value> ngfList = new List<Aussen_Rouns_all_value>();

                for (int s = 0; s < ngf.Count(); s++)
                {
                    var opValue = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == ngf[s].Id).ToList();

                    for (int i = 0; i < opValue.Count(); i++)
                    {
                        ngfList.Add(opValue[i]);

                    }
                    ViewBag.optionValueCount = opValue.Count();
                }

                var list = new List<int>();

                foreach (var fs in ngf)
                {
                    list.Add(fs.Aussen_Rouns_all_value.Count());
                }


                ViewBag.countOptionsList = list;

                ViewBag.optionsValue = ngfList.Select(x => x.Value).ToList();

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }


            return View("ProductAussenzylinder", profilInfo);
        }
        #endregion

        [HttpGet]
        public ActionResult AllOrders()
        {
            var User = db.Users.Select(x => x).ToList();
            var UserProduct = db.ProductSysteam.Select(x => x).ToList();
            var UserOrders = db.UserOrdersShop.Select(x => x).ToList();
            
            var OrderStatus = db.OrderStatus.Select(x => x).ToList();

            var OrderMans = new List<string>();

            foreach(var list in OrderStatus)
            {
                string result = new string(list.Order.Where(c => !char.IsDigit(c)).ToArray());
               
                OrderMans.Add(result);
            }
           

            var users = db.Users.Select(x => x).ToList();

            var queryOrder = from t1 in users
                             join t2 in UserOrders
                             on t1.Id equals t2.UserId
                             select new
                             {
                                 Id = t2.Id,
                                 FirstName = t1.FirstName,
                                 LastName = t1.LastName,
                                 Address = t1.Address,
                                 Email = t1.UserName,
                                 ProductName = t2.ProductName,
                                 OrderSum = t2.OrderSum,
                                 createData = t2.createData
                             };

            var Order = queryOrder.Distinct().ToList();

            ViewBag.Order = Order.ToList();


            return View();
        }
        
        public ActionResult Download(int? Id,string FirstName,string LastName)
        {
            var UserOrder = db.UserOrdersShop.FirstOrDefault(x => x.Id == Id).createData;
            
            var day = UserOrder.Value.Day;
            var month = UserOrder.Value.Month;
            var year = UserOrder.Value.Year;
            var hour = UserOrder.Value.Hour;
            var minuten = UserOrder.Value.Minute;
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            string loginInform = ident.Claims.Select(x => x.Value).First();
            var users = db.Users.FirstOrDefault(x => x.FirstName == FirstName && x.LastName == LastName);

            var filepath = Path.Combine($"~/Orders", $"{users.FirstName + users.LastName + minuten + hour + day + month + year} OrderFile.xlsx");

            return File(filepath, "xlsx/plain", $"{users.FirstName + users.LastName + day + month + year} OrderFile.xlsx");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
