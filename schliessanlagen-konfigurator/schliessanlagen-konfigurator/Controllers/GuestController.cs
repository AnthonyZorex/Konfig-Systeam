using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;
using Newtonsoft.Json;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Migrations;
using schliessanlagen_konfigurator.Models.OrdersOpen;
using schliessanlagen_konfigurator.Send_Data;
using schliessanlagen_konfigurator.Models;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace schliessanlagen_konfigurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly IHttpContextAccessor _contextAccessor;
        public GuestController(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment
        , IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            Environment = _environment;
            _contextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders(string UserKey)
        {
            var orders = db.Orders.Where(x => x.userKey == UserKey).ToList();
            var isOpenKey = new List<isOpen_value>();

            var optionsList = new List<isOpen_Order>();

            foreach (var order in orders) 
            {
                var option = db.isOpen_Order.Where(x => x.OrdersId == order.Id).ToList();

                foreach(var s in option)
                {
                    optionsList.Add(s);
                }
            }

            foreach (var o in optionsList)
            {
                //var optionValue = db.isOpen_value.Where(x => x.isOpen_OrderId == o.Id).OrderBy(x=>x.ForNameKey).ToList();

                var optionValue = db.isOpen_value.Where(x => x.isOpen_OrderId == o.Id).ToList();

                if (optionValue.Count()>0) 
                {
                    var sort = optionValue.GroupBy(x => x.NameKey).Select(g => g.OrderBy(x => x.Id).First()).OrderBy(x => x.NameKey).ToList();
                    foreach (var list in sort)
                    {
                        isOpenKey.Add(list);
                    }
                }

                

            }

            return Ok( new { orders = orders, isOpenKey = isOpenKey.ToList(), option = optionsList });
        }

        public class GetOrder()
        {
           public int Id { get; set; }
           public string userKey { get; set; }
        }


        [HttpPost]
        [Route("OrdersUpdate")]
        public async Task<IActionResult> OrdersUpdate([FromBody] SaveOrders model)
        {
            var System = db.SysteamPriceKey.FirstOrDefault(x => x.NameSysteam == model.SystemName[0]);

            var DoppelZylinder = db.Profil_Doppelzylinder.Where(x => x.NameSystem == System.NameSysteam).ToList();
            var KnayfZylinder = db.Profil_Knaufzylinder.Where(x => x.NameSystem == System.NameSysteam).ToList();
            var HalbZylinder = db.Profil_Halbzylinder.Where(x => x.NameSystem == System.NameSysteam).ToList();
            var HebelZylinder = db.Hebelzylinder.Where(x => x.NameSystem == System.NameSysteam).ToList();
            var VorhangZylinder = db.Vorhangschloss.Where(x => x.NameSystem == System.NameSysteam).ToList();
            var AussenZylinder = db.Aussenzylinder_Rundzylinder.Where(x => x.NameSystem == System.NameSysteam).ToList();

            var orders = db.Orders.Where(x=>x.userKey == model.UserKey).ToList();

            var ProductName = new List<string>();

            var isOpenKey = new List<isOpen_value>();

            float  keyPrice = System.Price;

            int Doppel = 0;
            int Knayf = 0;
            int Halb = 0;
            int Hebel = 0;
            int Vorhan = 0;
            int Aussen = 0;

            float Gram = 0; 

            var Doppel_listPrice = new List<float>();
            var Halb_listPrice = new List<float>();
            var Knayf_listPrice = new List<float>();
            var Hebel_listPrice = new List<float>();
            var Vorhang_listPrice = new List<float>();
            var AussenZylinder_listPrice = new List<float>();

            int Inter = 0;

            for (int z = 0; z <orders.Count(); z++)
            {
                if (orders[z].ZylinderId == 1)
                {
                    orders[z].aussen = model.aussen[z];
                    orders[z].innen = model.innen[Inter];
                    Doppel_listPrice.Add(model.Price[z]);

                    Gram = Gram + DoppelZylinder.Select(x=>x.Gramm).Max(x=>x.Value);

                    Inter++;
                    if (model.DoppelOption[Doppel] != "")
                    {
                        orders[z].Options = model.DoppelOption[Doppel];                       
                        Doppel++;
                    }
                    
                }
                if (orders[z].ZylinderId == 2)
                {
                    orders[z].aussen = model.aussen[z];
                    Halb_listPrice.Add(model.Price[z]);

                    Gram = Gram + HalbZylinder.Select(x => x.Gramm).Max(x => x.Value);

                    if (model.HalbOption[Halb] != "")
                    {
                        orders[z].Options = model.HalbOption[Halb];
                        Halb++;
                    }    
                }
                if (orders[z].ZylinderId == 3)
                {
                    orders[z].aussen = model.aussen[z];
                    orders[z].innen = model.innen[Inter];
                    Knayf_listPrice.Add(model.Price[z]);
                    Inter++;

                    Gram = Gram + KnayfZylinder.Select(x => x.Gramm).Max(x => x.Value);

                    if (model.KnayfOption[Knayf] != "")
                    {
                        orders[z].Options = model.KnayfOption[Knayf];
                        Knayf++;
                    }
                }
                if (orders[z].ZylinderId == 4)
                {
                    Hebel_listPrice.Add(model.Price[z]);

                    Gram = Gram + HebelZylinder.Select(x => x.Gramm).Max(x => x.Value);

                    if (model.HebelOption[Hebel] != "")
                    {
                        orders[z].Options = model.HebelOption[Hebel];
                        Hebel++;
                    }
                }
                if (orders[z].ZylinderId == 5)
                {
                    orders[z].aussen = model.aussen[z];
                    orders[z].Options = model.VorhnaOption[z];
                    Vorhang_listPrice.Add(model.Price[z]);

                    Gram = Gram + VorhangZylinder.Select(x => x.Gramm).Max(x => x.Value);

                    if (model.VorhnaOption[Vorhan] != "")
                    {
                        orders[z].Options = model.VorhnaOption[Vorhan];
                        Vorhan++;
                    }
                }
                if (orders[z].ZylinderId == 6)
                {
                    orders[z].Options = model.AussenOption[z];
                    AussenZylinder_listPrice.Add(model.Price[z]);
                    Gram = Gram + AussenZylinder.Select(x => x.Gramm).Max(x => x.Value);

                    if (model.AussenOption[Aussen] != "")
                    {
                        orders[z].Options = model.AussenOption[Aussen];
                        Aussen++;
                    }
                }
               
                orders[z].Count = model.count[z];

                db.Orders.Update(orders[z]);
                db.SaveChanges();
            }

            var option = db.isOpen_Order.Where(x => x.OrdersId == orders.First().Id).ToList();

            foreach (var o in option) 
            {
                var optionValue = db.isOpen_value.Where(x=>x.isOpen_OrderId == o.Id).ToList();

                foreach (var list in optionValue)
                {
                    isOpenKey.Add(list);
                }

            }

            for(int i = 0; i < isOpenKey.Count(); i++) 
            {
                isOpenKey[i].CountKey = model.CountKey[i];

                db.isOpen_value.Update(isOpenKey[i]);
                db.SaveChanges();
            }

            var Guest = new
            {
                keyPrice = keyPrice,
                SystemName = System.NameSysteam,
                Doppel_listPrice = Doppel_listPrice,
                Halb_listPrice = Halb_listPrice,
                Knayf_listPrice = Knayf_listPrice,
                Hebel_listPrice = Hebel_listPrice,
                Vorhang_listPrice = Vorhang_listPrice,
                AussenZylinder_listPrice = AussenZylinder_listPrice,
                Summe = model.Summe,
                DoppelName = DoppelZylinder.First().Name,
                HablName = HalbZylinder.First().Name,
                KnayfName = KnayfZylinder.First().Name,
                HebelZylinder = HebelZylinder.First().Name,
                VorhangZylinder = VorhangZylinder.First().Name,
                AussenZylinder = AussenZylinder.First().Name,
                ProjektName = model.ProjektName,
                SumGram = Gram
            };


            return Ok(Guest);
        }

        [HttpPost]
        [Route("SendRechnung")]
        public async Task<IActionResult> SendRechnung([FromBody] RehnungSend rehnung)
        {

            var guest = new Models.Guest()
            {
                Vorhname = rehnung.RechnungVorname,
                Nachname = rehnung.RechnungNachname,
                Email = rehnung.RechnungAdresse,
                Telefon = rehnung.RechnungTelefon,
                Liefer_Land = rehnung.SendLand,
                Liefer_Straße = rehnung.SendStrasse,
                Liefer_Postleitzahl = rehnung.SendZip,
                Liefer_Stadt = rehnung.SendStadt
            };

            db.Guest.Add(guest);
            db.SaveChanges();



            var model = new
            {
                SendVorname = rehnung.SendVorname,
                SendNachname = rehnung.SendNachname,
                SendFirma = rehnung.SendFirma,
                SendVat = rehnung.SendVat,
                SendStrasse = rehnung.SendStrasse,

                NettoSum = rehnung.NettoSumOrder,

                SendZip = rehnung.SendZip,
                SendStadt = rehnung.SendStadt,
                SendLand = rehnung.SendLand,
                Steuer = rehnung.Steuer,
                Versand = rehnung.Versand,
                OrderSum = rehnung.OrderSum,
                SendTelefon = rehnung.SendTelefon,
                DhlSend = rehnung.DhlSend,
                Rehnung = rehnung.Rehnung,
                Pay = rehnung.Pay,
                procent = rehnung.procent,

                RechnungVorname = rehnung.RechnungVorname,
                RechnungNachname = rehnung.RechnungNachname,
                RechnungFirma = rehnung.RechnungFirma,
                RechnungVat = rehnung.RechnungVat,
                RechnungStrasse = rehnung.RechnungStrasse,
                RechnungZip = rehnung.RechnungZip,
                RechnungStadt = rehnung.RechnungStadt,
                RechnungLand = rehnung.RechnungLand,
                RechnungTelefon = rehnung.RechnungTelefon,

            };

            var JsonObject = JsonConvert.SerializeObject(model, Formatting.Indented);
            return RedirectToAction("SendRehnung", "Konfigurator", new { info = JsonObject/*, userName = model.userName, OrderSum = OrderSum, aufRechnung = aufRechnung */});
        }
        public class SaveOrders()
        {
            public int Id { get; set; }
            public string UserKey { get; set; }
            public List<float> aussen { get; set; }
            public  List<float> innen { get; set; }
            public List<string> DoppelOption { get; set; }
            public List<string> KnayfOption { get; set; }
            public List<string> HalbOption { get; set; }
            public List<string> HebelOption { get; set; }
            public List<string> VorhnaOption { get; set; }
            public List<string> AussenOption { get; set; }
            public List<int> count { get; set; }
            public List<string> SystemName { get; set; }
            public List<float> Price { get; set; }
            public List<int> CountKey { get; set; }
            public string Summe { get; set; }
            public string ProjektName { get; set; }
        }

        public class RehnungSend()
        {
            public int Id { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string SendVorname {  get; set; }
            public string SendNachname { get; set; }
            public string SendFirma { get; set; }
            public string userKey { get; set; }
            public List<string> PreisProduct { get; set; }
            public string PreisKey { get; set; }
            public string SendVat { get; set; }
            public string SendStrasse { get; set; }
            public string SendZip { get; set; }
            public string SendStadt { get; set; }
            public string SendLand { get; set; }
            public string SendTelefon { get; set; }
            public string NettoSumOrder { get; set; }
            public string DhlSend { get; set; }
            public string Pay { get; set; }
            public string Steuer { get; set; }
            public string Versand { get; set; }
            public string OrderSum { get; set; }
            public string Rehnung { get; set; }
            public string RechnungAdresse{ get; set; }
            public string RechnungVorname { get; set; }
            public string RechnungNachname { get; set; }
            public string RechnungFirma { get; set; }
            public string RechnungVat { get; set; }
            public string RechnungStrasse { get; set; }
            public string RechnungZip { get; set; }
            public string RechnungStadt { get; set; }
            public string RechnungLand { get; set; }
            public string RechnungTelefon { get; set; }
            public string userName { get; set; }
            public string procent { get; set; }
            public bool aufRechnung { get; set; }
        }
    }
}
