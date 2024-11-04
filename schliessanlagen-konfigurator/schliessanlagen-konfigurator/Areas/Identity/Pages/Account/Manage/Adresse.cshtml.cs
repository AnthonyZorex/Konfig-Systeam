using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using schliessanlagen_konfigurator.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace schliessanlagen_konfigurator.Areas.Identity.Pages.Account.Manage
{
    public class Adresse : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Adresse(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }


        public string Rechnun_Land { get; set; }
        public string Liefer_Land { get; set; }

        public class InputModel
        {
            //[Required]
            //[Display(Name = "Rechnun Land")]
            //public string Rechnun_Land { get; set; }

            [Required]
            [Display(Name = "PLZ *")]

            public string Rechnun_Postleitzahl { get; set; }

            [Required]
            [Display(Name = "Stadt *")]
            public string Rechnun_Stadt { get; set; }

            [Required]
            [Display(Name = "Straße *")]
            public string Rechnun_Straße { get; set; }


            //[Required]
            //[Display(Name = "Liefer Land")]
            //public string Liefer_Land { get; set; }

            [Required]
            [Display(Name = "PLZ *")]

            public string Liefer_Postleitzahl { get; set; }

            [Required]
            [Display(Name = "Stadt *")]
            public string Liefer_Stadt { get; set; }

            [Required]
            [Display(Name = "Straße *")]
            public string Liefer_Straße { get; set; }

        }

        private async Task LoadAsync(User user)
        {

            Rechnun_Land = user.Rechnun_Land;

            Liefer_Land = user.Liefer_Land;

            Input = new InputModel
            {
              
                Liefer_Postleitzahl = user.Liefer_Postleitzahl,
                Liefer_Stadt = user.Liefer_Stadt,
                Liefer_Straße = user.Liefer_Straße,

              
                Rechnun_Postleitzahl = user.Rechnun_Postleitzahl,
                Rechnun_Stadt = user.Rechnun_Stadt,
                Rechnun_Straße = user.Rechnun_Straße,

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Rechnun_Land, string Liefer_Land)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.Liefer_Land = Liefer_Land;
            user.Liefer_Postleitzahl = Input.Liefer_Postleitzahl;
            user.Liefer_Stadt = Input.Liefer_Stadt;
            user.Liefer_Straße = Input.Liefer_Straße;

            user.Rechnun_Land = Rechnun_Land;
            user.Rechnun_Postleitzahl = Input.Rechnun_Postleitzahl;
            user.Rechnun_Stadt = Input.Rechnun_Stadt;
            user.Rechnun_Straße = Input.Rechnun_Straße;


            var updateResult = await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Ihr Profil wurde aktualisiert";
            return RedirectToPage();
        }
    }
}
