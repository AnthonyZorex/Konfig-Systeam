using Microsoft.AspNetCore.Mvc;

namespace schliessanlagen_konfigurator.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound"); // Отображает представление NotFound.cshtml
        }

        [Route("Error/500")]
        public IActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View("Error"); // Отображает представление Error.cshtml
        }

        [Route("Error/{statusCode}")]
        public IActionResult GeneralError(int statusCode)
        {
            Response.StatusCode = statusCode;
            return View("Error"); // Отображает представление Error.cshtml для других кодов
        }
    }
}
