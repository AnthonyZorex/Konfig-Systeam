using Microsoft.AspNetCore.Mvc;
using schliessanlagen_konfigurator.Models.System;
using schliessanlagen_konfigurator.Service;

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
                ABUS = _footerController.SystemABUS()
            };

            return View(model);
        }
    }
    public class FooterViewModel
    {
        public List<SysteamPriceKey> SystemCes { get; set; }
        public List<ProductGalery> SystemCesGalerry { get; set; }
        public List<SysteamPriceKey> ABUS { get; set; }
    }
}
