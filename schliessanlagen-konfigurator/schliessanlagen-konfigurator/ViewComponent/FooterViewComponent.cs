using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Service;
using System.Diagnostics;
namespace schliessanlagen_konfigurator.ViewComponent
{
    public class FooterViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly FooterService _footerService; 

        public FooterViewComponent(FooterService footerService)
        {
            _footerService = footerService; 
        }


        public IViewComponentResult Invoke()
        {
            var model = new FooterViewModel
            {
                SystemCes = _footerService.SystemCes(),
          
                ABUS = _footerService.SystemABUS(),
                
                EVVA = _footerService.SystemEVVA(),
              
                Basi = _footerService.SystemBasi(),
                
            };

            return View(model);
        }
    }
    
    public class FooterViewModel
    {

        public List<SysteamPriceKey> SystemCes { get; set; }
        //public List<ProductGalery> SystemCesGalerry { get; set; }
        public List<SysteamPriceKey> ABUS { get; set; }
        //public List<ProductGalery> SystemAbusGalerry { get; set; }
        public List<SysteamPriceKey> EVVA { get; set; }
        //public List<ProductGalery> SystemEvvaGalerry { get; set; }
        public List<SysteamPriceKey> Basi { get; set; }
        //public List<ProductGalery> SystemBasiGalerry { get; set; }

       
    }
}
