using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Migrations;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Service;

namespace schliessanlagen_konfigurator.Controllers
{
    public class BlogController:Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly ImageOptimizationService _imageOptimizationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public BlogController(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
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
            var allBlog = db.Blogs.ToList();

            ViewBag.AllBlog = allBlog;

            return View("../Blogs/Index");
        }
        [HttpGet]
        public async Task<IActionResult> Item(Guid Id)
        {
           var Item = db.Blogs.Where(x=>x.Id == Id).ToList();

            ViewData["Description"] = $"{Item.First().Name}";

            ViewBag.Item = Item;

            return View("../Blogs/InfoBlog");
        }
        [HttpGet]
        public async Task<IActionResult> Edit_Blog(Guid Id)
        {
            var Item = db.Blogs.Where(x => x.Id == Id).ToList();
            ViewBag.Item = Item.ToList();
            return View("../Blogs/Edit_Blog");
        }
        [HttpPost]
        public async Task<IActionResult> Save_Blog(Guid Id,string Name, string descriptions)
        {
            var Item = db.Blogs.Where(x => x.Id == Id).First();
            Item.Name = Name;
            Item.Description = descriptions;

            db.Blogs.Update(Item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete_Blog(Guid Id)
        {
            var Item = db.Blogs.Where(x => x.Id == Id).First();
         
            db.Blogs.Remove(Item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
