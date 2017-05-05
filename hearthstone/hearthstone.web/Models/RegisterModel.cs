using hearthstone.web.App_Code;
using Resources;
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
        [Display(Name = Labels.USERNAME, ResourceType = typeof(DisplayNames))]
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
        [Display(Name = Labels.PASSWORD, ResourceType = typeof(DisplayNames))]
        [DataType(DataType.Password)]
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
        [Display(Name = Labels.CONFIRMATION, ResourceType = typeof(DisplayNames))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(50,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.FIRSTNAME, ResourceType = typeof(DisplayNames))]
        public string FirstName { get; set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(50,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.LASTNAME, ResourceType = typeof(DisplayNames))]
        public string LastName { get; set; }

        [Required(
           AllowEmptyStrings = false,
           ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.REQUIRED
           )]
        [StringLength(20,
            ErrorMessageResourceType = typeof(ValidationMessages),
           ErrorMessageResourceName = Constants.Validation.MAX_LENGTH)]
        [Display(Name = Labels.GAMERTAG, ResourceType = typeof(DisplayNames))]
        public string GamerTag { get; set; }

    }
}