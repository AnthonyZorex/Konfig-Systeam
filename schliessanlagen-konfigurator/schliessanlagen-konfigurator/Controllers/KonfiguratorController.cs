using Microsoft.AspNetCore.Mvc;

namespace schliessanlagen_konfigurator.Controllers
{
    public class KonfiguratorController : Controller
    {
        public IActionResult IndexKonfigurator()
        {
            return View();
        }
    }
}
