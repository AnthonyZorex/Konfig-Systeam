using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using schliessanlagen_konfigurator.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using schliessanlagen_konfigurator.Models.Users;

namespace schliessanlagen_konfigurator.Areas.Identity.Pages.Account.Manage
{
    public class PagePersonalOrdersModel : PageModel
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly IHttpContextAccessor _contextAccessor;


        public PagePersonalOrdersModel(schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment
       , IHttpContextAccessor httpContextAccessor)
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

            var OrderList = db.UserOrdersShop.Where(x => x.UserId == users.Id && x.OrderStatus == "Nicht bezahlt").Distinct().ToList();

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