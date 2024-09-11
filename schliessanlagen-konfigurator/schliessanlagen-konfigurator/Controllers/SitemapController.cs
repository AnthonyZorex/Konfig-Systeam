using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.Aussen_Rund;
using schliessanlagen_konfigurator.Models.Halbzylinder;
using schliessanlagen_konfigurator.Models.Hebel;
using schliessanlagen_konfigurator.Models.Profil_KnaufzylinderZylinder;
using schliessanlagen_konfigurator.Models.ProfilDopelZylinder;
using schliessanlagen_konfigurator.Models.Vorhan;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using schliessanlagen_konfigurator.Models.Hebel;
namespace schliessanlagen_konfigurator.Controllers
{
    public class SitemapController : Controller
    {
        schliessanlagen_konfiguratorContext db;
        public SitemapController(schliessanlagen_konfiguratorContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            var urls = GetSitemapUrls();

            var sitemap = GenerateSitemapXml(urls);

            return Content(sitemap.ToString(), "text/xml");
        }
        private List<SitemapUrl> GetSitemapUrls()
        {

            var doppel = db.Profil_Doppelzylinder.ToList();
            var knayf = db.Profil_Knaufzylinder.ToList();
            var halb = db.Profil_Halbzylinder.ToList();
            var hebel = db.Hebelzylinder.ToList();
            var vorhangschloss = db.Vorhangschloss.ToList();
            var assunzylinder = db.Aussenzylinder_Rundzylinder.ToList();

            List<SitemapUrl> nodes = new List<SitemapUrl>
            {
                new SitemapUrl { Url = Url.Action("Index", "Schop", null, Request.Scheme), LastModified = DateTime.UtcNow, ChangeFrequency = "daily", Priority = 1.0 },
                new SitemapUrl { Url = Url.Action("IndexKonfigurator", "Konfigurator", null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.8 },       
            };

            foreach (var product in doppel)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });       
            }
            foreach (var product in knayf)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in halb)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in hebel)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name },  null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in vorhangschloss)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in assunzylinder)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }



            return nodes;
        }
        private XDocument GenerateSitemapXml(IEnumerable<SitemapUrl> urls)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var xdoc = new XDocument(
                new XElement(xmlns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")
                ));

            foreach (var url in urls)
            {
                xdoc.Root.Add(
                    new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", url.Url),
                        new XElement(xmlns + "lastmod", url.LastModified.ToString("yyyy-MM-dd")),
                        new XElement(xmlns + "changefreq", url.ChangeFrequency),
                        new XElement(xmlns + "priority", url.Priority.ToString("F1"))
                    ));
            }

            return xdoc;
        }
    
    }
    public class SitemapUrl
    {
        public string Url { get; set; }
        public DateTime LastModified { get; set; }
        public string ChangeFrequency { get; set; }
        public double Priority { get; set; }
    }
}
