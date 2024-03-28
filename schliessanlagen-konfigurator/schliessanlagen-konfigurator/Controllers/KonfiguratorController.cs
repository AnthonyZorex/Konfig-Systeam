using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
using schliessanlagenkonfigurator.Migrations;
using schliessanlagen_konfigurator.Models.Users;
using SkiaSharp;
using Spire.Xls;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;

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
        public async Task<ActionResult> UserSave(string Name, string VorName, string Login,string Password,string Email,string Telefonnumer,string Status)
        {
            var newUser = new Models.Users.User
            {
               Name = Name,
               Email = Email,
               Password = Password,
               Login = Login,
               Status = Status,
               Sername = VorName,
               PhoneNumber = Telefonnumer
            };
            
            db.User.Add(newUser);
            db.SaveChanges();
            ViewBag.UserInfo = newUser;
            return View();
        }
        public async Task<ActionResult> LoginPerson(string Login,string Password)
        {
            var UserLogin = db.User.Where(x => x.Login == Login && x.Password == Password);
            return View("IndexKonfigurator",UserLogin);
        }
        public ActionResult IndexKonfigurator(string Status)
        {
            ViewBag.Zylinder_Typ = db.Schliessanlagen.ToList();

            #region knayf allParametrsDoppel
            var a =  db.Aussen_Innen.Select(x=>x.aussen).ToList();
            var d =  db.Aussen_Innen.Select(x => x.Intern).ToList();

            var DoppeloptionsName = db.NGF.Select(x => x.Name).ToList();

            var listAllInnen = new List<float>();

            var ListAussenDopple = new List<float>();

            for (int i = 0; i < a.Count(); i++)
                ListAussenDopple.Add(a[i]);

            ViewBag.DoppelAussen = ListAussenDopple.Distinct();

            var ListInternDopple = new List<float>();
            for (int i = 0; i < d.Count(); i++)
                ListInternDopple.Add(d[i]);

            ViewBag.DoppelIntern = ListInternDopple.Distinct();

            var DoppelListOptions = new List<string>();

            for (int i = 0; i < DoppeloptionsName.Count; i++)
                DoppelListOptions.Add(DoppeloptionsName[i]);

            ViewBag.OptionsName = DoppelListOptions.Distinct();

            #endregion
            #region knayf allParametrsKnayf

            var b =  db.Aussen_Innen_Knauf.Select(x => x.aussen).ToList();
            var ba = db.Aussen_Innen_Knauf.Select(x => x.Intern).ToList();
            var KnayfOptions = db.Knayf_Options.Select(x => x.Name).ToList();

            var listKnayfAussen = new List<float>();
            var listKnayfIntern = new List<float>();
            var KnayfListOptions = new List<string>();

            for (int i = 0; i < b.Count(); i++)
                listKnayfAussen.Add(b[i]);

            ViewBag.KnayfAussen = listKnayfAussen.Distinct();

            for (int i = 0; i < ba.Count(); i++)
                listKnayfIntern.Add(ba[i]);

            ViewBag.KnayfIntern = listKnayfIntern.Distinct();

            for (int i = 0; i < KnayfOptions.Count(); i++)
                KnayfListOptions.Add(KnayfOptions[i]);

            ViewBag.KnayfOptions = KnayfOptions.Distinct();

            #endregion
            #region allPrametrsHalbzylinder
            var c =  db.Aussen_Innen_Halbzylinder.Select(x => x.aussen).ToList();
            
            var HalbzylinderAussen = new List<float>();

            for(int i =0;i<c.Count();i++)
                HalbzylinderAussen.Add(c[i]);

            ViewBag.HalbzylinderAllAussen = HalbzylinderAussen.Distinct();

            var HalbzylinderOptions = db.Halbzylinder_Options.Select(x=>x.Name).ToList();
            var HalbzylunderAllOptions = new List<string>();

            for (int i = 0; i < HalbzylinderOptions.Count(); i++)
                HalbzylunderAllOptions.Add(HalbzylinderOptions[i]);
            
            ViewBag.Halbzylunder_All_Options = HalbzylunderAllOptions.Distinct();
            #endregion
            #region VorhanSchloss

            var size =  db.Size.Select(x => x.sizeVorhangschloss).ToList();
            var VorhanSchlossSize = new List<float>();

            for (int i = 0; i < size.Count(); i++)
                VorhanSchlossSize.Add(size[i]);

            var VorhanSchloss = db.OptionsVorhan.Select(x => x.Name).ToList();
            var VorhanSchlossOptions = new List<string>();

            #endregion

            #region Hebel
           
            var HebelOptions =  db.Options.Select(x => x.Name).ToList();
            var HebelAllOptions = new List<string>();
            
            for(int i = 0; i < HebelOptions.Count(); i++)
            HebelAllOptions.Add(HebelOptions[i]);
            
            #endregion

            #region Aussen
            
            var AussenOptions =  db.Aussen_Rund_all.Select(x => x.Name).ToList();
            var AussenAllOptions = new List<string>();
            for (int i = 0; i < AussenOptions.Count(); i++)
            AussenAllOptions.Add(AussenOptions[i]);
           
            #endregion

            var session = _contextAccessor.HttpContext.Session;
            var UserKey = session.Id;
            Orders user = new Orders();
            user.userKey = UserKey;
            ViewBag.isOpen = db.KeyValue.Select(x => x.isOpen);

            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult> System_Auswählen(Orders userKey)
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
            var cheked2 = new List<Profil_Knaufzylinder>();
            var cheked3 = new List<Profil_Halbzylinder>();
            var cheked4 = new List<Hebel>(); 
            var cheked5 = new List<Vorhangschloss>();
            var cheked6 = new List<Aussenzylinder_Rundzylinder>();
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

                    for (int i = 0; i < item.Count(); i++)
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

                    for (int i = 0; i < cheked.Count(); i++)
                    {
                        var options = db.Profil_Doppelzylinder_Options.Where(x => x.DoppelzylinderId == cheked[i].Id).ToList();
                        for (int fs = 0; fs < options.Count(); fs++)
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
                            foreach (var list in ms)
                                dopelProduct.Add(list);
                        }
                    }

                }


                int KnayfType = 0;

                if (d >= profilK.Count())
                {
                    KnayfType = profilK.Last().schliessanlagenId;
                }
                else
                {
                    KnayfType = profilK[d].schliessanlagenId;
                }

                if (allUserListOrder[d].ZylinderId == KnayfType)
                {



                    if (d >= profilK.Count())
                    {
                        KnayfType = profilK.Last().schliessanlagenId;
                    }
                    else
                    {
                        KnayfType = profilK[d].schliessanlagenId;
                    }
                    if (allUserListOrder[d].ZylinderId == KnayfType)
                    {

                        var dopelProduct = new List<Profil_Knaufzylinder>();

                        var products = await db.Aussen_Innen_Knauf.ToListAsync();

                        var item = products.Where(x => x.aussen <= allUserListOrder[d].aussen & x.Intern <= allUserListOrder[d].innen).ToList();



                        var safeDoppelItem = new List<Profil_Knaufzylinder>();

                        for (int i = 0; i < item.Count(); i++)
                        {
                            var chekedItem = db.Profil_Knaufzylinder.Where(x => x.Id == item[i].Profil_KnaufzylinderId).ToList();

                            for (int g = 0; g < chekedItem.Count(); g++)
                                safeDoppelItem.Add(chekedItem[g]);

                        }

                        for (int j = 0; j < safeDoppelItem.Count(); j++)
                        {
                            cheked2.Add(safeDoppelItem[j]);
                        }

                        var OptionsList = new List<Profil_Knaufzylinder_Options>();

                        var results = new List<Knayf_Options>();

                        var opt = new List<Profil_Knaufzylinder_Options>();

                        for (int i = 0; i < cheked2.Count(); i++)
                        {
                            var options = db.Profil_Knaufzylinder_Options.Where(x => x.Profil_KnaufzylinderId == cheked2[i].Id).ToList();
                            for (int fs = 0; fs < options.Count(); fs++)
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

                            var resultList = new List<Knayf_Options>();

                            for (int f = 0; f < OptionsList.Count(); f++)
                            {
                                var OptionsName = db.Knayf_Options.Where(x => x.OptionsId == OptionsList[f].Id).ToList();

                                var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                                for (int s = 0; s < result.Count(); s++)
                                {
                                    resultList.Add(result[s]);

                                }


                            }


                            for (int s = 0; s < resultList.Count(); s++)
                            {
                                var ItemList = db.Profil_Knaufzylinder_Options.Where(x => x.Id == resultList[s].OptionsId).ToList();
                                OptionsList = ItemList;
                            }


                            for (int l = 0; l < OptionsList.Count(); l++)
                            {
                                var dopel = db.Profil_Knaufzylinder.Where(x => x.Id == OptionsList[l].Profil_KnaufzylinderId).ToList();

                                cheked2 = dopel;
                            }

                            for (int xf = 0; xf < cheked.Count(); xf++)
                            {
                                var ms = db.Profil_Knaufzylinder.Where(x => x.Id == cheked[xf].Id).Select(x => x).ToList();
                                foreach (var list in ms)
                                    dopelProduct.Add(list);
                            }
                            ViewBag.b = dopelProduct.ToList();
                        }

                    }

                }
                

                int HalbType = 0;

                if (d >= profilH.Count())
                {
                    HalbType = profilH.Last().schliessanlagenId;
                }
                else
                {
                    HalbType = profilH[d].schliessanlagenId;
                }

                if (allUserListOrder[d].ZylinderId == HalbType)
                {


                        var dopelProduct = new List<Profil_Halbzylinder>();

                        var products = await db.Aussen_Innen_Halbzylinder.ToListAsync();

                        var item = products.Where(x => x.aussen <= allUserListOrder[d].aussen).ToList();



                        var safeDoppelItem = new List<Profil_Halbzylinder>();

                        for (int i = 0; i < item.Count(); i++)
                        {
                            var chekedItem = db.Profil_Halbzylinder.Where(x => x.Id == item[i].Profil_HalbzylinderId).ToList();

                            for (int g = 0; g < chekedItem.Count(); g++)
                                safeDoppelItem.Add(chekedItem[g]);

                        }

                        for (int j = 0; j < safeDoppelItem.Count(); j++)
                        {
                            cheked3.Add(safeDoppelItem[j]);
                        }

                        var OptionsList = new List<Profil_Halbzylinder_Options>();

                        var results = new List<Halbzylinder_Options>();

                        var opt = new List<Profil_Halbzylinder_Options>();

                        for (int i = 0; i < cheked3.Count(); i++)
                        {
                            var options = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == cheked3[i].Id).ToList();
                            for (int fs = 0; fs < options.Count(); fs++)
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

                            var resultList = new List<Halbzylinder_Options>();

                            for (int f = 0; f < OptionsList.Count(); f++)
                            {
                                var OptionsName = db.Halbzylinder_Options.Where(x => x.OptionsId == OptionsList[f].Id).ToList();

                                var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                                for (int s = 0; s < result.Count(); s++)
                                {
                                    resultList.Add(result[s]);

                                }


                            }


                            for (int s = 0; s < resultList.Count(); s++)
                            {
                                var ItemList = db.Profil_Halbzylinder_Options.Where(x => x.Id == resultList[s].OptionsId).ToList();
                                OptionsList = ItemList;
                            }


                            for (int l = 0; l < OptionsList.Count(); l++)
                            {
                                var dopel = db.Profil_Halbzylinder.Where(x => x.Id == OptionsList[l].Profil_HalbzylinderId).ToList();

                                cheked3 = dopel;
                            }

                            for (int xf = 0; xf < cheked3.Count(); xf++)
                            {
                                var ms = db.Profil_Halbzylinder.Where(x => x.Id == cheked3[xf].Id).Select(x => x).ToList();
                                foreach (var list in ms)
                                    dopelProduct.Add(list);
                            }
                            ViewBag.c = dopelProduct.ToList();
                        }

                   

                }

                int HebelType = 0;

                if (d >= hebel.Count())
                {
                    HebelType = hebel.Last().schliessanlagenId;
                }
                else
                {
                    HebelType = hebel[d].schliessanlagenId;
                }

                if (allUserListOrder[d].ZylinderId == HebelType)
                {

                        var dopelProduct = new List<Hebel>();

                      
                        var safeDoppelItem = new List<Hebel>();

                        var chekedItem = db.Hebelzylinder.ToList();

                        for (int i = 0; i < chekedItem.Count(); i++)
                        {
                            safeDoppelItem.Add(chekedItem[i]);

                        }

                        for (int j = 0; j < safeDoppelItem.Count(); j++)
                        {
                            cheked4.Add(safeDoppelItem[j]);
                        }

                        var OptionsList = new List<Hebelzylinder_Options>();

                        var results = new List<Options>();

                        var opt = new List<Hebelzylinder_Options>();

                        for (int i = 0; i < cheked4.Count(); i++)
                        {
                            var options = db.Hebelzylinder_Options.Where(x => x.HebelzylinderId == cheked4[i].Id).ToList();
                           
                            for (int fs = 0; fs < options.Count(); fs++)
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

                            var resultList = new List<Options>();

                            for (int f = 0; f < OptionsList.Count(); f++)
                            {
                                var OptionsName = db.Options.Where(x => x.Id == OptionsList[f].Id).ToList();

                                var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                                for (int s = 0; s < result.Count(); s++)
                                {
                                    resultList.Add(result[s]);

                                }


                            }


                            for (int s = 0; s < resultList.Count(); s++)
                            {
                                var ItemList = db.Hebelzylinder_Options.Where(x => x.Id == resultList[s].Id).ToList();
                                OptionsList = ItemList;
                            }


                            for (int l = 0; l < OptionsList.Count(); l++)
                            {
                                var dopel = db.Hebelzylinder.Where(x => x.Id == OptionsList[l].Id).ToList();

                                cheked4 = dopel;
                            }
                            
                            ViewBag.d = dopelProduct.ToList();
                        }

                   

                }
                int VorhanType = 0;

                if (d >= Vorhangschloss.Count())
                {
                    VorhanType = Vorhangschloss.Last().schliessanlagenId;
                }
                else
                {
                    VorhanType = Vorhangschloss[d].schliessanlagenId;
                }

                if (allUserListOrder[d].ZylinderId == VorhanType)
                {

                    var dopelProduct = new List<Vorhangschloss>();


                    var safeDoppelItem = new List<Vorhangschloss>();

                    var chekedItem = db.Vorhangschloss.ToList();

                    for (int i = 0; i < chekedItem.Count(); i++)
                    {
                        safeDoppelItem.Add(chekedItem[i]);

                    }

                    for (int j = 0; j < safeDoppelItem.Count(); j++)
                    {
                        cheked5.Add(safeDoppelItem[j]);
                    }

                    var OptionsList = new List<Vorhan_Options>();

                    var results = new List<OptionsVorhan>();

                    var opt = new List<Vorhan_Options>();

                    for (int i = 0; i < cheked5.Count(); i++)
                    {
                        var options = db.Vorhan_Options.Where(x => x.VorhangschlossId == cheked5[i].Id).ToList();

                        for (int fs = 0; fs < options.Count(); fs++)
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

                        var resultList = new List<OptionsVorhan>();

                        for (int f = 0; f < OptionsList.Count(); f++)
                        {
                            var OptionsName = db.OptionsVorhan.Where(x => x.Id == OptionsList[f].Id).ToList();

                            var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                            for (int s = 0; s < result.Count(); s++)
                            {
                                resultList.Add(result[s]);

                            }


                        }


                        for (int s = 0; s < resultList.Count(); s++)
                        {
                            var ItemList = db.Vorhan_Options.Where(x => x.Id == resultList[s].Id).ToList();
                            OptionsList = ItemList;
                        }


                        for (int l = 0; l < OptionsList.Count(); l++)
                        {
                            var dopel = db.Vorhangschloss.Where(x => x.Id == OptionsList[l].Id).ToList();

                            cheked5 = dopel;
                        }

                        ViewBag.d = dopelProduct.ToList();
                    }



                }
                int AussenType = 0;

                if (d >= Aussenzylinder.Count())
                {
                    AussenType = Aussenzylinder.Last().schliessanlagenId;
                }
                else
                {
                    AussenType = Aussenzylinder[d].schliessanlagenId;
                }

                if (allUserListOrder[d].ZylinderId == AussenType)
                {

                    var dopelProduct = new List<Aussenzylinder_Rundzylinder>();


                    var safeDoppelItem = new List<Aussenzylinder_Rundzylinder>();

                    var chekedItem = db.Aussenzylinder_Rundzylinder.ToList();

                    for (int i = 0; i < chekedItem.Count(); i++)
                    {
                        safeDoppelItem.Add(chekedItem[i]);

                    }

                    for (int j = 0; j < safeDoppelItem.Count(); j++)
                    {
                        cheked6.Add(safeDoppelItem[j]);
                    }

                    var OptionsList = new List<Aussen_Rund_options>();

                    var results = new List<Aussen_Rund_all>();

                    var opt = new List<Aussen_Rund_options>();

                    for (int i = 0; i < cheked6.Count(); i++)
                    {
                        var options = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == cheked6[i].Id).ToList();

                        for (int fs = 0; fs < options.Count(); fs++)
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

                        var resultList = new List<Aussen_Rund_all>();

                        for (int f = 0; f < OptionsList.Count(); f++)
                        {
                            var OptionsName = db.Aussen_Rund_all.Where(x => x.Id == OptionsList[f].Id).ToList();

                            var result = OptionsName.Where(x => x.Name == allUserListOrder[d].Options).ToList();
                            for (int s = 0; s < result.Count(); s++)
                            {
                                resultList.Add(result[s]);

                            }


                        }


                        for (int s = 0; s < resultList.Count(); s++)
                        {
                            var ItemList = db.Aussen_Rund_options.Where(x => x.Id == resultList[s].Id).ToList();
                            OptionsList = ItemList;
                        }


                        for (int l = 0; l < OptionsList.Count(); l++)
                        {
                            var dopel = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == OptionsList[l].Id).ToList();

                            cheked6 = dopel;
                        }

                        ViewBag.d = dopelProduct.ToList();
                    }



                }

                if (cheked.Count() > 0)
                {
                    var query = from t1 in cheked
                                join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                select new
                                {
                                    Id = t1.Id,
                                    userKey = keyUser.userKey,
                                    Name = t1.Name,
                                    companyName = t1.companyName,
                                    description = t1.description,
                                    NameSystem = t1.NameSystem,
                                    Cost = t1.Cost,
                                    ImageName = t1.ImageName

                                };
                    var rl = query.Distinct().ToList();

                    ViewBag.Doppel = rl.ToList();
                }

                if (cheked2.Count() > 0)
                {
                    var queryOrder = from t1 in cheked2
                                     join t2 in allUserListOrder
                                    on t1.schliessanlagenId equals t2.ZylinderId
                                     select new
                                     {
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Knaufzylinder = queryOrder.Distinct().ToList();

                    ViewBag.Knaufzylinder = Knaufzylinder;
                }
                if (cheked3.Count() > 0)
                {
                    var query = from t1 in cheked3
                                join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                select new
                                {
                                    Id = t1.Id,
                                    userKey = keyUser.userKey,
                                    Name = t1.Name,
                                    companyName = t1.companyName,
                                    description = t1.description,
                                    NameSystem = t1.NameSystem,
                                    Cost = t1.Cost,
                                    ImageName = t1.ImageName

                                };
                    var rl = query.Distinct().ToList();

                    ViewBag.Halb = rl.ToList();
                }
                if (cheked4.Count() > 0)
                {
                    var query = from t1 in cheked4
                                join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                select new
                                {
                                    Id = t1.Id,
                                    userKey = keyUser.userKey,
                                    Name = t1.Name,
                                    companyName = t1.companyName,
                                    description = t1.description,
                                    NameSystem = t1.NameSystem,
                                    Cost = t1.Cost,
                                    ImageName = t1.ImageName

                                };
                    var rl = query.Distinct().ToList();

                    ViewBag.Hebel = rl.ToList();
                }
                if (cheked5.Count() > 0)
                {
                    var query = from t1 in cheked5
                                join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                select new
                                {
                                    Id = t1.Id,
                                    userKey = keyUser.userKey,
                                    Name = t1.Name,
                                    companyName = t1.companyName,
                                    description = t1.description,
                                    NameSystem = t1.NameSystem,
                                    Cost = t1.Cost,
                                    ImageName = t1.ImageName

                                };
                    var rl = query.Distinct().ToList();

                    ViewBag.VorhanSchloss = rl.ToList();
                }
                if (cheked6.Count() > 0)
                {
                    var query = from t1 in cheked5
                                join t2 in allUserListOrder
                                on t1.schliessanlagenId equals t2.ZylinderId
                                select new
                                {
                                    Id = t1.Id,
                                    userKey = keyUser.userKey,
                                    Name = t1.Name,
                                    companyName = t1.companyName,
                                    description = t1.description,
                                    NameSystem = t1.NameSystem,
                                    Cost = t1.Cost,
                                    ImageName = t1.ImageName

                                };
                    var rl = query.Distinct().ToList();

                    ViewBag.Aussen = rl.ToList();
                }
                if (cheked2.Count() > 0 && cheked.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked2
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();

                    ViewBag.Doppel = Join;
                    ViewBag.Knaufzylinder = Join;
                }

                if (cheked.Count() > 0 && cheked3.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked3
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked3 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };
                    var Join = queryOrder.Distinct().ToList();
                    ViewBag.a = Join;
                    ViewBag.DH = Join;
                }
                if (cheked.Count() > 0 && cheked4.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked4
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked4 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };
                    var Join = queryOrder.Distinct().ToList();
                    ViewBag.a = Join;
                    ViewBag.DHE = Join;
                }
                if (cheked.Count() > 0 && cheked5.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked5
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked5 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };
                    var Join = queryOrder.Distinct().ToList();
                    ViewBag.a = Join;
                    ViewBag.DV = Join;
                }

                if (cheked.Count() > 0 && cheked6.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked6
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked6 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };
                    var Join = queryOrder.Distinct().ToList();
                    ViewBag.a = Join;
                    ViewBag.DA = Join;
                }

//xxxxxxxxxxxxxxxxxxxxxxx

                if (cheked2.Count() > 0 && cheked3.Count() > 0)
                {
                    var queryOrder = from t1 in cheked2
                                     join t2 in cheked3
                                     on t1.NameSystem equals t2.NameSystem
                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();
                    ViewBag.b = Join;
                    ViewBag.KH = Join;
                }
              
                if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 )
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                     join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         cheked3 = t3.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost + t3.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();


                    ViewBag.a = Join;
                    ViewBag.b = Join;
                }
                if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0)
                {
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                     join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                     join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                  

                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         cheked3 = t3.Id,
                                         cheked4 = t4.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost + t3.Cost + t4.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();


                    ViewBag.a = Join;
                    ViewBag.b = Join;
                }
                if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 )
                {


                    var queryOrder = from t1 in cheked
                                     join t2 in cheked2 on t1.NameSystem equals t2.NameSystem
                                     join t3 in cheked3 on t2.NameSystem equals t3.NameSystem
                                     join t4 in cheked4 on t3.NameSystem equals t4.NameSystem
                                     join t5 in cheked5 on t4.NameSystem equals t5.NameSystem
                                   
                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         cheked3 = t3.Id,
                                         cheked4 = t4.Id,
                                         cheked5 = t5.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost = t1.Cost + t2.Cost + t3.Cost + t4.Cost + t5.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();


                    ViewBag.a = Join;
                    ViewBag.b = Join;
                }
                if (cheked2.Count() > 0 && cheked.Count() > 0 && cheked3.Count() > 0 && cheked4.Count() > 0 && cheked5.Count() > 0 && cheked6.Count() > 0)
                {

        
                    var queryOrder = from t1 in cheked
                                     join t2 in cheked2.Distinct() on t1.NameSystem equals t2.NameSystem
                                     join t3 in cheked3.Distinct() on t2.NameSystem equals t3.NameSystem
                                     join t4 in cheked4.Distinct() on t3.NameSystem equals t4.NameSystem
                                     join t5 in cheked5.Distinct() on t4.NameSystem equals t5.NameSystem
                                     join t6 in cheked6.Distinct() on t5.NameSystem equals t6.NameSystem
                                     select new
                                     {
                                         cheked2 = t2.Id,
                                         cheked3 = t3.Id,
                                         cheked4 = t4.Id,
                                         cheked5 = t5.Id,
                                         cheked6 = t6.Id,
                                         userKey = keyUser.userKey,
                                         Id = t1.Id,
                                         Name = t1.Name,
                                         companyName = t1.companyName,
                                         description = t1.description,
                                         NameSystem = t1.NameSystem,
                                         Cost =  t1.Cost + t2.Cost+ t3.Cost + t4.Cost + t5.Cost + t6.Cost,
                                         ImageName = t1.ImageName,
                                     };

                    var Join = queryOrder.Distinct().ToList();


                    ViewBag.Doppel = Join.Distinct().OrderBy(x=>x.Cost).ToList();
                    ViewBag.Knaufzylinder = "";
                    ViewBag.Halb = "";
                    ViewBag.Hebel = "";
                    ViewBag.VorhanSchloss = "";
                    ViewBag.Aussen = "";
                }
              
            }

            return View("System_Auswählen", keyUser);
        }
       
        [HttpGet]
        public async Task<IActionResult> OrdersKey(int DopelId, string param2,int KnayfID, int Halb,int Hebel,int Aussen,int Vorhan)
        {

            var key = db.Orders.Where(x => x.userKey == param2).Distinct().ToList();

            var DopelOrderlist = new List<Profil_Doppelzylinder>();

            var OrderList = db.Profil_Doppelzylinder.Where(x => x.Id == Convert.ToInt32(DopelId)).ToList();

            var AussenInen = db.Aussen_Innen.Where(x => x.Profil_DoppelzylinderId == Convert.ToInt32(DopelId)).ToList();

            var Halbzylinder = new List<Profil_Halbzylinder>();

            var SelectHalbzylinder = db.Profil_Halbzylinder.Where(x => x.Id == Halb).ToList();

            var halbAussen_Inter = db.Aussen_Innen_Halbzylinder.Where(x => x.Profil_HalbzylinderId == Halb).ToList();

            ViewBag.AussenHalb = halbAussen_Inter.Select(x=>x.aussen).ToList();

            var KnaufZelinder = db.Profil_Knaufzylinder.Where(x => x.Id == KnayfID).ToList();

            var Kanyf_AussenInen = db.Aussen_Innen_Knauf.Where(x => x.Profil_KnaufzylinderId == Convert.ToInt32(KnayfID)).ToList();

            var Vorhanschlos = new List<Vorhangschloss>();

            var SelectVorhanschlos = db.Vorhangschloss.Where(x => x.Id == Vorhan).ToList();

            var SizeVorhanschloss = db.Size.Where(x=>x.VorhangschlossId == Vorhan).Select(x=>x.sizeVorhangschloss).ToList();

           

            var listVorHanOptions = new List<Vorhan_Options>();

            foreach (var list in SelectVorhanschlos)
            {
                var VorhanOptions = db.Vorhan_Options.Where(x => x.VorhangschlossId == list.Id).ToList();
                
                foreach(var s in VorhanOptions)
                {
                    listVorHanOptions.Add(s);
                }
            }

            ViewBag.VorhanschlossCount = listVorHanOptions.Count();
           

            var listVorHanOptionsValueName = new List<OptionsVorhan>();

            foreach (var ls in listVorHanOptions)
            {
                var listOptionVorhanValue = db.OptionsVorhan.Where(x => x.OptioId == ls.Id).ToList();
                foreach (var lst in listOptionVorhanValue)
                {
                    listVorHanOptionsValueName.Add(lst);
                }
            }
            ViewBag.VorhanschlossOptionName = listVorHanOptionsValueName.Select(x => x.Name).ToList();

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
            ViewBag.VorhanValue = VorhanOptionValue.Select(x=>x.Value).ToList();
            ViewBag.VorhanSize = SizeVorhanschloss;

            float KhaufAussenCost = 0f;
            float DoppelAussenCost = 0f;
            float halbAussenCost = 0f;
            float VorhangschlossCost = 0f;

            var Aussenzylinder = new List<Aussenzylinder_Rundzylinder>();

            var SelectAussenzylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.Id == Aussen).ToList();

            var AussenOption = new List<Aussen_Rund_options>();

            foreach(var x in SelectAussenzylinder)
            {
                var listOptionsAussenZylinder = db.Aussen_Rund_options.Where(x => x.Aussenzylinder_RundzylinderId == x.Id).ToList();
                
                foreach(var f in listOptionsAussenZylinder)
                {
                    AussenOption.Add(f);
                }
                ViewBag.AussenCountOption = listOptionsAussenZylinder.Count();

            }

            var AussenListRundAll = new List<Aussen_Rund_all>();
            
            foreach(var ls in AussenOption)
            {
                var list = db.Aussen_Rund_all.Where(x=>x.Aussen_Rund_optionsId == ls.Id).ToList();
                foreach(var l in list)
                {
                    AussenListRundAll.Add(l);
                }
            }

            ViewBag.AussenName = AussenListRundAll.Select(x => x.Name).ToList();
            var AussenListvalue = new List<Aussen_Rouns_all_value>();

            foreach(var listValueAussen in AussenListRundAll)
            {
                var valueList = db.Aussen_Rouns_all_value.Where(x => x.Aussen_Rund_allId == listValueAussen.Id).ToList();

                foreach(var f in valueList)
                {
                    AussenListvalue.Add(f);
                }
            }

            ViewBag.AussenValue = AussenListvalue.Select(x=>x.Value).ToList();

            var HelbZ = new List<Hebel>();
            var HebelZylinder = db.Hebelzylinder.Where(x=>x.Id == Hebel).ToList();
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
                var HebelOptionsList = db.Options.Where(x => x.OptionId== list.Id).ToList();

                
                foreach (var Optionslist in HebelOptionsList)
                {
                    HebelOptionListAll.Add(Optionslist);
                }

            }

            ViewBag.OptionHebelName = HebelOptionListAll.Select(x => x.Name).Distinct().ToList();


            var HebelOptionValueList = new List<Options_value>();

            foreach (var listValue in HebelOptionListAll)
            {
                var list = db.Options_value.Where(x => x.OptionsId == listValue.Id).ToList();
                foreach(var l in list)
                {
                    HebelOptionValueList.Add(l);
                }
            }

            var listAllValueHebel = new List<int>();

           listAllValueHebel.Add(HebelOptionValueList.Count());

            ViewBag.CountValueHebel = listAllValueHebel;
            ViewBag.ValueHebel = HebelOptionValueList.Select(x=>x.Value).Distinct().ToList();

            var queryableOptionsHalb = db.Profil_Halbzylinder_Options.Where(x => x.Profil_HalbzylinderId == Convert.ToInt32(Halb)).Select(x => x.Id).ToList();
            var OptionsHalb = new List<Halbzylinder_Options>();
            
                for(int f =0;f< queryableOptionsHalb.Count(); f++)
                {
                    var optionsHabel = db.Halbzylinder_Options.Where(x => x.OptionsId == queryableOptionsHalb[f]).ToList();
                    foreach(var listHalb in optionsHabel)
                    {
                        OptionsHalb.Add(listHalb);
                    }
                }
            var OptionsValueHalb = new List<Halbzylinder_Options_value>();
            
            for(int t=0;t<OptionsHalb.Count(); t++)
            {
                var listValueOptionsHalb = db.Halbzylinder_Options_value.Where(x => x.Halbzylinder_OptionsId == OptionsHalb[t].Id).ToList();
                foreach (var listvalue in listValueOptionsHalb)
                    OptionsValueHalb.Add(listvalue);
            }

            var listCountHalb = new List<int>();
            foreach (var f in OptionsHalb)
                listCountHalb.Add(f.Halbzylinder_Options_value.Count());
          

            ViewBag.countOptionsQueryHalb = queryableOptionsHalb.Count();
            ViewBag.HalbOptionsName = OptionsHalb.Select(x=>x.Name).ToList();
   
            ViewBag.HalbOptionsValue = OptionsValueHalb.Select(x=>x.Value).ToList();
            ViewBag.HalbOptionsValueCount = listCountHalb.ToList();

            var KnayfOrderlist = new List<Profil_Knaufzylinder>();

            for (var i = 0; i < key.Count(); i++)
            {
                if (OrderList.Count() > 0)
                {
                    if (key[i].ZylinderId == OrderList.Select(x=>x.schliessanlagenId).First())
                        DopelOrderlist.Add(OrderList.Last());
                }
               

                if (KnaufZelinder.Count() > 0)
                {
                    if (key[i].ZylinderId == KnaufZelinder.Select(x => x.schliessanlagenId).First())
                        KnayfOrderlist.Add(KnaufZelinder.Last());
                }
                if (SelectHalbzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectHalbzylinder.Select(x => x.schliessanlagenId).First())
                        Halbzylinder.Add(SelectHalbzylinder.Last());
                }
               
                if(HebelZylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == HebelZylinder.Select(x => x.schliessanlagenId).First())
                        HelbZ.Add(HebelZylinder.Last());
                }
                if (SelectVorhanschlos.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectVorhanschlos.Select(x => x.schliessanlagenId).First())
                        Vorhanschlos.Add(SelectVorhanschlos.Last());
                }

                if (SelectAussenzylinder.Count() > 0)
                {
                    if (key[i].ZylinderId == SelectAussenzylinder.Select(x => x.schliessanlagenId).First())
                        Aussenzylinder.Add(SelectAussenzylinder.Last());
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

                foreach (var order in key)
                {
                    var optionDopel = ngf.Where(x => x.Name == order.Options).ToList();
                    foreach(var option in optionDopel)
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

                ViewBag.optionsPrise = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

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

                ViewBag.optionsNameKnayf = ngf.Select(x => x.Name).ToList();

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

                ViewBag.optionsPriseKnayf = JsonConvert.SerializeObject(ngfList.Select(x => x.Cost).ToList());

            }

            var keyOpenOrder = new List<isOpen_Order>();

            foreach (var order in key)
            {
                var isOpen = db.isOpen_Order.Where(x => x.OrdersId == order.id).ToList();

                foreach (var list in isOpen)
                    keyOpenOrder.Add(list);
            }
            var IsOpenValue = new List<isOpen_value>();

            foreach (var order in keyOpenOrder)
            {
                var opens = db.isOpen_value.Where(x => x.isOpen_OrderId == order.Id).ToList();
                foreach (var cheked in opens)
                    IsOpenValue.Add(cheked);
            }
            var ValueKeyOpen = new List<KeyValue>();

            foreach (var tl in IsOpenValue)
            {
                var listValueOpen = db.KeyValue.Where(x => x.OpenKeyId == tl.Id).ToList();
                foreach (var tlr in listValueOpen)
                    ValueKeyOpen.Add(tlr);
            }
            ViewBag.Order = IsOpenValue.Distinct().ToList();

            foreach (var order in key)
            {
                if (order.ZylinderId == 1)
                {
                    ViewBag.DopelAussen = order.aussen;
                    ViewBag.DopelInter = order.innen;

                    for (var i = 30; i < order.aussen;)
                    {
                        DoppelAussenCost = DoppelAussenCost + 5;
                        i = i + 5;

                    }
                    for (var j = 30; j < order.innen;)
                    {
                        DoppelAussenCost = DoppelAussenCost + 5;
                        j = j + 5;

                    }

                }
                if (order.ZylinderId == 2)
                {
                    ViewBag.HalbAussen = order.aussen;

                    for (var i = 30; i < order.aussen;)
                    {
                        halbAussenCost = halbAussenCost + 5;
                        i = i + 5;
                    }

                }
                if (order.ZylinderId == 3)
                {
                    ViewBag.KnayfAussen = order.aussen;
                    ViewBag.KnayfInter = order.innen;

                    for (var i = 30; i < order.aussen;)
                    {
                        KhaufAussenCost = KhaufAussenCost + 5;
                        i = i + 5;

                    }
                    for (var j = 30; j < order.innen;)
                    {
                        KhaufAussenCost = KhaufAussenCost + 5;
                        j = j + 5;

                    }
                }
                if (order.ZylinderId == 5)
                {
                    ViewBag.Vorhangschloss = order.aussen;

                    var NameSysteam = SelectVorhanschlos.Select(x => x.NameSystem).ToList();

                    if (NameSysteam.First() == "Vitess.4000")
                    {
                        if (order.aussen == 24)
                        {
                            VorhangschlossCost = 0;
                        }
                        if (order.aussen == 48)
                        {
                            VorhangschlossCost = 7;
                        }
                        if (order.aussen == 64)
                        {
                            VorhangschlossCost = 30;
                        }
                        if (order.aussen == 80)
                        {
                            VorhangschlossCost = 45;
                        }
                    }
                    if (NameSysteam.First() == "Bravus.2000")
                    {
                        if (order.aussen == 24)
                        {
                            VorhangschlossCost = 0;
                        }
                        if (order.aussen == 48)
                        {
                            VorhangschlossCost = 7;
                        }
                        if (order.aussen == 64)
                        {
                            VorhangschlossCost = 30;
                        }
                        if (order.aussen == 80)
                        {
                            VorhangschlossCost = 44;
                        }
                    }
                    if (NameSysteam.First() == "Zolit.1000")
                    {
                        if (order.aussen == 24)
                        {
                            VorhangschlossCost = 0;
                        }
                        if (order.aussen == 48)
                        {
                            VorhangschlossCost = 26.5f;
                        }
                        if (order.aussen == 64)
                        {
                            VorhangschlossCost = 26.5f;
                        }
                        if (order.aussen == 80)
                        {
                            VorhangschlossCost = 44.7f;
                        }
                    }
                }


            }


            ViewBag.KnayfZelinder = KnayfOrderlist.ToList();

            ViewBag.KnayfItemId = KnayfOrderlist.Select(x => x.Id).ToList();

            ViewBag.Halb = Halbzylinder.ToList();
            ViewBag.HelbZ = HelbZ.ToList();

            ViewBag.KnayfZelinderAussen = Kanyf_AussenInen.Select(x => x.aussen).ToList();
            ViewBag.KnayfZelinderIntern = Kanyf_AussenInen.Select(x => x.Intern).ToList();

            ViewBag.DAussen = key.Select(x => x.aussen).Distinct().ToList();
            ViewBag.DInter = key.Select(x => x.innen).Distinct().ToList();

            ViewBag.HalbList = Halbzylinder.Select(x => x.Id).ToList();


            ViewBag.Dopelzylinderaussen = AussenInen.Select(x=>x.aussen).ToList();
            ViewBag.DopelzylinderIntern = AussenInen.Select(x => x.Intern).ToList();
            ViewBag.Dopelzylinder = DopelOrderlist.ToList();

            ViewBag.DopelzylinderIdList = DopelOrderlist.Select(x => x.Id).ToList();

            ViewBag.Vorhanschlos = Vorhanschlos.ToList();
            ViewBag.Aussenzylinder = Aussenzylinder.ToList();
          

            ViewBag.KeyValue = ValueKeyOpen.Distinct().ToList();
          
            ViewBag.DorName = key.Select(x => x.DorName).Distinct().ToList();
            ViewBag.User = key.Select(x => x.userKey).Distinct().ToList();
            
            var SumCost = DopelOrderlist.Select(x => x.Cost).Sum() + KnaufZelinder.Select(x => x.Cost).Sum() + Halbzylinder.Select(x => x.Cost).Sum() +
                HelbZ.Select(x => x.Cost).Sum() + Vorhanschlos.Select(x => x.Cost).Sum() + Aussenzylinder.Select(x => x.Cost).Sum() + DoppelAussenCost
                + KhaufAussenCost + halbAussenCost + VorhangschlossCost;

            ViewBag.Cost = SumCost;



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
        [HttpGet]
        public ActionResult FinischerProductSelect(float SumCosted, List<int> DopelItem,  List<float> DAussen, List<float> DIntern,List<int> Knayf, string user, List<int> Halb,
            List<int> KnayfIntern, List<int> KnayfAusse)
        {
            var UserOrder = db.Orders.FirstOrDefault(x => x.userKey == user);
            var DoppelSylinder = new List<Profil_Doppelzylinder>();
            var OpenLis = new List<isOpen_value>();
            var Order = db.Orders.Where(x => x.userKey == user).Distinct().ToList();

            var OrderChekedKey = db.Orders.Where(x => x.userKey == user).ToList();

            var KeyValue = new List<KeyValue>();
            var isopen = new List<isOpen_Order>();

            var KnayfSylinder = new List<Profil_Knaufzylinder>();
            var HalbSylinder = new List<Profil_Halbzylinder>();

            ViewBag.SumCosted = SumCosted;

            foreach (var list in Order)
            {
                var IsOpen = db.isOpen_Order.Where(x => x.OrdersId == list.id).Distinct().ToList();
                foreach (var OpenList in IsOpen)
                {
                    isopen.Add(OpenList);
                   
                }
               
            }
            foreach (var OpenList in isopen.Distinct())
            {
                var OpenValue = db.isOpen_value.Where(x => x.isOpen_OrderId == OpenList.Id).Distinct().ToList();
                
                foreach (var allList in OpenValue.Distinct())
                    OpenLis.Add(allList);
            }
           
            foreach (var OpenList in OpenLis.Distinct().ToList())
            {
                var opened = db.KeyValue.Where(x => x.OpenKeyId == OpenList.Id).Distinct().ToList();
                foreach (var allList in opened.Distinct())
                    KeyValue.Add(allList);
               
            }

            if (DopelItem.Count() > 0)
            {
                for (int i =0;i< DopelItem.Count();i++)
                {
                    var DopelZylinder = db.Profil_Doppelzylinder.Where(x => x.Id == DopelItem.First()).ToList();
                     foreach(var list in DopelZylinder)
                        DoppelSylinder.Add(list);
                }
                
            }
            if (Knayf.Count() > 0)
            {
                for (int i = 0; i < Knayf.Count(); i++)
                {
                    var KnayfZylinder = db.Profil_Knaufzylinder.Where(x => x.Id == Knayf.First()).ToList();
                    foreach (var list in KnayfZylinder)
                        KnayfSylinder.Add(list);
                }

            }
            if (Halb.Count() > 0)
            {
                for (int i = 0; i < Halb.Count(); i++)
                {
                    var HalbZylinder = db.Profil_Halbzylinder.Where(x => x.Id == Halb.First()).ToList();
                    foreach (var list in HalbZylinder)
                        HalbSylinder.Add(list);
                }

            }

            


            ViewBag.Tur = Order.Select(x => x.DorName).ToList();
            var countRow = KeyValue.Distinct().ToList().Count()/OpenLis.Distinct().ToList().Count();
            ViewBag.EinRow = countRow;

            ViewBag.DoppelAussen = DAussen.ToList();
            ViewBag.DoppelIntern = DIntern.ToList();


            ViewBag.KnayfAussen = KnayfAusse.ToList();
            ViewBag.KnayfName = KnayfSylinder.Select(x => x.Name).ToList();
            ViewBag.KnayflIntern = KnayfIntern.ToList();


            var Doppelname = DoppelSylinder.Select(x => x.Name).ToList();

            ViewBag.Key = OpenLis.Distinct().ToList();
            ViewBag.KeyValueFT = KeyValue.ToList();
            ViewBag.DoppelItem = DoppelSylinder.Count();
            ViewBag.DopelZylinder = Doppelname.ToList();

            ViewBag.KnayfItem = KnayfSylinder.Count;

            return View("FinischerProductSelect", UserOrder );
        }
        [HttpPost]
        public ActionResult SaveOrder(Orders Key, List<string> Turname, List<string> ZylinderId, List<float> aussen, List<float> innen, List<int> Count, List<string> NameKey, List<int> CountKey, List<string> IsOppen, List<string> Options, List<int> ItemCount,List<string>Ssl)
        {
            int CountOrders = Turname.Count();

            string zylinderTyp;


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
                if (i >= Turname.Count())
                {
                    TurnameValue = Turname.Last();
                }
                else
                {
                    TurnameValue = Turname[i];
                }

                string artikul = "";
        
                if (i >= Ssl.Count())
                {
                    artikul = Ssl.Last();
                }
                else
                {
                    artikul = Ssl[i];
                }

                string optionValue;

                if (i >= Options.Count())
                {
                    optionValue = Options.Last();
                }
                else
                {
                    optionValue = Options[i];
                }

                var orders = new Orders
                {
                    userKey = Key.userKey,
                    DorName = TurnameValue,
                    ZylinderId = idZylinder,
                    Options = optionValue,
                    Artikelnummer = artikul
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

            }
            var order_open = db.isOpen_Order.Select(x => x.Id).ToList();
            var d = 0;
            if (ItemCount.Count() > 0)
            {
                var itemsCount = IsOppen.Count() / ItemCount.First();
                for (var s = 0; s < ItemCount.First(); s++)
                {
                    string NameKeyValue;
                    int CountkeyOrders;


                    if (s > CountKey.Count())
                    {
                        CountkeyOrders = CountKey.Last();
                    }
                    else
                    {
                        CountkeyOrders = CountKey[s];
                    }
                    if (s > NameKey.Count())
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

                        bool valueOppen = false;

                        if (IsOppen[d] == "true")
                        {
                            valueOppen = true;
                        }
                        if (IsOppen[d] == "false")
                        {
                            valueOppen = false;
                        }
                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = valueOppen
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            else
            {
                for (var s = 0; s < Turname.Count(); s++)
                {
                    string NameKeyValue;
                    int CountkeyOrders;


                    if (s > CountKey.Count())
                    {
                        CountkeyOrders = CountKey.Last();
                    }
                    else
                    {
                        CountkeyOrders = CountKey[s];
                    }
                    if (s > NameKey.Count())
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

                    for (var f = 0; f < IsOppen.Count(); f++)
                    {
                        bool valueOppen = false;

                        if (IsOppen[d] == "true")
                        {
                            valueOppen = true;
                        }
                        if (IsOppen[d] == "false")
                        {
                            valueOppen = false;
                        }
                        var KeyValueC = new KeyValue
                        {
                            OpenKeyId = Open_value.Id,
                            isOpen = valueOppen
                        };
                        db.KeyValue.Add(KeyValueC);
                        db.SaveChanges();
                        d++;
                    }

                }
            }
            db.SaveChanges();
            return RedirectToAction("System_Auswählen", "Konfigurator", new { Key } );
        }
        
    }
}
