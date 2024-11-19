using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.OrdersOpen;

namespace schliessanlagen_konfigurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : Controller
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
        [HttpPost]
        [Route("OrdersUpdate")]
        public async Task<IActionResult> OrdersUpdate([FromBody] SaveOrders model)
        {
            var orders = db.Orders.Where(x=>x.userKey == model.UserKey).ToList();

            var isOpenKey = new List<isOpen_value>();

            for (int z = 0; z <orders.Count(); z++)
            {
                orders[z].aussen = model.aussen[z];
                orders[z].innen = model.innen[z];
                orders[z].Options = model.Options[z];
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

            return Ok();
        }

        public class SaveOrders()
        {
            public int Id { get; set; }
            public string UserKey { get; set; }
            public List<float> aussen { get; set; }
            public  List<float> innen { get; set; }
            public List<string> Options{ get; set; }
            public List<int> count { get; set; }
            public List<int> CountKey { get; set; }
        }
    }
}
