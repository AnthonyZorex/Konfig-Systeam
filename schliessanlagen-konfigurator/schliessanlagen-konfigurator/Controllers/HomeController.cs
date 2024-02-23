using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models;
using System.Diagnostics;
using schliessanlagen_konfigurator.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using schliessanlagen_konfigurator.IEnumerable.DoppelZylinder;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
        public async Task<IActionResult> AdminConnect()
        {
            //MailAddress from = new MailAddress("bonettaanthony466@gmail.com", "Anthony");
            //MailAddress to = new MailAddress("anthonybonetta@icloud.com");
            //MailMessage m = new MailMessage(from, to);
            //m.Subject = "Тест";
            //m.Body = "Письмо-тест 2 работы smtp-клиента";
            //SmtpClient smtp = new SmtpClient("localhost", 25);
            //smtp.Credentials = new NetworkCredential("bonettaanthony466@gmail.com", "Anthony_2001");
            //smtp.EnableSsl = true;
            //await smtp.SendMailAsync(m);

            SmtpClient Smtp = new SmtpClient("sslin.de", 993);
            Smtp.UseDefaultCredentials = false;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.Credentials = new NetworkCredential("account@sicher.discount", "64tSf&ecJ6q62U");
            MailMessage email = new MailMessage();
            email.From = new MailAddress("account@sicher.discount");
            email.Subject = "subject";
            email.Body = "text Hello Body";
            //Attachment attach = new Attachment("filename"); ;
            //email.Attachments.Add(attach);
            email.To.Add("bonettaanthony466@gmail.com");
            await Smtp.SendMailAsync(email);

            return View();
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
        public async Task<IActionResult> Create_Profil_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder)
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

                db.Profil_Doppelzylinder.Add(profil_Doppelzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
        public async Task<IActionResult> Create_Profil_Knaufzylinder(Profil_Knaufzylinder Profil_Knaufzylinder)
        {
            try
            {
                if (Profil_Knaufzylinder.ImageFile != null)
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
                }

                db.Profil_Knaufzylinder.Add(Profil_Knaufzylinder);
                await db.SaveChangesAsync();
                return RedirectToAction("Profil_KnaufzylinderRout");
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
        public async Task<IActionResult> Create_Hebelzylinder(Hebelzylinder profil_Doppelzylinder)
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

        public async Task<IActionResult> Delete_Doppelzylinder(Profil_Doppelzylinder profil_Doppelzylinder, Profil_Halbzylinder profil_Halbzylinder, Profil_Knaufzylinder Profil_Knaufzylinder, Vorhangschloss Vorhangschloss, Hebelzylinder hebelzylinder, Aussenzylinder_Rundzylinder aussenzylinder_Rundzylinder)
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
                db.Profil_Doppelzylinder.Remove(doppelzylinder);
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
        public async Task<IActionResult> Edit_Halbzylinder(Hebelzylinder profil_Halbzylinder)
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
        public ActionResult SaveHebelzylinder(Hebelzylinder profil_Halbzylinder)
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
            
            if(profilInfo.companyName == "ABUS")
            {
                if(profilInfo.NameSystem == "Bravus.2000")
                {

                    var aussen = new List<float>();
                    aussen.Add(profilInfo.aussen);

                    var innen = new List<float>();
                    innen.Add(profilInfo.Intern);

                    for (; aussen.Last() < profilInfo.maxSizeAussen;)
                    {
                        aussen.Add(aussen.Last() + 5);
                    }
                    for (;  innen.Last() < profilInfo.maxSizeIntern;)
                    {
                        innen.Add(innen.Last() + 5);
                    }
                    var DoppelZylinderNGF = from OptionDoppelZylinderNGF e in Enum.GetValues(typeof(OptionDoppelZylinderNGF))
                                            select new
                                            {
                                                ID = (int)e,
                                                Name = e.ToString()
                                            };

                    var DoppelZylinder_Freilauffunktion = from Option_DoppelZylinder_Freilauffunktion e in Enum.GetValues(typeof(Option_DoppelZylinder_Freilauffunktion))
                                                          select new
                                                          {
                                                              ID = (int)e,
                                                              Name = e.ToString()
                                                          };


                    var Staubkappe = from OptionDoppelZylinderStaubkappe e in Enum.GetValues(typeof(OptionDoppelZylinderStaubkappe))
                                     select new
                                     {
                                         ID = (int)e,
                                         Name = e.ToString()
                                     };

                    ViewBag.DoppelZylinderNGF = new SelectList(DoppelZylinderNGF, "ID", "Name");
                    ViewBag.DoppelZylinder_Freilauffunktion = new SelectList(DoppelZylinder_Freilauffunktion, "ID", "Name");
                    ViewBag.Staubkappe = new SelectList(Staubkappe, "ID", "Name");
                    ViewBag.aussen = aussen;
                    ViewBag.innen = innen;
                }
                if (profilInfo.NameSystem == "Bravus.3000")
                {

                    var aussen = new List<float>();
                    aussen.Add(profilInfo.aussen);

                    var innen = new List<float>();
                    innen.Add(profilInfo.Intern);

                    for (; aussen.Last() < profilInfo.maxSizeAussen;)
                    {
                        aussen.Add(aussen.Last() + 5);
                    }
                    for (; innen.Last() < profilInfo.maxSizeIntern;)
                    {
                        innen.Add(innen.Last() + 5);
                    }
                    var DoppelZylinderNGF = from OptionDoppelZylinderNGF e in Enum.GetValues(typeof(OptionDoppelZylinderNGF))
                                            select new
                                            {
                                                ID = (int)e,
                                                Name = e.ToString()
                                            };

                    var DoppelZylinder_Freilauffunktion = from Option_DoppelZylinder_Freilauffunktion e in Enum.GetValues(typeof(Option_DoppelZylinder_Freilauffunktion))
                                                          select new
                                                          {
                                                              ID = (int)e,
                                                              Name = e.ToString()
                                                          };


                    var Staubkappe = from OptionDoppelZylinderStaubkappe e in Enum.GetValues(typeof(OptionDoppelZylinderStaubkappe))
                                     select new
                                     {
                                         ID = (int)e,
                                         Name = e.ToString()
                                     };

                    ViewBag.DoppelZylinderNGF = new SelectList(DoppelZylinderNGF, "ID", "Name");
                    ViewBag.DoppelZylinder_Freilauffunktion = new SelectList(DoppelZylinder_Freilauffunktion, "ID", "Name");
                    ViewBag.Staubkappe = new SelectList(Staubkappe, "ID", "Name");
                    ViewBag.aussen = aussen;
                    ViewBag.innen = innen;
                }
            }    
            if (profilInfo.companyName == "CES")
            {
                if (profilInfo.NameSystem == "WO")
                {
                    var aussen = new List<int>();
                    aussen.Add(27);
                    aussen.Add(31);
                    aussen.Add(35);

                    var innen = new List<int>();
                    innen.Add(27);
                    innen.Add(31);
                    innen.Add(35);

                    for (; aussen.Last() < profilInfo.maxSizeAussen;)
                    {
                        aussen.Add(aussen.Last() + 5);
                    }
                    for (; innen.Last() < profilInfo.maxSizeIntern;)
                    {
                        innen.Add(innen.Last() + 5);
                    }
                    var DoppelZylinderNGF = from OptionDoppelZylinderNGF e in Enum.GetValues(typeof(OptionDoppelZylinderNGF))
                                            select new
                                            {
                                                ID = (int)e,
                                                Name = e.ToString()
                                            };

                    var DoppelZylinder_Freilauffunktion = from Option_DoppelZylinder_Freilauffunktion e in Enum.GetValues(typeof(Option_DoppelZylinder_Freilauffunktion))
                                                          select new
                                                          {
                                                              ID = (int)e,
                                                              Name = e.ToString()
                                                          };


                    var Staubkappe = from OptionDoppelZylinderStaubkappe e in Enum.GetValues(typeof(OptionDoppelZylinderStaubkappe))
                                     select new
                                     {
                                         ID = (int)e,
                                         Name = e.ToString()
                                     };

                    ViewBag.DoppelZylinderNGF = new SelectList(DoppelZylinderNGF, "ID", "Name");
                    ViewBag.DoppelZylinder_Freilauffunktion = new SelectList(DoppelZylinder_Freilauffunktion, "ID", "Name");
                    ViewBag.Staubkappe = new SelectList(Staubkappe, "ID", "Name");
                    ViewBag.aussen = aussen;
                    ViewBag.innen = innen;
                }

                if (profilInfo.NameSystem == "UDM ")
                {
                    var aussen = new List<int>();
                    aussen.Add(27);
                    aussen.Add(31);
                    aussen.Add(35);

                    var innen = new List<int>();
                    innen.Add(27);
                    innen.Add(31);
                    innen.Add(35);

                    for (; aussen.Last() < profilInfo.maxSizeAussen;)
                    {
                        aussen.Add(aussen.Last() + 5);
                    }
                    for (; innen.Last() < profilInfo.maxSizeIntern;)
                    {
                        innen.Add(innen.Last() + 5);
                    }
                    var DoppelZylinderNGF = from OptionDoppelZylinderNGF e in Enum.GetValues(typeof(OptionDoppelZylinderNGF))
                                            select new
                                            {
                                                ID = (int)e,
                                                Name = e.ToString()
                                            };

                    var DoppelZylinder_Freilauffunktion = from Option_DoppelZylinder_Freilauffunktion e in Enum.GetValues(typeof(Option_DoppelZylinder_Freilauffunktion))
                                                          select new
                                                          {
                                                              ID = (int)e,
                                                              Name = e.ToString()
                                                          };


                    var Staubkappe = from OptionDoppelZylinderStaubkappe e in Enum.GetValues(typeof(OptionDoppelZylinderStaubkappe))
                                     select new
                                     {
                                         ID = (int)e,
                                         Name = e.ToString()
                                     };

                    ViewBag.DoppelZylinderNGF = new SelectList(DoppelZylinderNGF, "ID", "Name");
                    ViewBag.DoppelZylinder_Freilauffunktion = new SelectList(DoppelZylinder_Freilauffunktion, "ID", "Name");
                    ViewBag.Staubkappe = new SelectList(Staubkappe, "ID", "Name");
                    ViewBag.aussen = aussen;
                    ViewBag.innen = innen;
                }
                   
            }

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
            var options = db.Options.ToList();

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
                db.Options.Add(optionsItem);
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
