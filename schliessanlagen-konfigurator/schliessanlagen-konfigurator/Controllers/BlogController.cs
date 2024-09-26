using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using schliessanlagen_konfigurator.Data;
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

            ViewBag.Item = Item;

            return View("../Blogs/InfoBlog");
        }
    }
}
