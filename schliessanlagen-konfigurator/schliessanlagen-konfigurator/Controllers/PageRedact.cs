using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Service;

namespace schliessanlagen_konfigurator.Controllers
{
    public class PageRedact : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly ImageOptimizationService _imageOptimizationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public PageRedact(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            _imageOptimizationService = new ImageOptimizationService();
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var page = db.Page.Select(x=>x).ToList();

            ViewBag.AllBlog = page;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPage(Page page)
        {
            db.Page.Add(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Redux(Guid Id)
        {
            var page = db.Page.FirstOrDefault(x=>x.Id == Id);

            return View(page);
        }
        [HttpPost]
        public async Task<IActionResult> SiteUpdate(Guid Id, string Name, string Site)
        {
            var site = db.Page.FirstOrDefault(x=>x.Id==Id);

            site.Name = Name;
            site.Text = Site;

            db.Page.Update(site);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
