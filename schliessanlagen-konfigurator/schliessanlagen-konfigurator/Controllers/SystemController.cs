
using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models;
namespace schliessanlagen_konfigurator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly schliessanlagen_konfiguratorContext _context;

        public SystemController(schliessanlagen_konfiguratorContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("GetData")]
        public IActionResult GetData([FromBody] RequestModel request)
        {
            var system = _context.SysteamPriceKey.FirstOrDefault(x => x.Id == request.Id);

            if (system == null)
            {
                return NotFound(new { message = "System not found" });
            }

            var data = new
            {
                nameSysteam = system.NameSysteam,
                desctiptionsSysteam = system.DesctiptionsSysteam
            };

            return Ok(data);
        }
       
    }

    public class RequestModel
    {
        public int Id { get; set; }
    }
}
