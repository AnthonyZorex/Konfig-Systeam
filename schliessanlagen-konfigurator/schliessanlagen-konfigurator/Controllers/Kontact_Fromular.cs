using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Service;

namespace schliessanlagen_konfigurator.Controllers
{
    public class Kontact_Fromular : Controller
    {
        schliessanlagen_konfiguratorContext db;
        private IWebHostEnvironment Environment;
        private readonly ImageOptimizationService _imageOptimizationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Kontact_Fromular(UserManager<User> userManager, SignInManager<User> signInManager, schliessanlagen_konfiguratorContext context, IWebHostEnvironment _environment)
        {
            _imageOptimizationService = new ImageOptimizationService();
            db = context;
            Environment = _environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Kontakt()
        {

            return View("KontaktIndividuelleAnfrage/Kontakt");
        }
        [HttpPost]
        public IActionResult SendKontakt(Formular formular) 
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Schliessanlagen Store", "oceanwerbung@googlemail.com"));
            message.To.Add(new MailboxAddress(formular.Name, $"{formular.Email}"));
            message.Subject = $"{formular.Rubric}";
            message.Body = new TextPart("plain")
            {
                Text = $"{formular.Message} \n\n Adresse:{formular.Addresse}  \n\n   Telefon:{formular.Phone}",
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("oceanwerbung@googlemail.com", "bouo yqop xsdl qpar");
                client.Send(message);
                client.Disconnect(true);
            }

            return RedirectToAction("Kontakt");
        }
    }


    public class Formular {
        public int id { get; set; }
        public string Gender { get; set; }
        public string Addresse { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Rubric { get; set; }
        public string Message { get; set; }

    }
}
