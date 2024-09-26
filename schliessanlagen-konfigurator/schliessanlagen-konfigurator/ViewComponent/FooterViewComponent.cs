using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Service; // Пространство имен вашего сервиса
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Service; // Пространство имен для FooterServis

namespace schliessanlagen_konfigurator.ViewComponent
{
    public class FooterViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly FooterServis _footerServis;
        public FooterViewComponent(FooterServis footerController)
        {
            _footerServis = footerController;
        }
       
        public async  Task<IViewComponentResult> InvokeAsync()
        {

            var model = new FooterViewModel
            {
                Ces = await _footerServis.Ces(),
                ABUS = await _footerServis.Abus(),
                EVVA = await _footerServis.Evva(),
                Basi = await _footerServis.Basi(),

            };
            return View(model);
        }
        
    }
    public class FooterViewModel
    {
        public List<SysteamPriceKey> Ces { get; set; }
      
        public List<SysteamPriceKey> ABUS { get; set; }
      
        public List<SysteamPriceKey> EVVA { get; set; }
       
        public List<SysteamPriceKey> Basi { get; set; }

    }

}