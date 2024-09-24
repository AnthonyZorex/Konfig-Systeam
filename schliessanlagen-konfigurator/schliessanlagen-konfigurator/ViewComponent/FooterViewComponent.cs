using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Service;
using System.Diagnostics;
namespace schliessanlagen_konfigurator.ViewComponent
{
    public class FooterViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly FooterService _footerController;

        public FooterViewComponent(FooterService footerController)
        {
            _footerController = footerController;
        }

        public IViewComponentResult Invoke()
        {
            var model = new FooterViewModel
            {
                SystemCes = _footerController.SystemCes(),
                SystemCesGalerry = _footerController.System_CES_Galery(),
                ABUS = _footerController.SystemABUS(),
                SystemAbusGalerry = _footerController.System_ABUS_Galery(),
                EVVA = _footerController.SystemEVVA(),
                SystemEvvaGalerry = _footerController.System_EVVA_Galery(),
                Basi = _footerController.SystemBasi(),
                SystemBasiGalerry = _footerController.System_Basi_Galery()
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
