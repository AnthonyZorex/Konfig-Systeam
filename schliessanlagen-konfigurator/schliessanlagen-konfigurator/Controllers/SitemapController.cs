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
        private readonly schliessanlagen_konfiguratorContext _db;

        public SitemapController(schliessanlagen_konfiguratorContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            var urls = GetSitemapUrls();
            var sitemap = GenerateSitemapXml(urls);
            return Content(sitemap.ToString(), "text/xml");
        }

        public IActionResult Robots()
        {
            var robotsTxt = GenerateRobotsTxt();
            return Content(robotsTxt, "text/plain");
        }

        private List<SitemapUrl> GetSitemapUrls()
        {
            var doppel = _db.Profil_Doppelzylinder.ToList();
            var knayf = _db.Profil_Knaufzylinder.ToList();
            var halb = _db.Profil_Halbzylinder.ToList();
            var hebel = _db.Hebelzylinder.ToList();
            var vorhangschloss = _db.Vorhangschloss.ToList();
            var assunzylinder = _db.Aussenzylinder_Rundzylinder.ToList();

            var nodes = new List<SitemapUrl>
        {
            new SitemapUrl { Url = Url.Action("Index", "Schop", null, Request.Scheme), LastModified = DateTime.UtcNow, ChangeFrequency = "daily", Priority = 1.0 },
            new SitemapUrl { Url = Url.Action("IndexKonfigurator", "Konfigurator", null, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.8 }
        };

            foreach (var product in doppel)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in knayf)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in halb)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in hebel)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in vorhangschloss)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }
            foreach (var product in assunzylinder)
            {
                nodes.Add(new SitemapUrl { Url = Url.Action("zylinder_page", "Schop", new { product_Name = product.Name }, Request.Scheme), LastModified = DateTime.UtcNow.AddDays(-1), ChangeFrequency = "monthly", Priority = 0.6 });
            }

            return nodes;
        }

        private XDocument GenerateSitemapXml(IEnumerable<SitemapUrl> urls)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            var xdoc = new XDocument(
                new XElement(xmlns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XName.Get("schemaLocation", xsi.NamespaceName), "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd")
                ));

            foreach (var url in urls)
            {
                xdoc.Root.Add(
                    new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", url.Url),
                        new XElement(xmlns + "lastmod", url.LastModified.ToString("yyyy-MM-dd"))
                    ));
            }

            return xdoc;
        }

        private string GenerateRobotsTxt()
        {
            var sitemapUrl = Url.Action("Index", "Sitemap", null, Request.Scheme);

            return $@"
User-agent: Googlebot
Disallow: /nogooglebot/
Disallow: /wwwroot/Orders/
Disallow: /wwwroot/Rechnung/
Allow: /

Sitemap: {sitemapUrl}
";
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
