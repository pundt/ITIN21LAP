using hearthstone.web.App_Code;
using hearthstone.web.App_GlobalResources;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static hearthstone.web.App_Code.Constants;

namespace hearthstone.web.Models
{
    public class RegisterModel
    {
        /*
         * so schaut's aus 
         **/
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Constants.Validation.REQUIRED
            )]
        [StringLength(50,
             ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Constants.Validation.EMAIL)]
        [Display(Name = Labels.USERNAME)]
        public string Username { get; set; }

        /*
         * einfachste Variante - Warmduscher
         * [Required(ErrorMessage = "Pflichtfeld")]
         */
        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(50,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name=Labels.PASSWORD)]
        public string Password { get; set; }

        /*
         * mittlere Variante - lauwarm duscher
         * [Required(
            ErrorMessageResourceType =typeof(ValidationMessages),
            ErrorMessageResourceName = "REQUIRED")]*/

        [Required(
                AllowEmptyStrings = false,
                ErrorMessageResourceType = typeof(ValidationMessages),
                ErrorMessageResourceName = Constants.Validation.REQUIRED
                )]
        [StringLength(50,
             ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Compare(nameof(Password), 
            ErrorMessageResourceType = typeof(ValidationMessages),
            ErrorMessageResourceName = Constants.Validation.PASSWORD_EQUAL)]
        [Display(Name = Labels.CONFIRMATION)]
        public string PasswordConfirmation { get; set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(50,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.FIRSTNAME)]
        public string FirstName { get; internal set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(50,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.LASTNAME)]
        public string LastName { get; internal set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(20,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.GAMERTAG)]
        public string GamerTag { get; internal set; }

    }
}