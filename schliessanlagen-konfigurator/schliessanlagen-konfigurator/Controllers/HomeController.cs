using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Models.Vorhan;
using System.Diagnostics;
using System.Security.Claims;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using schliessanlagen_konfigurator.Models.Hebel;
using schliessanlagen_konfigurator.Models.System;
using Org.BouncyCastle.Asn1.Cms;
using schliessanlagen_konfigurator.Service;
using SixLabors.ImageSharp.Formats.Webp;
using OfficeOpenXml.ConditionalFormatting;
using NuGet.ContentModel;
using System;
namespace schliessanlagen_konfigurator.Controllers
{
    public class HomeController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly ImageOptimizationService _imageOptimizationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController( UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            _imageOptimizationService = new ImageOptimizationService();
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        [HttpGet]
        public async Task<IActionResult> ImageConfig()
        {

            string sourceFilePath = @"wwwroot/compression/";
            IEnumerable<string> imageFiles = Directory.GetFiles(sourceFilePath, "*").Select(Path.GetFileName);
            return View("../Edit/ImageConfig", imageFiles);

        }
        [HttpGet]
        public async Task<IActionResult> Blogs()
        {
            ViewBag.item = await db.Blogs.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create_Blog(string Name, string Descriptions)
        {

            var model = new Blog
            {
                Name = Name,
                Description = Descriptions,
                Data = DateTime.Now.Date
            };
                db.Blogs.Add(model);
                db.SaveChanges();
            

            return RedirectToAction("Blogs");
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
            string sourceFilePath = @"wwwroot/image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, imageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            return RedirectToAction("ImageConfig", "Home");
        }

        [HttpGet]
        public IActionResult GetUpdatedSystems()
        {
            var systems = db.SysteamPriceKey.ToList();
            return Json(systems);
        }

        [HttpPost]
        public async Task<IActionResult> Create_Sys(string KeyName, List<IFormFile> Images, float KeyCost, bool? coppy, int? coppyId)
        {
            var key = new SysteamPriceKey
            {
                NameSysteam = KeyName,
                Price = KeyCost,
            };
            db.SysteamPriceKey.Add(key);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        SysteamPriceKeyId = key.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        itemGalary.ImageName = fileName = fileName + extension;

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            if (coppyId!=0)
            {

                var AltSystem = db.SysteamPriceKey.Where(x => x.Id == coppyId).First();
                var options = db.SystemOptionen.Where(x => x.SystemId == AltSystem.Id).ToList();
                var SystemOptionInfo = new List<SystemOptionInfo>();

                foreach(var list in options)
                {
                    var item = db.SystemOptionInfo.Include(x=>x.SystemOptionValue).Where(x => x.OptionsId == list.Id).ToList();

                    foreach(var l in item)
                    {
                        SystemOptionInfo.Add(l);
                    }
                }

                var SystemOptionValue = new List<SystemOptionValue>();

                foreach (var list in SystemOptionInfo)
                {
                    var item = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();

                    foreach (var l in item)
                    {
                        SystemOptionValue.Add(l);
                    }
                }

                var SystemScheker = new List<SystemScheker>();

                foreach (var list in SystemOptionInfo)
                {
                    var item = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();

                    foreach (var l in item)
                    {
                        SystemScheker.Add(l);
                    }
                }

                var Options = SystemOptionInfo.Select(x => x.Name).ToList();
                
                var doppel = new List<string>();

                var Knayf = new List<string>();
               
                var Halb = new List<string>();

                var Hebel = new List<string>();

                var Vorhang = new List<string>();
                
                var Aussen = new List<string>();

                foreach (var list in SystemScheker.Select(x => x.Aussen).ToList())
                {
                    if (list == true)
                    {
                        Aussen.Add("true");
                    }
                    else
                    {
                        Aussen.Add("false");
                    }
                }

                foreach (var list in SystemScheker.Select(x => x.Vorhang).ToList())
                {
                    if (list == true)
                    {
                        Vorhang.Add("true");
                    }
                    else
                    {
                        Vorhang.Add("false");
                    }
                }

                foreach (var list in SystemScheker.Select(x => x.Hebel).ToList())
                {
                    if (list == true)
                    {
                        Hebel.Add("true");
                    }
                    else
                    {
                        Hebel.Add("false");
                    }
                }

                foreach (var list in SystemScheker.Select(x => x.Halb).ToList())
                {
                    if (list == true)
                    {
                        Halb.Add("true");
                    }
                    else
                    {
                        Halb.Add("false");
                    }
                }

                foreach (var list in SystemScheker.Select(x => x.Knayf).ToList())
                {
                    if (list == true)
                    {
                        Knayf.Add("true");
                    }
                    else
                    {
                        Knayf.Add("false");
                    }
                }

                foreach (var list in SystemScheker.Select(x => x.doppel).ToList())
                {
                    if (list == true)
                    {
                        doppel.Add("true");
                    }
                    else
                    {
                        doppel.Add("false");
                    }
                }

                var dopple = db.Profil_Doppelzylinder.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                var Descriptions = SystemOptionInfo.Select(x=>x.Description).ToList();

                var ImageNameOption = SystemOptionInfo.Select(x => x.ImageName).ToList();

                var value = SystemOptionValue.Select(x => x.Value).ToList();
                var cost = SystemOptionValue.Select(x => x.Cost).ToList();

                var inputCounter = new List<int>();

                foreach(var list in SystemOptionInfo)
                {
                    inputCounter.Add(list.SystemOptionValue.Count());
                }

                if (dopple != null)
                {
                    dopple.NameSystem = key.NameSysteam;

                    db.Profil_Doppelzylinder.Update(dopple);
                    db.SaveChanges();

                    var option = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == dopple.Id).ToList();

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
                            if (doppel[i] != "false")
                            {
                                var createOptions = new Profil_Doppelzylinder_Options
                                {
                                    DoppelzylinderId = dopple.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.NGF_Value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }
                            else
                            {

                            }
                        }

                    }

                }

                var knayf = db.Profil_Knaufzylinder.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                if (knayf != null)
                {
                    knayf.NameSystem = key.NameSysteam;
                    db.Profil_Knaufzylinder.Update(knayf);
                    db.SaveChanges();

                    var option = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == knayf.Id).ToList();

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
                            if (Knayf[i] != "false")
                            {
                                var createOptions = new Profil_Knaufzylinder_Options
                                {
                                    Profil_KnaufzylinderId = knayf.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.Knayf_Options_value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }

                        }

                    }
                }

                var halb = db.Profil_Halbzylinder.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                if (halb != null)
                {
                    halb.NameSystem = key.NameSysteam;
                    db.Profil_Halbzylinder.Update(halb);
                    db.SaveChanges();

                    var option = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == halb.Id).ToList();

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
                            if (Halb[i] != "false")
                            {
                                var createOptions = new Profil_Halbzylinder_Options
                                {
                                    Profil_HalbzylinderId = halb.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.Halbzylinder_Options_value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }
                        }

                    }
                }

                var hebel = db.Hebelzylinder.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                if (hebel != null)
                {
                    hebel.NameSystem = key.NameSysteam;

                    db.Hebelzylinder.Update(hebel);
                    db.SaveChanges();

                    var option = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel.Id).ToList();

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
                    if (Options.Count > 0)
                    {
                        int counter = 0;

                        for (var i = 0; i < Options.Count(); i++)
                        {
                            if (Hebel[i] != "false")
                            {
                                var createOptions = new Hebelzylinder_Options
                                {
                                    HebelzylinderId = hebel.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.Options_value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }

                        }

                    }
                }

                var vorhang = db.Vorhangschloss.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                if (vorhang != null)
                {
                    vorhang.NameSystem = key.NameSysteam;

                    db.Vorhangschloss.Update(vorhang);
                    db.SaveChanges();

                    var option = db.Vorhan_Options.Where(x => x.VorhangschlossId == vorhang.Id).ToList();

                    if (Options.Count() == 0 || option.Count() > 0)
                    {
                        var listAllOption = new List<Models.Vorhan.OptionsVorhan>();

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
                    if (Options.Count > 0)
                    {
                        int counter = 0;

                        for (var i = 0; i < Options.Count(); i++)
                        {
                            if (Vorhang[i] != "false")
                            {
                                var createOptions = new Vorhan_Options
                                {
                                    VorhangschlossId = vorhang.Id,
                                };

                                db.Vorhan_Options.Add(createOptions);
                                db.SaveChanges();

                                var createOptionsAussen = new Models.Vorhan.OptionsVorhan
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
                                        Value = value[counter],
                                        Cost = cost[counter]
                                    };
                                    db.OptionsVorhan_value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }

                        }
                    }
                }

                var Aus = db.Aussenzylinder_Rundzylinder.FirstOrDefault(x => x.NameSystem == key.NameSysteam);

                if (Aus != null)
                {
                    Aus.NameSystem = key.NameSysteam;

                    db.Aussenzylinder_Rundzylinder.Update(Aus);
                    db.SaveChanges();

                    var option = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == Aus.Id).ToList();

                    if (Options.Count() == 0 || option.Count() > 0)
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

                        foreach (var list in listAllOption)
                        {
                            db.Aussen_Rund_all.Remove(list);
                        }

                        db.SaveChanges();

                        foreach (var optionsList in option)
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
                            if (Aussen[i] != "false")
                            {
                                var createOptions = new Aussen_Rund_options
                                {
                                    Aussenzylinder_RundzylinderId = Aus.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.Aussen_Rouns_all_value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }

                        }

                    }
                }

                var sys = db.SysteamPriceKey.Where(x => x.NameSysteam == key.NameSysteam).First();

                sys.NameSysteam = key.NameSysteam;
                sys.Price = key.Price;
                sys.DesctiptionsSysteam = AltSystem.DesctiptionsSysteam;
                sys.Lieferzeit = AltSystem.Lieferzeit;
                sys.LieferzeitGrosse = AltSystem.LieferzeitGrosse;
                db.SysteamPriceKey.Update(sys);
                db.SaveChanges();

                var SystemOptionen = db.SystemOptionen.Where(x => x.SystemId == sys.Id).ToList();

                if (Options.Count() == 0 || SystemOptionen.Count() > 0)
                {
                    var listAllOption = new List<SystemOptionInfo>();

                    foreach (var item in SystemOptionen)
                    {
                        var OptionDescription = db.SystemOptionInfo.Where(x => x.OptionsId == item.Id).ToList();

                        foreach (var item2 in OptionDescription)
                        {
                            listAllOption.Add(item2);
                        }
                    }

                    foreach (var item in listAllOption)
                    {
                        var optionsValue = db.SystemScheker.Where(x => x.chekerId == item.Id).ToList();

                        foreach (var item2 in optionsValue)
                        {
                            db.SystemScheker.Remove(item2);
                        }
                    }

                    foreach (var item in listAllOption)
                    {
                        var optionsValue = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

                        foreach (var item2 in optionsValue)
                        {
                            db.SystemOptionValue.Remove(item2);
                        }
                    }
                    db.SaveChanges();

                    foreach (var list in listAllOption)
                    {
                        db.SystemOptionInfo.Remove(list);
                    }
                    db.SaveChanges();

                    foreach (var optionsList in SystemOptionen)
                    {
                        db.SystemOptionen.Remove(optionsList);
                    }
                    db.SaveChanges();
                }
                if (Options.Count > 0)
                {
                    int counter = 0;

                    for (var i = 0; i < Options.Count(); i++)
                    {

                        var createOptions = new SystemOptionen
                        {
                            SystemId = sys.Id,
                        };

                        db.SystemOptionen.Add(createOptions);
                        db.SaveChanges();

                        var createOptionsAussen = new SystemOptionInfo
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

                        db.SystemOptionInfo.Add(createOptionsAussen);
                        db.SaveChanges();


                        SystemScheker SysCheker = new SystemScheker
                        {
                            chekerId = createOptionsAussen.Id,
                        };

                        if (doppel[i] == "true")
                        {
                            SysCheker.doppel = true;
                        }
                        else
                        {
                            SysCheker.doppel = false;
                        }

                        if (Knayf[i] == "true")
                        {
                            SysCheker.Knayf = true;
                        }
                        else
                        {
                            SysCheker.Knayf = false;
                        }

                        if (Halb[i] == "true")
                        {
                            SysCheker.Halb = true;
                        }
                        else
                        {
                            SysCheker.Halb = false;
                        }

                        if (Hebel[i] == "true")
                        {
                            SysCheker.Hebel = true;
                        }
                        else
                        {
                            SysCheker.Hebel = false;
                        }

                        if (Vorhang[i] == "true")
                        {
                            SysCheker.Vorhang = true;
                        }
                        else
                        {
                            SysCheker.Vorhang = false;
                        }

                        if (Aussen[i] == "true")
                        {
                            SysCheker.Aussen = true;
                        }
                        else
                        {
                            SysCheker.Aussen = false;
                        }
                        db.SystemScheker.Add(SysCheker);
                        db.SaveChanges();


                        for (int f = 0; f < inputCounter[i]; f++)
                        {
                            var costedValue = new SystemOptionValue
                            {
                                SysteamPriceKeyId = createOptionsAussen.Id,
                                Value = value[counter],
                                Cost = cost[counter],
                            };
                            db.SystemOptionValue.Add(costedValue);
                            counter++;
                        }
                        db.SaveChanges();
                    }

                }
            }

            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!"});
        }
        public ActionResult Delete_System(int id)
        {
            var item = db.SysteamPriceKey.FirstOrDefault(x => x.Id == id);

            var Gallery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var OPtions = db.SystemOptionen.Where(x=>x.SystemId == item.Id).ToList();

            var optionsInfo = new List<SystemOptionInfo>();

            foreach (var option in OPtions)
            {
                var list = db.SystemOptionInfo.Where(x => x.OptionsId == option.Id).ToList();

                foreach(var i in list)
                {
                    optionsInfo.Add(i);
                }
            }

            foreach (var option in optionsInfo)
            {
                var list = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == option.Id).ToList();

                foreach (var i in list)
                {
                    db.SystemOptionValue.Remove(i);
                    db.SaveChanges();
                }
            }

            foreach (var option in optionsInfo)
            {
                var list = db.SystemScheker.Where(x => x.chekerId == option.Id).ToList();

                foreach (var i in list)
                {
                    db.SystemScheker.Remove(i);
                    db.SaveChanges();
                }
                db.SystemOptionInfo.Remove(option);
                db.SaveChanges();
            }

            foreach(var list in OPtions)
            {
                db.SystemOptionen.Remove(list);
                db.SaveChanges();
            }

            db.SysteamPriceKey.Remove(item);
            db.SaveChanges();

            return RedirectToAction("SystemInfo", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string wwwRootPath = Environment.WebRootPath;

                // Путь к папке для сохранения изображений
                string uploadFolderPath = Path.Combine(wwwRootPath, "image");
                string uploadFolderPathCompression = Path.Combine(wwwRootPath, "compression");

                // Проверяем наличие папки "image", создаем, если ее нет
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Формируем полный путь для сохранения загруженного файла
                string filePath = Path.Combine(uploadFolderPath, file.FileName);

                // Сохраняем загруженный файл
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Формируем имя и путь для файла в формате WebP
                string webpFileName = Path.GetFileNameWithoutExtension(file.FileName) + ".webp";
                string webpFilePath = Path.Combine(uploadFolderPathCompression, webpFileName);

                // Проверяем наличие папки "compression", создаем, если ее нет
                if (!Directory.Exists(uploadFolderPathCompression))
                {
                    Directory.CreateDirectory(uploadFolderPathCompression);
                }

                using (var imageSharpImage = await Image.LoadAsync(filePath)) // Загружаем по пути к оригинальному файлу
                {
                    await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder()); // Сохраняем как WebP
                }

                // Сжимаем изображение (если нужно)
                await _imageOptimizationService.CompressImageAsync(filePath, webpFilePath);

                ViewBag.Message = "File uploaded successfully.";
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return RedirectToAction("ImageConfig", "Home");

        }

        #region ViewZylinder
        [HttpGet]
        public async Task<IActionResult> SystemInfo(int id)
        {

            if (id == 0)
            {
                var System = db.SysteamPriceKey.ToList();
                ViewBag.item = System;
            }
            else
            {
                var System = db.SysteamPriceKey.ToList();
                ViewBag.item = System;

                var SystemSchois = System.Where(x => x.Id == id).First();

                ViewBag.SystemChois = SystemSchois;
            }
            



            return View();
        }
        public async Task<IActionResult> Edit_System(int id)
        {
            var item = db.SysteamPriceKey.Find(id);          

            var OptionItem = db.SystemOptionen.Where(x => x.SystemId == item.Id).ToList();

            var Galery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

            ViewBag.Galry = Galery;

            if (OptionItem.Count > 0)
            {
                var Options = new List<SystemOptionInfo>();

                ViewBag.optionV = true;

                foreach (var option in OptionItem)
                {
                    var Params = db.SystemOptionInfo.Where(x => x.OptionsId == option.Id).ToList();

                    foreach (var list in Params)
                    {
                        Options.Add(list);
                    }
                }

                var SysCheker = new List<SystemScheker>();

                foreach (var option in Options)
                {
                    var Params = db.SystemScheker.Where(x => x.chekerId == option.Id).ToList();

                    foreach (var list in Params)
                    {
                        SysCheker.Add(list);
                    }
                }

                ViewBag.SystemCheker = SysCheker;

                ViewBag.Options = Options;

                var OptionsValue = new List<SystemOptionValue>();

                var countV = new List<int>();

                foreach (var option in Options)
                {
                    var Value = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == option.Id).ToList();

                    foreach (var list in Value)
                    {
                        OptionsValue.Add(list);
                    }
                    countV.Add(Value.Count());
                }

                ViewBag.OptionValue = OptionsValue;
                ViewBag.CountOptions = countV.ToList();
            }
            else
            {
                ViewBag.optionV = false;
            }
            return View("../Edit/Edit_System", item);
        }

        [HttpGet]
        public async Task<IActionResult> Schipeed()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveSysteamInfo(SysteamPriceKey systeam, string AltName, List<string> Halb, List<string> Knayf,List<string> doppel,
        List<string> Hebel, List<string> Vorhang, List<string> Aussen,  List<string> Options, List<string> ImageNameOption, List<string> Descriptions, List<int> inputCounter,
        List<string> value , List<float> cost, List<string> GalleryImages,List<IFormFile> UploadGalleryImages)
        {
            var dopple = db.Profil_Doppelzylinder.FirstOrDefault(x => x.NameSystem == AltName);          

            if (dopple != null)
            {
                dopple.NameSystem = systeam.NameSysteam;

                db.Profil_Doppelzylinder.Update(dopple);
                db.SaveChanges();

                    var option = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == dopple.Id).ToList();

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
                            if (doppel[i] != "false")
                            {
                                var createOptions = new Profil_Doppelzylinder_Options
                                {
                                    DoppelzylinderId = dopple.Id,
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
                                        Value = value[counter],
                                        Cost = cost[counter],
                                    };
                                    db.NGF_Value.Add(costedValue);
                                    counter++;
                                }
                                db.SaveChanges();
                            }
                            else
                            {

                            } 
                        }

                    }
               
               
            }
           
            var knayf = db.Profil_Knaufzylinder.FirstOrDefault(x => x.NameSystem == AltName);

            if (knayf != null)
            {
                knayf.NameSystem = systeam.NameSysteam;
                db.Profil_Knaufzylinder.Update(knayf);
                db.SaveChanges();

                var option = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == knayf.Id).ToList();

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
                            if (Knayf[i] != "false")
                            {
                                    var createOptions = new Profil_Knaufzylinder_Options
                                    {
                                        Profil_KnaufzylinderId = knayf.Id,
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
                                            Value = value[counter],
                                            Cost = cost[counter],
                                        };
                                        db.Knayf_Options_value.Add(costedValue);
                                        counter++;
                                    }
                                    db.SaveChanges();
                            }
  
                        }
                    
                }
            }

            var halb = db.Profil_Halbzylinder.FirstOrDefault(x => x.NameSystem == AltName);

            if (halb != null)
            {
                halb.NameSystem = systeam.NameSysteam;
                db.Profil_Halbzylinder.Update(halb);
                db.SaveChanges();

                var option = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == halb.Id).ToList();

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
                        if (Halb[i] != "false")
                        {
                            var createOptions = new Profil_Halbzylinder_Options
                            {
                                Profil_HalbzylinderId = halb.Id,
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
                                    Value = value[counter],
                                    Cost = cost[counter],
                                };
                                db.Halbzylinder_Options_value.Add(costedValue);
                                counter++;
                            }
                            db.SaveChanges();
                        }
                    }

                }
            }

            var hebel = db.Hebelzylinder.FirstOrDefault(x => x.NameSystem == AltName);

            if (hebel != null)
            {
                hebel.NameSystem = systeam.NameSysteam;

                db.Hebelzylinder.Update(hebel);
                db.SaveChanges();

                var option = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == hebel.Id).ToList();

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
                if (Options.Count > 0)
                {
                    int counter = 0;

                    for (var i = 0; i < Options.Count(); i++)
                    {
                        if (Hebel[i] != "false")
                        {
                            var createOptions = new Hebelzylinder_Options
                            {
                                HebelzylinderId = hebel.Id,
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
                                    Value = value[counter],
                                    Cost = cost[counter],
                                };
                                db.Options_value.Add(costedValue);
                                counter++;
                            }
                            db.SaveChanges();
                        }
                        
                    }

                }
            }
          
            var vorhang = db.Vorhangschloss.FirstOrDefault(x => x.NameSystem == AltName);

            if (vorhang != null)
            {
                vorhang.NameSystem = systeam.NameSysteam;

                db.Vorhangschloss.Update(vorhang);
                db.SaveChanges();

                var option = db.Vorhan_Options.Where(x => x.VorhangschlossId == vorhang.Id).ToList();

                if (Options.Count() == 0 || option.Count() > 0)
                {
                    var listAllOption = new List<Models.Vorhan.OptionsVorhan>();

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
                if (Options.Count > 0)
                {
                    int counter = 0;

                    for (var i = 0; i < Options.Count(); i++)
                    {
                        if (Vorhang[i] != "false")
                        {
                            var createOptions = new Vorhan_Options
                            {
                                VorhangschlossId = vorhang.Id,
                            };

                            db.Vorhan_Options.Add(createOptions);
                            db.SaveChanges();

                            var createOptionsAussen = new Models.Vorhan.OptionsVorhan
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
                                    Value = value[counter],
                                    Cost = cost[counter]
                                };
                                db.OptionsVorhan_value.Add(costedValue);
                                counter++;
                            }
                            db.SaveChanges();
                        }
                        
                    }
                }
            }

            var Aus = db.Aussenzylinder_Rundzylinder.FirstOrDefault(x => x.NameSystem == AltName);

            if (Aus != null)
            {
                Aus.NameSystem = systeam.NameSysteam;

                db.Aussenzylinder_Rundzylinder.Update(Aus);
                db.SaveChanges();

                var option = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == Aus.Id).ToList();

                if (Options.Count() == 0 || option.Count() > 0)
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

                    foreach (var list in listAllOption)
                    {
                        db.Aussen_Rund_all.Remove(list);
                    }

                    db.SaveChanges();

                    foreach (var optionsList in option)
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
                        if (Aussen[i] != "false")
                        {
                            var createOptions = new Aussen_Rund_options
                            {
                                Aussenzylinder_RundzylinderId = Aus.Id,
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
                                    Value = value[counter],
                                    Cost = cost[counter],
                                };
                                db.Aussen_Rouns_all_value.Add(costedValue);
                                counter++;
                            }
                            db.SaveChanges();
                        }
                          
                    }

                }
            }

            var sys = db.SysteamPriceKey.Where(x => x.NameSysteam == AltName).First();

            var Gallery = db.ProductGalery.Where(x => x.SysteamPriceKeyId == sys.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    var itemGalary = new ProductGalery
                    {
                        SysteamPriceKeyId = sys.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        
                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            sys.NameSysteam = systeam.NameSysteam;
            sys.Price = systeam.Price;
            sys.DesctiptionsSysteam = systeam.DesctiptionsSysteam;
            sys.Lieferzeit = systeam.Lieferzeit;
            sys.LieferzeitGrosse = systeam.LieferzeitGrosse;
            db.SysteamPriceKey.Update(sys);
            db.SaveChanges();

            var SystemOptionen = db.SystemOptionen.Where(x=>x.SystemId==sys.Id).ToList();

            if (Options.Count() == 0 || SystemOptionen.Count() > 0)
            {
                var listAllOption = new List<SystemOptionInfo>();

                foreach (var item in SystemOptionen)
                {
                    var OptionDescription = db.SystemOptionInfo.Where(x => x.OptionsId == item.Id).ToList();

                    foreach (var item2 in OptionDescription)
                    {
                        listAllOption.Add(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.SystemScheker.Where(x => x.chekerId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.SystemScheker.Remove(item2);
                    }
                }

                foreach (var item in listAllOption)
                {
                    var optionsValue = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == item.Id).ToList();

                    foreach (var item2 in optionsValue)
                    {
                        db.SystemOptionValue.Remove(item2);
                    }
                }
                db.SaveChanges();

                foreach (var list in listAllOption)
                {
                    db.SystemOptionInfo.Remove(list);
                }
                db.SaveChanges();

                foreach (var optionsList in SystemOptionen)
                {
                    db.SystemOptionen.Remove(optionsList);
                }
                db.SaveChanges();
            }
            if (Options.Count > 0)
            {
                int counter = 0;

                for (var i = 0; i < Options.Count(); i++)
                {

                        var createOptions = new SystemOptionen
                        {
                            SystemId = sys.Id,
                        };

                        db.SystemOptionen.Add(createOptions);
                        db.SaveChanges();

                        var createOptionsAussen = new SystemOptionInfo
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

                        db.SystemOptionInfo.Add(createOptionsAussen);
                        db.SaveChanges();

                  
                        SystemScheker SysCheker = new SystemScheker
                        {
                            chekerId = createOptionsAussen.Id,
                        };

                        if (doppel[i] == "true")
                        {
                            SysCheker.doppel = true;
                        }
                        else
                        {
                            SysCheker.doppel = false;
                        }

                        if (Knayf[i] == "true")
                        {
                            SysCheker.Knayf = true;
                        }
                        else
                        {
                            SysCheker.Knayf = false;
                        }

                        if (Halb[i] == "true")
                        {
                            SysCheker.Halb = true;
                        }
                        else
                        {
                            SysCheker.Halb = false;
                        }

                        if (Hebel[i] == "true")
                        {
                            SysCheker.Hebel = true;
                        }
                        else
                        {
                            SysCheker.Hebel = false;
                        }

                        if (Vorhang[i] == "true")
                        {
                            SysCheker.Vorhang = true;
                        }
                        else
                        {
                            SysCheker.Vorhang = false;
                        }

                        if (Aussen[i] == "true")
                        {
                            SysCheker.Aussen = true;
                        }
                        else
                        {
                            SysCheker.Aussen = false;
                        }
                        db.SystemScheker.Add(SysCheker);
                        db.SaveChanges();
                    

                    for (int f = 0; f < inputCounter[i]; f++)
                        {
                            var costedValue = new SystemOptionValue
                            {
                                SysteamPriceKeyId = createOptionsAussen.Id,
                                Value = value[counter],
                                Cost = cost[counter],
                            };
                            db.SystemOptionValue.Add(costedValue);
                            counter++;
                        }
                        db.SaveChanges();
                }
               
            }
             return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!"});
        }

        public async Task<IActionResult> Index(int id)
        {
          
            if (id != 0)
            {

                var allSystem = db.SysteamPriceKey.Select(x => x.NameSysteam).ToList();

                ViewBag.System = allSystem;

                var Example = db.Profil_Doppelzylinder.Where(x => x.Id == id).First();

                ViewBag.Dopple = Example;

                var OptionsId = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Example.Id).ToList();

                var InfoOptions = new List<NGF>();

                foreach(var option in OptionsId)
                {
                    var item = db.NGF.Where(x => x.OptionsId == option.Id).ToList();

                    foreach(var list in item)
                    {
                        InfoOptions.Add(list);
                    }
                }

                var OptionsValue = new List<NGF_Value>();

                foreach(var list in InfoOptions)
                {
                    var item = db.NGF_Value.Where(x => x.NGFId == list.Id).ToList();
                   
                    foreach(var li in item)
                    {
                        OptionsValue.Add(li);
                    }
                }

                ViewBag.DoppelOptions = InfoOptions;

                ViewBag.OptionsValue = OptionsValue;

                var Size = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == Example.Id).ToList();
                
                ViewBag.Size = Size;

                var kleidDoppelSize = db.Doppel_Innen_klein.Where(x => x.Aussen_InnenId == Size[0].Id).ToList();

                if (kleidDoppelSize.Count > 0)
                {
                    ViewBag.KleinAussen = Size[0];
                }

                ViewBag.DoppelKleinSize = kleidDoppelSize;

                var AllDoppel = await db.Profil_Doppelzylinder.OrderBy(x => x.Price).Select(x => x.NameSystem).ToListAsync();

                ViewBag.item = await db.Profil_Doppelzylinder.OrderBy(x => x.Price).ToListAsync();

                var listPriceKey = new List<SysteamPriceKey>();

                foreach (var System in AllDoppel)
                {
                    var itemKey = await db.SysteamPriceKey.Where(x => x.NameSysteam == System).ToListAsync();

                    foreach (var i in itemKey)
                    {
                        listPriceKey.Add(i);
                    }

                }
                ViewBag.KeyCost = listPriceKey.Select(x => x.Price).ToList();
            }
            else
            {

                var AllDoppel = await db.Profil_Doppelzylinder.OrderBy(x => x.Price).Select(x => x.NameSystem).ToListAsync();

                ViewBag.item = await db.Profil_Doppelzylinder.OrderBy(x => x.Price).ToListAsync();

                var listPriceKey = new List<SysteamPriceKey>();

                foreach (var System in AllDoppel)
                {
                    var itemKey = await db.SysteamPriceKey.Where(x => x.NameSysteam == System).ToListAsync();

                    foreach (var i in itemKey)
                    {
                        listPriceKey.Add(i);
                    }

                }
                ViewBag.KeyCost = listPriceKey.Select(x => x.Price).ToList();
            }
            return View();
        }


        public async Task<IActionResult> Profil_KnaufzylinderRout(int id)
        {
            if (id != 0)
            {
                var Knauf = db.Profil_Knaufzylinder.Where(x => x.Id == id).First();
                
                ViewBag.item = db.Profil_Knaufzylinder.OrderBy(x => x.Price).ToList();

                ViewBag.Knayf = Knauf;

                var Size = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == id).ToList();

                ViewBag.Size = Size;

                ViewBag.System = db.SysteamPriceKey.Select(x => x.NameSysteam).ToList();
               
                var KleidKnayfSize = db.Aussen_Innen_Knauf_klein.Where(x => x.Aussen_Innen_KnaufId == Size[0].Id).ToList();

                if (KleidKnayfSize.Count > 0)
                {
                    ViewBag.KleinAussen = Size[0];
                }

                ViewBag.KnayfKleinSize = KleidKnayfSize;
            }
            else
            {
                ViewBag.item = db.Profil_Knaufzylinder.OrderBy(x => x.Price).ToList();
            }
           
            return View();
        }
        [HttpGet]
        public IActionResult HebelzylinderRout(int id)
        {

            if (id != 0)
            {
                var Hebelzylinder = db.Hebelzylinder.Where(x => x.Id == id).First();
                
                ViewBag.Hebel = Hebelzylinder;

                ViewBag.System = db.SysteamPriceKey.Select(x => x.NameSysteam).ToList();

                ViewBag.item = db.Hebelzylinder.OrderBy(x => x.Price).ToList();
            }
            else
            {
                ViewBag.item = db.Hebelzylinder.OrderBy(x => x.Price).ToList();
            }
           
            return View();
        }
        [HttpGet]
        public IActionResult VorhangschlossRout(int id)
        {

            if (id !=0)
            {
                var Vorhangschloss = db.Vorhangschloss.Where(x => x.Id == id).First();

                ViewBag.Vorhang = Vorhangschloss;

                ViewBag.item = db.Vorhangschloss.OrderBy(x => x.Price).ToList();

                var Size = db.Size.Where(x => x.VorhangschlossId == id).ToList();

                ViewBag.Groze = Size;

                ViewBag.System = db.SysteamPriceKey.Select(x => x.NameSysteam).ToList();
            }
            else
            {
                ViewBag.item = db.Vorhangschloss.OrderBy(x => x.Price).ToList();
            }
           
            return View();
        }
        [HttpGet]
        public IActionResult Aussenzylinder_RundzylinderRout(int id)
        {

            if (id!=0)
            {
                var Aussenzylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == id).First();

                ViewBag.AussenZyl = Aussenzylinder;

                ViewBag.System = db.SysteamPriceKey.Select(x => x.NameSysteam).ToList();

                ViewBag.item = db.Aussenzylinder_Rundzylinder.OrderBy(x => x.Price).ToList();
            }
            else
            {
                ViewBag.item = db.Aussenzylinder_Rundzylinder.OrderBy(x => x.Price).ToList();
            }
           
            return View();
        }
        [HttpGet]
        public IActionResult Profil_HalbzylinderRout(int id)
        {

            if (id != 0)
            {
                var Example = db.Profil_Halbzylinder.Where(x => x.Id == id).First();
                
                ViewBag.Habel = Example;

                var Size = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Example.Id).ToList();

                ViewBag.Size = Size;

                ViewBag.System = db.SysteamPriceKey.Select(x=>x.NameSysteam).ToList();  

                ViewBag.item = db.Profil_Halbzylinder.OrderBy(x => x.Price).ToList();
            }
            else
            {
                ViewBag.item = db.Profil_Halbzylinder.OrderBy(x => x.Price).ToList();
            }

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
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen, List<IFormFile> Images,
        List<float> innen,List<float> costSizeAussen, List<float> costSizeIntern, List<string> valueNGF, List<float> costNGF, List<int> input_counter,
        List<float> internDoppelKlein, List<float> priesDoppelKlein, float ausKlein,float ausKleinPreis)
        {

            var ftr = aussen.Count();

            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach(var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();
                
                foreach(var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach(var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x=>x.chekerId==list.Id).ToList();
                foreach(var elem in items)
                {
                    Cheker.Add(elem);
                }
            }

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName );

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(fileName + extension, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Profil_Doppelzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        DopelZylinderId = Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        
                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }
            
            if (ausKlein != 0)
            {
                var ausse_innenklein = new Aussen_Innen
                {
                    Profil_DoppelzylinderId = Profil_Doppelzylinder.Id,
                    aussen = ausKlein,
                    costSizeAussen = ausKleinPreis,
                    costSizeIntern = 0,
                    Intern = 0
                };
                db.Aussen_Innen.Add(ausse_innenklein);
                db.SaveChanges();

                for (int f = 0; f < internDoppelKlein.Count(); f++)
                {
                    var DoppelInternKlein = new Doppel_Innen_klein
                    {
                        Aussen_InnenId = ausse_innenklein.Id,
                        Intern = internDoppelKlein[f],
                        costSizeIntern = priesDoppelKlein[f]
                    };
                    db.Doppel_Innen_klein.Add(DoppelInternKlein);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen
                {
                    Profil_DoppelzylinderId = Profil_Doppelzylinder.Id,
                    aussen = aussen[i],
                    Intern = innen[i],
                    costSizeAussen = costSizeAussen[i],
                    costSizeIntern = costSizeIntern[i]
                };
                db.Aussen_Innen.Add(ausse_innen);
                db.SaveChanges();
            }

            int counter = 0;

            var countItem = Cheker.Select(x => x.doppel).ToList();

            if (countItem.Count() > 0)
            {
                for(int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Profil_Doppelzylinder_Options
                    {
                        DoppelzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Profil_Doppelzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new NGF
                    {
                        OptionsId = dopOptions.Id,
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.NGF.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new NGF_Value
                        {
                            NGFId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.NGF_Value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                for (var i = 0; i < Options.Count(); i++)
                {

                    var dopOptions = new Profil_Doppelzylinder_Options
                    {
                        DoppelzylinderId = Profil_Doppelzylinder.Id,

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

                        ngf.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

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
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Create_Profil_Knaufzylinder(Profil_Knaufzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen, List<IFormFile> Images,
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter, List<float> costSizeAussen, List<float> costSizeIntern)
        {
            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach (var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();

                foreach (var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();
                foreach (var elem in items)
                {
                    Cheker.Add(elem);
                }
            }

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Profil_Knaufzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        Profil_KnaufzylinderId= Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);
                        
                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen_Knauf
                {
                    Profil_KnaufzylinderId = Profil_Doppelzylinder.Id,
                    aussen = aussen[i],
                    Intern = innen[i],
                    costSizeAussen = costSizeAussen[i],
                    costSizeIntern = costSizeIntern[i]
                };
                db.Aussen_Innen_Knauf.Add(ausse_innen);
                db.SaveChanges();
            }

            var countItem = Cheker.Select(x => x.Knayf).ToList();

            if (countItem.Count() > 0)
            {
                for (int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Profil_Knaufzylinder_Options
                    {
                        Profil_KnaufzylinderId = Profil_Doppelzylinder.Id,
                    };
                    db.Profil_Knaufzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Knayf_Options
                    {
                        OptionsId = dopOptions.Id,
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.Knayf_Options.Add(ngf);
                    db.SaveChanges();

                    int counter = 0;

                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new Knayf_Options_value
                        {
                            Knayf_OptionsId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.Knayf_Options_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                if (Options != null)
                {
                    var dopOptions = new Profil_Knaufzylinder_Options
                    {
                        Profil_KnaufzylinderId = Profil_Doppelzylinder.Id,
                    };
                    db.Profil_Knaufzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    for (var i = 0; i < Options.Count(); i++)
                    {
                        var ngf = new Knayf_Options
                        {
                            OptionsId = dopOptions.Id,
                            Name = Options[i],
                            Description = NGFDescriptions[i],
                            ImageFile = postedFile
                        };


                        if (ngf.ImageFile != null)
                        {
                            string wwwRootPath = Environment.WebRootPath;

                            string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                            string extension = Path.GetExtension(ngf.ImageFile.FileName);

                            ngf.ImageName = fileName = fileName + extension;

                            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                            string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                            string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                            await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await ngf.ImageFile.CopyToAsync(fileStream);
                            }
                        }

                        db.Knayf_Options.Add(ngf);

                        db.SaveChanges();

                        int counter = 0;

                        for (var j = 0; j < input_counter[i]; j++)
                        {
                            var ngfValue = new Knayf_Options_value
                            {
                                Knayf_OptionsId = ngf.Id,
                                Value = valueNGF[counter],
                                Cost = costNGF[counter]
                            };
                            db.Knayf_Options_value.Add(ngfValue);

                            counter++;
                        }

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Profil_KnaufzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Profil_Halbzylinder(Profil_Halbzylinder Profil_Doppelzylinder,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<float> aussen, List<IFormFile> Images,
        List<float> innen, List<string> valueNGF, List<float> costNGF, List<int> input_counter,List<float> costSizeAussen)
        {

            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach (var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();

                foreach (var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();
                foreach (var elem in items)
                {
                    Cheker.Add(elem);
                }
            }
            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Profil_Halbzylinder.Add(Profil_Doppelzylinder);

            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        Profil_HalbzylinderId = Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Aussen_Innen_Halbzylinder
                {
                    Profil_HalbzylinderId = Profil_Doppelzylinder.Id,
                    aussen = aussen[i],
                    costAussen = costSizeAussen[i]
                };
                db.Aussen_Innen_Halbzylinder.Add(ausse_innen);
                db.SaveChanges();
            }
            var countItem = Cheker.Select(x => x.Halb).ToList();

            if (countItem.Count() > 0)
            {
                for (int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Profil_Halbzylinder_Options
                    {
                        Profil_HalbzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Profil_Halbzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Halbzylinder_Options
                    {
                        OptionsId = dopOptions.Id,
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.Halbzylinder_Options.Add(ngf);
                    db.SaveChanges();

                    int counter = 0;
                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new Halbzylinder_Options_value
                        {
                            Halbzylinder_OptionsId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.Halbzylinder_Options_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                if (Options != null)
                {
                    var dopOptions = new Profil_Halbzylinder_Options
                    {
                        Profil_HalbzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Profil_Halbzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    int counter = 0;

                    for (var i = 0; i < Options.Count(); i++)
                    {
                        var ngf = new Halbzylinder_Options
                        {
                            OptionsId = dopOptions.Id,
                            Name = Options[i],
                            Description = NGFDescriptions[i],
                            ImageFile = postedFile
                        };


                        if (ngf.ImageFile != null)
                        {
                            string wwwRootPath = Environment.WebRootPath;

                            string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                            string extension = Path.GetExtension(ngf.ImageFile.FileName);

                            ngf.ImageName = fileName = fileName + extension;

                            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                            string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                            string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                            await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await ngf.ImageFile.CopyToAsync(fileStream);
                            }
                        }

                        db.Halbzylinder_Options.Add(ngf);
                        db.SaveChanges();

                        for (var j = 0; j < input_counter[i]; j++)
                        {
                            var ngfValue = new Halbzylinder_Options_value
                            {
                                Halbzylinder_OptionsId = ngf.Id,
                                Value = valueNGF[counter],
                                Cost = costNGF[counter]
                            };
                            db.Halbzylinder_Options_value.Add(ngfValue);

                            counter++;

                        }

                        db.SaveChanges();

                    }
                }
            }
            

            return RedirectToAction("Profil_HalbzylinderRout");
        }

        public async Task<IActionResult> Create_Aussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder Profil_Doppelzylinder, List<IFormFile> Images,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
        {
            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach (var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();

                foreach (var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();
                foreach (var elem in items)
                {
                    Cheker.Add(elem);
                }
            }

            var countItem = Cheker.Select(x => x.Aussen).ToList();

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Aussenzylinder_Rundzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        Aussenzylinder_RundzylinderId = Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            int counter = 0;
            if (countItem.Count() > 0)
            {
                for (int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Aussen_Rund_options
                    {
                        Aussenzylinder_RundzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Aussen_Rund_options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Aussen_Rund_all
                    {
                        Aussen_Rund_optionsId = dopOptions.Id,
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.Aussen_Rund_all.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new Aussen_Rouns_all_value
                        {
                            Aussen_Rund_allId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.Aussen_Rouns_all_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                for (var i = 0; i < Options.Count(); i++)
                {

                    var dopOptions = new Aussen_Rund_options
                    {
                        Aussenzylinder_RundzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Aussen_Rund_options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Aussen_Rund_all
                    {
                        Aussen_Rund_optionsId = dopOptions.Id,
                        Name = Options[i],
                        Description = NGFDescriptions[i],
                        ImageFile = postedFile
                    };


                    if (ngf.ImageFile != null)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                        string extension = Path.GetExtension(ngf.ImageFile.FileName);

                        ngf.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await ngf.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    db.Aussen_Rund_all.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < input_counter[i]; j++)
                    {
                        var ngfValue = new Aussen_Rouns_all_value
                        {
                            Aussen_Rund_allId = ngf.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.Aussen_Rouns_all_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();

                }
            }
           
            return RedirectToAction("Aussenzylinder_RundzylinderRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Vorhangschloss(Vorhangschloss Profil_Doppelzylinder,List<float> costSize, List<IFormFile> Images,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<string> valueNGF, List<float> costNGF, List<float> aussen, List<int> input_counter)
        {
            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach (var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();

                foreach (var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();
                foreach (var elem in items)
                {
                    Cheker.Add(elem);
                }
            }
            var countItem = Cheker.Select(x => x.Vorhang).ToList();

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName );

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Vorhangschloss.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        VorhangschlossId = Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }
            for (int i = 0; i < aussen.Count(); i++)
            {
                var ausse_innen = new Models.Vorhan.Size
                {
                    VorhangschlossId = Profil_Doppelzylinder.Id,
                    sizeVorhangschloss = aussen[i],
                    Cost = costSize[i]
                };
                db.Size.Add(ausse_innen);
                db.SaveChanges();
            }

            int counter = 0;

            if (countItem.Count() > 0)
            {
                for (int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Profil_Doppelzylinder_Options
                    {
                        DoppelzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Profil_Doppelzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Models.Vorhan.OptionsVorhan
                    {
                        OptionId = dopOptions.Id,
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.OptionsVorhan.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new OptionsVorhan_value
                        {
                            OptionsId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.OptionsVorhan_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                for (var i = 0; i < Options.Count(); i++)
                {

                    var dopOptions = new Vorhan_Options
                    {
                        VorhangschlossId = Profil_Doppelzylinder.Id,

                    };
                    db.Vorhan_Options.Add(dopOptions);
                    db.SaveChanges();

                    var ngf = new Models.Vorhan.OptionsVorhan
                    {
                        OptionId = dopOptions.Id,
                        Name = Options[i],
                        Description = NGFDescriptions[i],
                        ImageFile = postedFile
                    };


                    if (ngf.ImageFile != null)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                        string extension = Path.GetExtension(ngf.ImageFile.FileName);

                        ngf.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await ngf.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    db.OptionsVorhan.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < input_counter[i]; j++)
                    {
                        var ngfValue = new OptionsVorhan_value
                        {
                            OptionsId = ngf.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.OptionsVorhan_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();

                }
            }
           
            return RedirectToAction("VorhangschlossRout");
        }

        [HttpPost]
        public async Task<IActionResult> Create_Hebelzylinder(Hebel Profil_Doppelzylinder, List<IFormFile> Images,
        List<string> Options, List<string> NGFDescriptions, IFormFile postedFile, List<string> valueNGF, List<float> costNGF, List<int> input_counter)
        {
            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == Profil_Doppelzylinder.NameSystem);

            var OptionS = db.SystemOptionen.Where(x => x.SystemId == System.Id).ToList();

            var SystemOptionItem = new List<SystemOptionInfo>();

            foreach (var list in OptionS)
            {
                var items = db.SystemOptionInfo.Where(x => x.OptionsId == list.Id).ToList();

                foreach (var elem in items)
                {
                    SystemOptionItem.Add(elem);
                }
            }

            var ValueSystemOptions = new List<SystemOptionValue>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemOptionValue.Where(x => x.SysteamPriceKeyId == list.Id).ToList();
                foreach (var elem in items)
                {
                    ValueSystemOptions.Add(elem);
                }
            }

            var Cheker = new List<SystemScheker>();

            foreach (var list in SystemOptionItem)
            {
                var items = db.SystemScheker.Where(x => x.chekerId == list.Id).ToList();
                foreach (var elem in items)
                {
                    Cheker.Add(elem);
                }
            }

            var countItem = Cheker.Select(x => x.Hebel).ToList();

            if (Profil_Doppelzylinder.ImageFile != null)
            {
                string wwwRootPath = Environment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Profil_Doppelzylinder.ImageFile.FileName);

                string extension = Path.GetExtension(Profil_Doppelzylinder.ImageFile.FileName);

                Profil_Doppelzylinder.ImageName = fileName = fileName + extension;

                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Profil_Doppelzylinder.ImageFile.CopyToAsync(fileStream);
                }
            }

            db.Hebelzylinder.Add(Profil_Doppelzylinder);
            db.SaveChanges();

            if (Images != null && Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    var itemGalary = new ProductGalery
                    {
                        HebelId = Profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        itemGalary.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            int counter = 0;

            if (countItem.Count() > 0)
            {
                for (int o = 0; o < countItem.Count(); o++)
                {
                    var dopOptions = new Hebelzylinder_Options
                    {
                        HebelzylinderId = Profil_Doppelzylinder.Id,

                    };
                    db.Hebelzylinder_Options.Add(dopOptions);
                    db.SaveChanges();

                    var x = db.Profil_Doppelzylinder_Options.Select(x => x.Id).ToList();

                    var ngf = new Options
                    {
                        OptionId = x.Last(),
                        Name = SystemOptionItem[o].Name,
                        Description = SystemOptionItem[o].Description,
                        ImageFile = SystemOptionItem[o].ImageFile,
                        ImageName = SystemOptionItem[o].ImageName,
                    };

                    db.Options.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < SystemOptionItem[o].SystemOptionValue.Count(); j++)
                    {
                        var ngfValue = new Options_value
                        {
                            OptionsId = ngf.Id,
                            Value = ValueSystemOptions[counter].Value,
                            Cost = ValueSystemOptions[counter].Cost
                        };
                        db.Options_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                for (var i = 0; i < Options.Count(); i++)
                {

                    var dopOptions = new Hebelzylinder_Options
                    {
                        HebelzylinderId = Profil_Doppelzylinder.Id,
                    };
                    db.Hebelzylinder_Options.Add(dopOptions);

                    db.SaveChanges();

                    var ngf = new Options
                    {
                        OptionId = dopOptions.Id,
                        Name = Options[i],
                        Description = NGFDescriptions[i],
                        ImageFile = postedFile
                    };


                    if (ngf.ImageFile != null)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(ngf.ImageFile.FileName);

                        string extension = Path.GetExtension(ngf.ImageFile.FileName);

                        ngf.ImageName = fileName = fileName + extension;

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                        string uploadFolderPathCompression = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "compression");

                        string filePathCompression = Path.Combine(uploadFolderPathCompression, fileName);

                        await _imageOptimizationService.CompressImageAsync(path, filePathCompression);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await ngf.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    db.Options.Add(ngf);
                    db.SaveChanges();

                    for (var j = 0; j < input_counter[i]; j++)
                    {
                        var ngfValue = new Options_value
                        {
                            OptionsId = ngf.Id,
                            Value = valueNGF[counter],
                            Cost = costNGF[counter]
                        };
                        db.Options_value.Add(ngfValue);

                        counter++;
                    }

                    db.SaveChanges();

                }
            }

            
            return RedirectToAction("HebelzylinderRout");
        }
        #endregion
        #region DelitZylinderItem
        [HttpGet]
        public async Task<IActionResult> Delete_KnayfZylinder(int id)
        {

            var Knaufzylinder = db.Profil_Knaufzylinder.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.Profil_KnaufzylinderId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var a = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == id).ToList();
            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, Knaufzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<Knayf_Options>();

            for (int i = 0; i < a.Count(); i++)
            {
                var of = db.Knayf_Options.Where(x => x.OptionsId == a[i].Id).ToList();

                for (int j = 0; j < of.Count(); j++)
                {
                    option.Add(of[j]);
                }

            }

            var Size = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == Knaufzylinder.Id).ToList();

            for (int i = 0; i < a.Count(); i++)
            {
                if (i == 0)
                {
                    var klein = db.Aussen_Innen_Knauf_klein.Where(x => x.Aussen_Innen_KnaufId == Size[i].Id).ToList();

                    foreach (var list in klein)
                    {
                        db.Aussen_Innen_Knauf_klein.Remove(list);
                        db.SaveChanges();
                    }
                }
                db.Aussen_Innen_Knauf.Remove(Size[i]);
                db.SaveChanges();
            }

            for (int i = 0; i < option.Count(); i++)
            {
                var optionV = db.Knayf_Options_value.Where(x => x.Knayf_OptionsId == option[i].Id).ToList();
                for (int j = 0; j < optionV.Count(); j++)
                {
                    db.Knayf_Options_value.Remove(optionV[j]);
                    db.SaveChanges();
                }
                db.Knayf_Options.Remove(option[i]);
                db.SaveChanges();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Profil_Knaufzylinder_Options.Remove(a[i]);
                db.SaveChanges();
            }

            db.Profil_Knaufzylinder.Remove(Knaufzylinder);
            db.SaveChanges();


            return RedirectToAction("Profil_KnaufzylinderRout");

        }
        public async Task<IActionResult> Delete_Hebel(int id)
        {
            var doppelzylinder = db.Hebelzylinder.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.HebelId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var a = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == doppelzylinder.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, doppelzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<Options>();

            for (int i = 0; i < a.Count(); i++)
            {
                var of = db.Options.Where(x => x.OptionId == a[i].Id).ToList();

                for (int j = 0; j < of.Count(); j++)
                {
                    option.Add(of[j]);
                }

            }

            for (int i = 0; i < option.Count(); i++)
            {
                var optionV = db.Options_value.Where(x => x.OptionsId == option[i].Id).ToList();
                for (int j = 0; j < optionV.Count(); j++)
                {
                    db.Options_value.Remove(optionV[j]);
                    db.SaveChanges();
                }
                db.Options.Remove(option[i]);
                db.SaveChanges();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Hebelzylinder_Options.Remove(a[i]);
                db.SaveChanges();
            }


            db.Hebelzylinder.Remove(doppelzylinder);
            db.SaveChanges();
            return RedirectToAction("HebelzylinderRout");

        }
        public async Task<IActionResult> Delete_Vorhan(int id)
        {
            var doppelzylinder = db.Vorhangschloss.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.VorhangschlossId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var a = db.Vorhan_Options.Where(x => x.VorhangschlossId == doppelzylinder.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, doppelzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<Models.Vorhan.OptionsVorhan>();

            for (int i = 0; i < a.Count(); i++)
            {
                var of = db.OptionsVorhan.Where(x => x.OptionId == a[i].Id).ToList();

                for (int j = 0; j < of.Count(); j++)
                {
                    option.Add(of[j]);
                }

            }

            var Size = db.Size.Where(x => x.VorhangschlossId == doppelzylinder.Id).ToList();

            for (int i = 0; i < a.Count(); i++)
            {
                db.Size.Remove(Size[i]);
                db.SaveChanges();
            }

            for (int i = 0; i < option.Count(); i++)
            {
                var optionV = db.OptionsVorhan_value.Where(x => x.OptionsId == option[i].Id).ToList();
                for (int j = 0; j < optionV.Count(); j++)
                {
                    db.OptionsVorhan_value.Remove(optionV[j]);
                    db.SaveChanges();
                }
                db.OptionsVorhan.Remove(option[i]);
                db.SaveChanges();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Vorhan_Options.Remove(a[i]);
                db.SaveChanges();
            }


            db.Vorhangschloss.Remove(doppelzylinder);
            db.SaveChanges();

            return RedirectToAction("VorhangschlossRout");
        }
        public async Task<IActionResult> Delete_Halbzylinder(int id)
        {
            var doppelzylinder = db.Profil_Halbzylinder.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.Profil_HalbzylinderId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var a = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == doppelzylinder.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, doppelzylinder.ImageName);

            if (System.IO.File.Exists(imagePathToDelete))
            {
                System.IO.File.Delete(imagePathToDelete);
            }

            var option = new List<Halbzylinder_Options>();

            for (int i = 0; i < a.Count(); i++)
            {
                var of = db.Halbzylinder_Options.Where(x => x.OptionsId == a[i].Id).ToList();

                for (int j = 0; j < of.Count(); j++)
                {
                    option.Add(of[j]);
                }

            }


            var Size = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == doppelzylinder.Id).ToList();

            for (int i = 0; i < a.Count(); i++)
            {
                db.Aussen_Innen_Halbzylinder.Remove(Size[i]);
                db.SaveChanges();
            }

            for (int i = 0; i < option.Count(); i++)
            {
                var optionV = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == option[i].Id).ToList();
                for (int j = 0; j < optionV.Count(); j++)
                {
                    db.Halbzylinder_Options_value.Remove(optionV[j]);
                    db.SaveChanges();
                }
                db.Halbzylinder_Options.Remove(option[i]);
                db.SaveChanges();
            }
            for (int i = 0; i < a.Count(); i++)
            {
                db.Profil_Halbzylinder_Options.Remove(a[i]);
                db.SaveChanges();
            }

            db.Profil_Halbzylinder.Remove(doppelzylinder);
            db.SaveChanges();

            return RedirectToAction("Profil_HalbzylinderRout");
        }
        public async Task<IActionResult> Delete_Aussen(int id)
        {
            var doppelzylinder = db.Aussenzylinder_Rundzylinder.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.Aussenzylinder_RundzylinderId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

            var a = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == doppelzylinder.Id).ToList();

            string sourceFilePath = @"wwwroot/Image/";

            string imagePathToDelete = Path.Combine(sourceFilePath, doppelzylinder.ImageName);

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

            db.Aussenzylinder_Rundzylinder.Remove(doppelzylinder);
            db.SaveChanges();

            return RedirectToAction("Aussenzylinder_RundzylinderRout");

        }
        public async Task<IActionResult> Delete_Doppelzylinder(int id)
        {
            var doppelzylinder = db.Profil_Doppelzylinder.Find(id);

            var Gallery = db.ProductGalery.Where(x => x.DopelZylinderId == id).ToList();

            foreach (var list in Gallery)
            {
                string sourceFilePathGallery = @"wwwroot/image/";

                string imagePathToDeleteGallery = Path.Combine(sourceFilePathGallery, list.ImageName);

                if (System.IO.File.Exists(imagePathToDeleteGallery))
                {
                    System.IO.File.Delete(imagePathToDeleteGallery);
                }

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list.ImageName);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }

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
                if (i == 0)
                {
                    var klein = db.Doppel_Innen_klein.Where(x => x.Aussen_InnenId == Size[i].Id).ToList();
                    
                    foreach(var list in klein)
                    {
                        db.Doppel_Innen_klein.Remove(list);
                        db.SaveChanges();
                    }
                }
                
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

                var Galery = db.ProductGalery.Where(x => x.DopelZylinderId == profil_Doppelzylinder.Id).ToList();

                ViewBag.Galry = Galery;

                var AllDoppel = await db.Profil_Doppelzylinder.Where(x => x.NameSystem == Doppel.NameSystem).Select(x => x.NameSystem).ToListAsync();
              
                ViewBag.AllDopel = db.Profil_Doppelzylinder.ToList();

                ViewBag.CountAllDoppel = db.Profil_Doppelzylinder.Select(x => x.Name).Count();

                var SizeDoppel = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).ToList();
                
                var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == Doppel.Id).ToList();

                var kleidDoppelSize = db.Doppel_Innen_klein.Where(x => x.Aussen_InnenId == SizeDoppel[0].Id).ToList();

                if (kleidDoppelSize.Count>0)
                {
                    ViewBag.KleinAussen = SizeDoppel[0];
                }

                ViewBag.DoppelKleinSize = kleidDoppelSize;

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

                var Galery = db.ProductGalery.Where(x => x.DopelZylinderId == profil_Doppelzylinder.Id).ToList();

                ViewBag.Galry = Galery;

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
        public async Task<IActionResult> SaveDoppelZylinder(Profil_Doppelzylinder profil_Doppelzylinder, List<int> SizeAus, List<int> SizeInen, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<float> costSizeAussen, List<float> costSizeIntern, List<IFormFile> UploadGalleryImages, List<string> GalleryImages,
        List<float> internDoppelKlein, List<float> priesDoppelKlein, float ausKlein, float ausKleinPreis)
        {
            var Items = db.Profil_Doppelzylinder.Find(profil_Doppelzylinder.Id);
            Items.schliessanlagenId = profil_Doppelzylinder.schliessanlagenId;
            Items.Type = profil_Doppelzylinder.Type;
            Items.Name = profil_Doppelzylinder.Name;
            Items.companyName = profil_Doppelzylinder.companyName;
            Items.NameSystem = profil_Doppelzylinder.NameSystem;
            Items.description = profil_Doppelzylinder.description;
            Items.Price = profil_Doppelzylinder.Price;
            Items.ImageName = profil_Doppelzylinder.ImageName;

            var Gallery = db.ProductGalery.Where(x => x.DopelZylinderId == profil_Doppelzylinder.Id).ToList();
            
            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    var itemGalary = new ProductGalery
                    {
                        DopelZylinderId = profil_Doppelzylinder.Id,
                    };
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        itemGalary.ImageName = fileName = fileName + extension;

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            } 


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

            var DoppeltemSize_Cost = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == profil_Doppelzylinder.Id).ToList();
            var DoppelKleinIten = db.Doppel_Innen_klein.Where(x => x.Aussen_InnenId == DoppeltemSize_Cost[0].Id).ToList();

            foreach (var list in DoppelKleinIten)
            {
                db.Doppel_Innen_klein.Remove(list);
            }
            db.SaveChanges();

            foreach (var list in DoppeltemSize_Cost)
            {
                db.Aussen_Innen.Remove(list);
            }
            db.SaveChanges();

            if (ausKlein != 0)
            {
                var ausse_innenklein = new Aussen_Innen
                {
                    Profil_DoppelzylinderId = Items.Id,
                    aussen = ausKlein,
                    costSizeAussen = ausKleinPreis,
                    costSizeIntern = 0,
                    Intern = 0
                };
                db.Aussen_Innen.Add(ausse_innenklein);
                db.SaveChanges();

                for (int f = 0; f < internDoppelKlein.Count(); f++)
                {
                    var DoppelInternKlein = new Doppel_Innen_klein
                    {
                        Aussen_InnenId = ausse_innenklein.Id,
                        Intern = internDoppelKlein[f],
                        costSizeIntern = priesDoppelKlein[f]
                    };
                    db.Doppel_Innen_klein.Add(DoppelInternKlein);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < SizeAus.Count(); i++)
            {
                var klein = db.Aussen_Innen.FirstOrDefault(x => x.Profil_DoppelzylinderId == Items.Id && x.aussen == SizeAus[i]);
                
                if (klein == null)
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
              
            }
            db.SaveChanges();

            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Hebel(Hebel profil_Halbzylinder)
        {

            var Halbzylinder = db.Hebelzylinder.Find(profil_Halbzylinder.Id);

            var options = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == Halbzylinder.Id).ToList();

            var Galery = db.ProductGalery.Where(x => x.HebelId == profil_Halbzylinder.Id).ToList();

            ViewBag.Galry = Galery;

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

            var Galery = db.ProductGalery.Where(x => x.Aussenzylinder_RundzylinderId == aussenzylinder.Id).ToList();

            ViewBag.Galry = Galery;

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

            var Galery = db.ProductGalery.Where(x => x.VorhangschlossId == Vorhan.Id).ToList();

            ViewBag.Galry = Galery;

            var countV = new List<int>();

            var VorhanSize = db.Size.Where(x => x.VorhangschlossId == Vorhan.Id).ToList();

            var options = db.Vorhan_Options.Where(x => x.VorhangschlossId == VorhanItem.Id).ToList();

            if (options != null)
            {
                var OptionsSylinder = new List<Models.Vorhan.OptionsVorhan>();

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

            var Galery = db.ProductGalery.Where(x => x.Profil_HalbzylinderId == profil_Halbzylinder.Id).ToList();

            ViewBag.Galry = Galery;

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

            var Galery = db.ProductGalery.Where(x => x.Profil_KnaufzylinderId== Knayf.Id).ToList();

            ViewBag.Galry = Galery;

            var SizeKnyfzylinder = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == profil_Halbzylinder.Id).ToList();

            var KnayfKleinSize = db.Aussen_Innen_Knauf_klein.Where(x => x.Aussen_Innen_KnaufId == SizeKnyfzylinder[0].Id).ToList();

            if (KnayfKleinSize.Count > 0)
            {
                ViewBag.KleinAussen = SizeKnyfzylinder[0];
            }

            ViewBag.KnayfKleinSize = KnayfKleinSize;

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

                ViewBag.Size = SizeKnyfzylinder;

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
        public async Task<IActionResult> SaveHalbzylinder(Profil_Halbzylinder profil_Halbzylinder, List<int> Size, List<float> CostSize, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter,List<float> costSizeAussen, List<IFormFile> UploadGalleryImages, List<string> GalleryImages)
        {
            var Items = db.Profil_Halbzylinder.Find(profil_Halbzylinder.Id);
            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.Type = profil_Halbzylinder.Type;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;


            var Gallery = db.ProductGalery.Where(x => x.Profil_HalbzylinderId == profil_Halbzylinder.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {
                //string sourceFilePath = @"wwwroot/image/";

                //string imagePathToDelete = Path.Combine(sourceFilePath, list);

                //if (System.IO.File.Exists(imagePathToDelete))
                //{
                //    System.IO.File.Delete(imagePathToDelete);
                //}

                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        string extension = Path.GetExtension(image.FileName);

                        string originalImagePath = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        using (var fileStream = new FileStream(originalImagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        string webpFileName = fileName + ".webp";
                        string webpFilePath = Path.Combine(wwwRootPath + "/compression/", webpFileName);


                        if (!Directory.Exists(Path.Combine(wwwRootPath, "compression")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath, "compression"));
                        }

                        using (var imageSharpImage = await Image.LoadAsync(webpFilePath))
                        {
                            await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder());
                        }

                        await _imageOptimizationService.CompressImageAsync(originalImagePath, webpFilePath);

                        var itemGalary = new ProductGalery
                        {
                            Profil_HalbzylinderId = profil_Halbzylinder.Id,
                            ImageName = webpFileName
                        };

                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();

                    }
                }
            }

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

            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
        }
        [HttpPost]
        public async Task<IActionResult> SaveHebelzylinder(Hebel profil_Halbzylinder, List<string> Options,List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<IFormFile> UploadGalleryImages, List<string> GalleryImages)
        {
            var Items = db.Hebelzylinder.Find(profil_Halbzylinder.Id);

            var Gallery = db.ProductGalery.Where(x => x.HebelId == Items.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {               
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        // Получаем имя файла без расширения
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        // Получаем расширение файла
                        string extension = Path.GetExtension(image.FileName);

                        // Путь для сохранения оригинального файла
                        string originalImagePath = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        // Сохраняем оригинальное изображение на диск
                        using (var fileStream = new FileStream(originalImagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        string webpFileName = fileName + ".webp";
                        string webpFilePath = Path.Combine(wwwRootPath + "/compression/", webpFileName);


                        if (!Directory.Exists(Path.Combine(wwwRootPath, "compression")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath, "compression"));
                        }

                        using (var imageSharpImage = await Image.LoadAsync(webpFilePath))
                        {
                            await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder());
                        }

                        await _imageOptimizationService.CompressImageAsync(originalImagePath, webpFilePath);


                        var itemGalary = new ProductGalery
                        {
                            HebelId = Items.Id,
                            ImageName = webpFileName
                        };
                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();

                    }
                }
            }

            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.Type = profil_Halbzylinder.Type;
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

            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAussenzylinder_Rundzylinder(Aussenzylinder_Rundzylinder profil_Halbzylinder, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions,List<string> Name, List<string> Description, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<IFormFile> UploadGalleryImages, List<string> GalleryImages)
        {
            var Items = db.Aussenzylinder_Rundzylinder.Find(profil_Halbzylinder.Id);

            var Gallery = db.ProductGalery.Where(x => x.Aussenzylinder_RundzylinderId == profil_Halbzylinder.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        // Получаем имя файла без расширения
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        // Получаем расширение файла
                        string extension = Path.GetExtension(image.FileName);

                        // Путь для сохранения оригинального файла
                        string originalImagePath = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        // Сохраняем оригинальное изображение на диск
                        using (var fileStream = new FileStream(originalImagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        string webpFileName = fileName + ".webp";
                        string webpFilePath = Path.Combine(wwwRootPath + "/compression/", webpFileName);


                        if (!Directory.Exists(Path.Combine(wwwRootPath, "compression")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath, "compression"));
                        }

                        using (var imageSharpImage = await Image.LoadAsync(webpFilePath))
                        {
                            await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder());
                        }

                        await _imageOptimizationService.CompressImageAsync(originalImagePath, webpFilePath);


                        var itemGalary = new ProductGalery
                        {
                            Aussenzylinder_RundzylinderId = profil_Halbzylinder.Id,
                            ImageName = webpFileName
                        };
                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();

                    }
                }
            }


            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.Type = profil_Halbzylinder.Type;
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
            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
        }
        [HttpPost]
        public async Task<IActionResult> SaveVorhangschloss(Vorhangschloss profil_Halbzylinder,List<int>Size,List<float> CostSize, List<string> Options, List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<IFormFile> UploadGalleryImages, List<string> GalleryImages)
        {
            var Items = db.Vorhangschloss.Find(profil_Halbzylinder.Id);

            var Gallery = db.ProductGalery.Where(x => x.VorhangschlossId == Items.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {               
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                   
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        // Получаем имя файла без расширения
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        // Получаем расширение файла
                        string extension = Path.GetExtension(image.FileName);

                        // Путь для сохранения оригинального файла
                        string originalImagePath = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        // Сохраняем оригинальное изображение на диск
                        using (var fileStream = new FileStream(originalImagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        string webpFileName = fileName + ".webp";
                        string webpFilePath = Path.Combine(wwwRootPath + "/compression/", webpFileName);


                        if (!Directory.Exists(Path.Combine(wwwRootPath, "compression")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath, "compression"));
                        }

                        using (var imageSharpImage = await Image.LoadAsync(webpFilePath))
                        {
                            await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder());
                        }

                        await _imageOptimizationService.CompressImageAsync(originalImagePath, webpFilePath);


                        var itemGalary = new ProductGalery
                        {
                            VorhangschlossId = Items.Id,
                            ImageName = webpFileName
                        };
                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();
                    }
                }
            }

            Items.schliessanlagenId = profil_Halbzylinder.schliessanlagenId;
            Items.Name = profil_Halbzylinder.Name;
            Items.Type = profil_Halbzylinder.Type;
            Items.companyName = profil_Halbzylinder.companyName;
            Items.NameSystem = profil_Halbzylinder.NameSystem;
            Items.description = profil_Halbzylinder.description;
            Items.Price = profil_Halbzylinder.Price;
            Items.ImageName = profil_Halbzylinder.ImageName;

            var option = db.Vorhan_Options.Where(x => x.VorhangschlossId == profil_Halbzylinder.Id).ToList();

            if (Options.Count() == 0 || option.Count() > 0)
            {
                var listAllOption = new List<Models.Vorhan.OptionsVorhan>();

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

                    var createOptionsAussen = new Models.Vorhan.OptionsVorhan
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
                var SizeV = new Models.Vorhan.Size
                {
                    VorhangschlossId = Items.Id,
                    sizeVorhangschloss = Size[i],
                    Cost = CostSize[i],
                };
               db.Size.Add(SizeV);
            }
            db.SaveChanges();

            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfil_Knaufzylinder(Profil_Knaufzylinder profil_Knayf,List<int> SizeAus, List<int> SizeInen, List<string> Options,List<string> ImageNameOption,
        List<string> Descriptions, List<string> valueNGF, List<float> costNGF, List<int> inputCounter, List<float> costSizeAussen, List<float> costSizeIntern, List<IFormFile> UploadGalleryImages, List<string> GalleryImages,
        List<float> internDoppelKlein, List<float> priesDoppelKlein, float ausKlein, float ausKleinPreis)
        {
            var Items = db.Profil_Knaufzylinder.Find(profil_Knayf.Id);
            Items.schliessanlagenId = profil_Knayf.schliessanlagenId;
            Items.Name = profil_Knayf.Name;
            Items.Type = profil_Knayf.Type;
            Items.companyName = profil_Knayf.companyName;
            Items.NameSystem = profil_Knayf.NameSystem;
            Items.description = profil_Knayf.description;
            Items.Price = profil_Knayf.Price;
            Items.ImageName = profil_Knayf.ImageName;

            var Gallery = db.ProductGalery.Where(x => x.Profil_KnaufzylinderId == profil_Knayf.Id).ToList();

            var listN = Gallery.Select(x => x.ImageName).Except(GalleryImages).ToList();

            foreach (var list in listN)
            {              
                var deletItem = Gallery.FirstOrDefault(x => x.ImageName == list);
                db.ProductGalery.Remove(deletItem);
                db.SaveChanges();
            }


            if (UploadGalleryImages != null && UploadGalleryImages.Count > 0)
            {
                foreach (var image in UploadGalleryImages)
                {
                    if (image.Length > 0)
                    {
                        string wwwRootPath = Environment.WebRootPath;

                        // Получаем имя файла без расширения
                        string fileName = Path.GetFileNameWithoutExtension(image.FileName);

                        // Получаем расширение файла
                        string extension = Path.GetExtension(image.FileName);

                        // Путь для сохранения оригинального файла
                        string originalImagePath = Path.Combine(wwwRootPath + "/Image/", fileName + extension);

                        // Сохраняем оригинальное изображение на диск
                        using (var fileStream = new FileStream(originalImagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        string webpFileName = fileName + ".webp";
                        string webpFilePath = Path.Combine(wwwRootPath + "/compression/", webpFileName);

              
                        if (!Directory.Exists(Path.Combine(wwwRootPath, "compression")))
                        {
                            Directory.CreateDirectory(Path.Combine(wwwRootPath, "compression"));
                        }

                        using (var imageSharpImage = await Image.LoadAsync(webpFilePath))
                        {
                            await imageSharpImage.SaveAsync(webpFilePath, new WebpEncoder());
                        }

                        await _imageOptimizationService.CompressImageAsync(originalImagePath, webpFilePath);


                        var itemGalary = new ProductGalery
                        {
                            Profil_KnaufzylinderId = profil_Knayf.Id,
                            ImageName = webpFileName
                        };
                        db.ProductGalery.Add(itemGalary);
                        db.SaveChanges();

                    }
                }
            }

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
            var KnayfKleinIten = db.Aussen_Innen_Knauf_klein.Where(x => x.Aussen_Innen_KnaufId == KnayfItemSize_Cost[0].Id).ToList();

            foreach (var list in KnayfKleinIten)
            {
                db.Aussen_Innen_Knauf_klein.Remove(list);
            }
            db.SaveChanges();

            foreach (var list in KnayfItemSize_Cost)
            {
                db.Aussen_Innen_Knauf.Remove(list);
            }
            db.SaveChanges();

            if (ausKlein != 0)
            {
                var ausse_innenklein = new Aussen_Innen_Knauf
                {
                    Profil_KnaufzylinderId = Items.Id,
                    aussen = ausKlein,
                    costSizeAussen = ausKleinPreis,
                    costSizeIntern = 0,
                    Intern = 0
                };
                db.Aussen_Innen_Knauf.Add(ausse_innenklein);
                db.SaveChanges();

                for (int f = 0; f < internDoppelKlein.Count(); f++)
                {
                    var KnayfInternKlein = new Aussen_Innen_Knauf_klein
                    {
                        Aussen_Innen_KnaufId = ausse_innenklein.Id,
                        Intern = internDoppelKlein[f],
                        costSizeIntern = priesDoppelKlein[f]
                    };
                    db.Aussen_Innen_Knauf_klein.Add(KnayfInternKlein);
                    db.SaveChanges();
                }
            }

            for (int i = 0; i < SizeAus.Count(); i++)
            {
                var klein = db.Aussen_Innen_Knauf.FirstOrDefault(x => x.Profil_KnaufzylinderId == Items.Id && x.aussen == SizeAus[i]);

                if (klein == null)
                {
                    var SizeV = new Aussen_Innen_Knauf
                    {
                        Profil_KnaufzylinderId = Items.Id,
                        aussen = SizeAus[i],
                        Intern = SizeInen[i],
                        costSizeAussen = costSizeAussen[i],
                        costSizeIntern = costSizeIntern[i],
                    };
                    db.Aussen_Innen_Knauf.Add(SizeV);
                }

            }
            db.SaveChanges();
            return Ok(new { message = "Die Daten wurden erfolgreich gespeichert!" });
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

                List<Models.Vorhan.OptionsVorhan> ngf = new List<Models.Vorhan.OptionsVorhan>();

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
            var UserOrders = db.UserOrdersShop.Select(x => x).Distinct().ToList();
            
          
            var users = db.Users.Select(x => x).Distinct().ToList();

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
                                 createData = t2.createData,
                                 Status = t2.OrderStatus,
                                 BezalenDate = t2.BezalenDate,
                                 ShippingStatus = t2.ShippingStatus
                             };

            var Order = queryOrder.ToList();

            ViewBag.Order = Order.Distinct().ToList();


            return View();
        }

        [HttpPost]
        public ActionResult AllOrders(int id, string ShippingStatus,string OrderStatus,string Name, string email)
        {
            var UserOrderItem = db.UserOrdersShop.FirstOrDefault(x => x.Id == id);

            if (OrderStatus == "Bezahlt" && UserOrderItem.OrderStatus != "Bezahlt")
            {
                var Rehnung = db.Rehnungs.FirstOrDefault(x => x.UserOrdersShopId == UserOrderItem.Id);

                var filepath = Path.Combine($"~/Rehnung", $"{Rehnung.RehnungsId}");

                User user = db.User.FirstOrDefault(x => x.UserName == email);

                UserOrderItem.BezalenDate = DateTime.Now.ToLocalTime();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Schliessanlagen Store", "oceanwerbung@googlemail.com"));
                message.To.Add(new MailboxAddress(user.FirstName + user.LastName, "info@schluessel.discount"));
                message.Subject = "Schlüssel Discount Store";
                message.Body = new TextPart("plain")
                {
                    Text = $"Online bestellt unter  https://schliessanlagen.discount/  \n\n Kunde:{user.FirstName + user.LastName}",
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("oceanwerbung@googlemail.com", "bouo yqop xsdl qpar");
                    client.Send(message);
                    client.Disconnect(true);
                }

                var message2 = new MimeMessage();
                message2.From.Add(new MailboxAddress("Schliessanlagen Store", "oceanwerbung@googlemail.com"));
                message2.To.Add(new MailboxAddress(user.FirstName + user.LastName, user.UserName));
                message2.Subject = "Schlüssel Discount Store";
                message2.Body = new TextPart("plain")
                {
                    Text = $"Ihre Bestellung {Rehnung.UserOrdersShopId} wird gerade bearbeitet\r\n",
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("oceanwerbung@googlemail.com", "bouo yqop xsdl qpar");
                    client.Send(message2);
                    client.Disconnect(true);
                }

            }
            else
            {
                if (UserOrderItem.OrderStatus == "Bezahlt")
                {
                    UserOrderItem.ShippingStatus = ShippingStatus;
                    var Rehnung = db.Rehnungs.FirstOrDefault(x => x.UserOrdersShopId == UserOrderItem.Id);

                    User user = db.User.FirstOrDefault(x => x.UserName == email);

                    UserOrderItem.BezalenDate = DateTime.Now.ToLocalTime();

                   
                    var message2 = new MimeMessage();
                    message2.From.Add(new MailboxAddress("Schliessanlagen Store", "oceanwerbung@googlemail.com"));
                    message2.To.Add(new MailboxAddress(user.FirstName + user.LastName, user.UserName));
                    message2.Subject = "Schlüssel Discount Store";
                    message2.Body = new TextPart("plain")
                    {
                        Text = $"Ihr Auftrag {Rehnung.UserOrdersShopId} ist erfüllt!\r\nVersandstatus: {ShippingStatus} ",
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("oceanwerbung@googlemail.com", "bouo yqop xsdl qpar");
                        client.Send(message2);
                        client.Disconnect(true);
                    }
                }
                else
                {
                    UserOrderItem.BezalenDate = null;
                }    
            }

            UserOrderItem.OrderStatus = OrderStatus;

            db.UserOrdersShop.Update(UserOrderItem);

            db.SaveChanges();


            var User = db.Users.Select(x => x).ToList();
            var UserProduct = db.ProductSysteam.Select(x => x).ToList();
            var UserOrders = db.UserOrdersShop.Select(x => x).Distinct().ToList();


            var users = db.Users.Select(x => x).Distinct().ToList();

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
                                 createData = t2.createData,
                                 Status = t2.OrderStatus,
                                 BezalenDate = t2.BezalenDate,
                                 ShippingStatus = t2.ShippingStatus
                             };

            var Order = queryOrder.ToList();

            ViewBag.Order = Order.Distinct().ToList();

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
        public ActionResult DowloadRehnung(int? Id)
        {
            var UserOrder = db.UserOrdersShop.FirstOrDefault(x => x.Id == Id).createData;

            var Rehnung = db.Rehnungs.FirstOrDefault(x => x.UserOrdersShopId == Id);

            var filepath = Path.Combine($"~/Rehnung", $"{Rehnung.RehnungsId}");

            return File(filepath, "application/pdf", $"{Rehnung.RehnungsId}");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Response.StatusCode = 404;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
