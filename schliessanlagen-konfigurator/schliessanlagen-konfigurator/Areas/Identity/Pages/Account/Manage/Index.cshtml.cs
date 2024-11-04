// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using schliessanlagen_konfigurator.Models.Users;

namespace schliessanlagen_konfigurator.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [Display(Name = "Vorname")]
            public string FirstName { get; set; }

            [Display(Name = "Firma")]
            public string Firma { get; set; }

            [Display(Name = "USt-IdNr.")]
            public string UStNumber { get; set; }

            [Required]
            public string Gender { get; set; }

            [Required]
            [Display(Name = "Nachname")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Telefon")]
            public string PhoneNumber { get; set; }

            [EmailAddress]
            [Display(Name = "E-Mail Adresse")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        private async Task LoadAsync(User user)
        {         
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.FirstName;  
            var lastName = user.LastName;
            var Email = user.UserName;
            var firma = user.Firma;
            var ust = user.USt_IdNr;
            var gender = user.Gender;

            Input = new InputModel
            {
                Gender = gender,
                UStNumber = ust,
                Firma = firma,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Email = Email,
                OldPassword = string.Empty,
                NewPassword = string.Empty,
                ConfirmPassword = string.Empty
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
           
            ViewData["user"] = user.FirstName + " " + user.LastName;

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;
            user.UserName = Input.Email;
            user.Firma = Input.Firma;
            user.USt_IdNr = Input.UStNumber;
            user.Gender = Input.Gender;
            var updateResult = await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Ihr Profil wurde aktualisiert";
            return RedirectToPage();
        }
    }
}
