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
                SystemCesGalerry =  _footerService.System_CES_Galery(),
                ABUS = _footerService.SystemABUS(),
                SystemAbusGalerry =  _footerService.System_ABUS_Galery(),
                EVVA = _footerService.SystemEVVA(),
                SystemEvvaGalerry =  _footerService.System_EVVA_Galery(),
                Basi = _footerService.SystemBasi(),
                SystemBasiGalerry = _footerService.System_Basi_Galery()
            };

            return View(model);
        }
    }
    
    public class FooterViewModel
    {

        public List<SysteamPriceKey> SystemCes { get; set; }
        public List<ProductGalery> SystemCesGalerry { get; set; }
        public List<SysteamPriceKey> ABUS { get; set; }
        public List<ProductGalery> SystemAbusGalerry { get; set; }
        public List<SysteamPriceKey> EVVA { get; set; }
        public List<ProductGalery> SystemEvvaGalerry { get; set; }
        public List<SysteamPriceKey> Basi { get; set; }
        public List<ProductGalery> SystemBasiGalerry { get; set; }

       
    }
}
