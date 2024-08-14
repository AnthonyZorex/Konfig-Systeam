using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.Users;
using System.Security.Claims;

namespace schliessanlagen_konfigurator.Areas.Identity.Pages.Account.Manage
{
    public class HistoriOrdersModel : PageModel
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly IHttpContextAccessor _contextAccessor;
        public HistoriOrdersModel(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment
       ,IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            Environment = _environment;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGet()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            string loginInform = ident.Claims.Select(x => x.Value).First();
            var users = db.Users.Find(loginInform);

            var ListItem = new List<UserOrdersShop>();

            var OrderList = db.UserOrdersShop.Where(x => x.UserId == users.Id && x.OrderStatus != "Nicht bezahlt").Distinct().ToList();

            foreach (var list in OrderList)
            {
                ListItem.Add(list);
            }

            var ListItemProduct = new List<Models.Users.ProductSysteam>();

            for (int f = 0; f < ListItem.Count(); f++)
            {
                var ProductList = db.ProductSysteam.Where(x => x.UserOrdersShopId == ListItem[f].Id).Distinct().ToList();

                foreach (var list in ProductList)
                {
                    ListItemProduct.Add(list);
                }
            }

            List<string> images = new List<string>();
            List<string> descriptions = new List<string>();
            foreach (var list in ListItemProduct)
            {
                var doppel = db.Profil_Doppelzylinder.FirstOrDefault(x => x.Name == list.Name);
                var knayf = db.Profil_Knaufzylinder.FirstOrDefault(x => x.Name == list.Name);
                var halb = db.Profil_Halbzylinder.FirstOrDefault(x => x.Name == list.Name);
                var hebel = db.Hebelzylinder.FirstOrDefault(x => x.Name == list.Name);
                var Vorhan = db.Vorhangschloss.FirstOrDefault(x => x.Name == list.Name);
                var Aussen = db.Aussenzylinder_Rundzylinder.FirstOrDefault(x => x.Name == list.Name);

                if (doppel!=null)
                {
                    images.Add(doppel.ImageName);
                    descriptions.Add(doppel.description);
                }
                if (knayf!=null)
                {
                    images.Add(knayf.ImageName);
                    descriptions.Add(knayf.description);
                }
                if (halb!=null)
                {
                    images.Add(halb.ImageName);
                    descriptions.Add(halb.description);
                }
                if (hebel!=null)
                {
                    images.Add(hebel.ImageName);
                    descriptions.Add(hebel.description);
                }
                if (Vorhan!=null)
                {
                    images.Add(Vorhan.ImageName);
                    descriptions.Add(Vorhan.description);
                }
                if (Aussen!=null)
                {
                    images.Add(Aussen.ImageName);
                    descriptions.Add(Aussen.description);
                }
            }

           ViewData["Image"] = images;
            ViewData["Descriptions"] = descriptions;
            ViewData["OrderLis"] = ListItem;
            ViewData["OrderItem"] = ListItemProduct.OrderBy(x => x.UserOrdersShopId).ToList();

            var countIterationProduct = new List<int>();

            foreach (var list in ListItem)
            {
                var p = ListItemProduct.Where(x => x.UserOrdersShopId == list.Id).ToList();
                countIterationProduct.Add(p.Count);
            }

            ViewData["CounterProduct"] = countIterationProduct;


            return Page();
        }
    }
}
